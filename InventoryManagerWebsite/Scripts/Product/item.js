function CancelProductItem() {
    window.open("/Product/Lookup", "_self");
}


function Move(inventoryId, locationId) {
    toastr["error"]("View Not Implemented.")
    //window.open("/Transfer/Move/" + locationId + "/" + inventoryId, "_self");
}

function Ship(inventoryId, locationId) {
    toastr["error"]("View Not Implemented.")
    //window.open("/Transfer/Ship/" + locationId + "/" + inventoryId, "_self");
}