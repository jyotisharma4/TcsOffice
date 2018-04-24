(function () {
    'use strict';

    var serviceId = 'tcHomeService';
    App.factory(serviceId, ['$http', tcHomeService]);
    function tcHomeService($http) {
        var baseUrl = 'api/Company';

        var service = {
            GetAll: GetAll,
        }

        return service;

        function GetAll(callback) {
            alert("service");
            var url = baseUrl + "/Get";
            alert(url);
            $http.get(url).then(function (response) {
                if (response.success) {
                    debugger;
                    if (callback)
                        callback(response.data);
                }
            });
        }

        

    };
})();
