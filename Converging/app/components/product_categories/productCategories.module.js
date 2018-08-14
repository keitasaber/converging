/// <reference path="../../../scripts/angular.js" />
(function () {
    angular.module('converging.productCategories', ['converging.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('product_categories', {
                url: "/product_categories",
                parent: 'baseView',
                templateUrl: "/app/components/product_categories/productCategoryListView.html",
                controller: "productCategoryListController"
            }).state('product_category_add', {
                url: "/product_category_add",
                parent: 'baseView',
                templateUrl: "/app/components/product_categories/productCategoryAddView.html",
                controller: "productCategoryAddController"
            }).state('product_category_edit', {
                url: "/product_category_edit/:id",
                parent: 'baseView',
                templateUrl: "/app/components/product_categories/productCategoryeditView.html",
                controller: "productCategoryEditController"
            });
    }
})();