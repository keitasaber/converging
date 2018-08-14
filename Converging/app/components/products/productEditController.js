(function (app) {
    app.controller('productEditController', productEditController);
    
    productEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService', '$stateParams']

    function productEditController(apiService, $scope, notificationService, $state, commonService, $stateParams) {
        $scope.product = {
            CreatedDate: new Date()
        }
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        $scope.categories = [];
        $scope.UpdateProduct = UpdateProduct;
        $scope.getSeoTitle = getSeoTitle;
        $scope.chooseImage = chooseImage;
        $scope.moreImages = [];
        $scope.chooseMoreImages = chooseMoreImages;
        $scope.getNameFromUrlImage = getNameFromUrlImage;
        $scope.deleteImage = deleteImage;
        $scope.hideMoreImage = false;

        function deleteImage(url) {
            var index = $scope.moreImages.indexOf(url);
            if (index > -1) {
                $scope.moreImages.splice(index, 1);
            }
        }

        function getNameFromUrlImage(url) {
            return url.replace(/^.*[\/\\]/g, '');
        }

        function chooseMoreImages() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    var index = $scope.moreImages.indexOf(fileUrl);
                    if (index === -1) {
                        $scope.moreImages.push(fileUrl);
                    }
                    else {
                        notificationService.displayWarning("Bạn đã chọn ảnh này !")
                    }
                })
            }
            finder.popup();
        }

        function chooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
            }
            finder.popup();
        }

        function getSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function loadProductDetail() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;                
                $scope.moreImages = JSON.parse($scope.product.MoreImages);
            }, function (error) {
                notificationService.displayError(error.data);
            })
        }

        function UpdateProduct() {
            $scope.product.moreImages = JSON.stringify($scope.moreImages);
            apiService.put("/api/product/update", $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được cập nhật")
                $state.go('products');
            }, function (error) {
                if (error.data === "DUPLICATE_TAG") {
                    notificationService.displayWarning("Yêu cầu mỗi tag phải khác nhau");
                }
                notificationService.displayError("Cập nhật không thành công ")
            });
        }

        function loadCategories() {
            apiService.get("/api/productcategory/getall", null, function (result) {
                $scope.categories = result.data;
            }, function () {
                console.log('Can not get list category')
            })
        }
        loadCategories();
        loadProductDetail();
    }
})(angular.module('converging.products'))