'use strict';
App.config(["$routeProvider", appConfig]);
function appConfig($routeProvider) {

   
    $routeProvider.otherwise({ redirectTo: '/' });
};