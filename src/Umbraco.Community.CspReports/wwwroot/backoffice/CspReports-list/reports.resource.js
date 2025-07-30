(function () {
    "use strict";

    function resource($http, umbRequestHelper) {
        return {
            getItems: function (page, pageSize) {
                return umbRequestHelper.resourcePromise(
                    $http.get(
                        "/umbraco/backoffice/api/CspReports/get",
                        {
                            params: {
                                page: page,
                                pageSize: pageSize,
                            }
                        }),
                    "Failed to get CSP violations"
                );
            },
            deleteAll: function () {
                return umbRequestHelper.resourcePromise(
                    $http.delete("/umbraco/backoffice/api/CspReports/deleteall"),
                    "Failed to delete all CSP violations"
                );
            },
        };
    }

    angular.module("umbraco.resources").factory("cspReportsResource", resource);
})();