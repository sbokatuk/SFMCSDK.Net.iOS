# SFMCSDK.Net.iOS

[![NuGet](https://img.shields.io/nuget/v/SFMCSDK.Net.iOS?label=nuget)](https://www.nuget.org/packages/SFMCSDK.Net.iOS)
[![release](https://github.com/sbokatuk/SFMCSDK.Net.iOS/actions/workflows/release.yml/badge.svg)](https://github.com/sbokatuk/SFMCSDK.Net.iOS/actions/workflows/release.yml)
[![Targets: net8.0 | net9.0 | net10.0](https://img.shields.io/badge/targets-net8.0%20%7C%20net9.0%20%7C%20net10.0-512BD4)](#installing)
[![SFMCSDK 4.0.1](https://img.shields.io/badge/SFMCSDK-4.0.1-099DFD)](https://github.com/salesforce-marketingcloud/sfmc-sdk-ios/releases)
[![Licence: MIT AND BSD-3-Clause](https://img.shields.io/badge/licence-MIT%20AND%20BSD--3--Clause-orange)](#licence)

**.NET for iOS bindings for the Salesforce Marketing Cloud SFMCSDK core framework — identity, event tracking, logging, and the module plumbing the MobilePush module plugs into.**

```sh
dotnet add package SFMCSDK.Net.iOS
```

```csharp
using SFMCSDK;

var config = new SFMCSdkConfigBuilder().Build();
SFMCSdk.InitializeSdk(config, statuses => { });

SFMCSdk.Identity.Edit(modifier =>
{
    modifier.ProfileId = "contact-key";
    return modifier;
});
```

Push notifications live in the sibling [MarketingCloudSDK.Net.iOS](https://github.com/sbokatuk/MarketingCloudSDK.Net.iOS), which depends on this package the same way the native MobilePush package depends on SFMCSDK. A cross-platform ergonomic layer lives in [SFMCSDK.Net](https://github.com/sbokatuk/SFMCSDK.Net).

---

## Contents

- [Packages](#packages)
- [What is bound, and how](#what-is-bound-and-how)
- [Installing](#installing)
- [How this repository works](#how-this-repository-works)
- [Building locally](#building-locally)
- [Tests](#tests)
- [Upgrading the SFMCSDK](#upgrading-the-sfmcsdk)
- [Releasing](#releasing)
- [Troubleshooting](#troubleshooting)
- [Licence](#licence)

## Packages

| Package | Wraps | What it is for |
| --- | --- | --- |
| `SFMCSDK.Net.iOS` | `SFMCSDK.xcframework` 4.0.1 | The SFMC SDK core: `SFMCSdk.InitializeSdk`, identity, custom events, logging, the v4 In-App-Messaging and push-feature event models |

Versions are `<SFMCSDK version>.<binding revision>` — `4.0.1.1` is SFMCSDK 4.0.1, binding revision 1.

## What is bound, and how

The whole Objective-C projection of the framework — 156 interfaces, 29 enums — in namespace `SFMCSDK`, the same namespace the old `sfmc-net-bindings-mobile` packages exposed.

**By hand, never by Sharpie.** [ApiDefinitions.cs](src/SFMCSDK.Net.iOS/ApiDefinitions.cs) descends from the hand-curated 1.x binding, ported to 4.0.1 by diffing the two versions' `SFMCSDK-Swift.h` and applying the changes — Objective Sharpie cannot parse recent iOS SDK module maps, and regenerating would lose every hand-applied fix. The port was verified selector-by-selector against the 4.0.1 header (every type present, every non-unavailable selector bound, no stray selectors), and the packed binding passes live smoke tests on a simulator: the native SDK reports its version, initializes, and answers identity and state reads.

What changed at v4, for consumers of the old 1.x packages: the identity class is `SFIdentity` (edits through `Identity.Edit(modifier => …)`), `SFMCSdkPUSH`/`PushInterface` are gone (push is the sibling package's `MarketingCloudSdk`), the config builder grew per-module setters (`SetPushFeature`, `SetEngagement`, `SetPersonalization`, `SetMAM`, `SetInAppMessagingFeature`, `SetCdp`), `InitializeSdk` gained a per-module completion block, and large In-App-Messaging, Personalization and push-feature event surfaces are new.

## Installing

```xml
<PackageReference Include="SFMCSDK.Net.iOS" Version="4.0.1.2" />
```

Target frameworks: `net8.0-ios18.0`, `net9.0-ios18.0`, `net10.0-ios26.0`. Floor: **iOS 12.2** (the framework is Swift and relies on the OS Swift runtime, ABI-stable from 12.2 — Salesforce's own podspec says 12.0, which predates that line).

## How this repository works

Nothing native is committed. Salesforce commits the built xcframework into the `sfmc-sdk-ios` repository tree and attaches nothing to its releases, so [build/FetchXcFrameworks.sh](build/FetchXcFrameworks.sh) downloads the tag's source archive and verifies every slice binary against the SHA-256 pins in [build/checksums.txt](build/checksums.txt) — pins are over the binaries, not the archive, because GitHub generates tag archives on the fly and has changed their bytes before.

Each .NET SDK's iOS workload builds two target frameworks, so [build/BuildNugets.sh](build/BuildNugets.sh) packs twice (net9 band, then net10 from a scratch `global.json`) and [build/merge-packages.py](build/merge-packages.py) grafts the net10 assets into one package. The native payload ships as one compressed `.resources.zip` (NU5123 and Windows MAX_PATH otherwise).

> **CocoaPods trap.** The pod for this framework is `MarketingCloud-SFMCSdk`. The pod literally named `SFMCSDK` on CocoaPods is an unrelated squatter package — never resolve by that name.

### Layout

| Path | What |
| --- | --- |
| `src/Sfmc.Binding.props` | TFM bands, xcframework validation, packaging — kept a one-file port from the sibling MarketingCloudSDK.Net.iOS |
| `src/SFMCSDK.Net.iOS/` | `ApiDefinitions.cs` + `StructsAndEnums.cs`, hand-maintained |
| `build/` | Fetch, checksum, pack and upgrade scripts; `packages.tsv` is the roster |
| `tests/` | `PackageTests` (nupkg shape, no workloads needed) and `DeviceTests` (a bare UIKit app driving the real SDK on a simulator) |
| `samples/` | A MAUI app driving initialization, identity and events |
| `.github/workflows/` | `pr`, `release`, `auto-release`, `upstream-drift` |

## Building locally

```sh
./build/FetchXcFrameworks.sh          # populate ./libs, checksum-verified
./build/BuildNugets.sh                # packs 4.0.1.2 into ./artifacts
dotnet test tests/SFMCSDK.Net.iOS.PackageTests
```

## Tests

```sh
dotnet test tests/SFMCSDK.Net.iOS.PackageTests
./.github/scripts/run-simulator-tests.sh 4.0.1.2 net9.0-ios18.0
```

The simulator tests run without credentials on purpose: the native SDK loads, reports its version, initializes standalone and answers state and identity reads. One verified upstream behaviour they encode: with zero modules configured, `initializeSdk`'s completion block never fires — it reports per-module statuses, and there are none.

## Upgrading the SFMCSDK

```sh
./build/BumpNativeVersion.sh 4.1.0
```

then port the `SFMCSDK-Swift.h` diff into `ApiDefinitions.cs` by hand — the script prints what it does not automate. The `upstream-drift` workflow files an issue when Salesforce tags a version this repository does not bind.

## Releasing

Merging a file named `docs/release-notes/<four-part-version>.md` to `main` **is** the release: `auto-release` tags it, `release` packs with verification off and publishes to nuget.org via trusted publishing (OIDC — no stored API key). Every PR publishes a `-beta.<pr>.<run>` prerelease.

## Troubleshooting

**`SFMCSDK.xcframework is missing`.** Run `./build/FetchXcFrameworks.sh` — nothing native is committed, and packing does not fetch.

**Xcode version errors building the net10 sample locally.** The net10 iOS workload pins an exact Xcode line (CI selects `Xcode_26.0`); with a newer local Xcode, build the net9-band device tests instead — they exercise the same packed package.

**A selector you need is missing.** The binding covers the whole 4.0.1 ObjC projection; if Salesforce adds surface in a newer tag, `upstream-drift` will have filed an issue — port the header diff or file an issue.

## Licence

The binding code in this repository is [MIT](LICENSE). The native SFMCSDK framework inside the package is © Salesforce, [BSD-3-Clause](licenses/BSD-3-Clause-Salesforce.txt). The package licence expression is `MIT AND BSD-3-Clause` and both texts ship in every package under `licenses/`.
