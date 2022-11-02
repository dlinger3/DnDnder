//"use strict";


var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();

//Disable the send button until connection is established.
document.getElementById("SendMessage").disabled = true;

connection.start().then(function () {
    document.getElementById("SendMessage").disabled = false;
    var listingID = document.getElementById("ListingID").value;
    var userID = document.getElementById("UserID").value;
    connection.invoke("AddUserToGroup", listingID, userID);
}).catch(function (err) {
    return console.error(err.toString());
});



////connection.on('ReceiveMessage', addMessageToChat);
//connection.on("ReceiveMessage", function (user, message) {
//    var li = document.createElement("li");
//    document.getElementById("messageList").appendChild(li);
//    // We can assign user-supplied strings to an element's textContent because it
//    // is not interpreted as markup. If you're assigning in any other way, you
//    // should be aware of possible script injection concerns.
//     li.textContent = `${message}`;

//    //let position = (user) ? " float-right" : "";
//    //thisMessage += `<div class="row chat_message` + position + `">
//    //                <b>` + user + `: </b>` + message +
//    //                `</div>`;
//    //$(".chat_body").html(thisMessage);

//});


    //Event Listener for the Send Message Button in a group chat
document.getElementById("SendMessage").addEventListener("click", function (event)
{
    event.preventDefault();
    var listingID = document.getElementById("ListingID").value;
    var userCharacter = document.getElementById("MessageSentBy").value;
    var message = document.getElementById("MessageInput").value;
    var thisMessage = "";
    connection.invoke("SendMessage", userCharacter, message, listingID).then(function () {
        //TODO: Remove this log
        document.getElementById("MessageInput").value = ""
        //Jquery HTTP POST to Message Table
        //TODO: LEFT OFF HERE --- Need to have data from here passed to the create function in chatcontroller to add to DB
        //$.post(PostURL.ActionControllerURL,
        //    {
        //        SentBy: userCharacter,
        //        MessageText: message,
        //        CampaignListingID: listingID
        //    },
        //    function (data, status) {
        //        let position = (data.SentBy) ? " float-right" : "";

        //        thisMessage += `<div class="row chat_message` + position + `">
        //                    <b>` + data.SentBy + `: </b>` + data.MessageText +
        //            `</div>`;
        //        $(".chat_body").html(thisMessage);
        //    });

        let newMessage = {
            SentBy: userCharacter,
            MessageText: message,
            CampaignListingID: listingID
        }

        $.ajax({
            type: "POST",
            url: PostURL.ActionControllerURL,
            data: JSON.stringify(newMessage),
            success: (newMessage) = {

            },
            dataType: 'json',
            contentType: 'application/json'
        });

    });
});

/*
 * 
 * 
 * LEFT OFF HERE.... Function is called and makes it to document.getElementById("chat_body") but is failing there.  
 * 
 * 
 * 
 * 
 */

//connection.on('ReceiveMessage', addMessageToChat);
connection.on("ReceiveMessage", function (userCharacter, message) {
    var ChatContainer = document.getElementById("ChatBody");
    var sentBy = userCharacter.toString();
    var newMessage = message.toString();
    var newMessageDiv = document.createElement("div");
    newMessageDiv.innerHTML = "<b>" + sentBy + ":" + "</b>&nbsp;" + newMessage;
    //newMessageDiv.appendChild(elementText);
    document.getElementById("ChatBody").appendChild(newMessageDiv);
    newMessageDiv.className = "row chat_message";
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you
    // should be aware of possible script injection concerns.
    //li.textContent = `${message}`;

    //let position = (user) ? " float-right" : "";
    //thisMessage += `<div class="row chat_message` + position + `">
    //                <b>` + user + `: </b>` + message +
    //                `</div>`;
    //$(".chat_body").html(newMessageDiv);

});

function getAllMessages() {
    let listingID = document.getElementById("ListingID").value;
    let thisMessage = "";
    $.ajax({
        type: "GET",
        url: GetURL.ActionControllerURL,
        dataType: "json",
        success: function (data, status) {

            for (var item in data) {
                let position = (data.SentBy) ? " float-right" : "";

                thisMessage += `<div class="row chat_message` + position + `">
                            <b>` + data.SentBy + `: </b>` + data.MessageText +
                    `</div>`;
                $(".chat_body").html(thisMessage);
            }
          
        },
        });
}

