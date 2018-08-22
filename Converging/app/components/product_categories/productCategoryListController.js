(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$state', '$filter'];
    
    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $state, $filter) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCategories = getProductCategories;
        $scope.keyword = '';
        $scope.search = search;
        $scope.deleteProductCategory = deleteProductCategory;
        $scope.selectAll = selectAll;
        $scope.isAll = false;
        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID)
            })
            var config = {
                params: {
                    checkedProductCategories: JSON.stringify(listId)
                }
            }
            apiService.delete('/api/productcategory/deletemultiple', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' danh mục');  
                $state.reload();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            })
        }
        

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("productCategories", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
                $('#btnDelete').attr('class', 'btn btn-sm btn-outline-danger');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
                $('#btnDelete').attr('class', 'btn btn-sm');
            }
        }, true);

        function deleteProductCategory(id) {
            $ngBootbox.confirm('Bạn có chắc chắn muốn xóa').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.delete('api/productcategory/delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công');  
                    $state.reload();
                }, function (error) {
                    notificationService.displayError('Xóa không thành công');
                })
            }, function () {
            })
        }

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
                notificationService.displayError("Có lỗi xảy ra!");
            });
        }
        $scope.getProductCategories();
    }
})(angular.module('converging.productCategories'));