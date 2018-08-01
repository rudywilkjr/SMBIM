$(document).ready(function() {
    $("[name=SelectedBeginDate]").datepicker();
    $("[name=SelectedEndDate]").datepicker();
});

function SearchTransferReport() {
    $.ajax({
        url: "/Report/Transfer",
        type: "POST",
        dataType: 'html',
        data: $("#report-transfer-form").serialize(),
        success: function (html) {
            $('#siteContentContainer').html(html);
        }
    });
}