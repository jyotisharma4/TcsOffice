(function () {
    'use strict';
    var controllerId = 'customersCtrl';
    App.controller(controllerId, ['$scope', '$routeParams', customersCtrl]);
    function customersCtrl($scope, $routeParams) {
        alert("tekmkl");
        $scope.test = "gvvdchjbdshchjdsb";
    }
})();