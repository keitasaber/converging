(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService'];

    function productCategoryListController($scope, apiService) {
        $scope.productCategories = [];

        $scope.getProductCategories = getProductCategories;

        function getProductCategories() {
            apiService.get('/api/productcategory/get', null, function (result) {
                $scope.productCategories = result.data;
                console.log(result.data)
            }, function () {
                console.log('Load list product category fail');
            });
        }
        $scope.getProductCategories();
    }
})(angular.module('converging.productCategories'));