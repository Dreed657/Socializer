$(document).ready(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

    connection.start().then(function () {
        var senderId = document.getElementById("senderId").textContent;
        var groupName = "random5";

        connection.invoke("JoinChannel", groupName, senderId)
            .then(function () {
                console.log(`added to the group: ${groupName} senderId: ${senderId}`);
                updateScrollToBottom();
            })
            .catch(function (err) {
                return console.error(err.toString());
            });
    }).catch(function (err) {
        return console.error(err.toString());
    });

    connection.on("MemberJoined", function (senderId, message, group) {
        $("#messageInbox").append(`<p class="border p-2 text-primary text-center">${message} in ${group}</p>`);
        updateScrollToBottom();
    });

    connection.on("ReceiveMessage", function (senderId, message, group) {
        var loggedUserId = document.getElementById("senderId").textContent;

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

    function updateScrollToBottom() {
        $("#messageInbox").scrollTop($("#messageInbox").height());
    }
});
