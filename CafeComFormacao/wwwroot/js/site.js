$(document).ready(function () {
    $(".opcao_nave-bar").click(function (event) {
        event.preventDefault();

        var url = $(this).attr("href");

        $.ajax({
            url: url,
            type: "GET",
            success: function (result) {
                $(".ajaxDiv").html(result);
            }
        });
    });
});