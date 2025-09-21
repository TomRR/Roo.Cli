#!/bin/bash
set -e

echo "[INFO] Starting NuGet package build..."
dotnet pack

if dotnet tool list --global | grep -q "Roo.Cli"; then
  echo "[INFO] Roo.Cli already installed, updating instead..."
  dotnet tool update --global Roo.Cli --add-source ./src/Roo.Application/nupkg
else
  echo "[INFO] Installing Roo.Cli nuget globally..."
  dotnet tool install --global Roo.Cli --add-source ./src/Roo.Application/nupkg
fi

echo "[INFO] NuGet installation complete"
