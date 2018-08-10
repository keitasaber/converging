/// <reference path="../../../scripts/angular.js" />
(function (app) {
    app.factory('apiService', apiService);

    apiService.$inject = ['$http'];

    function apiService($http) {
        return {
            get: getApiResult
        }

        function getApiResult(url, params, success, failure) {
            $http.get(url, params)
                .then(function (result) {
                    success(result);
                }, function (error) {
                    failure(error);
                })
        }
    }
})(angular.module('converging.common'));