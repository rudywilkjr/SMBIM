function SearchProductReport() {
    $.ajax({
        url: "/Report/Product",
        type: "POST",
        dataType: 'html',
        data: $("#report-inventory-form").serialize(),
        success: function (html) {
            $('#siteContentContainer').html(html);
        }
    });
}