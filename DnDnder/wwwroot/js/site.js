// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function getBannerImageForPage() {

    var title = document.title;
    console.log("retrieved title: " + title);

    if (title.includes("Home")) {
        console.log("Setting Home Image");
        document.getElementById("bannerImage").src = "/images/bannerImage.png"
    }
    else if (title.includes("Register")) {
        console.log("Setting Registration Image");
        document.getElementById("bannerImage").src = "/images/RegisterBannerImage.jpg";
    }
}

//TODO: METHODS FOR PUSHER. THESE CAN POTENTIALLY BE REMOVED IF SignalR api is used instead of Pusher for realtime chat functionality
//$("#groups").on("", ".group", function () {
//    let group_id = $(this).attr("data-group_id");

//    $('.group').css({ "border-style": "none", cursor: "pointer" });
//    $(this).css({ "border-style": "inset", cursor: "default" });

//    $("#currentGroup").val(group_id); // update the current group_id to html file...
//    currentGroupId = group_id;

//    // get all messages for the group and populate it...
//    $.get("/api/message/" + group_id, function (data) {
//        let message = "";

//        data.forEach(function (data) {
//            let position = (data.addedBy == $("#UserName").val()) ? " float-right" : "";

//            message += `<div class="row chat_message` + position + `">
//                             <b>` + data.addedBy + `: </b>` + data.message +
//                `</div>`;
//        });

//        $(".chat_body").html(message);
//    });

//});

//$("#SendMessage").click(function () {
//    console.log("VALUES PASSED TO ON CLICK SendMessage{ " + "ListingID: " + $("ListingID").val() + ", SentBy: " + $("#MessageSentBy").val() + ", MessageText: " + $("#Message").val() + " }")
//    $.ajax({
//        type: "POST",
//        url: "/GroupMessages/" + $("ListingID").val(),
//        data: JSON.stringify({
//            SentBy: $("#MessageSentBy").val().toString(),
//            //ListingChapGroupID: $("#currentGroup").val(),
//            MessageText: $("#Message").val(),
//            socketId: pusher.connection.socket_id
//        }),
//        success: (data) => {
//            $(".chat_body").append(`<div class="row chat_message float-right"><b>`
//                + data.data.SentBy + `: </b>` + $("#Message").val() + `</div>`
//            );
//            console.log("Message was added");
//            $("#Message").val('');
//        },
//        dataType: 'json',
//        contentType: 'application/json'
//    });
//});
