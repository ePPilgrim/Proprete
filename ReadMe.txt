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
"server=localhost;user=root;password=1;database=newproprettedb"

error NETSDK1112: The runtime pack for Microsoft.AspNetCore.App.Runtime.win-x64 was not downloaded. Try running a NuGet restore with the RuntimeIdentifier 'win-x64'.

1. Bleach - any chemical product used to remove color or stains from fabric or surfaces (mostly associated with sodium hypochlorite).
2. Domestos - used for liqide bleaching. As an item could be represented by brand and volume cattegories.
3. SAVO original - is the same propose as Domestos but less concetrated (produced and mostly sold in Czech/Slovak market)
4. Type - spray, liqide, powder
5. Usage - kitchen, bathroom, floor, desinfection, 
6. Volume, Color, Function, Type of substance, Brand, Purpose, Target
7. Katrin - is the finish made products of hygiena, it include tissues, paper towels, etc.


EF8:
1. Update method stay update attribute to all fields in the DB context in the case when the corresponding entity is previously remove.
2. ExcuteRemove/Update - has nothing to do with the DB contex;
3. In the one2one relation the main questio is how is parent and who is child. Remeber that. Child is the class that contains the FK.
4. IsRequired configuration is used to say that this property must be specify when the row is created, other way it is not requier and could be specify later.
5. Experiment with DB procedures (define SQL procedure in the migration files)
