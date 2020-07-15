# NetCoreApi
Net Core web Api  3.x C#\
El proyecto hace uso del api de Identity que abstrae una solución estable para administrar usuarios.\
Integra JWT como mecanismo de seguridad para peticiones de protocolo HTTP.\
Entity Framework como ORM para capa de acceso a datos.\
La clase con la definición solicitada es ApplicationUser.\
En la clase Startup se especifican reglas respecto a las fortalezas solicitadas para las contraseñas adenás de configuración de la app.\

##Servicios backend
* Login: En controlador ApplicationUserController verbo:POST
	EndPoint: http://localhost:51158/ApplicationUser/Login
* Alta de Usuarios ApplicationUserController verbo:POST
	EndPoint: http://localhost:51158/ApplicationUser
* Actualizacion de Usuarios ApplicationUserController verbo:PUT
	EndPoint: http://localhost:51158/ApplicationUser
* Alta de Usuarios ApplicationUserController verbo:DELETE
	EndPoint: http://localhost:51158/ApplicationUser
* Consulta de Usuarios ApplicationUserController verbo:GET
	EndPoint: http://localhost:51158/ApplicationUser

Nota: En la carpeta Evidencias/Invocación se muestran pantallas  de su ejecución.\
Fecha de modificación: 15/07/2020