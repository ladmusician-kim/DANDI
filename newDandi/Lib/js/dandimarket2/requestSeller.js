var windowWidth = $(window).width();

var app = angular.module('myApp', []).
controller('Market2Ctrl', function ($scope) {
    $scope.requestSeller = {
        name: $('#Name').val(),
        email: $('#Email').val(),
        phone: $("#Phone").val(),
        info: $("#Info").val(),
        aboutculture: $('#AboutCulture').val(),
        product: $('#Product').val(),
        isEdit: $('#IsEdit').val()
    };
    $scope.submit = function () {
        if ($('#dandi-request-input-email').val().length > 0) {
            $scope.requestSeller.email = $('#dandi-request-input-email').val();
            $scope.requestSeller.emailValidation = true;
        }

        if ($scope.requestSeller.emailValidation && $scope.requestSeller.nameValidation &&
            $scope.requestSeller.phoneValidation && $scope.requestSeller.infoValidation &&
            $scope.requestSeller.aboutcultureValidation && $scope.requestSeller.productValidation) {

            $(".page-loader").show();
            $(".page-loader b").show();

            $('#requestSeller').submit();
        } else {
            errorHandleClass();
            errorMessasge();
        }

    }

    function errorMessasge() {
        if (!$scope.requestSeller.emailValidation) {
            alert("이메일을 정확히 입력해주세요!");
        } else {
            if (!$scope.requestSeller.nameValidation) {
                alert("이름을 정확히 입력해주세요!");
            } else {
                if (!$scope.requestSeller.phoneValidation) {
                    alert("연락처를 정확히 입력해주세요!");
                } else {
                    if (!$scope.requestSeller.productValidation) {
                        alert("품목을 정확히 입력해주세요!");
                    } else {
                        if (!$scope.requestSeller.infoValidation) {
                            alert("자기소개를 20자 이상으로 입력해주세요!");
                        } else {
                            if (!$scope.requestSeller.aboutcultureValidation) {
                                alert("문화에 대한 생각을 20자 이상으로 입력해주세요!");
                            }
                        }
                    }
                }
            }
        }
    }
    function errorHandleClass() {
        if (!$scope.requestSeller.emailValidation) {
            $scope.requestSeller.emailClass = "dandi-request-input-error";
        }
        if (!$scope.requestSeller.nameValidation) {
            $scope.requestSeller.nameClass = "dandi-request-input-error";
        }
        if (!$scope.requestSeller.phoneValidation) {
            $scope.requestSeller.phoneClass = "dandi-request-input-error";
        }
        if (!$scope.requestSeller.infoValidation) {
            $scope.requestSeller.infoClass = "dandi-request-input-error";
        }
        if (!$scope.requestSeller.aboutcultureValidation) {
            $scope.requestSeller.aboutcultureClass = "dandi-request-input-error";
        }
        if (!$scope.requestSeller.productValidation) {
            $scope.requestSeller.productClass = "dandi-request-input-error";
        }
    }
    function IsEmail(email) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        return regex.test(email);
    }

    //email validation
    $scope.$watch('requestSeller.email', function (newValue, oldValue) {
        if (newValue) {
            if (!IsEmail(newValue)) {
                $scope.requestSeller.emailValidation = false;
                $scope.requestSeller.emailClass = "dandi-request-input-error";
            } else {
                $scope.requestSeller.emailValidation = true;
                $scope.requestSeller.emailClass = "dandi-request-input-right";
            }
        }
    });
    // name valiation
    $scope.$watch('requestSeller.name', function (newValue, oldValue) {
        if (newValue) {
            if (newValue.length <= 1) {
                $scope.requestSeller.nameValidation = false;
                $scope.requestSeller.nameClass = "dandi-request-input-error";
            } else {
                $scope.requestSeller.nameValidation = true;
                $scope.requestSeller.nameClass = "dandi-request-input-right";
            }
        } else {
            $scope.requestSeller.nameValidation = false;
            $scope.requestSeller.nameClass = "dandi-request-input-error";
        }
    });
    // product valiation
    $scope.$watch('requestSeller.product', function (newValue, oldValue) {
        if (newValue) {
            if (newValue.length <= 1) {
                $scope.requestSeller.productValidation = false;
                $scope.requestSeller.productClass = "dandi-request-input-error";
            } else {
                $scope.requestSeller.productValidation = true;
                $scope.requestSeller.productClass = "dandi-request-input-right";
            }
        } else {
            $scope.requestSeller.productValidation = false;
            $scope.requestSeller.productClass = "dandi-request-input-error";
        }
    });
    //phone validation
    $scope.$watch('requestSeller.phone', function (newValue, oldValue) {
        if (newValue) {
            if (newValue.length <= 10) {
                $scope.requestSeller.phoneValidation = false;
                $scope.requestSeller.phoneClass = "dandi-request-input-error";
            } else {
                $scope.requestSeller.phoneValidation = true;
                $scope.requestSeller.phoneClass = "dandi-request-input-right";
            }
        } else {
            $scope.requestSeller.phoneValidation = false;
            $scope.requestSeller.phoneClass = "dandi-request-input-error";
        }
    });
    //info validation
    $scope.$watch('requestSeller.info', function (newValue, oldValue) {
        if (newValue) {
            if (newValue.length <= 20) {
                $scope.requestSeller.infoValidation = false;
                $scope.requestSeller.infoClass = "dandi-request-input-error";
            } else {
                $scope.requestSeller.infoValidation = true;
                $scope.requestSeller.infoClass = "dandi-request-input-right";
            }
        } else {
            $scope.requestSeller.infoValidation = false;
            $scope.requestSeller.infoClass = "dandi-request-input-error";
        }
    });
    //aboutculture validation
    $scope.$watch('requestSeller.aboutculture', function (newValue, oldValue) {
        if (newValue) {
            if (newValue.length <= 20) {
                $scope.requestSeller.aboutcultureValidation = false;
                $scope.requestSeller.aboutcultureClass = "dandi-request-input-error";
            } else {
                $scope.requestSeller.aboutcultureValidation = true;
                $scope.requestSeller.aboutcultureClass = "dandi-request-input-right";
            }
        } else {
            $scope.requestSeller.aboutcultureValidation = false;
            $scope.requestSeller.aboutcultureClass = "dandi-request-input-error";
        }
    });
});

// Page loader
$(".page-loader b").delay(0).fadeOut();
$(".page-loader").delay(100).fadeOut("slow");




