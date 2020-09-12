let extendedData = false;

$(document).ready(function () {
	i18next.use(window.i18nextXHRBackend).init({
		backend: {
			loadPath: '../i18n/en.json'
		}
	}, function (err, t) {
		jqueryI18next.init(i18next, $, {
			optionsAttr: 'i18n-options',
			useOptionsAttr: true,
			parseDefaultValueFromContent: true
		});

		$(document).localize();
	});
});

function initializePlayerData(name, age, sex, money, bank, job, rank, extended) {
	// Check if is asking for extended data or not	
	extendedData = (extended.toLowerCase() === 'true');

	if (extendedData) {
		// Show the extended menu option
		document.getElementById('extended').classList.remove('invisible');
	} else {
		// Show the logo
		document.getElementById('logo').classList.remove('hidden');
	}

	// Populate the basic data
	populateBasicData(name, age, sex, money, bank, job, rank);
}

function populateBasicData(name, age, sex, money, bank, job, rank) {
	// Hide all the panels
	hidePanels();

	// Fill the data
	document.getElementById('name').innerHTML = name;
	document.getElementById('age').innerHTML = age;
	document.getElementById('sex').innerHTML = sex;
	document.getElementById('bank').innerHTML = bank;
	document.getElementById('job').innerHTML = job;

	if(extendedData) {
		// Show the hand money
		let moneyNode = document.getElementById('money');
		moneyNode.previousElementSibling.classList.remove('hidden');
		moneyNode.classList.remove('hidden');
		moneyNode.innerHTML = money;
	}

	if(rank !== undefined && rank.length > 0) {
		// Show the player's rank
		let rankNode = document.getElementById('rank');
		rankNode.previousElementSibling.classList.remove('hidden');
		rankNode.classList.remove('hidden');
		rankNode.innerHTML = rank;
	}
	
	// Show the panel
	document.getElementById('basicData').classList.remove('hidden');
}

function populatePropertiesData(propertiesJson, rented) {
	// Hide all the panels
	hidePanels();

	// Get the properties node and delete the children
	let node = document.getElementById('property-list');

	// Get the properties
	let properties = JSON.parse(propertiesJson);

	while(node.firstChild) {
		// Remove each child
		node.removeChild(node.firstChild);
	}

	if(properties === null || properties === undefined || properties.length === 0) {
		// Show the message
		let child = document.createElement('li');
		child.textContent = i18next.t('data.no-properties');
		node.appendChild(child);
	} else {
		for(let i = 0; i < properties.length; i++) {
			// Get the name of the property
			let child = document.createElement('li');
			child.textContent = properties[i];
			node.appendChild(child);
		}
	}

	document.getElementById('rented-property').textContent = rented.length > 0 ? rented : i18next.t('data.no-properties');

	// Show the panel
	document.getElementById('propertiesData').classList.remove('hidden');
}

function populateVehiclesData(ownedVehiclesJson, lentVehiclesJson) {
	// Hide all the panels
	hidePanels();

	// Get the vehicles nodes and delete the children
	let ownedNode = document.getElementById('owned-vehicles');
	let lentNode = document.getElementById('lent-vehicles');

	// Get the vehicles
	let ownedVehicles = JSON.parse(ownedVehiclesJson);
	let lentVehicles = JSON.parse(lentVehiclesJson);

	while(ownedNode.firstChild) {
		// Remove each child
		ownedNode.removeChild(ownedNode.firstChild);
	}

	while(lentNode.firstChild) {
		// Remove each child
		lentNode.removeChild(lentNode.firstChild);
	}

	if(ownedVehicles === null || ownedVehicles === undefined || ownedVehicles.length === 0) {
		// Show the message
		let child = document.createElement('li');
		child.textContent = i18next.t('data.no-vehicles');
		ownedNode.appendChild(child);
	} else {
		for(let i = 0; i < ownedVehicles.length; i++) {
			// Get the name of the vehicle
			let child = document.createElement('li');
			child.textContent = ownedVehicles[i];
			ownedNode.appendChild(child);
		}
	}

	if(extendedData) {
		if(lentVehicles === null || lentVehicles === undefined || lentVehicles.length === 0) {
			// Show the message
			let child = document.createElement('li');
			child.textContent = i18next.t('data.no-vehicles');
			lentNode.appendChild(child);
		} else {
			for(let i = 0; i < lentVehicles.length; i++) {
				// Get the name of the vehicle
				let child = document.createElement('li');
				child.textContent = lentVehicles[i];
				lentNode.appendChild(child);
			}
		}

		// Show the lent vehicles
		lentNode.classList.remove('hidden');
	} else {
		// Hide the lent vehicles
		lentNode.classList.add('hidden');
	}

	// Show the panel
	document.getElementById('vehiclesData').classList.remove('hidden');
}

function showPanel(panel) {
	// Get the data from the server
	let event = 'retrieve' + panel.charAt(0).toUpperCase() + panel.slice(1);
	mp.trigger('retrievePanelData', event);
}

function hidePanels() {
	let panels = document.getElementById('data-container').children;

	for(let i = 0; i < panels.length; i++) {
		if(panels[i].nodeName === 'DIV' && !panels[i].classList.contains('hidden')) {
			// Hide the panel
			panels[i].classList.add('hidden');
		}
	}
}