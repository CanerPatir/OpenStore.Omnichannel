#!/usr/bin/env bash

ACTIVE_CONN=$(python -c 'import sys, json; print(json.load(sys.stdin)["ActiveConnection"])' < src/OpenStore.Omnichannel/appsettings.json)
echo $ACTIVE_CONN
case $ACTIVE_CONN in

  'SqLite')
    ACTIVE_CONTEXT="SqliteDbContext"
    ACTIVE_FOLDER="Sqlite"
    ;;

  'PostgreSql')
    ACTIVE_CONTEXT="PostgreSqlDbContext"
    ACTIVE_FOLDER="PostgreSql"
    ;;

  'MySql')
    ACTIVE_CONTEXT="MySqlDbContext"
    ACTIVE_FOLDER="MySql"
    ;;

  'MsSql')
    ACTIVE_CONTEXT="MsSqlDbContext"
    ACTIVE_FOLDER="MsSql"
    ;;
esac

MIG_NAME=Migration-$(date +"%Y-%m-%d-%H-%M")
echo $MIG_NAME
echo $ACTIVE_CONTEXT
echo $ACTIVE_FOLDER
dotnet-ef migrations add $MIG_NAME --project ./src/OpenStore.Omnichannel.Infrastructure --startup-project ./src/OpenStore.Omnichannel --context $ACTIVE_CONTEXT --output-dir Data/EntityFramework/Migrations/$ACTIVE_FOLDER
# dotnet-ef database update --context $ACTIVE_CONTEXT --project ./OpenStore.Omnichannel