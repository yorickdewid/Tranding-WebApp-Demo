//Controllers//
'use strict';
///////////////

var forexControllers = angular.module('forexControllers', []);

forexControllers.controller('LoginCtrl', ['$scope', '$location', 'User',
  function ($scope, $location, User) {

      if (sessionStorage.getItem("user") != undefined) {
          var user = $.parseJSON(sessionStorage.user);
          if (user) {
              $location.path('wallet/' + user.Id);
          }
      }

      $scope.users = User.get({}, function (list) {
          console.log(list);
          $scope.users = list.Users;
      });

      $scope.onChange = function () {
          for (var i = 0; i < $scope.users.length; i++) {
              if ($scope.users[i].Id == $scope.userSelected) {
                  console.log($scope.users[i]);
                  sessionStorage.setItem('user', JSON.stringify($scope.users[i]));
                  $location.path('wallet/' + $scope.users[i].Id);
                  break;
              }
          }
      }

  }]
);

forexControllers.controller('WalletCtrl', ['$scope', '$routeParams', '$http',
 function ($scope, $routeParams, $http) {
     $scope.userid = $routeParams.id;

     $http.get('/api/wallet/' + $scope.userid).then(function (response) {
         var fTrades = [];
         var amount = response.data.Amount;
         angular.forEach(response.data.Trades, function (value, key) {
             if (value.SellRate)
                 value.profit = (value.SellRate - value.BuyRate) * value.Amount;
             value.BuyDate = new Date(value.BuyDate * 1000);
             if (value.SellDate)
                 value.SellDate = new Date(value.SellDate * 1000);
             fTrades.push(value);
         });

         $scope.Amount = amount;
         $scope.trades = fTrades;
     })

     $scope.sell = function (order) {
         order.BuyDate = Math.round((order.BuyDate).getTime() / 1000);
         order.SellDate = Math.round((new Date()).getTime() / 1000);
         getForexRate(order.Currency, function (rate) {
         	order.SellRate = rate.Ratio;

             $http.put('/api/order/', order).then(function successCallback(response) {
             	order.BuyDate = Math.round(order.BuyDate * 1000);
             	order.SellDate = Math.round(order.SellDate * 1000);
             	order.profit = (order.SellRate - order.BuyRate) * order.Amount;
             }, function errorCallback(response) {
                 console.log(response);
             });
         });
     }

     function getForexRate(currency,callback) {
         console.log(currency);
         var url = '/api/forex/' + currency;
         console.log(url);
         $http.get(url).then(function successCallback(response) {
             callback(response.data);
         }, function errorCallback(response) {
         	 console.log(response);
         });
     }

     $scope.checkProfit = function (trade) {
         if (trade.SellRate) {
             var profit = (trade.SellRate - trade.BuyRate);
             if (profit>0)
                 return "success";
             if (profit<0)
                 return "danger";
         }
     }

 }]
);

forexControllers.controller('ForexCtrl', ['$scope', '$http', 'Forex',
  function ($scope, $http, Forex) {
      $scope.forex = Forex.get({}, function (forex) {
          $scope.rates = forex.Rates;
          /*console.log(forex.rates);*/
      });

      $scope.succes = false;
      $scope.failure = false;

      $scope.order = function (amount, rate) {
          var user = $.parseJSON(sessionStorage.user);
          if (amount > 0) {
              var data = {
                  "UserId": user.Id, // Creates new user..
                  "Currency": rate.Code,
                  "BuyDate":  Math.round((new Date()).getTime() / 1000),
                  "BuyRate" : rate.Ratio,
                  "Amount": amount
              };
              $http.post('/api/order/', data).then(function successCallback(response) {
                  rate.success = true;
              }, function errorCallback(response) {
                  rate.failure = false;
              });
          }
      }

  }]
);

forexControllers.controller('UsersCtrl', ['$scope', '$http', '$route', 'User',
  function ($scope, $http, $route, User) {
      $scope.users = User.get({}, function (user) {
          $scope.users = user.Users;
      });

      $scope.delete = function (user) {
          console.log("delete user " + user.Id);

          $http.delete('/api/user/' + user.Id).then(function successCallback(response) {
              console.log(response);
              $route.reload();
          }, function errorCallback(response) {
              console.log("Error :" + response)
          });

      }
  }]
);
