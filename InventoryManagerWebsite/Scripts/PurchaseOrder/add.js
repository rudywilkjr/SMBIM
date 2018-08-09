function AddItem() {
    resetFormValidation();
    formValidation();
    var formIsInvalid;
    $(".invalid-tooltip").each(function () { if ($(this).css("display") != "none") formIsInvalid = true; })
    if (formIsInvalid) {
        return;
    }

    var productId = $('select[name=SelectedProductId] option:selected')[0].value;
    var productName = $('select[name=SelectedProductId] option:selected')[0].text;
    var totalCost = parseFloat($('input[name=TotalCost]')[0].value).toFixed(2) == 'NaN' ? null : parseFloat($('input[name=TotalCost]')[0].value).toFixed(2);
    var quantity = $('input[name=Quantity]')[0].value;

    var itemCount = $(".dynamicRow").length;

    $("#ItemsGridBody").append("<tr class='dynamicRow'></tr>")
    $(".dynamicRow:last").append("<td class='dynamicCell'></td>")
    $(".dynamicCell:last").append("<input type='hidden' name='InvoiceProducts[" + itemCount + "].Id' value='" + productId + "' />");
    $(".dynamicCell:last").append(productName);

    $(".dynamicRow:last").append("<td class='dynamicCell'></td>")
    $(".dynamicCell:last").append("<input type='hidden' name='InvoiceProducts[" + itemCount + "].TotalCost' value='" + totalCost + "' />");
    $(".dynamicCell:last").append("$" + totalCost);

    $(".dynamicRow:last").append("<td class='dynamicCell'></td>")
    $(".dynamicCell:last").append("<input type='hidden' name='InvoiceProducts[" + itemCount + "].Quantity' value='" + quantity + "' />");
    $(".dynamicCell:last").append(quantity);

    $(".dynamicRow:last").append("<td class='dynamicCell'></td>")

    $('#SelectedProductId').prop('selectedIndex', 0);
    $('input[name=TotalCost]')[0].value = null;
    $('input[name=Quantity]')[0].value = null;
}

function formValidation() {
    if (!$('select[name=SelectedProductId] option:selected')[0].value || $('select[name=SelectedProductId] option:selected')[0].value === 0) {
        $("#SelectedProductId").css("border-color", "red");
        $("#productInvalidTooltip").css("display", "block");
    }

    if (!parseFloat($('input[name=TotalCost]')[0].value) || parseFloat($('input[name=TotalCost]')[0].value) == 'NaN') {
        $("#TotalCost").css("border-color", "red");
        $("#costInvalidTooltip").css("display", "block");
    }

    if (!$('input[name=Quantity]')[0].value || $('input[name=Quantity]')[0].value <= 0) {
        $("#Quantity").css("border-color", "red");
        $("#quantityInvalidTooltip").css("display", "block");
    }
}

function resetFormValidation() {
    $("#SelectedProductId").prop('style').removeProperty('border-color');
    $("#TotalCost").prop('style').removeProperty('border-color');
    $("#Quantity").prop('style').removeProperty('border-color');
    $(".invalid-tooltip").each(function () {
        $(this).prop('style').removeProperty('display');
    });
}