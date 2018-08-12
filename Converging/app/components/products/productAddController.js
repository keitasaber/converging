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

        function chooseImage() {
            var finder = new CKFinder();                        
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Image = fileUrl;
                $("#product_image").val(fileUrl)
            }
            finder.popup();
        }
        
        function getSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }
        
        function addProduct() {
            apiService.post("/api/product/create", $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + " đã được thêm mới")
                $state.go('products');
            }, function (error) {
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