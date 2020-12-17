$(document).ready(function () {
    updateScrollToBottom();
    var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

    connection.start().then(function () {
        var senderId = $("#loggedInUser").text();
        var groupName = $("#groupName").text();

        connection.invoke("JoinChannel", groupName, senderId)
            .then(function () {
                console.log(`added to the group: ${groupName} senderId: ${senderId}`);
            })
            .catch(function (err) {
                return console.error(err.toString());
            });
    }).catch(function (err) {
        return console.error(err.toString());
    });

    //connection.on("MemberJoined", function (message, group) {
    //    $("#messageInbox").append(`<p class="border p-2 text-primary text-center">${message} in ${group}</p>`);
    //    updateScrollToBottom();
    //});

    connection.on("ReceiveMessage", function (senderId, message) {
        var loggedUserId = $("#loggedInUser").text();

        var currentTime = moment().local().format('L LT');

        if (loggedUserId === senderId) {
            $("#messageInbox").append(`<div class="outgoing_msg">
                                        <div class="sent_msg">
                                            <p>${message}</p>
                                            <span class="time_date"> ${currentTime} | ${loggedUserId}  Today</span>
                                        </div>
                                    </div>`);
        } else {
            $("#messageInbox").append(`<div class="incoming_msg">
                        <div class="incoming_msg_img"> <img src="https://ptetutorials.com/images/user-profile.png" alt="sunil"> </div>
                        <div class="received_msg">
                            <div class="received_withd_msg">
                                <p>${message}</p>
                                <span class="time_date"> ${currentTime} | Today ${senderId} </span>
                            </div>
                        </div>
                    </div>`);
        }
        updateScrollToBottom();
    });

    document.getElementById("msg_send").addEventListener("click", function (event) {
        var groupName = $("#groupName").text();
        var senderId = $("#loggedInUser").text();
        var message = $("#messageInput").val();

        connection.invoke("SendMessage", senderId, message, groupName)
            .then(function () {
                console.log(`message send: ${groupName} senderId: ${senderId} message: ${message}`);
            })
            .catch(function (err) {
                return console.error(err.toString());
            });

        document.getElementById("messageInput").value = "";
        event.preventDefault();
    });

    function updateScrollToBottom() {
        $("#messageInbox").scrollTop($("#messageInbox").height());
    }
});
