(function () {
    'use strict';
    var controllerId = 'homeCtrl';
    App.controller(controllerId, ['$scope','$routeParams', homeCtrl]);
    function homeCtrl($scope, $routeParams) {

        //$scope.dtOptions = DTOptionsBuilder.newOptions()
        //    .withDisplayLength(5)
        //    .withOption('bLengthChange', true);

      // $scope.users = {};

        //$scope.users = [
        //    {
        //        'fullname': 'Alauddin',
        //        'email': 'testing@domain.com'
        //    },
        //    {
        //        'fullname': 'TheWonder',
        //        'email': 'wonder@domain.com'
        //    }, {
        //        'fullname': 'Alauddin',
        //        'email': 'testing@domain.com'
        //    },
        //    {
        //        'fullname': 'TheWonder',
        //        'email': 'wonder@domain.com'
        //    }, {
        //        'fullname': 'Alauddin',
        //        'email': 'testing@domain.com'
        //    },
        //    {
        //        'fullname': 'TheWonder',
        //        'email': 'wonder@domain.com'
        //    }, {
        //        'fullname': 'Alauddin',
        //        'email': 'testing@domain.com'
        //    },
        //    {
        //        'fullname': 'TheWonder',
        //        'email': 'wonder@domain.com'
        //    }, {
        //        'fullname': 'Alauddin',
        //        'email': 'testing@domain.com'
        //    },
        //    {
        //        'fullname': 'TheWonder',
        //        'email': 'wonder@domain.com'
        //    }, {
        //        'fullname': 'Alauddin',
        //        'email': 'testing@domain.com'
        //    },
        //    {
        //        'fullname': 'TheWonder',
        //        'email': 'wonder@domain.com'
        //    }
        //];

        //$http.get('')
        //    .success(function (data, status) {
        //        // Assign http data into scope variable here

        
        //    })
        //    .error(function (data, status) {

        //    });
    }
})();