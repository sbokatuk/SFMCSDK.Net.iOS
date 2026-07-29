#!/bin/sh
# Regenerates build/checksums.txt: one SHA-256 per slice binary of every framework this
# repository binds, at the version currently pinned in Directory.Build.props.
#
# Run it after bumping SfmcNativeVersion, then diff the result - a hash that CHANGED for an
# unchanged version is exactly the event the pins exist to catch, and wants investigating rather
# than committing. Pins are over the framework binaries, not the tag archive; see
# FetchXcFrameworks.sh for why.
set -eu

cd "$(dirname "$0")/.."

VERSION="$(sed -n 's:.*<SfmcNativeVersion>\(.*\)</SfmcNativeVersion>.*:\1:p' Directory.Build.props | head -1)"
URL="https://github.com/salesforce-marketingcloud/sfmc-sdk-ios/archive/refs/tags/v$VERSION.zip"

work="$(mktemp -d)"
trap 'rm -rf "$work"' EXIT

echo "==> downloading sfmc-sdk-ios v$VERSION"
curl -fsSL -o "$work/src.zip" "$URL"
unzip -q "$work/src.zip" -d "$work"

out="$work/checksums.txt"
cat > "$out" <<'EOF'
# SHA-256 pins for every native framework binary this repository binds.
#
# Verified by build/FetchXcFrameworks.sh before anything lands in libs/. Keys are
# <Framework>-<version>/<xcframework slice>. Regenerate with build/UpdateChecksums.sh after a
# version bump; a hash that changes for an UNCHANGED version is the tampering event these pins
# exist to catch.
EOF

for slice in "$work/sfmc-sdk-ios-$VERSION/Frameworks/SFMCSDK.xcframework"/*/SFMCSDK.framework/SFMCSDK; do
    rel="SFMCSDK-$VERSION/$(basename "$(dirname "$(dirname "$slice")")")"
    printf '%s %s\n' "$rel" "$(shasum -a 256 "$slice" | cut -d' ' -f1)" >> "$out"
done

mv "$out" build/checksums.txt
echo "==> wrote build/checksums.txt - review the diff before committing"
