#!/bin/bash
set -e

echo "[INFO] Publishing AOT Mac version..."
echo ""

dotnet publish -c Release -r osx-arm64 -p:PublishAot=true
if [ -f /usr/local/bin/roo ]; then
  echo "[INFO] Replacing existing /usr/local/bin/roo"
else
  echo "[INFO] Copying Roo to /usr/local/bin/roo..."
fi
sudo cp ./src/Roo.Application/bin/Release/net9.0/osx-arm64/publish/Roo.Application /usr/local/bin/roo
echo ""

echo "[INFO] Setting execution permissions..."
sudo chmod +x /usr/local/bin/roo
echo ""