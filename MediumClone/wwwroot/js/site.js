// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//function GetInterestedArticles() {
//    $.ajax({
//        url: "/Home/GetInterestedArticlesPartial",
//        type: "GET",
//        success: function (response) {
//            $("#InterestedArticles").html(response);
//        }
//    })
//}
//$(document).ready(function () {
//    if (window.location == "https://localhost:44375/Home/UserIndex")
//        GetInterestedArticles();
//});

function CloseAddCategoryToUser() {
    $("#CategoryList").html("");
}


function GetAddCategoryToUser() {
    $.ajax({
        url: "/Home/AddCategoryToUser",
        type: "GET",
        success: function (response) {
            $("#CategoryList").html(response)
        }
    })
}
function AddCategoryToUser() {
    let a = {
        categoryID: $("#CategoryID").val()        
    }
    $.ajax({
        url: "/Home/AddCategoryToUser",
        type: "Post",
        data: a,
        dataType: "Json",
        success: function (response) {
            if (response == "Ok") {
                CloseAddCategoryToUser();                
            }
        }
    })
}