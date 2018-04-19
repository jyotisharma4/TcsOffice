(function () {
    'use strict';
    var controllerId = 'homeCtrl';
    App.controller(controllerId, ['$scope', '$routeParams', 'DTOptionsBuilder','DTColumnBuilder', homeCtrl]);
    function homeCtrl($scope, $routeParams, DTOptionsBuilder, DTColumnBuilder) {
        $scope.columns = [
            DTColumnBuilder.newColumn("Id", "ID"),
            DTColumnBuilder.newColumn("name", "Name"),
        ];

        $scope.dtOptions = DTOptionsBuilder.newOption('ajax', {})
    }
})();