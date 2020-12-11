$(document).ready(function () {
    console.log("JS Loaded!");
});

$("#add-comment").submit(function (event) {
    event.preventDefault(); //prevent default action 
    var post_url = $(this).attr("action"); //get form action url
    var form_data = $(this).serialize(); //Encode form elements for submission

    console.log(form_data);

    $.ajax({
        url: post_url,
        contentType: "application/jsonrequest; charset=utf-8",
        data: form_data,
        type: 'POST',
        dataType: 'json',
        succsess: function() {
            ReloadComments(form_data.postId);
        }
    });
});

$("a#like").click(function () {
    var postId = $(this).attr("post-like-id");
    $.ajax({
        url: "/api/Like?postId=" + postId,
        success: function (data) {
            console.log("bad");
        },
        error: function (errors) {
            console.log(errors);
        },
        complete: function () {
            console.log("good");
            $("a[post-like-Id=" + postId + "]").removeClass("btn-outline-success").addClass("btn-outline-danger");
            $("a[post-like-Id=" + postId + "] i").removeClass("far").addClass("fas");
            $("a[post-like-Id=" + postId + "]").attr("id", "unlike");
        },
        type: "GET",
        dataType: "json"
    });
});

$("a#unlike").click(function () {
    var postId = $(this).attr("post-like-id");
    $.ajax({
        url: "/api/UnLike?postId=" + postId,
        success: function (data) {
            console.log("bad");
        },
        error: function (errors) {
            console.log(errors);
        },
        complete: function () {
            console.log("good");
            $("a[post-like-Id=" + postId + "]").removeClass("btn-outline-danger").addClass("btn-outline-success");
            $("a[post-like-Id=" + postId + "] i").removeClass("fas").addClass("far");
            $("a[post-like-Id=" + postId + "]").attr("id", "like");
        },
        type: "GET",
        dataType: "json"
    });
});

$("a#comment-toggle").click(function () {
    var postId = $(this).attr("post-id");
    $("div#comment-controls-" + postId).show();
    $("div.comments-body-" + postId).text("");

    ReloadComments(postId);
});

function ReloadComments(postId) {
    $.ajax({
        url: "/api/post/comments?postId=" + postId,
        success: function (data) {
            $.each(data, function (item, index) {
                $("div.comments-body-" + postId)
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
            $("div.loader").show();
        },
        complete: function () {
            $("div.loader").hide();
            $("div.comments-body-" + postId).show();
        },
        type: "GET",
        dataType: "json"
    });
}