# DRSystem
Document Review System, with JIRA or Email support. It offers trackable reviews with JIRA which give company auditors are great overview of traceable reviews.

This project is partly a POC for using Microsoft's WebAPI 2. 

# Features

The system has the following components to send audit notifications to:
* JIRA (Needs access to JIRA API) - Offers tracking.
* Email - Does not offer tracking.

# Dependencies and preparation to get the project started

## Database preparations and migrations.

Configure the database, both for the web project and the data layer project:

```
<connectionStrings>
  <add name="DRSConnection" connectionString="Data Source=database01.corporation.com;Initial Catalog=DatabaseNAME;Persist Security Info=True;User ID=samskipdrs;Password=PASS" providerName="System.Data.SqlClient" />
</connectionStrings>
```

Use Package Manager Console in VS, to prepare the database.

*You need to choose the Data project as the default project before doing this.*

Start by enabling migration:

``` Enable-Migrations ```

Then create the initial migration:

``` Add-Migration Initial ``` 

Then update the database:

``` Update-Database ```

# WebAPI

## Resource resource 

This project is using the following convention for WebAPI resources.

| Action                     | HTTP verbs    | Relative URI                                    |
|:---------------------------|:-------------:|:-----------------------------------------------:|
| Get all systems            | GET           | /api/systems                                    |
| Get single system          | GET           | /api/systems/<int:id>                           |
| Add new system             | POST          | /api/systems (New system is sent in POST body)  |
| Updated existing system    | PUT or PATCH  | /api/systems/<int:id>                           |
| Delete system              | DELETE        | /api/systems/<int:id>                           |

Also the project uses HTTP response codes, for results:
* When a system is created we return, 201 (Resource Created).
* Fetching a valid system we return, 200 (OK).
* Updating a valid system we return, 200 (OK).
* Updating a valid system, but know fields changed we return, 304 (Not Modified).
* If anything is not found, we always return 404 (Not Found).
* Parsing failed in POST body we return, 400 (Bad Request).

Same goes for other resources.
