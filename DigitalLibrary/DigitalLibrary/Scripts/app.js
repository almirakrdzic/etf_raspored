﻿var UserApp = angular.module('UserApp', ['ngResource', 'ngRoute', 'ui.bootstrap']).
 config(function ($routeProvider) {
     $routeProvider
     .when("/book/details/:id", {
         templateUrl: function ($routeParams) { return "http://localhost:52464/Book/Details/?id=" + $routeParams.id; }
     })
     .when("/", { templateUrl: "http://localhost:52464/Book/Index" })
      .when("/genre/books/:genreid", {
          templateUrl: function ($routeParams) { return "http://localhost:52464/Book/GenreCategory/?id=" + $routeParams.genreid; }
      })
 });
var url = 'http://localhost:52464/api/user/?query=';
var url1 = 'http://localhost:52464/api/genre/?query=';
var url2 = 'http://localhost:52464/api/book/?query=';

//the factory object for the webAPI call.
UserApp.factory('userRepository', function ($http) {
    return {
        getUsers: function (query, callback) {

            $http.get(url + query).success(callback);
        }
    }
});


//the factory object for the webAPI call.
UserApp.factory('genreRepository', function ($http) {
    return {
        getGenres: function (query, callback) {

            $http.get(url1 + query).success(callback);
        }
    }
});

//the factory object for the webAPI call.
UserApp.factory('bookRepository', function ($http) {
    return {
        getBooks: function (query, callback) {

            $http.get(url2 + query).success(callback);
        }
    }
});

//controller   
UserApp.controller('userCtrl', function ($scope, userRepository) {
    function getUsers() {
        userRepository.getUsers($scope.query, function (results) {
            $scope.items = results;
        })
    };
    getUsers();
    $scope.search = function () {
        getUsers();
    };
});


//controller 
UserApp.controller('genreCtrl', function ($scope, genreRepository) {
    function getGenres() {
        genreRepository.getGenres($scope.query, function (results) {
            $scope.items = results;
        })
    };
    getGenres();
    $scope.search = function () {
        getGenres();
    };
});


//controller   
UserApp.controller('bookAllCtrl', function ($scope, bookRepository) {
    function getBooks() {
        bookRepository.getBooks($scope.query, function (results) {
            $scope.items = results;
        })
    };
    getBooks();
    $scope.search = function () {
        getBooks();
    };

    $scope.totalItems = 64;
    $scope.currentPage = 4;

    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
    };

    $scope.pageChanged = function () {
        console.log('Page changed to: ' + $scope.currentPage);
    };

    $scope.maxSize = 5;
    $scope.bigTotalItems = 175;
    $scope.bigCurrentPage = 1;
});



