(function (app) {
    app.controller('rootController', rootController);

    rootController.$inject = ['$state', 'authData', 'loginService', '$scope', 'authenticationService'];

    function rootController($state, authData, loginService, $scope, authenticationService) {
        $scope.logOut = function () {
            loginService.logOut();
            $state.go('login');
        }
        
        if (authenticationService.getTokenInfo() != null) {
            authData.authenticationData.IsAuthenticated = true;
            authData.authenticationData.userName = authenticationService.getTokenInfo().userName;
        }
        $scope.authentication = authData.authenticationData;
        //authenticationService.validateRequest();
    }
})(angular.module('converging'));


