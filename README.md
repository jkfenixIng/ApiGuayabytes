# ApiGuayabytes
Api  para consumo del juego Guaya-Bytes

Intengrantes:

- Juan Camilo Rodriguez
- Alex Alberto Gomez
- Miguel Alirio Rojas

Se integra con ORM EntityFrameworkCore y SqlServer

Librerias :
Comando: Install-Package
- Microsoft.EntityFrameworkCore -Version 6.0.0
- Microsoft.EntityFrameworkCore.SqlServer -Version 6.0.0
- System.IdentityModel.Tokens.Jwt - -Version 6.8.0
- Microsoft.AspNetCore.Authentication.JwtBearer -Version 6.0.0

Seguridad:
- Token JWT

EndPoints:
## Endpoint de Inicio de Sesión

Este endpoint permite a los usuarios iniciar sesión en la aplicación proporcionando un nombre de usuario (NickName) y una contraseña (password).

- **URL**

  `/api/login`

- **Método**

  `POST`

- **Parámetros de la Solicitud**

  | Parámetro | Tipo   | Descripción                  |
  | --------- | ------ | ---------------------------- |
  | NickName  | string | Nombre de usuario del usuario|
  | password  | string | Contraseña del usuario       |

- **Respuestas**

  - **Código de Estado 200 OK**
  
    Indica que la autenticación fue exitosa. Devuelve un objeto JSON que contiene los detalles de la autenticación.

    ```json
    {
      "message": "Autenticación exitosa",
      "token": "<token_de_autenticación>"
    }
    ```

  - **Código de Estado 401 Unauthorized**
  
    Indica que la autenticación falló debido a credenciales incorrectas o falta de acceso.

    ```json
    {
      "message": "Credenciales incorrectas"
    }
    ```
 ## Endpoint de Actualización de Token de Acceso

Este endpoint permite a los usuarios actualizar su token de acceso, siempre que el token de actualización sea válido y esté autorizado para realizar esta acción.

- **URL**

  `/api/refresh`

- **Método**

  `GET`

- **Encabezados de la Solicitud**
  
  | Encabezado       | Valor              | Descripción                    |
  | ---------------- | ------------------ | ------------------------------ |
  | Authorization    | Bearer <token>     | Token de acceso del usuario    |

- **Respuestas**

  - **Código de Estado 200 OK**
  
    Indica que el token de acceso ha sido actualizado exitosamente. Devuelve un objeto JSON que contiene el nuevo token de acceso.

    ```json
    {
      "token": "<nuevo_token_de_acceso>"
    }
    ```

  - **Código de Estado 400 Bad Request**
  
    Indica que la actualización del token de acceso ha fallado debido a un error en la solicitud o que el token de actualización no es válido.

    ```json
    {
      "message": "Error al actualizar el token de acceso"
    }
    ```
    ## Endpoint de Creación de Nuevo Usuario

Este endpoint permite crear un nuevo usuario en la aplicación.

- **URL**

  `/api/CreateNewUser`

- **Método**

  `POST`

- **Encabezados de la Solicitud**

  | Encabezado   | Valor            | Descripción                      |
  | ------------ | ---------------- | -------------------------------- |
  | Content-Type | application/json | Tipo de contenido de la solicitud|

- **Cuerpo de la Solicitud**

  El cuerpo de la solicitud debe contener un objeto JSON que represente los datos del nuevo usuario a crear. El formato del objeto debe seguir la estructura del modelo de datos `UsersDto`.

