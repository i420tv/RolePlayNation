let bankSelectedOption = 0;
let vehicleArray = undefined;
let catalogSelectedOption = 0;
let messagesLoaded = false;
let timeout = undefined;

$(document).ready(function() {
	i18next.use(window.i18nextXHRBackend).init({
		backend: {
			loadPath: '../i18n/en.json'
		}
	}, function(err, t) {
		jqueryI18next.init(i18next, $);
		$(document).localize();
		messagesLoaded = true;
	});
    
	$('#colorpicker').farbtastic(function (color) {
		var colorMain = $('#color-main').prop('checked');
		mp.trigger('previewVehicleChangeColor', color, colorMain);
	});
});

function loginAccount() {
	// Validate the password
	let pass = document.getElementById('pass').value;
	mp.trigger('requestPlayerLogin', pass);
}

function registerAccount() {
	// Validate the password
	let pass = document.getElementById('pass').value;
	mp.trigger('createPlayerAccount', pass);
}

function showLoginError() {
	// Remove hidden class
	$('#error').removeClass('d-none');
}

function withdrawMoney() {
	bankSelectedOption = 1;
	$('#bank-menu').addClass('d-none');
	$('#bank-withdraw').removeClass('d-none');
	$('#bank-accept').removeClass('d-none');
	$('#bank-exit').html(i18next.t('general.cancel'));
	mp.trigger('updateBankAccountMoney');
}

function depositMoney() {
	bankSelectedOption = 2;
	$('#bank-menu').addClass('d-none');
	$('#bank-deposit').removeClass('d-none');
	$('#bank-accept').removeClass('d-none');
	$('#bank-exit').html(i18next.t('general.cancel'));
	mp.trigger('updateBankAccountMoney');
}

function transferMoney() {
	bankSelectedOption = 3;
	$('#bank-menu').addClass('d-none');
	$('#bank-transfer').removeClass('d-none');
	$('#bank-accept').removeClass('d-none');
	$('#bank-exit').html(i18next.t('general.cancel'));
	mp.trigger('updateBankAccountMoney');
}

function updateAccountMoney(money) {
	// Update the label with the money
	switch(bankSelectedOption) {
		case 1:
		$('#bank-withdraw-balance').html(money + '$');
		break;
		case 2:
		$('#bank-deposit-balance').html(money + '$');
		break;
		case 3:
		$('#bank-transfer-balance').html(money + '$');
		break;
	}
}

function showBalance() {
	bankSelectedOption = 4;
	$('#bank-menu').addClass('d-none');
	$('#bank-balance').removeClass('d-none');
	$('#bank-exit').html(i18next.t('general.cancel'));
	mp.trigger("loadPlayerBankBalance");
}

function showBankOperations(bankOperationsJson, playerName) {
	var bankOperationsArray = JSON.parse(bankOperationsJson);
	var tableBody = document.getElementById('bankBalanceTableBody');
	while (tableBody.firstChild) {
		tableBody.removeChild(tableBody.firstChild);
	}
	for (var i = 0; i < bankOperationsArray.length; i++) {
		// Create row elements
		var tableRow = document.createElement("TR");
		var dateColumn = document.createElement("TD");
		var operationColumn = document.createElement("TD");
		var involvedColumn = document.createElement("TD");
		var amountColumn = document.createElement("TD");

		// Add array data
		dateColumn.innerHTML = bankOperationsArray[i].day + " " + bankOperationsArray[i].time;
		operationColumn.innerHTML = bankOperationsArray[i].type;
		switch(bankOperationsArray[i].type) {
			case i18next.t('bank.transfer'):
			if(bankOperationsArray[i].source === playerName) {
				amountColumn.innerHTML = "-" + bankOperationsArray[i].amount + "$";
				involvedColumn.innerHTML = bankOperationsArray[i].receiver;
			} else {
				amountColumn.innerHTML = bankOperationsArray[i].amount + "$";
				involvedColumn.innerHTML = bankOperationsArray[i].source;
			}
			break;
			case i18next.t('bank.withdraw'):
			amountColumn.innerHTML = "-" + bankOperationsArray[i].amount + "$";
			break;
			default:
			amountColumn.innerHTML = bankOperationsArray[i].amount + "$";
			break;
		}

		// Add the columns to the row
		tableRow.appendChild(dateColumn);
		tableRow.appendChild(operationColumn);
		tableRow.appendChild(involvedColumn);
		tableRow.appendChild(amountColumn);

		// Insert the new row
		tableBody.appendChild(tableRow);
	}
}

