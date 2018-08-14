/// <reference path="../../../scripts/angular.js" />
(function () {
    angular.module('converging.products', ['converging.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('products', {
                url: "/products",
                parent: 'baseView',
                templateUrl: "/app/components/products/productListView.html",
                controller: "productListController"
            }).state('product_add', {
                url: "/product_add",
                parent: 'baseView',
                templateUrl: "/app/components/products/productAddView.html",
                controller: "productAddController"
            }).state('product_edit', {
                url: "/product_edit/:id",
                parent: 'baseView',
                templateUrl: "/app/components/products/productEditView.html",
                controller: "productEditController"
            });
    }
})();