(function () {
    'use strict';
    var controllerId = 'homeCtrl';
    App.controller(controllerId, ['$scope', '$routeParams', homeCtrl]);
    function homeCtrl($scope, $routeParams) {
        $scope.test = "hello";
    }
})();