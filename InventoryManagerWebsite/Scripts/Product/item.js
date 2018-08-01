function SaveInventoryItem() {
    $.ajax({
        url: "/Product/Item/Update",
        type: "POST",
        dataType: 'html',
        data: $("#inventory-item-form").serialize(),
        success: function (html) {
            $('#siteContentContainer').html(html);
        }
    });
}

function CancelInventoryItem() {
    window.open("/Product/", "_self");
}


function Move(inventoryId, locationId) {
    window.open("/Transfer/Move/" + locationId + "/" + inventoryId, "_self");
}

function Ship(inventoryId, locationId) {
    window.open("/Transfer/Ship/" + locationId + "/" + inventoryId, "_self");
}