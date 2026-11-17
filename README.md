# SFMCSDK.Net.iOS

[![NuGet](https://img.shields.io/nuget/v/SFMCSDK.Net.iOS?label=nuget)](https://www.nuget.org/packages/SFMCSDK.Net.iOS)
[![release](https://github.com/sbokatuk/SFMCSDK.Net.iOS/actions/workflows/release.yml/badge.svg)](https://github.com/sbokatuk/SFMCSDK.Net.iOS/actions/workflows/release.yml)
[![Targets: net8.0 | net9.0 | net10.0](https://img.shields.io/badge/targets-net8.0%20%7C%20net9.0%20%7C%20net10.0-512BD4)](#installing)
[![SFMCSDK 5.0.0](https://img.shields.io/badge/SFMCSDK-5.0.0-099DFD)](https://github.com/salesforce-marketingcloud/sfmc-sdk-ios/releases)
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
| `SFMCSDK.Net.iOS` | `SFMCSDK.xcframework` 5.0.0 | The SFMC SDK core: `SFMCSdk.InitializeSdk`, identity, custom events, logging, the In-App-Messaging and push-feature event models, and the v5 Inbox models |

Versions are `<SFMCSDK version>.<binding revision>` — `5.0.0.1` is SFMCSDK 5.0.0, binding revision 1.

**SFMCSDK 5 pairs with MobilePush 12.** The native core accepts only the module generation it was released with — 5.0.0 reports `12.0.0 - 12.9.9` as its compatible range for the engagement (MobilePush) module, where 4.0.1 reported `11.0.0 - 11.9.9`. Use this package with [MarketingCloudSDK.Net.iOS](https://github.com/sbokatuk/MarketingCloudSDK.Net.iOS) 12.x, and stay on `4.0.1.x` for as long as you are on MobilePush 11.

## What is bound, and how

The whole Objective-C projection of the framework — 188 interfaces, 37 enums — in namespace `SFMCSDK`, the same namespace the old `sfmc-net-bindings-mobile` packages exposed.

**By hand, never by Sharpie.** [ApiDefinitions.cs](src/SFMCSDK.Net.iOS/ApiDefinitions.cs) descends from the hand-curated 1.x binding, ported to 4.0.1 and then to 5.0.0 by diffing each pair of versions' `SFMCSDK-Swift.h` and applying the changes — Objective Sharpie cannot parse recent iOS SDK module maps, and regenerating would lose every hand-applied fix. The port was verified selector-by-selector against the 5.0.0 header (every type present, every non-unavailable selector bound, no stray selectors, every enum case and raw value equal), and the packed binding passes live smoke tests on a simulator: the native SDK reports its version, initializes and calls back, answers identity, state and module-status reads, and round-trips the new Inbox objects.

What changed at v4, for consumers of the old 1.x packages: the identity class is `SFIdentity` (edits through `Identity.Edit(modifier => …)`), `SFMCSdkPUSH`/`PushInterface` are gone (push is the sibling package's `MarketingCloudSdk`), the config builder grew per-module setters (`SetPushFeature`, `SetEngagement`, `SetPersonalization`, `SetMAM`, `SetInAppMessagingFeature`, `SetCdp`), `InitializeSdk` gained a per-module completion block, and large In-App-Messaging, Personalization and push-feature event surfaces are new.

What changed at v5: the Inbox feature arrived — `SFMCSdkConfigBuilder.SetInboxFeature`, the `ISFMCSdkInboxMessage` family of protocols, the `SFMCSdkInbox…Event` classes and `SFMCSdkModuleName.InboxFeature` — along with `SFMCSdk.GetStatus (module)`, `SFMCSdkBadgeAggregator` and `SFMCSdkURLType`. One raw value moved: `SFMCSdkEventCategory.FlushEvents` is 7, not 6. The only members removed are 26 parameterless constructors that could not be called without aborting the process — the behaviours (`SFMCSdkAppBackgrounded` and its siblings, `SFMCSdkScreenEntry`), which only the SDK creates, and the cart, order and catalog object events, which are built through the constructor the native class declares, such as `new SFMCSdkAddToCartEvent (lineItem)`. The [5.0.0.1 release note](https://github.com/sbokatuk/SFMCSDK.Net.iOS/blob/main/docs/release-notes/5.0.0.1.md) has the full list.

## Installing

```xml
<PackageReference Include="SFMCSDK.Net.iOS" Version="5.0.0.1" />
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
./build/BuildNugets.sh                # packs 5.0.0.1 into ./artifacts
dotnet test tests/SFMCSDK.Net.iOS.PackageTests
```

## Tests

```sh
dotnet test tests/SFMCSDK.Net.iOS.PackageTests
./.github/scripts/run-simulator-tests.sh 5.0.0.1 net9.0-ios18.0
```

The simulator tests run without credentials on purpose: the native SDK loads, reports its version, initializes standalone, and answers state, module-status and identity reads; the objects new in 5.0.0 and the cart, order and catalog object events are built and read back.

One measured upstream behaviour they encode: with zero modules configured, `initializeSdk`'s completion block fires once per call, a few milliseconds after the call returns, on a background thread, with an empty array — it reports per-module statuses, and there are none. That is the same on 4.0.1 and 5.0.0. Earlier versions of this README said the block never fires; that was a simulator app without keychain access (see [Troubleshooting](#troubleshooting)), observed on the net8 and net9 legs and mistaken for SDK behaviour. The first check the tests run now is that the keychain is usable.

## Upgrading the SFMCSDK

```sh
./build/BumpNativeVersion.sh 5.1.0
```

then port the `SFMCSDK-Swift.h` diff into `ApiDefinitions.cs` and `StructsAndEnums.cs` by hand — the script prints what it does not automate. Salesforce publishes no release notes and its major tags share no git history, so the shipped framework is the only record of what changed: diff the header, the `Info.plist` (minimum iOS, slices) and the linked libraries, and read the compatibility ranges out of `SFMCSdk.State`. The `upstream-drift` workflow files an issue when Salesforce tags a version this repository does not bind.

## Releasing

Merging a file named `docs/release-notes/<four-part-version>.md` to `main` **is** the release: `auto-release` tags it, `release` packs with verification off and publishes to nuget.org via trusted publishing (OIDC — no stored API key). Every PR publishes a `-beta.<pr>.<run>` prerelease.

## Troubleshooting

**`SFMCSDK.xcframework is missing`.** Run `./build/FetchXcFrameworks.sh` — nothing native is committed, and packing does not fetch.

**`InitializeSdk`'s completion never fires, and identity edits do nothing, on a simulator.** The native SDK keeps its keys in the keychain and does not initialize without it — silently. A simulator app that carries no entitlements is refused the keychain (`errSecMissingEntitlement`, -34018), and that is how the .NET 8 and .NET 9 iOS workloads build one; the .NET 10 workload embeds simulated entitlements by itself, so the same app works there. Give the app an `Entitlements.plist` with a `keychain-access-groups` entry — the sample and the device tests in this repository show a simulator-only variant, with `CodesignRequireProvisioningProfile` off so that no provisioning profile is needed. A device build is signed with a profile and is not affected.

**A module stays inactive after upgrading to 5.x.** SFMCSDK 5.0.0 only accepts MobilePush 12.x (see [Packages](#packages)); `SFMCSdk.State` prints the range each module must fall in.

**A net8 app using Shell hangs at launch on iOS 26.0.** The app shows a black screen at 100% CPU. It's a bug in `Microsoft.Maui.Controls`, seen in apps built with Xcode 26 and fixed in 9.0.110 by [dotnet/maui#31149](https://github.com/dotnet/maui/pull/31149). This package can't work around it, and 8.0.100, the last 8.0.x, doesn't have the fix. iOS 26.5 and 27.0 aren't affected. On net9, use Controls 9.0.110 or newer. On net8, subclass `ShellItemRenderer` and override `TabBarHidden` to forward only real changes, returning the subclass from `ShellRenderer.CreateShellItemRenderer`. Setting `UIDesignRequiresCompatibility` to `true` in `Info.plist` also works, but it changes the look of the whole app.

**Xcode version errors building the net10 sample locally.** The net10 iOS workload pins an exact Xcode line (CI selects `Xcode_26.0`); with a newer local Xcode, build the net9-band device tests instead — they exercise the same packed package.

**A selector you need is missing.** The binding covers the whole 5.0.0 ObjC projection; if Salesforce adds surface in a newer tag, `upstream-drift` will have filed an issue — port the header diff or file an issue.

## Licence

The binding code in this repository is [MIT](LICENSE). The native SFMCSDK framework inside the package is © Salesforce, [BSD-3-Clause](licenses/BSD-3-Clause-Salesforce.txt). The package licence expression is `MIT AND BSD-3-Clause` and both texts ship in every package under `licenses/`.