function showOperationError(message) {
	switch (bankSelectedOption) {
		case 1:
		$('#bank-withdraw-error').html(message);
		$('#bank-withdraw-error').removeClass('d-none');
		break;
		case 2:
		$('#bank-deposit-error').html(message);
		$('#bank-deposit-error').removeClass('d-none');
		break;
		case 3:
		$('#bank-transfer-error').html(message);
		$('#bank-transfer-error').removeClass('d-none');
		break;
	}
}

function bankBack() {
	switch (bankSelectedOption) {
		case 1:
		$('#bank-withdraw-amount').val('0');
		$('#bank-withdraw').addClass('d-none');
		$('#bank-withdraw-error').addClass('d-none');
		$('#bank-menu').removeClass('d-none');
		break;
		case 2:
		$('#bank-deposit-amount').val('0');
		$('#bank-deposit').addClass('d-none');
		$('#bank-deposit-error').addClass('d-none');
		$('#bank-menu').removeClass('d-none');
		break;
		case 3:
		$('#bank-transfer-person').val('');
		$('#bank-transfer-amount').val('0');
		$('#bank-transfer').addClass('d-none');
		$('#bank-transfer-error').addClass('d-none');
		$('#bank-menu').removeClass('d-none');
		break;
		case 4:
		$('#bank-balance').addClass('d-none');
		$('#bank-menu').removeClass('d-none');
		break;
		default:
		mp.trigger('closeATM');
		break;
	}
	$('#bank-accept').addClass('d-none');
	$('#bank-exit').html(i18next.t('general.exit'));
	bankSelectedOption = 0;
}

function catalogBack() {
	switch (catalogSelectedOption) {
		case 1:
		$('#vehicle-container').removeClass('hidden');
		break;
	}
	mp.trigger('closeCatalog');
	$('#catalog-exit').html(i18next.t('general.exit'));
	catalogSelectedOption = 0;
}

function bankAccept() {
	var target = " ";
	var amount = 0;

	switch (bankSelectedOption) {
		case 1:
			amount = $('#bank-withdraw-amount').val();
			$('#bank-withdraw-amount').val(0);
			break;
		case 2:
			amount = $('#bank-deposit-amount').val();
			$('#bank-deposit-amount').val(0);
			break;
		case 3:
			target = $('#bank-transfer-person').val();
			amount = $('#bank-transfer-amount').val();

			$('#bank-transfer-person').val(0);
			$('#bank-transfer-amount').val(0);
			break;
	}
	
	mp.trigger('executeBankOperation', bankSelectedOption, amount, target);

}

function getVehicleList() {
	mp.trigger('getCarShopVehicleList');
}

