$(document).ready(() => {
    $("#products-container").infiniteTemplate({
        templateSelector: "#product-box-tmpl",
        dataPath: "/all/page",
        loadAtStart: true,
        loaderSelector: $('#products-loading'),
        // query: "",
        // method: "GET",
        // templateHelpers: null,
        // loadSelector: null,
        // initialPage: 1,
        // preventCache: false,
        // zeroCallback:function () {
        //     alert("zero result alert");
        // },
    });
});