﻿$(document).ready(function () {
    $('#dtExpenses').DataTable({
        dom: "<'row mb-3'<'col-sm-4'l><'col-sm-8 text-end'<'d-lg-flex justify-content-end'<'mb-3 mb-lg-0'f>B>>>t<'d-lg-flex align-items-center mt-3'<'me-auto mb-lg-0 mb-3'i><'mb-0'p>>",
        order: [[1, 'desc']],
        lengthMenu: [50, 100],
        responsive: true,
        buttons: [
            { extend: 'csv', className: 'btn btn-sm' },
        ]
    });
});