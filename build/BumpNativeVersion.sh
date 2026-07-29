#!/bin/sh
# Runs every scripted step of an SFMCSDK upgrade, in order, stopping at the first failure.
#
#   ./build/BumpNativeVersion.sh 4.1.0
#
# What it does NOT automate, in the order you will meet it:
#   - reviewing the checksum diff (a hash that changed for an unchanged version is the event
#     the pins exist to catch);
#   - the ApiDefinitions port: diff the new tag's SFMCSDK-Swift.h against the previous one and
#     hand-apply the changes (never regenerate from scratch - the file is hand-curated);
#   - the README's version pins (build/CheckReadmeVersions.sh will tell you);
#   - a release-notes file under docs/release-notes/;
#   - bumping SfmcBindingRevision back to 1 for the new native line.
set -eu

version="$1"

case "$version" in
    *[!0-9.]*|'')
        echo "usage: $0 <sfmcsdk-version>" >&2
        exit 1
        ;;
esac

root="$(cd "$(dirname "$0")/.." && pwd)"
props="$root/Directory.Build.props"

echo "==> pinning SfmcNativeVersion $version"
sed -i '' "s:<SfmcNativeVersion>.*</SfmcNativeVersion>:<SfmcNativeVersion>$version</SfmcNativeVersion>:" "$props"

echo "==> regenerating framework checksums"
"$root/build/UpdateChecksums.sh"

echo "==> fetching the new xcframework"
"$root/build/FetchXcFrameworks.sh" "$version"

echo "==> packing"
"$root/build/BuildNugets.sh"

echo "==> package tests"
dotnet test "$root/tests/SFMCSDK.Net.iOS.PackageTests"

echo "==> done. Review the checksum diff, port the -Swift.h changes into ApiDefinitions.cs,"
echo "    update the README and docs/release-notes/ - this script does not do that for you."
