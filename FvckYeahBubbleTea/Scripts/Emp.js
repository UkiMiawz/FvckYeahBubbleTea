﻿/*function baseTeaController($scope, $http) {
    $scope.loading = true;
    $scope.addMode = false;

    //Used to display the data
    $http.get('/api/BaseTea/').success(function (data) {
        $scope.baseTeas = data;
        $scope.loading = false;
    })
    .error(function () {
        $scope.error = "An Error has occured while loading posts!";
        $scope.loading = false;
    });

    $scope.toggleEdit = function () {
        this.baseTea.editMode = !this.baseTea.editMode;
    };
    $scope.toggleAdd = function () {
        $scope.addMode = !$scope.addMode;
    };

    //Used to save a record after edit
    $scope.save = function () {
        alert("Edit");
        $scope.loading = true;
        var baseTe = this.baseTea;
        alert(emp);
        $http.put('/api/BaseTea/', baseTe).success(function (data) {
            alert("Saved Successfully!!");
            emp.editMode = false;
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving BaseTea! " + data;
            $scope.loading = false;

        });
    };

    //Used to add a new record
    $scope.add = function () {
        $scope.loading = true;
        $http.post('/api/BaseTea/', this.newBaseTea).success(function (data) {
            alert("Added Successfully!!");
            $scope.addMode = false;
            $scope.baseTeas.push(data);
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Adding BaseTea! " + data;
            $scope.loading = false;

        });
    };

    //Used to edit a record
    $scope.deleteBaseTea = function () {
        $scope.loading = true;
        var baseTeaId = this.baseTea.Id;
        $http.delete('/api/BaseTea/' + baseTeaId).success(function (data) {
            alert("Deleted Successfully!!");
            $.each($scope.baseTeas, function (i) {
                if ($scope.baseTeas[i].Id === baseTeaId) {
                    $scope.baseTeas.splice(i, 1);
                    return false;
                }
            });
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving BaseTea! " + data;
            $scope.loading = false;

        });
    };

}*/

var app = angular.module("FvckYeahBubbleTea", ["checklist-model"]);
app.controller('OrderController', function($scope, $http) {

    $scope.loading = true;

    $scope.selectedBaseTea = null;
    $scope.selectedFlavor = null;
    $scope.selectedSize = null;
    $scope.selectedToppings = [];

    $scope.totalPrice = 0;

    //Get base tea
    $http.get('/api/baseTea/').success(function (data) {
        $scope.baseTeas = data;
        $scope.loading = false;
    })
    .error(function () {
        $scope.error = "An Error has occured!";
        $scope.loading = false;
    });

    $scope.loading = true;
    //Get flavors
    $http.get('/api/flavor/').success(function (data) {
        $scope.flavors = data;
        $scope.loading = false;
    })
    .error(function () {
        $scope.error = "An Error has occured!";
        $scope.loading = false;
    });

    $scope.loading = true;
    //Get size
    $http.get('/api/size/').success(function (data) {
        $scope.teaSizes = data;
        $scope.loading = false;
    })
    .error(function () {
        $scope.error = "An Error has occured!";
        $scope.loading = false;
    });

    $scope.loading = true;
    //Get toppings
    $http.get('/api/topping/').success(function (data) {
        $scope.toppings = data;
        $scope.loading = false;
    })
    .error(function () {
        $scope.error = "An Error has occured!";
        $scope.loading = false;
    });

    $scope.loading = true;
    //Get orders
    $http.get('/api/order/').success(function (data) {
        $scope.orders = data;
        $scope.loading = false;
    })
    .error(function () {
        $scope.error = "An Error has occured!";
        $scope.loading = false;
    });

    $scope.updatePrice = function () {

        var totalPrice = 0;

        if ($scope.selectedSize != null) {

            totalPrice = $scope.selectedSize.Price;

            for (var i = 0; i < $scope.selectedToppings.length; i++) {
                totalPrice += $scope.selectedToppings[i].Price;
            }
        }

        return totalPrice;

    };

    $scope.calculateOrder = function (order) {

        var totalPrice = 0;

        if (order.Size != null) {

            totalPrice = order.Size.Price;

            for (var i = 0; i < order.Toppings.length; i++) {
                totalPrice += order.Toppings[i].Price;
            }
        }
        return totalPrice;

    };

    $scope.calculateOrders = function () {

        var totalPrice = 0;

        for (var i = 0; i < $scope.orders.length; i++) {
            if ($scope.orders[i].Size != null) {

                totalPrice += $scope.orders[i].Size.Price;

                for (var j = 0; j < $scope.orders[i].Toppings.length; j++) {
                    totalPrice += $scope.orders[i].Toppings[j].Price;
                }
            }
        }

        return totalPrice;

    };

    //add order
    $scope.add = function () {
        $scope.loading = true;
        var newOrder = {
            BaseTea: $scope.selectedBaseTea,
            Flavor: $scope.selectedFlavor,
            Size: $scope.selectedSize,
            Toppings: $scope.selectedToppings
        };
        $http.post('/api/order/', newOrder).success(function (data) {
            alert("Added Successfully!!");
            $scope.orders.push(data);
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured!" + data;
            $scope.loading = false;

        });
    };

    //edit order
    $scope.delete = function () {
        $scope.loading = true;
        var orderId = this.order.Id;
        $http.delete('/api/order/' + orderId).success(function (data) {
            alert("Deleted Successfully!!");
            $.each($scope.orders, function (i) {
                if ($scope.orders[i].Id === orderId) {
                    $scope.orders.splice(i, 1);
                    return false;
                }
            });
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured!" + data;
            $scope.loading = false;

        });
    };

    //Used to save a record after edit
    $scope.save = function () {
        alert("Edit");
        $scope.loading = true;
        var orderEdit = this.order;
        $http.put('/api/order/' + orderEdit.Id, orderEdit).success(function (data) {
            alert("Saved Successfully!!");
            emp.editMode = false;
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured!" + data;
            $scope.loading = false;

        });
    };

    $scope.toggleEdit = function () {
        this.order.editMode = !this.order.editMode;
    };

    //handle edit radio button change
    $scope.radioButtonChange = function (data) {
        console.log("TEST");
        this.order.Size = data;
    };

})