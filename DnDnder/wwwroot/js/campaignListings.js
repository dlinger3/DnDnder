
function confirmDeleteAlert(){
    let listingID = document.getElementById("ListingID").value;

    $.ajax({
        type: "POST",
        url: PostURL.ActionControllerURL,
        data: JSON.stringify(newMessage),
        success: (newMessage) = {

        },
        dataType: 'json',
        contentType: 'application/json'
    });
}