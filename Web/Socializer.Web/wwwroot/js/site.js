$(document).ready(function () {
    console.log("JS Loaded!");
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

    $.ajax({
        url: "/api/post/comments?postId=" + postId,
        success: function (data) {
            $.each(data, function (item, index) {
                $("div.comments-body-" + postId)
                    .append($("<div/>").addClass("row p-2 my-2 border")
                        .append($("<div/>").addClass("my-2")
                            .append($("<p/>").addClass("m-0").text(index.creatorUserName))
                            .append($("<sub />").text(index.createdOn))
                        ).append($("<p/>").addClass("px-4").text(index.content))
                    );
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
});