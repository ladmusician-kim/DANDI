var app = angular.module('myApp', []).
controller('HomeCtrl', function ($scope) {
    $scope.user = {
        isEmailValidate: false,
        isPasswordValidate: false
    };
    $scope.userForLogin = {
    }

    // �����ϱ�
    $scope.sign = function () {
        if ($scope.user.username && $scope.user.username.length > 1) {
            getJson('/Home/CheckUsername', { username: $scope.user.username },
               function (data) {
                   if (data) {
                       if ($scope.user.isEmailValidate) {
                           if ($scope.user.isPasswordValidate) {
                               getJson('/Home/Sign', { username: $scope.user.username, password: $scope.user.password, email: $scope.user.email },
                                   function (data) {
                                       window.location.href = "#LOGIN";
                                   },
                                   function (arg) {
                                       alert("�����ϴµ� ������ �߻��߽��ϴ�");
                                   }, $scope);
                           } else {
                               alert("�ùٸ� ��й�ȣ�� �ٽ� �ۼ����ּ���!");
                           }
                       } else {
                           alert("�ùٸ� �̸��� �ٽ� �ۼ����ּ���!");
                       }
                   } else {
                       alert("�ߺ��� �г����Դϴ�!");
                   }
               },
               function (arg) {
                   alert("������ �߻��߽��ϴ�.");
               }, $scope);
        }
    }

    // �г��� valiation
    $scope.$watch('user.username', function (newValue, oldValue) {
        if (newValue) {
            if (newValue.length <= 1) {
                $('.dandi-sign-username').addClass('error');
            } else {
                $('.dandi-sign-username').removeClass('error');
            }
        }
    });

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



    $scope.goLogin = function () {
        window.location = "/Mobile/Login";
    }
    $scope.goSign = function () {
        window.location = "/Mobile/Sign";
    }
});


$(document).ready(function () {
    $('#fullpage').fullpage({
        sectionsColor: ['none', 'none', 'none'],
        anchors: ['DANDIMARKET', 'SIGN', 'LOGIN'],
        css3: true,
        scrollingSpeed: 1000,
        menu: '#menu',
        navigation: false,
        continuousVertical: true,
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
    $('.dandi-footer').hide();
});

// Page loader
$(".page-loader b").delay(0).fadeOut();
$(".page-loader").delay(100).fadeOut("slow");
