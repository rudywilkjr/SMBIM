function SearchProduct() {
    $.ajax({
        url: "Product/Lookup",
        type: "POST",
        dataType: 'html',
        data: $("#inventory-lookup-form").serialize(),
        success: function (html) {
            $('#inventory-lookup-view').html(html);
        }
    });
}

function SelectProduct(id)
{
    window.open("/Product/Item/" + id, "_self");
}

function CreateNewItem() {
    window.open("/Product/Item/New", "_self");
}