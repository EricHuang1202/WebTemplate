myApp.controller('masterPageCtrl', function ($scope, session, $location) {

    $scope.validation = function (sessionToken) {
        var urlPathName = window.location.pathname;

        if (urlPathName.replace(webSitePathName, "") == "")
            return;

        //不用登入即可瀏覽的頁面有 
        //if (urlPathName.replace(webSitePathName, "").search("index") >= 0)
         //   return;

        //判定使用者是否有登入
        if (sessionToken == null) {
            
            window.location = 'login.html';
        }
    }

    session.getSession("SessionToken").then(function (response) {
        var sessionToken = response.data.Value;
        $scope.validation(sessionToken);
    });

    session.getSession("AccName").then(function (response) {
        $scope.accName = response.data.Value;
    });



    $scope.logout = function () {
        session.delSession("SessionToken").then(function (response) {
            session.delSession("AccName").then(function (response) {
                window.location = 'login.html';
            });
        });
    };

    
});


myApp.controller('menuCtrl', function ($scope, session) {
    $scope.isActive = function (path) {
        return window.location.pathname == path;
    };
});