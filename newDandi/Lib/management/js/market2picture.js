var app = angular.module('myApp', ['myApp.filters', 'ngTable']).
       controller('ManagementCtrl', function ($scope, $timeout, ngTableParams) {
           $scope.daypic = {
               items: [],
               page: 1,
               perPage: 10
           }
           $scope.resetCacheData = {};
           $scope.tableParams = new ngTableParams({
               page: 1,            // show first page
               count: 10,          // count per page
               sorting: {
                   Id: 'desc',     // initial sorting
               },
               filter: {
               },
           }, {
               total: 0,
               getData: function ($defer, params) {
                   tableResetPageWhenIfNeeded($scope.resetCacheData, $scope.tableParams, function () {
                       $.ajax({
                           url: '/Management/GetMarket2Picture',
                           type: 'POST',
                           contentType: 'application/json',
                           data: JSON.stringify(params.url()),
                           dataType: 'json',
                           success: function (data) {
                               $scope.$apply(function () {
                                   $scope.daypic.page = $scope.tableParams.page();
                                   $scope.daypic.perPage = $scope.tableParams.count();
                                   $scope.daypic.items = data.Items;

                                   _.each($scope.daypic.items, function (item) {
                                       item.$selected = false;
                                   });

                                   // update table params
                                   params.total(data.RowCount);
                                   $scope.totalLength = data.RowCount;
                                   // set new datax
                                   $defer.resolve(data.Items);
                               })
                           }
                       })
                   });
               }
           });
       });