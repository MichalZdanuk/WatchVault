#!/bin/bash
set -e

# Go to the backend project directory
cd "$(dirname "$0")/../WatchVault"

echo "🚀 Starting WatchVault stack..."
docker compose -f docker-compose.yml -f docker-compose.override.yml up -d

echo ""
echo "✅ All containers are running!"
docker ps --format "table {{.Names}}\t{{.Status}}\t{{.Ports}}"