1. docker run -e PROPRETTE_CONNECTION_STRING="Database=proprettedb;Server=host.docker.internal;Port=3306;User=root;Password=1" plddpavlo/createdb
2. docker build -t plddpavlo/createdb .
3. docker run -e PROPRETTE_CONNECTION_STRING="Database=proprettedb;Server=host.docker.internal;Port=3307;User=root;Password=@06@June@1981" plddpavlo/createdb
4. http://192.168.0.186/
5. Q7C4@8&b
6. ssh Pavlo@192.168.0.186
7. --add-host host.docker.internal:host-gateway
8. sudo docker run --network="host" --add-host host.docker.internal:host-gateway -e PROPRETTE_CONNECTION_STRING="database=proprettedb;server=host.docker.internal;port=3307;user=Pavlo;password=@06@June@1981" plddpavlo/createdb
9. http://192.168.0.186:4444/swagger/index.html
10. dotnet ef migrations add -s ./API -p ./Infrastructure InitMigration
11. dotnet ef migrations bundle -s ./API -p ./Infrastructure -o ./Infrastructure/efbundle.exe