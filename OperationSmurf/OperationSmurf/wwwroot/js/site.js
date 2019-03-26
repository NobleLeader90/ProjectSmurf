// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$('#myModal').on('show.bs.modal', function (e) {
    var gradeId = $(e.relatedTarget).data('id');
    $(e.currentTarget).find('p[name="state"]').text(gradeId);
    var eventId = $(e.relatedTarget).data('id2');
    $(e.currentTarget).find('p[name="eventId"]').text(eventId);
    var lastName = $(e.relatedTarget).data('id3');
    $(e.currentTarget).find('p[name="lastName"]').text(lastName);
});

//$('#ZeroField').click(function () {
//    $('#changetext').html("testing");
//});