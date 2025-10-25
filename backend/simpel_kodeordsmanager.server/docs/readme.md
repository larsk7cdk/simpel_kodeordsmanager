# Simpel Kodeordsmanager - Server

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=P@ssword2025" \
-p 1433:1433 --name db_kodeordsmanager \
-v sql_data:/var/opt/mssql \
-d mcr.microsoft.com/mssql/server:2022-latest
```


