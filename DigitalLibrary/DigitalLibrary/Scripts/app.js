
var url = 'http://localhost:52464/api/user/?query=';
var url1 = 'http://localhost:52464/api/genre';
var url2 = 'http://localhost:52464/api/book/?query=';
var allBooks = 'http://localhost:52464/api/book';
var bookByGenre = 'http://localhost:52464/api/genre/?id=';
var book = 'http://localhost:52464/api/book/?id=';

var UserApp = angular.module('UserApp', ['ngResource', 'ngRoute', 'ui.bootstrap']).
 config(function ($routeProvider) {
     $routeProvider
     .when("/book/details/:bookid", {
         templateUrl: "http://localhost:52464/User/BookDetails",
         controller: "bookDetails"
     })
      .when("/", {
          templateUrl: "http://localhost:52464/User/BookIndex",
          controller:"bookAllCtrl"
      })
      .when("/genre/books/:genreid", {
          templateUrl: "http://localhost:52464/User/BookIndex",
          controller: "bookByGenreCtrl"
      })
 });

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
        getGenres: function (callback) {

            $http.get(url1).success(callback);
        }
    }
});

//the factory object for the webAPI call.
UserApp.factory('bookRepository', function ($http) {
    return {       
        getAllBooks: function (callback) {

        $http.get(allBooks).success(callback);
        },
        getBooksByGenres: function (id,callback) {

            $http.get(bookByGenre + id).success(callback);
        },
        getBook: function (id, callback) {

            $http.get(book + id).success(callback);
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
        genreRepository.getGenres(function (results) {
            $scope.items = results;
        })
    };
    getGenres();
    $scope.search = function () {
        getGenres();
    };
});

var data = [];
//controller   
UserApp.controller('bookAllCtrl', function ($scope, bookRepository) {
       
    $scope.books = []
    $scope.booksSize = 0;
    function getAllBooks() {
        bookRepository.getAllBooks(function (results) {
            data = results;
            data1 = data;
            $scope.books = data1.slice(0,$scope.itemsPerPage);
            $scope.booksSize = results.length;
        })
    };
    getAllBooks();
    $scope.search = function () {
        getAllBooks();
    };   
    
    $scope.maxSize = 5;
    $scope.currentPage = 1;
    $scope.itemsPerPage = 4;   
    $scope.$watch("currentPage", function (newValue, oldValue) {
        data1 = data;
        offset = (newValue-1) * $scope.itemsPerPage;
        limit = $scope.itemsPerPage;
        $scope.books = data1.slice(offset, offset + limit);        
    });   
});

UserApp.controller('bookByGenreCtrl', function ($scope, bookRepository, $routeParams) {
    $scope.books = [];
    $scope.booksSize = 0;
    function getBooks() {
        bookRepository.getBooksByGenres($routeParams.genreid, function (results) {
            data = results;
            data1 = data;
            $scope.books = data1.slice(0, $scope.itemsPerPage);
            $scope.booksSize = results.length;
        })
    };
    getBooks();
    $scope.maxSize = 5;
    $scope.currentPage = 1;
    $scope.itemsPerPage = 4;
    $scope.$watch("currentPage", function (newValue, oldValue) {
        data1 = data;
        offset = (newValue - 1) * $scope.itemsPerPage;
        limit = $scope.itemsPerPage;
        $scope.books = data1.slice(offset, offset + limit);
    });
});

UserApp.controller('bookDetails', function ($scope, bookRepository, $routeParams) {
    function getBook() {
        bookRepository.getBook($routeParams.bookid, function (results) {
            $scope.book = results;
        })
    };
    getBook();

    $scope.Userrate = 0;
    $scope.rate =7;
    $scope.max = 10;
    $scope.isReadonly = false;  
});