- **Modelo de Datos para el Cuerpo de la Solicitud (UsersDto)**

  ```json
  {
    "Name": "nombre de usuario",
    "NickName": "Nick del ususario",
    "email": "correo electronico",
    "password": "contraseña"
  }
  ## Endpoint para Obtener Dinero por ID de Usuario

Este endpoint permite obtener la cantidad de Dinero asociada a un usuario específico.

- **URL**

  `/api/GetCashByUserIdAsync`

- **Método**

  `GET`

- **Encabezados de la Solicitud**

  | Encabezado   | Valor            | Descripción                      |
  | ------------ | ---------------- | -------------------------------- |
  | Authorization | Bearer \<token> | Token de acceso del usuario      |

- **Respuestas**

  - **Código de Estado 200 OK**
  
    Indica que se ha obtenido la cantidad de efectivo asociada al usuario correctamente. Devuelve un objeto JSON que contiene los detalles del efectivo.

    ```json
    {
      "cash": 1000 // Ejemplo de cantidad de efectivo
    }
    ```

  - **Código de Estado 400 Bad Request**
  
    Indica que la obtención del efectivo ha fallado debido a un error en la solicitud o falta de acceso.

    ```json
    {
      "message": "Error al obtener el Dinero del usuario"
    }
    ```
## Endpoint para Actualizar Efectivo de Usuario

Este endpoint permite actualizar la cantidad de efectivo asociada a un usuario específico.

- **URL**

  `/api/UpdateUserCashAsync`

- **Método**

  `PATCH`

- **Encabezados de la Solicitud**

  | Encabezado   | Valor            | Descripción                      |
  | ------------ | ---------------- | -------------------------------- |
  | Authorization | Bearer \<token> | Token de acceso del usuario      |

- **Parámetros de la Solicitud**

  | Parámetro | Tipo   | Descripción                  |
  | --------- | ------ | ---------------------------- |
  | newCash   | int    | Nueva cantidad de efectivo a asignar al usuario |

- **Respuestas**

  - **Código de Estado 200 OK**
  
    Indica que la actualización del efectivo del usuario se ha realizado correctamente. Devuelve un objeto JSON que contiene los detalles de la actualización.

    ```json
    {
      "message": "Dinero del usuario actualizado exitosamente"
    }
    ```

  - **Código de Estado 400 Bad Request**
  
    Indica que la actualización del efectivo ha fallado debido a un error en la solicitud o falta de acceso.

    ```json
    {
      "message": "Error al actualizar el Dinero del usuario"
    }
    ```
## Endpoint para Obtener Nickname de Usuario

Este endpoint permite obtener el nombre de usuario asociado al token de acceso proporcionado.

- **URL**

  `/api/GetUserNickNameAsync`

- **Método**

  `GET`

- **Encabezados de la Solicitud**

  | Encabezado   | Valor            | Descripción                      |
  | ------------ | ---------------- | -------------------------------- |
  | Authorization | Bearer \<token> | Token de acceso del usuario      |

- **Respuestas**

  - **Código de Estado 200 OK**
  
    Indica que se ha obtenido el nombre de usuario correctamente. Devuelve un objeto JSON que contiene el nombre de usuario.

    ```json
    {
      "nickname": "nombre_de_usuario"
    }
    ```

  - **Código de Estado 400 Bad Request**
  
    Indica que la obtención del nombre de usuario ha fallado debido a un error en la solicitud o falta de acceso.

    ```json
    {
      "message": "Error al obtener el nombre de usuario"
    }
    ```
## Endpoint para Obtener Avatar por ID de Usuario

Este endpoint permite obtener el avatar asociado a un usuario específico.

- **URL**

  `/api/GetAvatarByUserIdAsync`

- **Método**

  `GET`

- **Encabezados de la Solicitud**

  | Encabezado   | Valor            | Descripción                      |
  | ------------ | ---------------- | -------------------------------- |
  | Authorization | Bearer \<token> | Token de acceso del usuario      |

- **Respuestas**

  - **Código de Estado 200 OK**
  
    Indica que se ha obtenido el avatar del usuario correctamente. Devuelve un objeto JSON que contiene el enlace al avatar.

    ```json
    {
      "avatarUrl": "enlace_al_avatar"
    }
    ```

  - **Código de Estado 400 Bad Request**
  
    Indica que la obtención del avatar ha fallado debido a un error en la solicitud o falta de acceso.

    ```json
    {
      "message": "Error al obtener el avatar del usuario"
    }
    ```

