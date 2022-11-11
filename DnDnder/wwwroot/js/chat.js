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




//Event Listener for the Send Message Button in a group chat
document.getElementById("SendMessage").addEventListener("click", function (event)
{
    event.preventDefault();
    var listingID = document.getElementById("ListingID").value;
    var userCharacter = document.getElementById("MessageSentBy").value;
    var message = document.getElementById("MessageInput").value;
    var thisMessage = "";
    connection.invoke("SendMessage", userCharacter, message, listingID).then(function () {
        document.getElementById("MessageInput").value = ""

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

connection.on("ReceiveMessage", function (userCharacter, message) {
    var ChatContainer = document.getElementById("ChatBody");
    var sentBy = userCharacter.toString();
    var newMessage = message.toString();
    var newMessageDiv = document.createElement("div");
    newMessageDiv.innerHTML = "<b>" + sentBy + ":" + "</b>&nbsp;" + newMessage;
    document.getElementById("ChatBody").appendChild(newMessageDiv);
    newMessageDiv.className = "row chat_message";
});


