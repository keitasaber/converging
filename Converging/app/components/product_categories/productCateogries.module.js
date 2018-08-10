/// <reference path="../../../scripts/angular.js" />
(function () {
    angular.module('converging.productCategories', ['converging.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('productCategories', {
            url: "/productCategories",
            templateUrl: "/app/components/product_categories/productCategoryListView.html",
            controller: "productCategoryListController"
        });
    }
})();