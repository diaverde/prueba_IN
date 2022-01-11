# prueba_IN

Este proyecto permite manejar un catálogo de libros y su información asociada alojads
en una base de datos que a su vez puede sincronizarse desde la Web.

## Detalles de la aplicación
La aplicación incluye desarrollo de Back-End y Front-End con las siguientes características:

BACKEND
- Se ha desarrollado en _.NET5_ y puede ejecutarse en Visual Studio 2019 sin inconvenientes.
- Existe una capa de acceso a datos que consume información de una base de datos SQL 
Server como repositorio de datos. Las credenciales se deben actualizar en el archivo _Database.cs_
de la carpeta _Models_.
- Existe una capa de lógica de negocios para recuperar la información de los
Autores y Libros a través de servicios REST que devuelven información para un autor
determinado o Múltiples autores y Libros.
- Existe un Endpoint para sincronizar la información de los autores y libros
desde la web api: https://fakerestapi.azurewebsites.net/index.html con la base de datos SQL Server.

FRONTEND (Angular 7+)
- La aplicación consume los servicios implementados en el BACKEND
- Utiliza Bootstrap como apoyo para los estilos.
- Implementa un formulario de Autenticación de usuarios consumiendo el controlador
de usuarios de la Web Api https://fakerestapi.azurewebsites.net/index.html
- Permite realizar el consumo del servicio de sincronización de autores Y libros.
- Permite visualizar el catálogo de libros obtenido, ya sea completo o filtrado por autor.