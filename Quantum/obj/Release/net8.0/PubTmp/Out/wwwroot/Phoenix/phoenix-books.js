$(document).ready(function () {
    $("#BookSearch").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#T_Book tr.bookrow").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});

$(document).ready(function () {
    $(document).on("click", ".bookrow", PopulateEditModal);
});

function PopulateEditModal(obj) {

}