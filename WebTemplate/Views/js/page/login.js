myApp.controller('loginCtrl', function ($scope, $http, $q, session, cookie) {

    var cookieAccId = cookie.getCookie("AccountId");
    if (cookieAccId != null) {
        $scope.username = cookieAccId;
        $scope.chkRemember = true;
    };

    $scope.login = function () {
        var data = { "AccountId": $scope.username, "Password": $scope.password};

        $http({
            method: 'POST',
            url: webSitePathName + "Login/ChkLogin",
            data: data,
        }).then(function mySuccess(response) {
            if ($scope.chkRemember) {
                cookie.setCookie("AccountId", $scope.username);
            }
            else {
                cookie.delCookie("AccountId");
            }

            $scope.accName = response.data.AccName;
            $scope.setSessionToken();  
        }, function myError(response) {
            alert(response.data.Message);
        });
    };

    $scope.setSessionToken = function () {
        var sessionToken = new String($scope.username + '&' + $scope.password);
        var data = { "Param": sessionToken };

        $http({
            method: 'POST',
            url: webSitePathName + "Session/TokenEncrypt",
            data: data,
        }).then(function mySuccess(response) {
            sessionToken = response.data.Value;
            session.setSession("SessionToken", sessionToken).then(function () {
                session.setSession("AccName", $scope.accName).then(function () {
                    window.location = "index.html";
                })
            });
        }, function myError(response) {
            alert(response.data.Message);
        });
    }
});


