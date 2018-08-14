function CancelInventoryItem() {
    window.open("/Product/", "_self");
}


function Move(inventoryId, locationId) {
    window.open("/Transfer/Move/" + locationId + "/" + inventoryId, "_self");
}

function Ship(inventoryId, locationId) {
    window.open("/Transfer/Ship/" + locationId + "/" + inventoryId, "_self");
}