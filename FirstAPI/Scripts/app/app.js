(function () {
    "use strict";

    angular.module("todolist", []);
}());

/*
var ViewModel = function () {
    var self = this;
    self.items = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    

    var itemsUri = '/api/items/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllItems() {
        ajaxHelper(itemsUri, 'GET').done(function (data) {
            self.items(data);
        });
    }

    self.getItemDetail = function (item) {
        ajaxHelper(itemsUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    self.addItem = function (formElement) {
        var item = {
            Title: self.newItem.Title(),
            Description: self.newItem.Description(),
            Date: self.newItem.Date(),
            Status: self.newItem.Status()
        };

        ajaxHelper(itemsUri, 'POST', item).done(function (item) {
            self.books.push(item);
        });
    }

    // Fetch the initial data.
    getAllItems();
};

ko.applyBindings(new ViewModel());
*/