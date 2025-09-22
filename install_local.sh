#!/bin/bash
set -e  # exit on error

chmod +x ./scripts/install_as_nuget_tool.sh ./scripts/install_aot_mac.sh ./scripts/install_aot_linux.sh ./scripts/install_aot_win.sh

log() {
  echo "[$(date +'%Y-%m-%d %H:%M:%S')] $1"
}

log "Installing Roo.Cli"
echo "------------------"

# Help option
if [[ "$1" == "--help" || "$1" == "-h" ]]; then
  echo "Usage: $0 [nuget|mac|linux|windows|exit]"
  exit 0
fi

# Pick OS from argument or prompt
if [ -n "$1" ]; then
  target_os="$1"
else
  echo -e "\nSelect installation target (nuget, mac, linux, windows, exit):"
  read -r target_os
fi

# Validate input
valid_choices=("nuget" "mac" "linux" "windows" "exit")
while [[ ! " ${valid_choices[@]} " =~ " ${target_os} " ]]; do
  echo "[ERROR] Invalid choice: $target_os"
  echo "Please choose one of: nuget, mac, linux, windows or exit (close script)"
  read -r target_os
done

log "Chosen target: ${target_os}"
echo ""

# Run the appropriate installer / cleanup
case "${target_os}" in
  nuget)
    log "Running NuGet installer..."
    ./scripts/install_as_nuget_tool.sh
    ;;
  mac)
    log "Running Mac installer..."
    ./scripts/install_aot_mac.sh
    ;;
  linux)
    log "Running Linux installer..."
    ./install_aot_linux.sh
    ;;
  windows)
    log "Running Windows installer..."
    ./scripts/install_aot_windows.sh
    ;;
  cleanup)
    log "Exit..."
    exit 0
    ;;
esac

echo ""
log "Roo.Cli installation finished successfully ✅"
which roo
log "Try running: 'roo --help' or 'roo --version'"