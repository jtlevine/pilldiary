$(document).ready(function () {
    $('a').on('click', function (e) {
        e.preventDefault();
        $('.side-bar-button').removeClass('selected');
        $(this).parent().addClass('selected');

        var pageRef = $(this).attr('href');
        callPage(pageRef);
        $('.navbar-collapse').collapse('hide');
    });
  
});

function callPage(page) {
    $.ajax({
        url: page,
        type: "GET",
        dataType: 'text',
        success: function (response) {
            console.log('the page was loaded', response);
            $('#content').html(response);
        },
        error: function (error) {
            console.log("the page was not loaded", error);
        }
    })
}