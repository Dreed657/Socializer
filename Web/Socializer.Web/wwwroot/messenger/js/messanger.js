var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    var senderId = document.getElementById("senderId").textContent;
    var groupName = "random5";

    connection.invoke("JoinChannel", groupName, senderId)
    .then(function() {
        console.log(`added to the group: ${groupName} senderId: ${senderId}`);
    })
    .catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("MemberJoined", function (senderId, message, group) {
    $("#messagesList").append(`<li class="border p-2 text-primary text-center">${message} in ${group}</li>`);
});

connection.on("ReceiveMessage", function (senderId, message, group) {
    var loggedUserId = document.getElementById("senderId").textContent;

    if (loggedUserId === senderId) {
        $("#messagesList").append(`<li class="border p-2 text-danger text-right">${message} in ${group}</li>`);
    } else {
        $("#messagesList").append(`<li class="border p-2 text-secondary text-left">${message} in ${group}</li>`);
    }
});


document.getElementById("sendButton").addEventListener("click", function(event) {
    var group = "random5";
    var senderId = document.getElementById("senderId").textContent;
    var message = document.getElementById("messageInput").value;

    connection.invoke("SendMessage", senderId, message, group)
    .then(function () {
        console.log(`message send: ${group} senderId: ${senderId} message: ${message}`);
    })
    .catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("messageInput").value = "";
    event.preventDefault();
});

