# ClientsManagerAPI
ASP.NET Core 3 Web API to manage clients and legal clases

- [**Description**:](#Description)
- [**Technologies used**:](#Technologies-used)
- [**Live Site**:](#Live-Site)
- [**Set up**:](#Set-up) 
- [**Configuration: Authentication / Authorization**:](#Configuration-:-Authentication/Authorization)
- [**Configuration: Database**:](#Configuration-:-Database)
- [**Run Application**:](#Run-Application) 
- [**Endpoints**:](#Endpoints)
- [**Related Repos**:](#Related-Repos)



## Description: 

ClientsManager is an ASP.NET Core 3.1 Web API.

Using ClientsManager you can manage your clients, legal cases, and billable activities.


## Technologies used: 

ASP.NET Core 3.1, C#, AutoMapper, Swagger, Serilog, Entity Framework Core, SQL Server


## Live Site

[ClientsManager API](https://homemanagerrh.herokuapp.com/)


## Set up

Complete the following steps to start a new project (NEW-PROJECT-NAME):

1. Clone this repository to your local machine `git clone https://github.com/rodrigohervas/ClientManagerAPI.git NEW-PROJECT-NAME`
2. `cd` into the cloned repository
3. Make a fresh start of the git history for this project with `rm -r -Force .git`, amd then `git init`
4. Make sure that the .gitignore file is encoded as 'UTF-8'

Note. This server can be used with the following client repo: [xxxx](xxxxx)


## Configuration: Azure web app and Azure Active Directory:

Before using the API there are several steps to follow in order to create the API in Azure App Services and register it in Azure Active Directory:

1. Register the API in Azure Active Directory (you'll need to have an Azure Active Directory tenant configured)

[Recommended Read: Protected Web API app registration](https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-protected-web-api-app-registration)

2. Configure the API registration in Azure Active Directory (the registration must expose at least one scope/role in the *Expose an API* section)

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


## Configuration: Authentication / Authorization:

All API requests must be made using an access_token. The access_token is a JWT Bearer Token, issued by Azure Active Directory.

After the API configuration in Azure Web Services and Azure Active Directory the app is ready to authenticate JWT Tokens.

Regarding Token validation, check *ClientsManager.WebAPI.Authentication.AddAuthenticationExtensions.cs*, called from the *Startup.ConfigureServices* method.

Authorization is configured at the controller level using the *[Authorize]* attribute.


## Configuration: Database:

During the API Publish process you can create the Database in Azure. If you want to run the scripts, you can find them in **_ClientsManager.SQLscripts_**:

1. Run *Create_ClientsManager_DB_Script.sql* to create the **_ClientManagers_** database

2. Run *Create_EmployeeTypes_Table_Script.sql* to create the ***EmployeeTypes*** table

3. Run *Create_Employees_Table_Script.sql* to create the ***Employees*** table

4. Run *Create_Clients_Table_Script.sql* to create the **_Clients_** table

5. Run *Create_Addresses_Table_Script.sql* to create the **_Addresses_** table

6. Run *Create_Contacts_Table_Script.sql* to create the ***Contacts*** table

7. Run *Create_Logs_Table_Script.sql* to create the ***EventLogging.Logs*** table


## Run Application:

Build the solution

Run the application


## Endpoints:

The API has the following endpoints: 

* [**Clients**](#Clients-endpoint): CRUD *Clients*

* [**LegalCases**](#LegalCases-endpoint): CRUD *LegalCases* for a *Client*

* [**BillableActivities**](#BillableActivities-endpoint): CRUD *BillableActivities* for a *LegalCase*

* [**Addresses**](#Addresses-endpoint): CRUD *Adresses* for a *Client*

* [**Contacts**](#Contacts-endpoint): CRUD *Contacts* for a *Client*

* [**Employees**](#Employees-endpoint): CRUD *Employees* for a *LegalCase*

* [**EmployeeTypes**](#EmployeeTypes-endpoint): CRUD *EmployeeTypes* for an *Employee*



## Clients endpoint: 

### URI: get => /api/clients?pageNumber=[number]&pageSize=[size]

* **Description**: returns an array of Client objects, paged by the provided pageNumber and pageSize

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

* **Description**: returns an array of Client objects, with its related LegalCases

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| id            | Number   | route        |

* **Response**:

An array of Client objects, with its LegalCase objects:

| param name    | type      |
| ------------- |:---------:|
| id            | Number    |
| name          | String    |
| description   | String    |
| website       | String    |
| legalcases    | Array     |


### URI: get => /api/clients/addresses/[id]

* **Description**: returns a Client object for the provided client id, including its related Address objects

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
| addresses     | Array    |


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
| contacts      | Array    |


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


### URI: put/patch => /api/clients/[id]

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
| id            | Number   | Route        |

* **Response**:

The number of Client objects deleted

| param name    | type     |
| ------------- |:--------:|
| number        | Number   |


## LegalCases endpoint: 

### URI: get => api/legalcases?pageNumber=2&pageSize=3

* **Description**: returns an array of LegalCase objects, paged by the provided pageNumber and pageSize

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| pageNumber    | String   | querystring  |
| pageSize      | String   | querystring  |

* **Response**:

An array of LegalCase of  objects.

| param name  | type     |
| ------------|:--------:|
| id          | Number   |
| client_id   | Number   |
| title       | String   |
| description | String   |
| trustfund   | Number   |

### URI: get => /api/legalcases/client/[id]

* **Description**: returns all LegalCases for a provided Client id

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| client_id     | Number   | Route        |

* **Response**:

An array of LegalCase objects.

| param name  | type     |
| ------------|:--------:|
| id          | Number   |
| client_id   | Number   |
| title       | String   |
| description | String   |
| trustfund   | Number   | 

### URI: get => /api/legalcases/[id]

* **Description**: returns a LegalCase object for a provided id

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| id            | Number   | Route        |

* **Response**:

A LegalCase object.

| param name  | type     |
| ------------|:--------:|
| id          | Number   |
| client_id   | Number   |
| title       | String   |
| description | String   |
| trustfund   | Number   | 

### URI: get => /api/legalcases/details/[1]

* **Description**: returns a LegalCase object for a provided id, with all its related BillableActivity objects

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| id            | Number   | Route        |

* **Response**:

A LegalCase object

| param name        | type     |
| ------------------|:--------:|
| id                | Number   |
| client_id         | Number   |
| title             | String   |
| description       | String   |
| trustfund         | Number   | 
| BillableActivities| Array    |

### URI: post => /api/legalcases/

* **Description**: creates a LegalCase object

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| client_id     | Number   | Body         |
| title         | String   | Body         |
| description   | String   | Body         |
| trustfund     | Number   | Body         |

* **Response**:

The created LegalCase object

| param name        | type     |
| ------------------|:--------:|
| id                | Number   |
| client_id         | Number   |
| title             | String   |
| description       | String   |
| trustfund         | Number   | 

### URI: put/patch => /api/legalcases/[id]

* **Description**: updates an existing LegalCase object

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| id            | Number   | Route        |
| id            | Number   | Body         |
| client_id     | Number   | Body         |
| title         | String   | Body         |
| description   | String   | Body         |
| trustfund     | Number   | Body         |

* **Response**:

The updated LegalCase object

| param name        | type     |
| ------------------|:--------:|
| id                | Number   |
| client_id         | Number   |
| title             | String   |
| description       | String   |
| trustfund         | Number   | 

### URI: delete => /api/legalcases/[id]

* **Description**: deletes an existing LegalCase object

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| id            | Number   | Route        |

* **Response**:

The number of deleted LegalCase objects

| param name        | type     |
| ------------------|:--------:|
| number            | Number   |



## BillableActivities endpoint: 

### URI: get => /api/billableactivities?pageNumber=[number]&pageSize=[size]

* **Description**: returns an array of BillableActivity objects, paged by the provided pageNumber and pageSize

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| pageNumber    | String   | querystring  |
| pageSize      | String   | querystring  |

* **Response**:

An array of BillableActivity objects.

| param name      | type     |
| ----------------|:--------:|
| id              | Number   |
| legalcase_id    | Number   |
| employee_id     | Number   |
| title           | String   |
| description     | String   |
| price           | Number   |
| start_datetime  | String   |
| finish_datetime | String   |  

### URI: get => /api/billableactivities/employee/[employee_id]

* **Description**: returns the an array of BillableActivity objects, for a provided Employee id

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| employee_id   | Number   | Route        |

* **Response**:

An array of BillableActivity objects

| param name      | type     |
| ----------------|:--------:|
| id              | Number   |
| legalcase_id    | Number   |
| employee_id     | Number   |
| title           | String   |
| description     | String   |
| price           | Number   |
| start_datetime  | String   |
| finish_datetime | String   | 


### URI: get => /api/billableactivities/legalcase/[legalcase_id]

* **Description**: returns an array of BillableActivity objects, for a provided LegalCase id 

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| legalcase_id  | Number   | Route        |

* **Response**:

A LegalCase object.

An array of BillableActivity objects

| param name      | type     |
| ----------------|:--------:|
| id              | Number   |
| legalcase_id    | Number   |
| employee_id     | Number   |
| title           | String   |
| description     | String   |
| price           | Number   |
| start_datetime  | String   |
| finish_datetime | String   | 



### URI: get => /api/billableactivity/[id]

* **Description**: returns a BillableActivity object, for a provided id 

* **Request params**:

| param name  | type     | param type   |
| ----------- |:--------:| ------------:|
| id          | Number   | Route        |

* **Response**:

A BillableActivity object.

| param name      | type     |
| ----------------|:--------:|
| id              | Number   |
| legalcase_id    | Number   |
| employee_id     | Number   |
| title           | String   |
| description     | String   |
| price           | Number   |
| start_datetime  | String   |
| finish_datetime | String   | 


### URI: post => /api/billableactivities

* **Description**: creates a BillableActivity object

* **Request params**:

| param name      | type     | param type   |
| --------------- |:--------:| ------------:|
| legalcase_id    | Number   |  Body        |
| employee_id     | Number   |  Body        |
| title           | String   |  Body        |
| description     | String   |  Body        |
| price           | Number   |  Body        |
| start_datetime  | String   |  Body        |
| finish_datetime | String   |  Body        |

* **Response**:

The created BillableActivity object.

| param name      | type     |
| ----------------|:--------:|
| id              | Number   |
| legalcase_id    | Number   |
| employee_id     | Number   |
| title           | String   |
| description     | String   |
| price           | Number   |
| start_datetime  | String   |
| finish_datetime | String   | 


### URI: put/patch => /api/billableactivities/[id]

* **Description**: updates an existing BillableActivity object

* **Request params**:

| param name      | type     | param type   |
| --------------- |:--------:| ------------:|
| id              | Number   |  Route       |
| id              | Number   |  Body        |
| legalcase_id    | Number   |  Body        |
| employee_id     | Number   |  Body        |
| title           | String   |  Body        |
| description     | String   |  Body        |
| price           | Number   |  Body        |
| start_datetime  | String   |  Body        |
| finish_datetime | String   |  Body        |

* **Response**:

The updated BillableActivity object.

| param name      | type     |
| ----------------|:--------:|
| id              | Number   |
| legalcase_id    | Number   |
| employee_id     | Number   |
| title           | String   |
| description     | String   |
| price           | Number   |
| start_datetime  | String   |
| finish_datetime | String   | 


### URI: delete => /api/billableactivities/[id]

* **Description**: deletes an existing BillableActivity object

* **Request params**:

| param name      | type     | param type   |
| --------------- |:--------:| ------------:|
| id              | Number   |  Route       |

* **Response**:

The number of deleted BillableActivity objects

| param name        | type     |
| ------------------|:--------:|
| number            | Number   |


## Addresses endpoint: 

### URI: get => /api/addresses?pageNumber=[number]&pageSize=[size]

* **Description**: returns an array of Address objects, paged by the provided pageNumber and pageSize

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| pageNumber    | String   | querystring  |
| pageSize      | String   | querystring  |

* **Response**:

An array of Address objects.

| param name    | type     |
| --------------|:--------:|
| id            | Number   |
| client_id     | Number   |
| streetnumber  | String   |
| city          | String   |
| stateprovince | String   |
| zipcode       | String   |
| country       | String   |


### URI: get => /api/addresses/client/[client_id]

* **Description**: returns an array of Address objects, for a provided Client id 

* **Request params**:

| param name  | type     | param type   |
| ----------- |:--------:| ------------:|
| client_id   | Number   | Route        |

* **Response**:

An array of Address objects

| param name    | type     |
| --------------|:--------:|
| id            | Number   |
| client_id     | Number   |
| streetnumber  | String   |
| city          | String   |
| stateprovince | String   |
| zipcode       | String   |
| country       | String   |


### URI: get => /api/addresses/[id]

* **Description**: returns an Address object, for a provided id 

* **Request params**:

| param name  | type     | param type   |
| ----------- |:--------:| ------------:|
| id          | Number   | Route        |

* **Response**:

An Address object

| param name    | type     |
| --------------|:--------:|
| id            | Number   |
| client_id     | Number   |
| streetnumber  | String   |
| city          | String   |
| stateprovince | String   |
| zipcode       | String   |
| country       | String   |


### URI: get => /api/addresses/details/[id]

* **Description**: returns an Address object, for a provided id, with its related Contact objects

* **Request params**:

| param name  | type     | param type   |
| ----------- |:--------:| ------------:|
| id          | Number   | Route        |

* **Response**:

An Address object

| param name    | type     |
| --------------|:--------:|
| id            | Number   |
| client_id     | Number   |
| streetnumber  | String   |
| city          | String   |
| stateprovince | String   |
| zipcode       | String   |
| country       | String   |
| contacts      | Array    |


### URI: post => /api/addresses

* **Description**: creates an Address object

* **Request params**:

| param name     | type      | param type   |
| ---------------|:---------:| ------------:|
| client_id      | Number    |  Body        |
| streetnumber   | String    |  Body        |
| city           | String    |  Body        |
| stateprovince  | String    |  Body        |
| zipcode        | String    |  Body        |
| country        | String    |  Body        |

* **Response**:

The created Address object.

| param name    | type     |
| --------------|:--------:|
| id            | Number   |
| client_id     | Number   |
| streetnumber  | String   |
| city          | String   |
| stateprovince | String   |
| zipcode       | String   |
| country       | String   | 


### URI: put/patch => /api/addresses/[id]

* **Description**: updates an existing Address object

* **Request params**:

| param name      | type     | param type   |
| --------------- |:--------:| ------------:|
| id              | Number   |  Route       |
| id              | Number   |  Body        |
| client_id       | Number   |  Body        |
| streetnumber    | String   |  Body        |
| city            | String   |  Body        |
| stateprovince   | String   |  Body        |
| zipcode         | String   |  Body        |
| country         | String   |  Body        |

* **Response**:

The updated Address object.

| param name    | type     |
| --------------|:--------:|
| id            | Number   |
| client_id     | Number   |
| streetnumber  | String   |
| city          | String   |
| stateprovince | String   |
| zipcode       | String   |
| country       | String   | 


### URI: delete => /api/addresses/[id]

* **Description**: deletes an existing Address object

* **Request params**:

| param name      | type     | param type   |
| --------------- |:--------:| ------------:|
| id              | Number   |  Route       |

* **Response**:

The number of deleted Address objects

| param name        | type     |
| ------------------|:--------:|
| number            | Number   |



## Contacts endpoint: 


### URI: get => /api/contacts?pageNumber=[number]&pageSize=[size]

* **Description**: returns an array of Contact objects, paged by the provided pageNumber and pageSize

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| pageNumber    | String   | querystring  |
| pageSize      | String   | querystring  |

* **Response**:

An array of Contact objects

| param name    | type     |
| --------------|:--------:|
| id            | Number   |
| client_id     | Number   |
| address_id    | Number   |
| name          | String   |
| position      | String   |
| telephone     | String   |
| cellphone     | String   |
| email         | String   |


### URI: get => /api/contacts/client/[client_id]

* **Description**: returns an array of Contact objects, for a provided Client id 

* **Request params**:

| param name  | type     | param type   |
| ----------- |:--------:| ------------:|
| client_id   | Number   | Route        |

* **Response**:

An array of Contact objects

| param name    | type     |
| --------------|:--------:|
| id            | Number   |
| client_id     | Number   |
| address_id    | Number   |
| name          | String   |
| position      | String   |
| telephone     | String   |
| cellphone     | String   |
| email         | String   |


### URI: get => /api/contacts/[id]

* **Description**: returns a Contact object, for a provided id 

* **Request params**:

| param name  | type     | param type   |
| ----------- |:--------:| ------------:|
| id          | Number   | Route        |

* **Response**:

A Contact object

| param name    | type     |
| --------------|:--------:|
| id            | Number   |
| client_id     | Number   |
| address_id    | Number   |
| name          | String   |
| position      | String   |
| telephone     | String   |
| cellphone     | String   |
| email         | String   |


### URI: get => /api/contacts/details/1

* **Description**: returns a Contact object, for a provided id, with its related Address objects

* **Request params**:

| param name  | type     | param type   |
| ----------- |:--------:| ------------:|
| id          | Number   | Route        |

* **Response**:

A Contact object

| param name    | type     |
| --------------|:--------:|
| id            | Number   |
| client_id     | Number   |
| address_id    | Number   |
| name          | String   |
| position      | String   |
| telephone     | String   |
| cellphone     | String   |
| email         | String   |
| addresses     | Array    |


### URI: post => /api/contacts

* **Description**: creates a Contact object

* **Request params**:

| param name    | type     | param type   |
| --------------|:--------:| ------------:|
| client_id     | Number   | Body         |
| address_id    | Number   | Body         |
| name          | String   | Body         |
| position      | String   | Body         |
| telephone     | String   | Body         |
| cellphone     | String   | Body         |
| email         | String   | Body         |

* **Response**:

The created Contact object.

| param name    | type     |
| --------------|:--------:|
| client_id     | Number   |
| address_id    | Number   |
| name          | String   |
| position      | String   |
| telephone     | String   |
| cellphone     | String   |
| email         | String   |


### URI: put/patch => /api/contacts[id]

* **Description**: updates an existing Contact object

* **Request params**:

| param name    | type     | param type   |
| --------------|:--------:| ------------:|
| id            | Number   | Route        |
| id            | Number   | Body         |
| client_id     | Number   | Body         |
| address_id    | Number   | Body         |
| name          | String   | Body         |
| position      | String   | Body         |
| telephone     | String   | Body         |
| cellphone     | String   | Body         |
| email         | String   | Body         |

* **Response**:

The updated Contact object.

| param name    | type     |
| --------------|:--------:|
| id            | Number   |
| client_id     | Number   |
| address_id    | Number   |
| name          | String   |
| position      | String   |
| telephone     | String   |
| cellphone     | String   |
| email         | String   |


### URI: delete => /api/contacts/[id]

* **Description**: deletes an existing Contact object

* **Request params**:

| param name    | type     | param type   |
| --------------|:--------:| ------------:|
| id            | Number   | Route        |

* **Response**:

The number of deleted Contact objects

| param name        | type     |
| ------------------|:--------:|
| number            | Number   |



## Employees endpoint: 


### URI: get => /api/employees?pageNumber=[number]&pageSize=[size]

* **Description**: returns an array of Employee objects, paged by the provided pageNumber and pageSize

* **Request params**:

| param name    | type     | param type   |
| ------------- |:--------:| ------------:|
| pageNumber    | String   | querystring  |
| pageSize      | String   | querystring  |

* **Response**:

An array of Employee objects

| param name      | type     |
| ----------------|:--------:|
| id              | Number   |
| name            | String   |
| employeetype_id | String   |
| position        | String   |


### URI: get => /api/employees/[id]

* **Description**: returns an array of Employee objects, for a provided id 

* **Request params**:

| param name  | type     | param type   |
| ----------- |:--------:| ------------:|
| id          | Number   | Route        |

* **Response**:

An array of Employee objects

| param name      | type     |
| ----------------|:--------:|
| id              | Number   |
| name            | String   |
| employeetype_id | String   |
| position        | String   |


### URI: get => /api/employees/[employeetype_id]

* **Description**: returns an array of Employee objects, for a provided EmployeeType id 

* **Request params**:

| param name        | type     | param type   |
| ----------------- |:--------:| ------------:|
| employeetype_id   | Number   | Route        |

* **Response**:

An array of Employee objects

| param name      | type     |
| ----------------|:--------:|
| id              | Number   |
| name            | String   |
| employeetype_id | String   |
| position        | String   |


### URI: post => /api/employees

* **Description**: creates an Employee object

* **Request params**:

| param name      | type     | param type   |
| ----------------|:--------:| ------------:|
| name            | Number   | Body         |
| employeetype_id | String   | Body         |
| position        | String   | Body         |

* **Response**:

The created Employee object

| param name    | type     |
| --------------|:--------:|
| id              | Number   |
| name            | String   |
| employeetype_id | String   |
| position        | String   |


### URI: put/patch => /api/employees/[id]

* **Description**: updates an existing Employee object

* **Request params**:

| param name      | type     | param type   |
| ----------------|:--------:| ------------:|
| id              | Number   | Route        |
| id              | Number   | Body         |
| name            | String   | Body         |
| employeetype_id | String   | Body         |
| position        | String   | Body         |

* **Response**:

The updated Employee object

| param name    | type     |
| --------------|:--------:|
| id              | Number   |
| name            | String   |
| employeetype_id | String   |
| position        | String   |


### URI: put/patch => /api/employees/[id]

* **Description**: deletes an existing Employee object

* **Request params**:

| param name      | type     | param type   |
| ----------------|:--------:| ------------:|
| id              | Number   | Route        |

* **Response**:

The number of deleted Employee objects

| param name        | type     |
| ------------------|:--------:|
| number            | Number   |



## EmployeeTypes endpoint: 


### URI: get => /api/employeetypes

* **Description**: returns an array of EmployeeType objects

* **Request params**:

None

* **Response**:

An array of EmployeeType objects

| param name      | type     |
| ----------------|:--------:|
| id              | Number   |
| description     | String   |

### URI: get => /api/employeetypes/[id]

* **Description**: returns an EmployeeType object, for a provided id 

* **Request params**:

| param name  | type     | param type   |
| ----------- |:--------:| ------------:|
| id          | Number   | Route        |

* **Response**:

An array of Employee objects

| param name      | type     |
| ----------------|:--------:|
| id              | Number   |
| description     | String   |


### URI: post => /api/employeetypes

* **Description**: creates an EmployeeType object

* **Request params**:

| param name      | type     | param type   |
| ----------------|:--------:| ------------:|
| description     | String   | Body         |

* **Response**:

The created EmployeeType object

| param name    | type     |
| --------------|:--------:|
| id              | Number   |
| description     | String   |


### URI: put/patch => /api/employeetypes/[id]

* **Description**: updates an existing EmployeeType object

* **Request params**:

| param name   | type     | param type   |
| -------------|:--------:| ------------:|
| id           | Number   | Route        |
| id           | Number   | Body         |
| description  | String   | Body         |

* **Response**:

The updated EmployeeType object

| param name    | type     |
| --------------|:--------:|
| id              | Number   |
| description     | String   |

### URI: delete => /api/employeetypes/[id]

* **Description**: deletes an existing EmployeeType object

* **Request params**:

| param name   | type     | param type   |
| -------------|:--------:| ------------:|
| id           | Number   | Route        |
| id           | Number   | Body         |
| description  | String   | Body         |

* **Response**:

The number of deleted EmployeeType objects

| param name        | type     |
| ------------------|:--------:|
| number            | Number   |


## Related Repos

[Clients Manager client - Pending](xxxxx)


