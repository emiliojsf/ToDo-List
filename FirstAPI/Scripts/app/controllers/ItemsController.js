(function () {
    
    const statusInfo = [
                { abbrev: "TODO", value: 1, message: "Ok, I letssay you'll really do this ..." },
                { abbrev: "IN PROGRESS", value: 2, message: "Come back when you finish this!" },
                { abbrev: "COMPLETED", value: 3, message: "Why are you registering this here?..." },
    ];

    const imagePathForStatus = ["error", "cached", "check_circle"];
    const messagesForStatus = new Array(
        "You still need to separate time for this",
        "You said you're going to do this, but...",
        "Finally, let's do another task!");

    angular
        .module("todolist", ['ngMaterial', 'ngMdIcons', 'ngMessages', 'material.svgAssetsCache'])
        .controller("ItemsController",
    ['$scope', '$mdDialog', '$mdSidenav', '$mdBottomSheet', function ($scope, $mdDialog, $mdSidenav, $mdBottomSheet) {
        
        $scope.myDate = new Date();

        $scope.status = statusInfo;
        $scope.imagePath = imagePathForStatus;
        $scope.statusMessage = messagesForStatus;
        
        $scope.toggleSidenav = function (menuId) {
            $mdSidenav(menuId).toggle();
        };

        $scope.menu = [
            {
                title: 'Late Items'
            },
            {
                title: 'Tomorrow'
            },
            {
                title: 'Next Week'
            }
        ];

        $scope.searchTyped = "";

        $scope.filterbySearch = function () {
            var search = $scope.searchTyped.trim();

            if (search.length > 2) {
                ajaxHelper(itemsUri, 'GET').done(function (data) {
                    $scope.items = data;

                    var newArray = new Array();

                    $scope.items.forEach(function (item, index) {
                        if (item.Title.includes(search))
                            newArray.push(item);
                    });

                    $scope.items = newArray;

                });
            }
        }

        $scope.getAllItems = function () {
            ajaxHelper(itemsUri, 'GET').done(function (data) {
                $scope.items = data;
                $scope.$apply();
            });
        }

        $scope.getItemDetail = function (id) {
            ajaxHelper(itemsUri + id, 'GET').done(function (data) {
                dateStringToObjectFormat(data);
                $scope.detailedItem = data;
                $scope.$apply();
            });
        }

        $scope.saveNewItem = function (item) {
            ajaxHelper(itemsUri, 'POST', item).done(function (item) {
                $scope.detailedItem = undefined;
                $scope.$apply();
            });
        };

        $scope.updateItem = function (item) {
           ajaxHelper(itemsUri + item.Id, 'PUT', item).done(function (item) {
                $scope.detailedItem = undefined;
                $scope.$apply();
                $scope.getAllItems();
            });
        };

        $scope.hideCard = function () {
            $scope.detailedItem = undefined;
        }
        
        $scope.deleteItem = function(id) {
            ajaxHelper(itemsUri + id, 'DELETE').done(function (item) {
                $scope.detailedItem = undefined;
                $scope.$apply();
                $scope.getAllItems();
            });
        }

        $scope.cleanFilters = function () {
            $scope.getAllItems();
        }

        $scope.showAdd = function (ev) {
            $mdDialog.show({
                controller: DialogController,
                template:                    
                    '<md-dialog class="myDialog">' +
                    '<md-card > ' +
                    '<md-card-title>' +
                    '<md-card-title-text>' +
                    '<form name="itemForm"> ' +
                    '<div layout="column"> ' +
                    '<md-input-container flex> ' +
                    '<label>Title</label> ' +
                    '<input ng-model="item.Title">' +
                    '</md-input-container> ' +
                    '<md-input-container> ' +
                    '<md-input-container class="allWidth"> <label>Description</label> <textarea ng-model="item.Description" md-maxlength="250" rows="2"></textarea>' +
                    '</md-input-container> ' +
                    '<div layout="column"> ' +
                    '<md-input-container flex> ' +
                    '<div layout="row">' +
                    '<md-input-container>' +
                    '<label>Date</label>' +
                    '<md-datepicker ng-model="item.Date" md-placeholder="Enter date"></md-datepicker>' +
                    '</md-input-container>' +
                    '<md-input-container>' +
                    '<label>Hours</label>' +
                    '<md-select ng-model="detailedItem.Hours">' +
                    '<md-option ng-repeat="hour in hours" ng-value="hour">' +
                    '{{hour}}' +
                    '</md-option>' +
                    '</md-select>' +
                    '</md-input-container>' +
                    '<md-input-container>' +
                    '<label>Minutes</label>' +
                    '<md-select ng-model="detailedItem.Minute">' +
                    '<md-option ng-repeat="minute in minutes" ng-value="minute">' +
                    '{{minute}}' +
                    '</md-option>' +
                    '</md-select>' +
                    '</md-input-container>' +
                    '</div>' +
                    '<md-input-container flex> ' +
                    '<label>Status</label>' +
                    '<md-select ng-model="item.Status">' +
                    '<md-option ng-repeat="stat in status" ng-value="stat.value">' +
                    '{{stat.abbrev}}' +
                    '</md-option></md-select>' +
                    '</md-input-container>'+
                    
                    //'<label>{{ statusMessage[item.Status -1] }}</label>' +
                    '</div>' +
                    '</div>' +
                    '</md-card-title>' +
                    '</md-card-title-text>' +
                    '</form></md-card> ' +
                    '<div class="md-dialog-actions" layout="row"> <span flex></span> <md-button ng-class="md-raised" aria-label="Cancel" ng-click="cancel()"> Cancel </md-button> ' +
                    '<md-button aria-label="Save" ng-click="save(item)" class="md-primary md-raised"> Save </md-button>' +
                    '</div></md-dialog>',
                targetEvent: ev,
            });
        };

        $scope.hours = hours;
        $scope.minutes = minutes;

        $scope.getAllItems();
    }]);
    
    function DialogController($scope, $mdDialog) {

        $scope.status = statusInfo;
        $scope.imagePath = imagePathForStatus;
        $scope.statusMessage = messagesForStatus;
        $scope.hours = hours;
        $scope.minutes = minutes;

        $scope.save = function (item) {
            console.log("Date: " + item.Date)
            ajaxHelper(itemsUri, 'POST', item).done(function () {
                $mdDialog.hide();
            });
        };

        $scope.cancel = function () {
            $mdDialog.cancel();
        };
    }

    //ItemsController.$inject = ["$scope"];
    var itemsUri = '/api/items/';
    function ajaxHelper(uri, method, data) {
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.log(errorThrown);
        });
    }

    function dateStringToObjectFormat(item) {
        var year = item.Date.toString().substr(0, 4);
        var month = item.Date.toString().substr(5, 2);
        var day = item.Date.toString().substr(8, 2);

        item.Date = new Date(year, month - 1, day);
        item.Hours = item.Hours > 10 ? item.Hours : "0" + item.Hours;
        item.Minute = item.Minute > 10 ? item.Minute : "0" + item.Minute;
    }

    hours = []
    minutes = []
    for (var i = 0; i < 60; i++) {
        var i2 = i;
        if (i < 10) i2 = "0" + i;

        if (i < 24) hours.push(i2);
        minutes.push(i2);
    }

}());