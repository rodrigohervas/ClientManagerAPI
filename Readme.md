# ClientsManagerAPI
ASP.NET Core 3 Web API to manage clients and legal clases


## Description: 

ClientsManager is an ASP.NET Core 3.1 Web API.

Using ClientsManager you can manage your clients, legal cases, and billable activities.


## Technologies used: 

ASP.NET Core 3.1, C#, AutoMapper, Swagger, Serilog, Entity Framework Core, SQL Server


## Live Site

[ClientsManager API](https://homemanagerrh.herokuapp.com/)


## Azure web app and Azure Active Directory:

Before using the API there are several you need to follow to create the API in Azure and register it in Azure Active Directory:

1. Register the API in Azure Active Directory (you'll need to have an Azure AD tenant configured)

[Recommended Read: Protected Web API app registration](https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-protected-web-api-app-registration)

2. Configure the API registration in Azure AD (the registration must expose at least one scope/role in the *Expose an API* section)

3. In **appsettings.json** / **secrets.json**, add the following configuration (get your config data from the Azure AD API registration):
  
  ```
  {
  "AzureAd": {
        "ClientId": "your-API-Azure-client-Id",
        "Instance": "https://login.microsoftonline.com/",
        "TenantId": "your-azure-AD-tenant-Id",
        "APIApplicationIdUri": "api://your-API-Azure-client-Id",
        "ClientAudience": "your-client-Azure-AD-Id", //This is the intended recipient(s) for this token (the calling client Id in Azure AD).
        "APIAudience": "your-API-Azure-client-Id" //This claim identifies the audience for this token (your API Azure AD Client Id)
    }
  }
  ```

4. Create a web app in Azure App Services for your API

[Recommended read: Publish web app to Azure](https://docs.microsoft.com/en-us/aspnet/core/tutorials/publish-to-azure-webapp-using-vs?view=aspnetcore-3.1)

5. Publish the API to Azure

6. Configure the web app created in Azure App Services, specially the appsettings in Azure (or a Key Vault if preferred). 


## Authentication / Authorization:

All API requests must be made using an access_token. The access_token is a JWT Token, and is issued by Azure Active Directory.

After the API configuration in Azure Web Services and Azure Active Directory the app is ready to authenticate JWT Tokens.

Regarding Token validation check *ClientsManager.WebAPI.Authentication.AddAuthenticationExtensions.cs*, called from the *Startup.ConfigureServices* method.

Authorization is configured at the controller level using the *[Authorize]* attribute.


## Database:

During the API Publish process you can create the Database in Azure. If you want to run the scripts, you can find them in **_ClientsManager.SQLscripts_**:

1. Run *Create_ClientsManager_DB_Script.sql* to create the **_ClientManagers_** database

2. Run *Create_EmployeeTypes_Table_Script.sql* to create the ***EmployeeTypes*** table

3. Run *Create_Employees_Table_Script.sql* to create the ***Employees*** table

4. Run *Create_Clients_Table_Script.sql* to create the **_Clients_** table

5. Run *Create_Addresses_Table_Script.sql* to create the **_Addresses_** table

6. Run *Create_Contacts_Table_Script.sql* to create the ***Contacts*** table

7. Run *Create_Logs_Table_Script.sql* to create the ***EventLogging.Logs*** table


## Endpoints:

The API has the following endpoints: 

* **Clients**: CRUD *Clients*

* **LegalCases**: CRUD *LegalCases* for a *Client*

* **BillableActivities**: CRUD *BillableActivities* for a *LegalCase*

* **Addresses**: CRUD *Adresses* for a *Client*

* **Contacts**: CRUD *Contacts* for a *Client*

* **Employees**: CRUD *Employees* for a *LegalCase*

* **EmployeeTypes**: CRUD *EmployeeTypes* for an *Employee*


## Clients endpoint: 

### URI: get => /api/clients?pageNumber=[number]&pageSize=[size] :

* **Description**: returns a collection of Client objects, paged by the provided pageNumber and pageSize

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| pageNumber    | String   | querystring  |
| pageSize      | String   | querystring  |

* **Response**:

A collection of Client objects:

| param name    | type     |
| ------------- |:--------:|
| id            | Number   |
| name          | String   |
| description   | String   |
| website       | String   |

### URI: get => /api/clients/[id]

* **Description**: returns a Client object for the provided client id

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| id            | Number   | route        |

* **Response**:

A Client object:

| param name    | type     |
| ------------- |:--------:|
| id            | Number   |
| name          | String   |
| description   | String   |
| website       | String   |


### URI: get => /api/clients/legalcases/[id]

* **Description**: returns a collection of Client objects, with its related LegalCases

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| id            | Number   | route        |

* **Response**:

A collection of Client objects, with its LegalCase objects:

| param name    | type      |
| ------------- |:---------:|
| id            | Number    |
| name          | String    |
| description   | String    |
| website       | String    |
| Legalcases    | LegalCase |


### URI: get => /api/clients/addresses/[id]

* **Description**: returns a Client object for the provided client id, including its related Address objects

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| id            | Number   | route        |

* **Response**:

A collection of Client objects:

| param name    | type     |
| ------------- |:--------:|
| id            | Number   |
| name          | String   |
| description   | String   |
| website       | String   |


### URI: get => /api/clients/contacts/[id]

* **Description**: returns a Client object for the provided id, including its related Contact objects

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| id            | Number   | route        |

* **Response**:

A Client object with its related Contact objects:

| param name    | type     |
| ------------- |:--------:|
| id            | Number   |
| name          | String   |
| description   | String   |
| website       | String   |


### URI:  post => /api/clients

* **Description**: creates a Client object

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| name          | String   | Body         |
| description   | String   | Body         |
| website       | String   | Body         |

* **Response**:

The Client object created.

| param name    | type     |
| ------------- |:--------:|
| id            | Number   |
| name          | String   |
| description   | String   |
| website       | String   |


### URI: put => /api/clients/[id]

* **Description**: updates an existing Client object

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| id            | Number   | Route        |
| id            | Number   | Body         |
| name          | String   | Body         |
| description   | String   | Body         |
| website       | String   | Body         |

* **Response**:

The Client object updated

| param name    | type     |
| ------------- |:--------:|
| id            | Number   |
| name          | String   |
| description   | String   |
| website       | String   |


### URI: delete => /api/clients/[id]

* **Description**: deletes an existing Client object

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| id            | Number   | Body         |
| name          | String   | Body         |
| description   | String   | Body         |
| website       | String   | Body         |

* **Response**:

The Client object updated

| param name    | type     |
| ------------- |:--------:|
| id            | Number   |
| name          | String   |
| description   | String   |
| website       | String   |

*** ***

## LegalCases endpoint: 

### URI: get => api/legalcases?pageNumber=2&pageSize=3

* **Description**: xx

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| xx       | xx   | xx  |

* **Response**:

An array of xxx objects.

| param name  | type     |
| ------------|:--------:|
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |

### URI: get => /xx/xx/:xx

* **Description**: xx

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| xx            | xx       | xx           |

* **Response**:

An xx object.

| param name  | type     |
| ------------|:--------:|
| xx          | xx   |
| xx     | xx   |
| xx      | xx   |
| xx        | xx   |
| xx | xx   |
| xx        | xx     |
| xx     | xx   |


## BillableActivities endpoint: 

### URI: get => /xxx/xxx:xxx

* **Description**: xx

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| xx       | xx   | xx  |

* **Response**:

An array of xxx objects.

| param name  | type     |
| ------------|:--------:|
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |


### URI: get => /xx/xx/:xx

* **Description**: xx

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| xx            | xx       | xx           |

* **Response**:

An xx object.

| param name  | type     |
| ------------|:--------:|
| xx          | xx   |
| xx     | xx   |
| xx      | xx   |
| xx        | xx   |
| xx | xx   |
| xx        | xx     |
| xx     | xx   |


## Addresses endpoint: 

### URI: get => /xxx/xxx:xxx

* **Description**: xx

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| xx       | xx   | xx  |

* **Response**:

An array of xxx objects.

| param name  | type     |
| ------------|:--------:|
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |


### URI: get => /xx/xx/:xx

* **Description**: xx

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| xx            | xx       | xx           |

* **Response**:

An xx object.

| param name  | type     |
| ------------|:--------:|
| xx          | xx   |
| xx     | xx   |
| xx      | xx   |
| xx        | xx   |
| xx | xx   |
| xx        | xx     |
| xx     | xx   |


## Contacts endpoint: 

### URI: get => /xxx/xxx:xxx

* **Description**: xx

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| xx       | xx   | xx  |

* **Response**:

An array of xxx objects.

| param name  | type     |
| ------------|:--------:|
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |


### URI: get => /xx/xx/:xx

* **Description**: xx

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| xx            | xx       | xx           |

* **Response**:

An xx object.

| param name  | type     |
| ------------|:--------:|
| xx          | xx   |
| xx     | xx   |
| xx      | xx   |
| xx        | xx   |
| xx | xx   |
| xx        | xx     |
| xx     | xx   |


## Employees endpoint: 

### URI: get => /xxx/xxx:xxx

* **Description**: xx

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| xx       | xx   | xx  |

* **Response**:

An array of xxx objects.

| param name  | type     |
| ------------|:--------:|
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |


### URI: get => /xx/xx/:xx

* **Description**: xx

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| xx            | xx       | xx           |

* **Response**:

An xx object.

| param name  | type     |
| ------------|:--------:|
| xx          | xx   |
| xx     | xx   |
| xx      | xx   |
| xx        | xx   |
| xx | xx   |
| xx        | xx     |
| xx     | xx   |


## EmployeeTypes endpoint: 

### URI: get => /xxx/xxx:xxx

* **Description**: xx

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| xx       | xx   | xx  |

* **Response**:

An array of xxx objects.

| param name  | type     |
| ------------|:--------:|
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |
| xx          | xx       |


### URI: get => /xx/xx/:xx

* **Description**: xx

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| xx            | xx       | xx           |

* **Response**:

An xx object.

| param name  | type     |
| ------------|:--------:|
| xx          | xx   |
| xx     | xx   |
| xx      | xx   |
| xx        | xx   |
| xx | xx   |
| xx        | xx     |
| xx     | xx   |




## Set up

Complete the following steps to start a new project (NEW-PROJECT-NAME):

1. Clone this repository to your local machine `git clone https://github.com/rodrigohervas/ClientManagerAPI.git NEW-PROJECT-NAME`
2. `cd` into the cloned repository
3. Make a fresh start of the git history for this project with `rm -r -Force .git`, amd then `git init`
4. Make sure that the .gitignore file is encoded as 'UTF-8'

Note. This server can be used with the following client repo: [xxxx](xxxxx)

## Run Application

Build the solution

Run the application

## Related Repos

[Clients Manager client - Pending](xxxxx)


