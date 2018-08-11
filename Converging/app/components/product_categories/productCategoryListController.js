(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService'];

    function productCategoryListController($scope, apiService, notificationService) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCategories = getProductCategories;
        $scope.keyword = '';
        $scope.search = search;

        function search() {
            getProductCategories();
        }

        function getProductCategories(page) {
            page = page || 0
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 8
                }
            }

            apiService.get('/api/productcategory/get', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning("Không có danh mục nào được tìm thấy");
                }

                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load list product category fail');
            });
        }
        $scope.getProductCategories();
    }
})(angular.module('converging.productCategories'));