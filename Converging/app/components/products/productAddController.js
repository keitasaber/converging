(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService']

    function productAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true,
            Name: "Tên sản phẩm",
            Alias: "ten-san-pham"
        };
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px',
            cloudServices_tokenUrl: '/Assets/Admin/ckeditor/cloudservices/plugin.js'
        };
        $scope.categories = [];
        $scope.addProduct = addProduct;
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
        
        function addProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post("/api/product/create", $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được thêm mới")
                $state.go('products');
            }, function (error) {                
                if (error.data === "DUPLICATE_TAG") {
                    notificationService.displayWarning("Yêu cầu mỗi tag phải khác nhau");
                }
                notificationService.displayError("Thêm mới không thành công ")
            });
        }

        function loadListsCategory() {
            apiService.get("/api/productcategory/getall", null, function (result) {
                $scope.categories = result.data;
            }, function () {
                console.log('Can not get list category')
            })
        }
        loadListsCategory();
    }
})(angular.module('converging.products'))