'use strict';
App.config(["$routeProvider", appConfig]);
function appConfig($routeProvider) {
    alert("1");
    $routeProvider.when('/customer', { templateUrl: 'TCSOffice/Pages/Customer/customers.html', controller: 'customersCtrl' });
   
    $routeProvider.otherwise({ redirectTo: '/' });
};