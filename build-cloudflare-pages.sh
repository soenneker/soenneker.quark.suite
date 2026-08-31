#!/usr/bin/env bash

set -euo pipefail

task_dir="${TMPDIR:-/tmp}/quark-cloudflare-pages"
dotnet_dir="${task_dir}/dotnet"
install_script="${task_dir}/dotnet-install.sh"

mkdir -p "$task_dir"

curl --fail --silent --show-error --location https://dot.net/v1/dotnet-install.sh --output "$install_script"
chmod +x "$install_script"

"$install_script" --channel 10.0 --install-dir "$dotnet_dir"

export DOTNET_ROOT="$dotnet_dir"
export PATH="$dotnet_dir:$PATH"

"$dotnet_dir/dotnet" workload install wasm-tools

PipelineEnvironment=true \
NUGET_XMLDOC_MODE=skip \
UseLocalProjects=false \
"$dotnet_dir/dotnet" publish \
    test/Soenneker.Quark.Suite.Demo/Soenneker.Quark.Suite.Demo.csproj \
    --configuration Release \
    --output artifacts/cloudflare-pages
