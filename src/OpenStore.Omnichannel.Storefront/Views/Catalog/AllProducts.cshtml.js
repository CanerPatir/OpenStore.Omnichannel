$(document).ready(() => {
    $("#products-container").infiniteTemplate({
        templateSelector: "#product-box-tmpl",
        dataPath: "/all-products/page",
        query: "",
        // loadSelector: $('#loadmore'),
        // method: "GET",
        // templateHelpers: null,
        loadAtStart: true,
        // loadSelector: null,
        // initialPage: 1,
        // preventCache: false,
        // zeroCallback:function () {
        //     alert("zero result alert");
        // },
    });
});

// {
//     "data": [
//     {
//         "id": 1,
//         "title": "Title 1"
//     },
//     {
//         "id": 2,
//         "title": "Title 2"
//     },
//     {
//         "id": 3,
//         "title": "Title 3"
//     },
//     // more data here
// ]
// }