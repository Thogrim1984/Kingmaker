# Kingmaker

I build a programm-supported own version of the Kingdom-building rules of the rpg pathfinder. This is my first big project and my first project on github.

To run the project, you need a "Kingmaker/connections.config" with following code:
```xml
<?xml version="1.0" encoding="utf-8" ?>
 <configuration>
  <connectionStrings>
    # For each tablegroup:
    <add name="[table-group-Name]Context" connectionString="metadata=res://*/[table-group-Name]Model.csdl|res://*/[table-group-Name]Model.ssdl|res://*/[table-group-Name]Model.msl;provider=MySql.Data.MySqlClient;provider connection string=&quot;server=[serveradress];user id=[username];password=[password];persistsecurityinfo=True;database=[databasename]&quot;" providerName="System.Data.EntityClient" />
    # For example: 
    <add name="MasscombatContext" connectionString="metadata=res://*/MasscombatModel.csdl|res://*/MasscombatModel.ssdl|res://*/MasscombatModel.msl;provider=MySql.Data.MySqlClient;provider connection string=&quot;server=[serveradress];user id=[username];password=[password];persistsecurityinfo=True;database=[databasename]&quot;" providerName="System.Data.EntityClient" />
  </connectionStrings>
</configuration>
```
I design my databse with dbdesigner.net
