# Angular 2 Demo Project with ASP.NET VNext 5

**Please note**: This was created for use as a demo project for a 6hr workshop at Boise Tech Fest. *It is not an example of best practices* and was meant to be used for multiple examples of how the Angular 2 works compared with Angular 1.

Slides are available at https://goo.gl/EQoPMJ

This is an angular 2 and asp.net vnext 5 starter with an example scrum type board and users.

## Requirements

1. Visual studio / VSCode
2. Latest MVC 5 installed (https://get.asp.net/)
3. SqlServer (or re-write the data access layer)
4. NodeJS / NPM (install 4.4 from https://nodejs.org/)
5. Gulp installed (npm install -g gulp after installing node/npm)    

## Setup Required

    git clone https://github.com/jmackay/angular2-demoproject-aspnetcore.git

or download / unzip into your folder of choice

Navigate to the install directory -> source/Angular2Demo.Web folder in a command prompt 

> *TIP: You can open command prompt in that folder by **holding shift + right-clicking** the empty area and selecting the command prompt option*

    npm install

// This copies the required angular2 node_modules into wwwroot since systemjs needs them to do its "lazy loading". Hopefully future angular2-cli will remove this requirement!!

    gulp angular2:moveLibs


Note if you make changes to the Model classes, please make sure to update the .d.ts files.

>*TIP: You can enable auto generation of these by using Web Essentials Typescript Generation by right-clicking on the `Models/*.cs files` and selecting `Web Essentials -> Create Typescript Intelisense File`*

Restore Packages / Set DNX to use latest

    Using PowerShell under Angular2Demo.Web folder:
    
    dnvm install latest -r clr -arch x64
    
    dnu restore
    
    tsc

Open Solution file in Visual Studio 2015 and build

or

Open in VS Code and make sure all typescript files create their js files and map by running tsc.



### Option 1 - MSSQL Database Configuration - READ / WRITE access, full application!

You will also need to configure the database location, it is located in the **config.json**

    // config.json, you need to update this!
    {
	    "AppSettings": {
		    "SiteTitle": "Angular 2 Demo"
	    },
	    "Data": {
		    "DefaultConnection": {
			    "ConnectionString": "Server=(localdb)\\MSSQLLocalDB;Database=Angular2Demo;Trusted_Connection=True;MultipleActiveResultSets=true"
		    }
	    }
    }

By default it will try to create the database in a local MSSql instance, you will need to modify that or create another database adapter to connect to some non-mssql database.


From the command line window you have pointing into the Angular2Demo.Web folder

    //
    // BUILD the project first - make sure the nuget packages auto-restore on build as well!
    //
    dnx ef database update

Now to reload your test data, run the website and go to `/api/reset`, this will clear any existing data and load up dummy information.


### Option 2 - Reading plain .json files - READ ONLY

I've also included .json files for each api which you can use by modifying the data.service.ts file if you do not want to setup / use mssql. This will not allow you to save information but will allow you to load it :)

You will need to go to `app/shared/services/data.service.ts` and change `useStatic` to `true`

### Option 3 - Use another backend

This will require you to DIY but you should be able to easily hook into other databases by using **Entity Framework 7 configuration** in the config above (`config.json`) and `Startup.cs` or something like **Firebase** in `data.service.ts`

## Using the Demo Application

Once you've completed the setup, run the project from Visual Studio 2015 or VSCode and launch it using the debuger, it should open to localhost:#####

You will be able to view users, and add new cards. Please feel free to add more functionality (and submit pull requests if you want!).

We use [JS-Data](http://www.js-data.io/) to help us read and save data as well as tie our relationships back together very easily client side, to allow for a very rich experience.

## Disclaimer

Please note that things will change and break as both Angular 2 and ASP.Net vNext 5 update, ASP.NET is turning into "ASP.Net Core v1" soon which should break a lot. I'll try to keep the project updated with the changes, but no guarantees!
