$(window).scroll(function () {
    if ($(window).scrollTop() == $(document).height() - $(window).height()) {
        InfiniteScroll();
    }
});

function InfiniteScroll() {
    $("div#page-loader").show();

    ////send a query to server side to present new content
    //$.ajax({
    //    type: "POST",
    //    url: "Default.aspx/GetData",
    //    data: "{}",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (data) {
    //        if (data != "") {
    //            $('.divLoadData:last').after(data.d);
    //        }
    //        $('#divPostsLoader').empty();
    //    }
    //})
};