var UserApp = angular.module('UserApp', ['ngResource']);
var url = 'http://localhost:52464/api/user/?query=';

//the factory object for the webAPI call.
UserApp.factory('userRepository', function ($http) {
    return {
        getUsers: function (query,callback) {

            $http.get(url + query).success(callback);
        }
    }
});

//controller   
UserApp.controller('userCtrl', function ($scope, userRepository) {    
    function getUsers() {
        userRepository.getUsers($scope.query,function (results) {
            $scope.items = results;
        })
    };   
    getUsers();
$scope.search = function () {
getUsers();
};
});

