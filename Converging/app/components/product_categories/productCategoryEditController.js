﻿(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController);

    productCategoryEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService']

    function productCategoryEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.productCategory = {
            UpdatedDate: new Date(),
            Status: true,
            Name: "Danh mục 1"
        }

        $scope.parentCategories = [];

        $scope.UpdateProductCategory = UpdateProductCategory;

        $scope.getSeoTitle = getSeoTitle;

        function getSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        function loadProductCategoryDetail() {
            apiService.get('/api/productcategory/getbyid/' + $stateParams.id, null, function (result) {
                $scope.productCategory = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            })
        }

        function UpdateProductCategory() {
            apiService.put("/api/productcategory/update", $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được cập nhật")
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError("Cập nhật không thành công ")
            });
        }

        function loadParentCategory() {
            apiService.get("/api/productcategory/getallparents", null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('Can not get list parents')
            })
        }
        loadParentCategory();
        loadProductCategoryDetail();
    }
})(angular.module('converging.productCategories'));