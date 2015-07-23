var windowWidth = $(window).width();

var app = angular.module('myApp', ['ui.bootstrap']).
controller('ProjectCtrl', function ($scope) {
    $scope.dandimarket2 = [{ title: 'seller' }, { title: 'designer' },
        { title: 'band' }, { title: 'plan1' },
        { title: 'plan2' }, { title: 'plan3' },
        { title: 'plan4' }];

    // 카테고리 클릭 이벤트
    $scope.selectCategory = function (categoryIdx) {
        // 1 : 셀러신청
        // 2 : 디자이너 신청
        // 3 : 밴드 신청
        // 4 : 기획서 보기
        _.each($('.dandimarket2-footer-item-content'), function (item, idx) {
            if (idx == categoryIdx - 1) {
                $(item).addClass('active');
            } else {
                $(item).removeClass('active');
            }
        });
        
        
    }
});


$(document).ready(function () {
    $('#fullpage').fullpage({
        sectionsColor: ['#36465d', '#56bc84', '#36465d', '#bb766a'],
        anchors: ['PROJECT', 'DANDIMARKET#1', 'DANDIMARKET#2', 'DANDICALENDAR'],
        css3: true,
        scrollingSpeed: 1000,
        menu: '#menu',
        navigation: true,
        navigationPosition: 'left',
        navigationTooltips: ['프로젝트', '단디마켓#1', '단디마켓#2', '단디달력'],
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
    
    initLayout();
});

function initLayout() {
    var windowHeight = $(window).height();
    var sectionHeight = windowHeight - 50;


    // dandimarket1
    $('.dandimarket1-fullscreen').css('height', windowHeight * 0.8 + 'px');
    $('.dandimarket1-footer').css('height', windowHeight * 0.2 + 'px');
    $('.dandimarket1-footer-item-content').css('height', windowHeight * 0.2 - 70 + 'px');
    $('.dandimarket1-footer-item-content').css('line-height', windowHeight * 0.2 - 70 + 'px');

    // dandimarket2
    $('.dandimarket2-fullscreen').css('height', windowHeight + 'px');
    $('.dandimarket2-slide').css('height', windowHeight + 'px');
    $('.dandimarket2-slide-main').css('height', windowHeight * 0.8 + 'px');
    $('.dandimarket2-slide-footer').css('height', windowHeight * 0.2 + 'px');
    $('.dandimarket2-footer-item-content').css('height', windowHeight * 0.2 - 70 + 'px');
    $('.dandimarket2-footer-item-content').css('line-height', windowHeight * 0.2 - 70 + 'px');
    
}

// Page loader
$(".page-loader b").delay(0).fadeOut();
$(".page-loader").delay(100).fadeOut("slow");
