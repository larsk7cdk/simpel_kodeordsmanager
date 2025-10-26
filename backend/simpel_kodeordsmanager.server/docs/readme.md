# Simpel Kodeordsmanager - Server

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=P@ssword2025" \
-p 1433:1433 --name db_kodeordsmanager \
-v sql_data:/var/opt/mssql \
-d mcr.microsoft.com/mssql/server:2022-latest
```

Links:

### Pasword hashing
https://www.youtube.com/watch?v=J4ix8Mhi3rs


### Password encrypt/decrypt
https://www.youtube.com/watch?v=Npi7kAgFUjY

https://generate-random.org/encryption-keys



### 
Role-Based Auth System in .NET
https://www.youtube.com/watch?v=24r7UmuRh0A