function populateVehicleList(dealership, vehicleJSON) {
	if(messagesLoaded) {
		// Show the vehicle list
		switch (dealership) {
			case '4':
			$('#vehicle-list-cars').removeClass('hidden');
			break;
			case '1':
			$('#vehicle-list-motorbikes').removeClass('hidden');
			break;
		}

		vehicleArray = JSON.parse(vehicleJSON);
		var mainContainer = document.getElementById('vehicle-container');
		for (var i = 0; i < vehicleArray.length; i++) {
			var container = document.createElement('div');
			var image = document.createElement('div');
			var model = document.createElement('div');
			var speed = document.createElement('div');
			var price = document.createElement('div');
			image.id = vehicleArray[i].model;
			image.className = vehicleArray[i].model + ' center-block';
			image.style.width = '120px';
			image.style.height = '90px';
			model.textContent = i18next.t('cardealer.model') + vehicleArray[i].model;
			speed.textContent = i18next.t('cardealer.speed') + vehicleArray[i].speed + 'km/h';
			price.textContent = i18next.t('general.price') + vehicleArray[i].price + '$';
			container.className = 'col-lg-2';
			container.onclick = function () {
				mp.trigger('previewCarShopVehicle', this.firstChild.id);
			};
			container.appendChild(image);
			container.appendChild(model);
			container.appendChild(speed);
			container.appendChild(price);
			mainContainer.appendChild(container);

			clearTimeout(timeout);
		}
	} else {
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateVehicleList(dealership, vehicleJSON); }, 100);
	}
}

$("input[type='checkbox']").change(function () {
	var vehicleTypes = [];
	$("input[type='checkbox']").each(function () {
		if (this.checked) {
			let type = parseInt($(this).val());
			vehicleTypes.push(type);
		}
	});

	for (var i = 0; i < vehicleArray.length; i++) {
		// Get the vehicle's type as integer
		let type = parseInt(vehicleArray[i].type);

		if (vehicleTypes.length == 0 || vehicleTypes.indexOf(type) > -1) {
			$('#vehicle-container').children().eq(i).show();
		} else {
			$('#vehicle-container').children().eq(i).hide();
		}
	}
});

function checkVehiclePayable() {
	// Check if the player can pay the vehicle
	mp.trigger('checkVehiclePayable');
}

function showVehiclePurchaseButton() {
	// Enable purchase button
	$('#catalog-purchase').removeClass('hidden');
}

function rotatePreviewVehicle() {
	var rotation = parseFloat(document.getElementById('vehicle-slider').value);
	mp.trigger('rotatePreviewVehicle', rotation);
}

function goBackToCatalog() {
	mp.trigger('showCatalog');
}

function purchaseVehicle() {
	mp.trigger('purchaseVehicle');
}

function testVehicle() {
	mp.trigger('testVehicle');
}

function namePoliceControl() {
	var name = document.getElementById('name').value;
	mp.trigger('policeControlSelectedName', name);
	mp.trigger('destroyBrowser');
}

function populateContactData(number, name) {
	document.getElementById('number').value = number;
	document.getElementById('name').value = name;
}

function setContactData() {
	// Get the number and the name
	let number = document.getElementById('number').value;
	let name = document.getElementById('name').value;

	// Update the contact
	mp.trigger('setContactData', number, name);
}

function sendPhoneMessage() {
	// Get the message
	let message = document.getElementById('message').value;
	mp.trigger('sendPhoneMessage', message);
}

function cancelMessage() {
	// Cancel SMS sending
	mp.trigger('cancelPhoneMessage');
}

function getFirstTestQuestion() {
	// Get first question and answers
	mp.trigger('getNextTestQuestion');
}

function populateQuestionAnswers(question, answersJSON) {
	// Create an array from the JSON
	let answers = JSON.parse(answersJSON);

	// Create the question
	$('#license-question').text(question);

	// Delete the previous answers
	$('#license-answers').empty();

	for(let i = 0; i < answers.length; i++) {
		// Create the row elements
		let div = document.createElement("div");
		let label = document.createElement("label");
		let radio = document.createElement("input");

		// Add some properties
		radio.type = "radio";
		radio.name = "answer";
		radio.value = answers[i].id;

		// Add the text from the answers
		label.innerHTML = answers[i].text;

		// Insert the new elements
		div.appendChild(radio);
		div.appendChild(label);

		// Add the row to the list
		document.getElementById('license-answers').appendChild(div);
	}
}

function submitAnswer() {
	// Get the answer and submit it
	let answer = $('input[name=answer]:checked', '#testForm').val();
	mp.trigger('submitAnswer', answer);
}
