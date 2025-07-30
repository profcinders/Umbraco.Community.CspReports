(function () {
    "use strict";

    function controller(navigationService, overlayService, notificationsService, cspReportsResource) {
        let vm = this;

        let init = () => {
            navigationService.syncTree({ tree: "CspReports-list", path: -1 });

            vm.page = {
                name: "CSP Violation Reports",
                loading: true,
            };

            vm.reports = [];
            vm.total = 0;

            vm.accordions = {
                expanded: [],
                toggle: toggleAccordion,
            };

            vm.pagination = {
                currentPage: 1,
                pageSize: 20,
                totalPages: 0,
                changePage: changePage,
            };

            vm.state = {
                deletingAll: undefined,
            };

            vm.getItems = getItems;
            vm.getUrlOrigin = getUrlOrigin;
            vm.parseDate = parseDate;
            vm.confirmDeleteAll = confirmDeleteAll;

            getItems();
        };

        let getItems = () => {
            vm.page.loading = true;

            cspReportsResource.getItems(vm.pagination.currentPage, vm.pagination.pageSize)
                .then(function (result) {
                    vm.reports = result.results;
                    vm.total = result.totalResults;
                    vm.accordions.expanded = [];
                    vm.pagination.totalPages = result.totalPages;

                    if (vm.reports.length == 0 && vm.total > 0) {
                        vm.pagination.currentPage = 1;
                        getItems();
                        return;
                    }

                    vm.page.loading = false;
                }, function (error) {
                    raiseError(error, "Failed to retrieve reports");
                    vm.page.loading = false;
                });
        };

        let getUrlOrigin = (url) => URL.parse(url).origin;

        let parseDate = (dateStr) => new Date(dateStr).toLocaleString();

        let toggleAccordion = (event, reportIndex) => {
            if (vm.accordions.expanded.includes(reportIndex)) {
                vm.accordions.expanded = vm.accordions.expanded.filter(e => e !== reportIndex);
            } else {
                vm.accordions.expanded.push(reportIndex);
            }
        };

        let changePage = (event) => {
            const pagination = event.target;
            vm.pagination.currentPage = pagination.current;
            getItems();
        };

        let confirmDeleteAll = () => {
            vm.state.deletingAll = "waiting";
            overlayService.open({
                title: "Delete all",
                content: "This is a permanent operation. Are you sure you want to delete all CSP reports?",
                closeButtonLabel: "Cancel",
                submitButtonLabel: "Confirm",
                submitButtonStyle: "danger",
                close: () => {
                    vm.state.deletingAll = undefined;
                    overlayService.close();
                },
                submit: deleteAll,
            });
        };

        let deleteAll = () => {
            overlayService.close();

            cspReportsResource.deleteAll()
                .then(function () {
                    vm.state.deletingAll = "success";

                    vm.reports = [];
                    vm.total = 0;
                    vm.pagination.currentPage = 1;
                    vm.pagination.totalPages = 0;
                    getItems();
                }, function (error) {
                    raiseError(error, "Something went wrong while deleting all reports");
                    vm.state.deletingAll = "failed";
                });
        };

        let raiseError = (error, message) => {
            console.error(error);
            notificationsService.error("Error", message);
        };

        init();
    }

    angular.module("umbraco").controller("Umbraco.Community.CspReports.List.Controller", controller);
})();