function onListItemClick(item) {

}

window.onerror = function(msg, url, linenumber) {
    alert('Error message: ' + msg + '\nURL: ' + url + '\nLine Number: ' + linenumber);
    return true;
}

function onExitButtonClick() {
    mp.trigger("CLOSE_BROWSER");

}

function selectionChanged(dropDownValueId, dropDownId) {
    const hiddenVal = document.getElementById(dropDownValueId).getAttribute('data-value');
    const val = document.getElementById(dropDownValueId).innerHTML;
    document.getElementById(dropDownId).setAttribute('data-value', hiddenVal);
    document.getElementById(dropDownId).innerHTML = val;
    show();
}

function show() {
    var selectbox = document.getElementById("options");
    if (selectbox.className == "hidden") {
        selectbox.setAttribute("class", "visible");
    } else {
        selectbox.setAttribute("class", "hidden");
    }
}

function createBrowser(browser) {

}

function btnClickEventByElement(ele) {
    var inputId = ele[0].Id;

    var returnEv = ele[0].RemoteEvent;
    var returnObjects = ele[0].ReturnValues;
    var returnValues = [];
    returnObjects.forEach(returner => {

        var element = document.getElementById(returner.Id);

        var eleValue = "";
        switch (returner.ElementType) {
            case 3: //Checkbox
                eleValue = element.checked;
                break;
            case 1: // TextBox
                eleValue = element.value; //element.value;
                break;
            case 4: // Dropdown
                eleValue = element.innerHTML;
                returner.HiddenValue = element.getAttribute('data-value');
                break;
            default:
                eleValue = element.value;
                break;
        }

        returnValues.push({ Id: inputId, Value: eleValue, HiddenValue: returner.HiddenValue, Type: returner.ElementType });
    });

    //alert(JSON.stringify(returnValues));
    mp.trigger("BROWSER_ELEMENT_CLICKED_EVENT", inputId, returnEv, JSON.stringify(returnValues));
}