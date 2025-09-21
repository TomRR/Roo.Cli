#!/bin/bash
set -e  # exit on error

log() {
  echo "[$(date +'%Y-%m-%d %H:%M:%S')] $1"
}

log "Uninstalling Roo.Cli"
echo "--------------------"

# Help option
if [[ "$1" == "--help" || "$1" == "-h" ]]; then
  echo "Usage: $0 [nuget|mac|linux|windows|all]"
  exit 0
fi

# Pick OS from argument or prompt
if [ -n "$1" ]; then
  target_os="$1"
else
  echo -e "\nSelect uninstallation target (nuget, mac, linux, windows, all)"
  echo "Press Enter to uninstall ALL versions"
  read -r target_os
  target_os=${target_os:-all}  # default to "all" if user presses Enter
fi

# Validate input
valid_choices=("nuget" "mac" "linux" "windows" "all")
while [[ ! " ${valid_choices[@]} " =~ " ${target_os} " ]]; do
  echo "[ERROR] Invalid choice: $target_os"
  echo "Please choose one of: nuget, mac, linux, windows, all"
  read -r target_os
  target_os=${target_os:-all}
done

log "Chosen target: ${target_os}"
echo ""

# Function to uninstall NuGet tool
uninstall_nuget() {
  log "Uninstalling NuGet global tool..."
  if dotnet tool list --global | grep -q "roo"; then
    dotnet tool uninstall --global Roo.Cli
    rm -r nupkg
    log "NuGet tool uninstalled ✅"
  else
    log "NuGet global tool Roo.Cli not installed."
  fi
}

# Function to uninstall Mac/Linux binary
uninstall_local() {
  local path="$1"
  local name="$2"
  log "Uninstalling ${name} binary..."
  if [ -f "$path" ]; then
    sudo rm "$path"
    log "${name} binary removed ✅"
  else
    log "${name} binary not found."
  fi
}

# Run uninstallation based on target
case "${target_os}" in
  nuget)
    uninstall_nuget
    ;;
  mac)
    uninstall_local "/usr/local/bin/roo" "Mac"
    ;;
  linux)
    uninstall_local "/usr/local/bin/roo" "Linux"
    ;;
  windows)
    uninstall_local "C:\\Program Files\\Roo\\roo.exe" "Windows"
    ;;
  all)
    uninstall_nuget
    uninstall_local "/usr/local/bin/roo" "Mac/Linux"
    uninstall_local "C:\\Program Files\\Roo\\roo.exe" "Windows"
    ;;
esac

# Optional: also check both NuGet and binary for warnings
log "Checking for remaining Roo.Cli installations..."

if dotnet tool list --global | grep -q "Roo.Cli"; then
  log "NuGet tool still installed. Consider removing manually."
fi

if [ -f /usr/local/bin/roo ]; then
  log "Local binary still exists at /usr/local/bin/roo"
fi

log "Uninstallation finished ✅"
