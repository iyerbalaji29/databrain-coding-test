# DataBrainPAYGApp

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 15.2.6.

## API

*Open databrain-coding-test/DataBrainCodeTest.sln with Visual Studio to open the API project.
*The project consists of DataBrain.PAYG.Service which implements the business logic for calculating tax based on paycalculator.com.au website.
*The Service project and API project has corresponding tests that covers the unit tests for validating the logic implemented in the applications.
*Once all projects are loaded successfully, right click the DataBrain.PAYG.Api project and set as startup project.
*Then click on Run button, to run the API project. 
*This should open up a command window showing the api port where the project is exposing its api something like (https://localhost:7295/).
*Copy this url and make sure the below mentioned environments settings has the same apiUrl to communicate the backend API and Angular front end app successfully.
*You can also access the swagger documentation for the api with the following url (https://localhost:7295/swagger/index.html).
*The swagger api can be used for testing different inputs with earnings and payment frequency.

## Build

*Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.
*There are two environments set up for switching between development (dev) and production (prod).
*There are environment files setup under src/environments folder for updating environment specific urls
*Please run `ng build --configuration=dev` or `ng build --configuration=prod` respectively to switch between specific urls.


## FrontEnd App

*Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.
*Also you can run environment specific server with `ng serve --configuration=dev` or `ng serve --configuration=prod` respectively to switch between specific environments.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.
