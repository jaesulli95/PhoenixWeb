$(document).ready(function () {
    $("#ProjectSearch").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#T_Project tr.projectrow").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});

$('tr.project-task-row').on('click', function (e) {
    var $name = $(this).find("td:nth-child(1)").text();
    var $description = $(this).find("td:nth-child(2)").text();
    var $type = $(this).find("td:nth-child(3)").text();
    var $timestamp = $(this).find("td:nth-child(4)").text();

});


//Entry Code

$('#datepicker').datepicker({
    autoclose: true
})