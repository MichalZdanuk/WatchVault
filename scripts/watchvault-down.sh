#!/bin/bash
set -e

cd "$(dirname "$0")/../WatchVault"

echo "🧹 Stopping all WatchVault containers..."
docker compose -f docker-compose.yml -f docker-compose.override.yml down

echo "✅ Containers stopped (volumes persisted)."