(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$state', '$filter'];

    function productListController($scope, apiService, notificationService, $ngBootbox, $state, $filter) {
        $scope.products = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProducts = getProducts;
        $scope.keyword = '';
        $scope.search = search;
        $scope.deleteProduct = deleteProduct;
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
                    checkedProducts: JSON.stringify(listId)
                }
            }
            apiService.delete('/api/product/deletemultiple', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' sản phẩm');
                $state.reload();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            })
        }


        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("products", function (n, o) {
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

        function deleteProduct(id) {
            $ngBootbox.confirm('Bạn có chắc chắn muốn xóa').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.delete('api/product/delete', config, function (result) {
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

        function getProducts(page) {
            page = page || 0
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 8
                }
            }

            apiService.get('/api/product/get', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning("Không có sản phẩm nào được tìm thấy");
                }

                $scope.products = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load list product fail');
            });
        }
        $scope.getProducts();
    }
})(angular.module('converging.products'))