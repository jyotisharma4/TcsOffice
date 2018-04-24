(function () {
    'use strict';

    App.directive('tcHome', ['tcHomeService','DTOptionsBuilder', tcHome]);

    function tcHome(tcHomeService,DTOptionsBuilder) {

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
            console.log(tcHomeService);
            $scope.dtOptions = DTOptionsBuilder.newOptions()     
                .withDisplayLength(5)
                .withOption('bLengthChange', true);

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

           
            function active()
            {
                alert("Active");
                getData();
            }

            function getData() {
                $scope.items = [];
                var methodName = 'GetAll';
                tcHomeService.GetAll(function (data) {
                    alert("Data");
                    $scope.items = data;
                });
            }

            active();
            //$http.get('')
            //    .success(function (data, status) {
            //        // Assign http data into scope variable here


            //    })
            //    .error(function (data, status) {

            //    });
        }   
        
    };
})();
