$("#add-comment").submit(function (event) {
    event.preventDefault();
    var post_url = $(this).attr("action");
    var form_data = $(this).serialize();
    var antiForgeryToken = $('#add-comment input[name=__RequestVerificationToken]').val();

    var formData = {
        postId: $('#add-comment input[name=postId]').val(),
        content: $('#add-comment input[name=content]').val()
    }

    $.ajax({
        url: post_url,
        contentType: "application/json",
        data: JSON.parse(formData),
        type: "POST",
        beforeSend: function() {
            console.log(JSON.stringify(formData));
            console.log(JSON.parse(formData));
        },
        headers: {
            'X-CSRF-TOKEN': antiForgeryToken
        },
        success: function () {
            console.log("good");
        },
        error: function(errors) {
            console.log(errors);
        }
    });
    
    ReloadComments($('#add-comment input[name=postId]').val());
});



$("a#like").click(function () {
    var postId = $(this).attr("post-like-id");
    $.ajax({
        url: `api/like?postId=${postId}`,
        success: function (data) {
            console.log("bad");
        },
        error: function (errors) {
            console.log(errors);
        },
        complete: function () {
            console.log("good");
            $(`a[post-like-Id=${postId}]`).removeClass("btn-outline-success").addClass("btn-outline-danger");
            $(`a[post-like-Id=${postId}] i`).removeClass("far").addClass("fas");
            $(`a[post-like-Id=${postId}]`).attr("id", "unlike");
        },
        type: "GET",
        dataType: "json"
    });
});

$("a#unlike").click(function () {
    var postId = $(this).attr("post-like-id");
    $.ajax({
        url: `api/unlike?postId=${postId}`,
        success: function (data) {
            console.log("bad");
        },
        error: function (errors) {
            console.log(errors);
        },
        complete: function () {
            console.log("good");
            $(`a[post-like-Id=${postId}]`).removeClass("btn-outline-danger").addClass("btn-outline-success");
            $(`a[post-like-Id=${postId}] i`).removeClass("fas").addClass("far");
            $(`a[post-like-Id=${postId}]`).attr("id", "like");
        },
        type: "GET",
        dataType: "json"
    });
});

$("a#comment-toggle").click(function () {
    var postId = $(this).attr("post-id");
    $(`div#comment-controls-${postId}`).show();
    $(`div.comments-body-${postId}`).text("");

    ReloadComments(postId);
});

function ReloadComments(postId) {
    $.ajax({
        url: `/api/getallcomments?postId=${postId}`,
        success: function (data) {
            $.each(data, function (item, index) {
                $(`div.comments-body-${postId}`)
                    .append($("<div/>").html(`<div class="row p-2 my-2 border">
                                                <div class="my-2">
                                                    <p class="m-0">${index.creatorUserName}</p>
                                                    <sub>${index.createdOn}</sub>
                                                </div>
                                                <p class="px-4">${index.content}</p>
                                            </div>`
                ));
            });
        },
        error: function (errors) {
            console.log(errors);
        },
        beforeSend: function () {
            $(`div#loader-${postId}`).show();
        },
        complete: function () {
            $(`div#loader-${postId}`).hide();
            $(`div.comments-body-${postId}`).show();
        },
        type: "GET",
        dataType: "json"
    });
}