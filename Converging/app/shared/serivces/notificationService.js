(function (app) {
    app.factory('notificationService', notificationService);

    function notificationService() {
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        return {
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayWarning: displayWarning,
            displayInfo: displayInfo
        }

        function displaySuccess(message) {
            toastr.success(message)
        }

        function displayError(error) {
            if (Array.isArray(error)) {
                error.each(function (error) {
                    toastr.error(error)
                })
            }
            else {
                toastr.displayError(error)
            }
        } 

        function displayWarning(message) {
            toastr.warning(message)
        }

        function displayInfo(message) {
            toastr.info(message)
        }
    }
})(angular.module('converging.common'))