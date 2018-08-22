/// <reference path="../scripts/angular.js" />
(function () {
    angular.module('converging',
        [
            'converging.products',
            'converging.productCategories',
            'converging.common'
        ])
        .config(config)
        .config(configAuthentication);

    angular.module('converging').run(function ($rootScope, $state, authenticationService, ) {
        $rootScope.$on('$stateChangeStart', function (event, toState, toParams) {                     
            // if route requires auth and user is not logged in
            if (authenticationService.getTokenInfo() == null) {
                if (toState.name === 'login') return
                // Abort transition
                event.preventDefault();
                // Redirect to login page
                $state.go('login');
            }
            else {
                if (toState.name === 'login') {
                    // Abort transition
                    event.preventDefault();
                    // Redirect to login page
                    $state.go('home');
                }
            }
        });
    });

    config.$inject = ['$stateProvider', '$urlRouterProvider', '$locationProvider'];

    function config($stateProvider, $urlRouterProvider, $locationProvider) {
        $locationProvider.hashPrefix('');
        $stateProvider
            .state('baseView', {
                url: '',
                templateUrl: 'app/shared/views/baseView.html',
                abstract: true
            })
            .state('login', {
                url: "/login",
                templateUrl: "/app/components/login/loginView.html",
                controller: "loginController"
            })
            .state('home', {
                url: "/home",
                parent: 'baseView',
                templateUrl: "/app/components/home/homeView.html",
                controller: "homeController"
            });
    }

    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {
                    return config;
                },
                requestError: function (rejection) {
                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {
                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }

})();