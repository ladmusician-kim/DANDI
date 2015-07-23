var app = angular.module('myApp', []).
controller('HomeCtrl', function ($scope) {
    $scope.user = {
        isEmailValidate: false,
        isPasswordValidate: false,
        isConfirmPasswordValidate: false
    };
    $scope.userForLogin = {
    }

    // 가입하기
    $scope.sign = function () {
        getJson('/Home/CheckEmail', { email: $scope.user.email },
            function (data) {
                if (data) {
                    if ($scope.user.isEmailValidate) {
                        if ($scope.user.isPasswordValidate) {
                            if ($scope.user.isConfirmPasswordValidate) {
                                getJson('/Home/Sign', { email: $scope.user.email, password: $scope.user.password, confirmPassword: $scope.user.confirmPassword },
                                function (data) {
                                    alert("가입에 성공하였습니다!");
                                    window.location.href = "#LOGIN";

                                    $scope.user.email = "";
                                    $scope.user.password = "";
                                    $scope.user.confirmPassword = "";
                                },
                                function (arg) {
                                    alert("가입하는데 오류가 발생했습니다");
                                }, $scope);
                            } else {
                                alert("올바른 비밀번호를 다시 작성해주세요!");
                                $('#dandi-sign-input-confirmPwd').focus();
                            }
                        } else {
                            alert("올바른 비밀번호를 다시 작성해주세요!");
                            $('#dandi-sign-input-pwd').focus();
                        }
                    } else {
                        alert("올바른 이메일 다시 작성해주세요!");
                        $('#dandi-sign-input-email').focus();
                    }
                } else {
                    alert("중복된 이메일 입니다!");
                    $('#dandi-sign-input-email').focus();
                }
            },
            function (arg) {
                alert("오류가 발생했습니다.");
            }, $scope);
    }

    // password validation
    $scope.$watch('user.password', function (newValue, oldValue) {
        if (newValue) {
            if (newValue.length < 6) {
                $('.dandi-sign-password').addClass('error');
            } else {
                $scope.user.isPasswordValidate = true;
                $('.dandi-sign-password').removeClass('error');
            }
        }
    });
    // confirmPassword validation
    $scope.$watch('user.confirmPassword', function (newValue, oldValue) {
        if (newValue) {
            if (newValue.length < 6) {
                $('.dandi-sign-confirmPassword').addClass('error');
            } else {
                if ($scope.user.password === newValue) {
                    console.log($scope.user.password);
                    console.log($scope.user.confirmPassword);
                    $scope.user.isConfirmPasswordValidate = true;
                    $('.dandi-sign-confirmPassword').removeClass('error');
                } else {
                    $scope.user.isConfirmPasswordValidate = false;
                    $('.dandi-sign-confirmPassword').addClass('error');
                }
            }
        }
    });
    //email validation
    $scope.$watch('user.email', function (newValue, oldValue) {
        if (newValue) {
            if (IsEmail(newValue)) {
                $scope.user.isEmailValidate = true;
                $('.dandi-sign-email').removeClass('error');
            } else {
                $('.dandi-sign-email').addClass('error');
            }
        }
    });

    function IsEmail(email) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        return regex.test(email);
    }
});


$(document).ready(function () {
    $('#fullpage').fullpage({
        sectionsColor: ['none', 'none', 'none'],
        anchors: ['DANDIMARKET', 'SIGN', 'LOGIN'],
        css3: true,
        scrollingSpeed: 1000,
        menu: '#menu',
        navigation: true,
        navigationPosition: 'left',
        navigationTooltips: ['가입하기', '로그인'],
        'keyboardScrolling': true,
        slidesNavigation: true,
        afterLoad: function (anchorLink, index) {
            if (anchorLink === "SIGN") {
                $("#dandi-sign-input-username").focus();
            }
            $(".appear-animate").find(".animate-init").addClass("animate-in").removeClass("animate-init");
        },
        onLeave: function (index, nextIndex, direction) {
            $(".appear-animate").find(".animate-init").addClass("animate-in").removeClass("animate-init");
        }
    });
});

// Page loader
$(".page-loader b").delay(0).fadeOut();
$(".page-loader").delay(100).fadeOut("slow");
