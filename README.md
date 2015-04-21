# DRSystem
Document Review System, with JIRA or Email support. It offers trackable reviews with JIRA which give company auditors are great overview of traceable reviews.

This project is partly a personal POC for using Microsoft's WebAPI 2. 

Design pattern, tools, frameworks and remarks: 
* Entity Framework 6 (Code first)
* Repository pattern for data layer 
  * Testable API
* AngularJS Front-End
  * Testable web app.
* DI design with Ninject
* Model factory pattern to control WebAPI responses
* Scalable authorization service using JWT.
* Windows Service - Pooling

# Features

The system has the following components to send audit notifications to:
* JIRA (Needs access to JIRA API) - Offers tracking.
* Email - Does not offer tracking.

# Configuration

## Database preparations

Configure the database, both for the web project and the data layer project:

```
<connectionStrings>
  <add name="DRSConnection" connectionString="Data Source=database01.corporation.com;Initial Catalog=DatabaseNAME;Persist Security Info=True;User ID=samskipdrs;Password=PASS" providerName="System.Data.SqlClient" />
</connectionStrings>
```

Use Package Manager Console in VS, to prepare the database.

*You need to choose the Data project as the default project before doing this.*

``` Update-Database -Verbose ```

If you are a developer and are doing changes to the EF entities you can simply do the following, do update the database.

``` Update-Database -Verbose -Force ```

*Note, that by using force could mean that you might loose data.*

## Logging configuration

This project uses NLog to simplify logging.

The following NLog.config is a good example of how to use it:

```
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
         xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" >

    <extensions>
    <add assembly="NLog.Targets.Sentry" />
    </extensions>
    
    <variable name="appName" value="DRS" />
    
    <targets>
      <target name="console" xsi:type="Console" 
            layout="${longdate}|${level}|${message}" />
      <target name="file" xsi:type="File"
            layout="${longdate} ${logger} ${message}" 
            fileName="${basedir}/logs/drs.data.log" 
            keepFileOpen="false"
            encoding="iso-8859-2" />
      <target name="Sentry" type="Sentry" dns="http://hashkey@sentry.hauxi.is/7645" />
    </targets>

    <rules>
      <logger name="*" minlevel="Debug" writeTo="file" />
      <logger name="*" appendTo="Sentry" minLevel="Debug"/>
    </rules>
</nlog>
```

This examples logs everything to a file logs/drs.data.log (all severities) and errors to a (Sentry)[https://github.com/getsentry/sentry] log aggregator.

### Note to developers

There is an built-in target in DRSLogger component, that enables logging to sentinel (https://sentinel.codeplex.com) when in DEBUG mode.

```
#if DEBUG
     // Setup the logging view for Sentinel - http://sentinel.codeplex.com
      var sentinalTarget = new NLogViewerTarget()
      {
        Name = "sentinal",
        Address = "udp://127.0.0.1:9999"
      };
      var sentinalRule = new LoggingRule("*", LogLevel.Trace, sentinalTarget);
      LogManager.Configuration.AddTarget("sentinal", sentinalTarget);
      LogManager.Configuration.LoggingRules.Add(sentinalRule);
#endif
```

# WebAPI

## Resource conventions

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

