function AddItem() {
    resetFormValidation();
    formValidation();
    var formIsInvalid;
    $(".invalid-tooltip").each(function () { if ($(this).css("display") !== "none") formIsInvalid = true; })
    if (formIsInvalid) {
        return;
    }

    var productId = $('select[name=SelectedProductId] option:selected')[0].value;
    var productName = $('select[name=SelectedProductId] option:selected')[0].text;
    var totalCost = parseFloat($('input[name=TotalCost]')[0].value).toFixed(2) === 'NaN' ? null : parseFloat($('input[name=TotalCost]')[0].value).toFixed(2);
    var quantity = $('input[name=Quantity]')[0].value;

    var itemCount = $(".dynamicRow").length;

    $("#ItemsGridBody").append("<tr class='dynamicRow'></tr>")
    $(".dynamicRow:last").append("<td class='dynamicCell syncStatusCell pending'></td>")
    $(".dynamicCell:last").append("<input type='hidden' name='InvoiceProducts[" + itemCount + "].IsSynced' value='false' />");
    $(".dynamicCell:last").append("&nbsp;");

    $(".dynamicRow:last").append("<td class='dynamicCell gridActionCell'></td>")
    $(".dynamicCell:last").append("<img style='height:20px' class='editIcon' src='../../Content/images/edit.png' onClick='editItem(this)'/>");
    $(".dynamicCell:last").append("<img style='height:20px' class='doneIcon hiddenIcon' src='../../Content/images/checkmark.png' onClick='completeItem(this)' />");
    $(".dynamicCell:last").append("<img style='height:20px' class='deleteIcon hiddenIcon' src='../../Content/images/delete.png' onClick='deleteItem(this)' />");

    $(".dynamicRow:last").append("<td class='dynamicCell'></td>")
    $(".dynamicCell:last").append("<input type='hidden' name='InvoiceProducts[" + itemCount + "].ProductId' value='" + productId + "' />");
    $(".dynamicCell:last").append("<input type='hidden' name='InvoiceProducts[" + itemCount + "].Name' value='" + productName + "' />");
    $(".dynamicCell:last").append("<label>" + productName + "</label>");

    $(".dynamicRow:last").append("<td class='dynamicCell'></td>")
    $(".dynamicCell:last").append("<input type='hidden' name='InvoiceProducts[" + itemCount + "].TotalCost' value='" + totalCost + "' />");
    $(".dynamicCell:last").append("<label>$" + totalCost + "</label>");

    $(".dynamicRow:last").append("<td class='dynamicCell'></td>")
    $(".dynamicCell:last").append("<input type='hidden' name='InvoiceProducts[" + itemCount + "].OrderedQuantity' value='" + quantity + "' />");
    $(".dynamicCell:last").append("<label>" + quantity + "</label>");

    $('#SelectedProductId').prop('selectedIndex', 0);
    $('input[name=TotalCost]')[0].value = null;
    $('input[name=Quantity]')[0].value = null;
}

function editItem(sourceElement) {
    $("button[type = 'submit']").prop('disabled', true);
    $(sourceElement).siblings().removeClass("hiddenIcon");
    $(sourceElement).addClass("hiddenIcon");

    var totalCostInput = $(sourceElement).parent().siblings().find("input[name*='TotalCost']");
    totalCostInput.siblings("label").remove();
    totalCostInput.attr("type", "number");
    totalCostInput.addClass("inline");
    totalCostInput.addClass("form-control");
    totalCostInput.parent().val = "";

    var quantityInput = $(sourceElement).parent().siblings().find("input[name*='OrderedQuantity']");
    quantityInput.siblings("label").remove();
    quantityInput.attr("type", "number");
    quantityInput.addClass("inline");
    quantityInput.addClass("form-control");

}

function completeItem(sourceElement) {
    $(sourceElement).parent().children().addClass("hiddenIcon");
    $(sourceElement).siblings(".editIcon").removeClass("hiddenIcon");

    var totalCostInput = $(sourceElement).parent().siblings().find("input[name*='TotalCost']");
    totalCostInput.parent().append("<label>$" + parseFloat(totalCostInput[0].value).toFixed(2) + "</label>");
    totalCostInput.attr("type", "hidden");

    var quantityInput = $(sourceElement).parent().siblings().find("input[name*='OrderedQuantity']");
    quantityInput.parent().append("<label>" + quantityInput[0].value + "</label>");
    quantityInput.attr("type", "hidden");

    $("button[type = 'submit']").prop('disabled', false);
    
}

function deleteItem(sourceElement) {
    $(sourceElement).parent().parent().remove();
    renumberNameSequence();

    $("button[type = 'submit']").prop('disabled', false);
}


function formValidation() {
    if (!$('select[name=SelectedProductId] option:selected')[0].value || $('select[name=SelectedProductId] option:selected')[0].value === 0) {
        $("#SelectedProductId").css("border-color", "red");
        $("#productInvalidTooltip").css("display", "block");
    }

    if (!parseFloat($('input[name=TotalCost]')[0].value) || parseFloat($('input[name=TotalCost]')[0].value) === 'NaN') {
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

function renumberNameSequence() {
    $(".invalid-tooltip").each(function () { if ($(this).css("display") !== "none") formIsInvalid = true; })
    var lastIndex = 0;
    var currentSet = 0;
    $("input[name*='InvoiceProducts']").each(
        function () {
            var thisIndex = $(this)[0].name.substring($(this)[0].name.indexOf("[") + 1, $(this)[0].name.indexOf("]"));
            currentSet = thisIndex;
            if (thisIndex - lastIndex > 1) {
                $(this).attr("name", $(this)[0].name.replace("[" + thisIndex + "]", "[" + (lastIndex + 1) + "]"));
            }
            if (currentSet !== thisIndex) { lastIndex = thisIndex; }
        });

}