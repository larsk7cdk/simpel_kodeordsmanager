#!/usr/bin/env bash

cd virtualization
docker compose down --remove-orphans
docker compose build
docker compose up -d

echo "Started!"