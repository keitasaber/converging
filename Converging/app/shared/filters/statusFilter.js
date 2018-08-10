(function (app) {
    app.filter('statusFilter', function () {
        return function (status) {
            if (status == true)
                return 'Kích hoạt';
            else return 'Khóa'
        }
    })
})(angular.module('converging.common'));