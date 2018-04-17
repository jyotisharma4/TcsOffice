(function () {
    'use strict';
    var controllerId = 'homeCtrl';
    App.controller(controllerId, ['$scope', '$routeParams', homeCtrl]);
    function homeCtrl($scope, $routeParams) {
        alert("Home");
    }
})();