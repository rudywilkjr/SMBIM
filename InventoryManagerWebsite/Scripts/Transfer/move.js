function selectDestinationLocation(row, id) {
    $("#destinationTable tr").removeClass("selected-row");
    $(row).addClass("selected-row");
    $("[name=SelectedArrivingLocationId]").val(id);
    toggleTransferButtonState();
}

function selectSourceLocation(row, id) {
    $("#sourceTable tr").removeClass("selected-row");
    $(row).addClass("selected-row");
    $("[name=SelectedDepartureLocationId]").val(id);
    toggleTransferButtonState();
}

function toggleTransferButtonState() {
    if ($("[name=SelectedArrivingLocationId]").val() > 0) {
        if ($("[name=SelectedQuantity]").val() > 0) {
            $("#Transfer").removeClass("disabled");
            return;
        }
    }
    $("#Transfer").addClass("disabled");
}

function transferInventory() {
    $.ajax({
        url: "/Transfer/SendTransfer",
        type: "POST",
        dataType: 'html',
        data: $("#transfer-move-form").serialize(),
        success: function (html) {
            $('#siteContentContainer').html(html);
        }
    });
}