/// <reference path="../../../scripts/angular.js" />
(function (app) {
    app.factory('apiService', apiService);

    apiService.$inject = ['$http', 'notificationService'];

    function apiService($http, notificationService) {
        return {
            get: getApiResult,
            post: postApiResult,
            put: putApiResult,
            delete: deleteApiResult
        }

        function deleteApiResult(url, params, success, failure) {
            $http.delete(url, params)
                .then(function (result) {
                    success(result);
                }, function (error) {
                    if (error.status === 401) {
                        notificationService.displayError("Authenticate is required");
                    }
                    failure(error);
                })
        }

        function postApiResult(url, data, success, failure) {
            $http.post(url, data)
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

        function putApiResult(url, data, success, failure) {
            $http.put(url, data)
                .then(function (result) {
                    success(result);
                }, function (error) {
                    if (error.status === 401) {
                        notificationService.displayError("Authenticate is required");
                    }
                    failure(error);
                })
        }
    }
})(angular.module('converging.common'));