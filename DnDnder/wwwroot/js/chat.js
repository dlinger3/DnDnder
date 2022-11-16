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
    var userCharacter = GetSendersCharacter.SenderCharacter;
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

function rollDice()
 {
    var diceValue = [4, 6, 8, 10, 12, 20];
    var numOfDice = new Array(diceValue.length);
    var totalForEachDice = new Array(diceValue.length);
    var currDiceTotal = 0;

    for (var i = 0; i < diceValue.length; i++)
    {
        numOfDice[i] = Number(document.getElementById("diceValues").elements[i].value);
        document.getElementById("diceValues").elements[i].value = ""
    }

    for (var i = 0; i < diceValue.length; i++)
    {
        //+1 is due to ran.Next() being up to but not including higherBound
        var higherBound = diceValue[i];
        for (var j = 0; j < numOfDice[i]; j++)
        {
            currDiceTotal += Math.floor(Math.random() * higherBound) + 1;
        }
        totalForEachDice[i] = currDiceTotal;
        currDiceTotal = 0;
    }
    var overallSum = 0;
    for (var i = 0; i < totalForEachDice.length; i++) {
        overallSum += totalForEachDice[i];
    }
    document.getElementById("DiceTotal").innerHTML = overallSum.toString();
}


