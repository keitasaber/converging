/// <reference path="../../../scripts/angular.js" />
(function (app) {
    app.factory('apiService', apiService);

    apiService.$inject = ['$http', 'notificationService'];

    function apiService($http, notificationService) {
        return {
            get: getApiResult,
            post: postApiResult
        }

        function postApiResult(url, params, success, failure) {
            $http.post(url, params)
                .then(function (result) {
                    success(result);
                }, function (error) {
                    if (error.status === 401) {
                        notificationService.displayError("Authenticate is required");
                    }
                    failure(error);
                })
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