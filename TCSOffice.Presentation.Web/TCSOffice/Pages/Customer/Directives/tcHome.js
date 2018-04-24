(function () {
    'use strict';

    App.directive('tcHome', ['DTOptionsBuilder', tcHome]);

    function tcHome(DTOptionsBuilder) {

        var directive = {
            replace: true,
            restrict: 'E',
            templateUrl: 'TCSOffice/Pages/Customer/Directives/tcHome.html',
            controller: ctrl,
            scope: {
                view: '@?', //details (default), edit, delete
            }
        };

        return directive;

        function ctrl($scope) {
            alert(1);
            $scope.dtOptions = DTOptionsBuilder.newOptions()     
                .withDisplayLength(5)
                .withOption('bLengthChange', true);

            // $scope.users = {};

            $scope.users = [
                {
                    'fullname': 'Alauddin',
                    'email': 'testing@domain.com'
                },
                {
                    'fullname': 'TheWonder',
                    'email': 'wonder@domain.com'
                }, {
                    'fullname': 'Alauddin',
                    'email': 'testing@domain.com'
                },
                {
                    'fullname': 'TheWonder',
                    'email': 'wonder@domain.com'
                }, {
                    'fullname': 'Alauddin',
                    'email': 'testing@domain.com'
                },
                {
                    'fullname': 'TheWonder',
                    'email': 'wonder@domain.com'
                }, {
                    'fullname': 'Alauddin',
                    'email': 'testing@domain.com'
                },
                {
                    'fullname': 'TheWonder',
                    'email': 'wonder@domain.com'
                }, {
                    'fullname': 'Alauddin',
                    'email': 'testing@domain.com'
                },
                {
                    'fullname': 'TheWonder',
                    'email': 'wonder@domain.com'
                }, {
                    'fullname': 'Alauddin',
                    'email': 'testing@domain.com'
                },
                {
                    'fullname': 'TheWonder',
                    'email': 'wonder@domain.com'
                }
            ];

            //$http.get('')
            //    .success(function (data, status) {
            //        // Assign http data into scope variable here


            //    })
            //    .error(function (data, status) {

            //    });
        }   
        
    };
})();
