$(document).ready(function() {
    $("a#comment-toggle").click(function () {
        var postId = $(this).attr("comment-id");
        $("div#comment-controls-" + postId).show();
        $("div.comments-body-" + postId).text("");

        $.ajax({
            url: "/api/post/comments?postId=" + postId,
            success: function(data) {
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
            error: function(errors) {
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
});