function selectDestinationLocation(row, id) {
    $("#destinationTable tr").removeClass("selected-row");
    $(row).addClass("selected-row");
    $("[name=SelectedDestinationLocationId]").val(id);
    toggleTransferButtonState();
}

function selectSourceLocation(row, id) {
    $("#sourceTable tr").removeClass("selected-row");
    $(row).addClass("selected-row");
    $("[name=SelectedInventoryId]").val(id);
    toggleTransferButtonState();
}

function toggleTransferButtonState() {
    if ($("[name=SelectedInventoryId]").val() > 0) {
        if ($("[name=SelectedQuantity]").val() > 0) {
            if ($("[name=SelectedDestinationLocationId]").val() > 0) {
                $("#Transfer").removeClass("disabled");
            }
            return;
        }
    }
    $("#Transfer").addClass("disabled");
}

function receiveInventory() {
    $.ajax({
        url: "/Transfer/ReceiveProduct",
        type: "POST",
        dataType: 'html',
        data: $("#transfer-receive-form").serialize(),
        success: function (html) {
            $('#siteContentContainer').html(html);
        }
    });
}