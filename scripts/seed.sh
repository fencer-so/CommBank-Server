#!/usr/bin/env bash
# Seed CommBank database with data from ../data/
# Requires: MongoDB Database Tools (brew install mongodb-database-tools)
# Usage:
#   CONNECTION_STRING="mongodb+srv://..." ./seed.sh
#   or: ./seed.sh "mongodb+srv://..."
set -e
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
DATA_DIR="$(cd "$SCRIPT_DIR/../data" && pwd)"
if [ -n "$1" ]; then
  URI="$1"
elif [ -n "$CONNECTION_STRING" ]; then
  URI="$CONNECTION_STRING"
else
  echo "Usage: CONNECTION_STRING=<uri> $0   OR   $0 <connection_string>"
  echo "Get the URI from Atlas Connect > Connect your application, or from CommBank-Server/Secrets.json"
  exit 1
fi
DB="CommBank"
for collection in Accounts Goals Tags Transactions Users; do
  file="$DATA_DIR/${collection}.json"
  if [ ! -f "$file" ]; then echo "Missing $file"; exit 1; fi
  echo "Importing $collection..."
  mongoimport --uri="$URI" --db="$DB" --collection="$collection" --file="$file" --jsonArray --mode=upsert --upsertFields=_id
done
echo "Done. Database $DB seeded."
