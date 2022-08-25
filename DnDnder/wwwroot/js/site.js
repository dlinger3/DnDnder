// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function getBannerImageForPage() {

    var title = document.title;
    console.log("retrieved title: " + title);

    if (title.includes("Home")) {
        console.log("Setting Home Image");
        document.getElementById("bannerImage").src = "../images/bannerImage.png"
    }
    else if (title.includes("Register")) {
        console.log("Setting Registration Image");
        document.getElementById("bannerImage").src = "../images/RegisterBannerImage.jpg";
    }
}
