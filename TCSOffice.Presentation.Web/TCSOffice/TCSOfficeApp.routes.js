'use strict';
App.config(["$routeProvider", appConfig]);
function appConfig($routeProvider) {
    $routeProvider.when('/customer', { templateUrl: 'TCSOffice/Pages/Customer/customers.html', controller: 'customersCtrl' });

    $routeProvider.when('/Home#/', { templateUrl: 'TCSOffice/Pages/Home/home.html', controller: 'homeCtrl' });

    $routeProvider.otherwise({ redirectTo: '/' });
};