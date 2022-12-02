// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function getBannerImageForPage() {
    //var page = window.location.pathname;
    //var page = window.location.pathname.split("/").pop().split('#').shift();
    //page.split('#').shift();

    //if (page.includes("/CampaignListings/Details/")) {
    //    //imgPath = @CampaignImg.campaignImgPath;
    //    document.getElementById("bannerImageId").src.value = imgPath;
    //}
    //else if (title.includes("Register")) {
    //    console.log("Setting Registration Image");
    //    document.getElementById("bannerImage").src = "/images/RegisterBannerImage.jpg";
    //}
}



document.getElementById("btnSaveCharacterImg").addEventListener("click", function (event) {
    event.preventDefault
    var ele = document.getElementsByName('CharacterImg');
    
    for (i = 0; i < ele.length; i++) {
        if (ele[i].checked) {
            document.getElementById("CharacterImgValue").value = ele[i].value;
            var imgPathID = "characterImg" + ele[i].value;
            var imgPath = document.getElementById(imgPathID).value;
            document.getElementById("CharacterImgIcon").src = imgPath;
            $('#CharacterImgModal').modal('toggle');
            break;
        }
    }
})

document.getElementById("btnSaveCampaignImg").addEventListener("click", function (event) {
    event.preventDefault
    var ele = document.getElementsByName('CampaignImg');

    for (i = 0; i < ele.length; i++) {
        if (ele[i].checked) {
            document.getElementById("CampaignImgValue").value = ele[i].value;
            var imgPathID = "campaignImg" + ele[i].value;
            var imgPath = document.getElementById(imgPathID).value;
            document.getElementById("CampaignImg").src = imgPath;
            $('#CampaignImgModal').modal('toggle');
            break;
        }
    }
})

