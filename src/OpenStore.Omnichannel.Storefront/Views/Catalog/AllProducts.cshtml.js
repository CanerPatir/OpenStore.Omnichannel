$(document).ready(() => {
    $("#products-container").infiniteTemplate({
        templateSelector: "#product-box-tmpl",
        dataPath: "/all-products/page",
        loadAtStart: true,
        // query: "",
        loaderSelector: $('#products-loading'),
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