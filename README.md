# Proyecto Programacion 4 - Aplicacion Web
La siguiente es una aplicacion web utilizando C# como lenguaje de backend.

## Descripción
La aplicacion sirve para gestionar proyectos, y asignar tareas a los proyectos, y asignar multiples usuarios a las tareas, cuenta con un sistema de gestion de usuarios y asignacion de roles a los usuarios.
Además cuenta con una opcion para auditoria donde no necesitas iniciar sesion para ingresar a esa pagina. Tiene una conexion a SQL Server para la gestion de datos.

## Ejecucion del programa
Para el funcionamiento del programa necesitas lo siguiente:
* Visual Studio Code o Visual Studio 2022 Community, cualquiera de los 2 programas funciona.
* .NET Core 7.0
* SQL Server 2019 o 2022, cualquiera de esas 2 versiones funciona.

Si vas a utilizar Visual Studio Code, debes de tener instalado junto con el .NET 7.0:
* Entity Framework
```console
dotnet tool install --global dotnet-ef
```

* ASP.NET Code Generator
```console
dotnet tool install --global dotnet-aspnet-codegenerator
```

Si vas a utilizar Visual Studio 2022 Community, debes de tener instalado el .NET 7.0 y además, debes tener instalado en el apartado de "Cargas de trabajo"
* Desarrollo de ASP.NET y web

En el repositorio se te adjuntó una backup de la base de datos que utilicé para realizar las pruebas del programa. Está backup fue realizada en SQL Server 2022, por lo tanto, no funcionará si lo restauras en una version anterior a 2022.
Aunque si quieres, puedes crear tu mismo la base de datos y despues te explicaré como se generarán las tablas en la base de datos

Si abres el proyecto en Visual Studio 2022 Community abres el proyecto a partir de el archivo de solución.
Si deseas abrir el proyecto en Visual Studio Code debes de seleccionar la carpeta donde el archivo "ProyectoProgra4v2.csproj" esta ubicado, es la segunda carpeta de ProyectoProgra4v2.

Una vez abierto el proyecto en el Visual Studio, te diriges al archivo de appsettings.json y encontrarás esta siguiente linea de codigo:
```console
"DefaultConnection": "Server=NombreServidor; Database=NombreBD; Trusted_Connection=True; Encrypt=false;"
```

Está linea es la que permitirá la conexion a la base de datos de SQL Server, deberás de vincularlo a tu servido y base de datos correspondiente.
Donde dice "NombreServidor" es donde irá el nombre del servidor de SQL Server, y donde dice "NombreBD" es donde irá el nombre de la base de datos ya sea que la hayas creado o restaurado del backup previamente adjuntado.

Una vez que ya cambiaste el nombre del servidor y la base de datos.
Si restauraste el backup de la base de datos, puedes saltarte los siguientes pasos... pero si creaste la base de datos desde 0, debes de realizar los siguientes pasos:

En Visual Studio Code:
* Debes de abrir una terminar y en ella escribir los 2 comandos en orden
```console
dotnet ef database update --context ApplicationDbContext
```
```console
dotnet ef database update --context MvcDbContext
```

En Visual Studio 2022 Community:
* Debes de abrir la Consola de Administrador de Paquetes y ejecutar los siguientes comandos en orden
```console
Update-Database -Context ApplicationDbContext
```
```console
Update-Database -Context MvcDbContext
```
Los siguientes comandos crearan las tablas en la Base de Datos que creaste en SQL Server.

Ahora crearemos al usuario con el rol de Administrador.
* En la carpeta de Controller abrimos el archivo de UsuarioController.cs
* Al fondo del archivo se encuentra el metodo para crear el usuario con el Rol, remplazas los campos por los datos que necesites del usuario.
* Para darle un rol al usuario administrador necesitamos el Id de el Rol de Administrador, debido a que el programa genera el Id de manera aleatoria, hay que obtener el Id de la base de datos, por lo tanto realizas un Select a la tabla AspNetRoles, y copias el Id del Rol de Administrador y lo pegas en el campo donde dice IdRole
* Una vez con los datos colocados, guardamos el archivos y procedemos a ejecutar el programa.

En Visual Studio Code para ejecutar el programa lo realizas con el siguiente comando
```console
dotnet run
```
Cuando lo ejecutas, la consola te dará una direccion web de localhost, lo abres en el navegador y podras ver la aplicacion en funcionamiento.
En Visual Studio 2022 se abrirá el navegador de forma automatica.

Ya con la aplicacion ejecutandose en la barra de la direccion web añades al url "/Usuario/RegisterAdmin" y accedes a esa direccion, esa direccion será la encargada de crear al usuario administrador.
Una vez creado el usuario podrás iniciar sesion y empezar a usar el programa.

## Proyecto desarrollado por:
* Aaron Alfaro Zamora
* Veronica Castro Murillo
* Emmanuel Corrales Ramirez
* Susan Salas Jimenez
