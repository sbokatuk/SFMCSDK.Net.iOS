#!/bin/sh
# Fetches the SFMCSDK.xcframework this repository binds, into ./libs/.
#
#   ./build/FetchXcFrameworks.sh            # version from Directory.Build.props
#   ./build/FetchXcFrameworks.sh 4.0.1      # explicit version
#
# Where it comes from: Salesforce commits the built xcframework INTO the sfmc-sdk-ios repository
# tree and attaches nothing to its GitHub releases (every release from v1 to v4 has zero assets),
# so the download is the tag's source archive. The SHA-256 pins in build/checksums.txt are taken
# over the FRAMEWORK BINARIES rather than the archive: GitHub generates tag archives on the fly
# and has changed their byte representation before (the January 2023 archive-checksum incident),
# so a zip-level pin would rot on GitHub's schedule rather than Salesforce's.
set -eu

cd "$(dirname "$0")/.."

VERSION="${1:-$(sed -n 's:.*<SfmcNativeVersion>\(.*\)</SfmcNativeVersion>.*:\1:p' Directory.Build.props | head -1)}"
case "$VERSION" in
    *[!0-9.]*|'')
        echo "error: invalid version '$VERSION'" >&2
        exit 1
        ;;
esac

CHECKSUMS="build/checksums.txt"
URL="https://github.com/salesforce-marketingcloud/sfmc-sdk-ios/archive/refs/tags/v$VERSION.zip"

work="$(mktemp -d)"
trap 'rm -rf "$work"' EXIT

echo "==> downloading sfmc-sdk-ios v$VERSION"
curl -fsSL -o "$work/src.zip" "$URL"
unzip -q "$work/src.zip" -d "$work"

framework="$work/sfmc-sdk-ios-$VERSION/Frameworks/SFMCSDK.xcframework"
if [ ! -d "$framework" ]; then
    echo "error: the v$VERSION archive has no Frameworks/SFMCSDK.xcframework - upstream moved it" >&2
    exit 1
fi

# Verify every slice binary against the pins before anything lands in libs/.
status=0
for slice in "$framework"/*/SFMCSDK.framework/SFMCSDK; do
    rel="SFMCSDK-$VERSION/$(basename "$(dirname "$(dirname "$slice")")")"
    actual="$(shasum -a 256 "$slice" | cut -d' ' -f1)"
    pinned="$(grep "^$rel " "$CHECKSUMS" | cut -d' ' -f2 || true)"
    if [ -z "$pinned" ]; then
        echo "error: no pin for $rel in $CHECKSUMS (actual: $actual). Run build/UpdateChecksums.sh after a version bump and review the diff." >&2
        status=1
    elif [ "$pinned" != "$actual" ]; then
        echo "error: $rel does not match its pin (pinned: $pinned, actual: $actual) - a tampered or corrupted download, or a stale pin." >&2
        status=1
    else
        echo "==> verified $rel"
    fi
done
[ "$status" -eq 0 ] || exit "$status"

mkdir -p libs
rm -rf libs/SFMCSDK.xcframework
mv "$framework" libs/
echo "==> libs/SFMCSDK.xcframework is ready"
