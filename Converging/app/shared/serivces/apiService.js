/// <reference path="../../../scripts/angular.js" />
(function (app) {
    app.factory('apiService', apiService);

    apiService.$inject = ['$http', 'notificationService', 'authenticationService'];

    function apiService($http, notificationService, authenticationService) {
        return {
            get: getApiResult,
            post: postApiResult,
            put: putApiResult,
            delete: deleteApiResult
        }

        function deleteApiResult(url, params, success, failure) {
            authenticationService.setHeader();
            $http.delete(url, params)
                .then(function (result) {
                    success(result);
                }, function (error) {
                    if (error.status === 401) {
                        notificationService.displayError("Authenticate is required");
                    }
                    else if (failure != null) {
                        failure(error);
                    }
                })
        }

        function postApiResult(url, data, success, failure) {
            authenticationService.setHeader();
            $http.post(url, data)
                .then(function (result) {
                    success(result);
                }, function (error) {
                    if (error.status === 401) {
                        notificationService.displayError("Authenticate is required");
                    }
                    else if (failure != null) {
                        failure(error);
                    }
                })
        }

        function getApiResult(url, params, success, failure) {
            authenticationService.setHeader();
            $http.get(url, params)
                .then(function (result) {
                    success(result);
                }, function (error) {
                    failure(error);
                })
        }

        function putApiResult(url, data, success, failure) {
            authenticationService.setHeader();
            $http.put(url, data)
                .then(function (result) {
                    success(result);
                }, function (error) {
                    if (error.status === 401) {
                        notificationService.displayError("Authenticate is required");
                    }
                    else if (failure != null) {
                        failure(error);
                    }
                })
        }
    }
})(angular.module('converging.common'));