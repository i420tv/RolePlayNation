const PRICE_PIZZA = 20;
const PRICE_HAMBURGER = 10;
const PRICE_SANDWICH = 5;

let tunningComponents = [];
let tattooZones = [];
let clothesTypes = [];
let selectedOptions = [];
let purchasedAmount = 1;
let multiplier = 0.0;
let selected = undefined;
let drawable = undefined;
let messagesLoaded = false;
let timeout = undefined;
let police = false;

$(document).ready(function() {
	i18next.use(window.i18nextXHRBackend).init({
		backend: {
			loadPath: '../i18n/en.json'
		}
	}, function(err, t) {
        jqueryI18next.init(i18next, $);
		messagesLoaded = true;
	});
});

function populateBusinessItems(businessItemsJson, businessName, multiplier) {
	// Check if the messages are loaded
	if(messagesLoaded) {
		// Initialize the values
		purchasedAmount = 1;
		selected = undefined;

		// Get items to show
		let businessItemsArray = JSON.parse(businessItemsJson);
		let header = document.getElementById('header');
		let content = document.getElementById('content');
		let options = document.getElementById('options');
		
		// Show business name
		header.textContent = businessName;
		
		for(let i = 0; i < businessItemsArray.length; i++) {
			let item = businessItemsArray[i];
			
			let itemContainer = document.createElement('div');
			let imageContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let purchaseContainer = document.createElement('div');
			let priceContainer = document.createElement('div');
			let itemAmountContainer = document.createElement('div');
			let amountTextContainer = document.createElement('div');
			let addSubstractContainer = document.createElement('div');
			let itemImage = document.createElement('img');
			let itemDescription = document.createElement('span');
			let itemPrice = document.createElement('span');
			let itemAmount = document.createElement('span');
			let itemAdd = document.createElement('span');
			let itemSubstract = document.createElement('span');
			
			itemContainer.classList.add('item-row');
			imageContainer.classList.add('item-image');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			purchaseContainer.classList.add('item-purchase');
			priceContainer.classList.add('item-price-container');
			itemAmountContainer.classList.add('item-amount-container', 'hidden');
			amountTextContainer.classList.add('item-amount-desc-container');
			addSubstractContainer.classList.add('item-add-substract-container');
			itemDescription.classList.add('item-description');
			itemPrice.classList.add('item-price');
			itemAmount.classList.add('item-amount-description');
			itemAdd.classList.add('item-adder');
			itemSubstract.classList.add('item-substract', 'hidden');
			
			itemImage.src = '../img/inventory/' + item.hash + '.png';
			itemDescription.textContent = item.description;
			itemPrice.innerHTML = '<b>' + i18next.t('general.unit-price') + '</b>' + Math.round(item.products * parseFloat(multiplier)) + '$';
			itemAmount.innerHTML = '<b>' + i18next.t('general.amount') + '</b>' + purchasedAmount;
			itemAdd.textContent = '+';
			itemSubstract.textContent = '-';
			
			itemContainer.onclick = (function() {
				// Check if the item is not selected
				if(selected !== i) {
					// Check if there was any item selected
					if(selected != undefined) {
						let previousSelected = document.getElementsByClassName('item-row')[selected];
						let previousAmountNode = findFirstChildByClass(previousSelected, 'item-amount-container');
						previousSelected.classList.remove('active-item');
						previousAmountNode.classList.add('hidden');
					}
					
					// Select the item
					let currentSelected = document.getElementsByClassName('item-row')[i];
					let currentAmountNode = findFirstChildByClass(currentSelected, 'item-amount-container');
					currentSelected.classList.add('active-item');
					currentAmountNode.classList.remove('hidden');
					
					// Store the item and initialize the amount
					purchasedAmount = 1;
					selected = i;
					
					// Update the element's text
					itemAmount.innerHTML = '<b>' + i18next.t('general.amount') + '</b>' + purchasedAmount;
					document.getElementsByClassName('item-adder')[selected].classList.remove('hidden');
					document.getElementsByClassName('item-substract')[selected].classList.add('hidden');
				}
			});
			
			itemAdd.onclick = (function() {
				// Add one unit
				purchasedAmount++;
				
				let adderButton = document.getElementsByClassName('item-adder')[selected];
				let substractButton = document.getElementsByClassName('item-substract')[selected];
				
				if(purchasedAmount == 10) {
					// Maximum amount reached
					adderButton.classList.add('hidden');
				} else if(substractButton.classList.contains('hidden') === true) {
					// Show the button
					substractButton.classList.remove('hidden');
				}
				
				// Update the amount
				let amountSpan = document.getElementsByClassName('item-amount-description')[selected];
				amountSpan.innerHTML = '<b>' + i18next.t('general.amount') + '</b>' + purchasedAmount;
			});
			
			itemSubstract.onclick = (function() {
				// Substract one unit
				purchasedAmount--;
				
				let adderButton = document.getElementsByClassName('item-adder')[selected];
				let substractButton = document.getElementsByClassName('item-substract')[selected];
				
				if(purchasedAmount == 1) {
					// Minimum amount reached
					substractButton.classList.add('hidden');
				} else if(adderButton.classList.contains('hidden') === true) {
					// Show the button
					adderButton.classList.remove('hidden');
				}
				
				// Update the amount
				let amountSpan = document.getElementsByClassName('item-amount-description')[selected];
				amountSpan.innerHTML = '<b>' + i18next.t('general.amount') + '</b>' + purchasedAmount;
			});
			
			content.appendChild(itemContainer);
			itemContainer.appendChild(imageContainer);
			itemContainer.appendChild(infoContainer);
			imageContainer.appendChild(itemImage);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
			infoContainer.appendChild(purchaseContainer);
			purchaseContainer.appendChild(priceContainer);
			priceContainer.appendChild(itemPrice);
			purchaseContainer.appendChild(itemAmountContainer);
			itemAmountContainer.appendChild(amountTextContainer);
			amountTextContainer.appendChild(itemAmount);
			itemAmountContainer.appendChild(addSubstractContainer);
			addSubstractContainer.appendChild(itemAdd);
			addSubstractContainer.appendChild(itemSubstract);
		}
		
		// Add option buttons
		let purchaseButton = document.createElement('div');
		let cancelButton = document.createElement('div');
		
		// Add classes for the buttons
		purchaseButton.classList.add('double-button', 'accept-button');
		cancelButton.classList.add('double-button', 'cancel-button');
		
		// Add text for the buttons
		purchaseButton.textContent = i18next.t('general.purchase');
		cancelButton.textContent = i18next.t('general.exit');
		
		purchaseButton.onclick = (function() {
			// Check if the user purchased anything
			if(selected != undefined) {
				mp.trigger('purchaseItem', selected, purchasedAmount);
			}
		});
		
		cancelButton.onclick = (function() {
			// Close the purchase window
			mp.trigger('destroyBrowser');
		});
		
		options.appendChild(purchaseButton);
		options.appendChild(cancelButton);
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateBusinessItems(businessItemsJson, businessName, multiplier); }, 100);
	}
}

function populateTunningMenu(tunningComponentsJSON) {
	if(messagesLoaded) {
		// Add the title to the menu
		let header = document.getElementById('header');
		header.textContent = i18next.t('tunning.title');
		
		// Get the components list
		tunningComponents = JSON.parse(tunningComponentsJSON);
		
		// Show the main menu
		populateTunningHome();
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateTunningMenu(tunningComponentsJSON); }, 100);
	}
}
function populateTunningMenuShowcase(tunningComponentsJSON) {
	if (messagesLoaded) {
		// Add the title to the menu
		let header = document.getElementById('header');
		header.textContent = i18next.t('tunning.title');

		// Get the components list
		tunningComponents = JSON.parse(tunningComponentsJSON);

		// Show the main menu
		populateTunningHomeShowcase();

		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function () { populateTunningMenuShowcase(tunningComponentsJSON); }, 100);
	}
}


function populateTunningHome() {
	if(messagesLoaded) {
		let content = document.getElementById('content');
		let options = document.getElementById('options');
		
		// Initialize the options
		selected = undefined;
		drawable = undefined;
		
		while(content.firstChild) {
			content.removeChild(content.firstChild);
		}
		
		while(options.firstChild) {
			options.removeChild(options.firstChild);
		}
		
		for(let i = 0; i < tunningComponents.length; i++) {
			let group = tunningComponents[i];
			
			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let itemDescription = document.createElement('span');
			
			// Get the classes for each element
			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			itemDescription.classList.add('item-description');
			
			// Add the description
			itemDescription.textContent = i18next.t(group.desc);
			
			itemContainer.onclick = (function() {
				selected = i;
				
				// Show components from this type
				populateTunningComponents();
			});
			
			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
		}
		
		// Create the exit button
		let exitButton = document.createElement('div');
		
		// Add the classes and text for the button
		exitButton.classList.add('single-button', 'cancel-button');
		exitButton.textContent = i18next.t('general.exit');
		
		exitButton.onclick = (function() {
			// Close the menu
			mp.trigger('destroyBrowser');
		});
		
		options.appendChild(exitButton);
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateTunningHome(); }, 100);
	}
}
function populateTunningHomeShowcase() {
	if (messagesLoaded) {
		let content = document.getElementById('content');
		let options = document.getElementById('options');

		// Initialize the options
		selected = undefined;
		drawable = undefined;

		while (content.firstChild) {
			content.removeChild(content.firstChild);
		}

		while (options.firstChild) {
			options.removeChild(options.firstChild);
		}

		for (let i = 0; i < tunningComponents.length; i++) {
			let group = tunningComponents[i];

			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let itemDescription = document.createElement('span');

			// Get the classes for each element
			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			itemDescription.classList.add('item-description');

			// Add the description
			itemDescription.textContent = i18next.t(group.desc);

			itemContainer.onclick = (function () {
				selected = i;

				// Show components from this type
				populateTunningComponentsShowcase();
			});

			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
		}

		// Create the exit button
		let exitButton = document.createElement('div');

		// Add the classes and text for the button
		exitButton.classList.add('single-button', 'cancel-button');
		exitButton.textContent = i18next.t('general.exit');

		exitButton.onclick = (function () {
			// Close the menu
			mp.trigger('destroyBrowser');
		});

		options.appendChild(exitButton);

		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function () { populateTunningHomeShowcase(); }, 100);
	}
}

function populateTunningComponents() {
	if(messagesLoaded) {
		let content = document.getElementById('content');
		let options = document.getElementById('options');

		while(content.firstChild) {
			content.removeChild(content.firstChild);
		}

		while(options.firstChild) {
			options.removeChild(options.firstChild);
		}

		for(let i = 0; i < tunningComponents[selected].components.length; i++) {
			let component = tunningComponents[selected].components[i];
			
			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let purchaseContainer = document.createElement('div');
			let priceContainer = document.createElement('div');
			let itemDescription = document.createElement('span');
			let itemPrice = document.createElement('span');
			
			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			purchaseContainer.classList.add('item-purchase');
			priceContainer.classList.add('item-price-container');
			itemDescription.classList.add('item-description');
			itemPrice.classList.add('item-price');
			
			// Add the description and price
			let description = component.desc.split(' ');
			itemDescription.textContent = i18next.t(description[0]) + ' ' + description[1];
			itemPrice.innerHTML = '<b>' + i18next.t('general.unit-price') + '</b>' + tunningComponents[selected].products + '$';
			
			itemContainer.onclick = (function() {
				if(drawable !== i) {
					// Check if there was any item selected
					if(drawable != undefined) {
						let previousSelected = document.getElementsByClassName('item-row')[drawable];
						previousSelected.classList.remove('active-item');
					}
					
					let currentSelected = document.getElementsByClassName('item-row')[i];
					currentSelected.classList.add('active-item');
					
					drawable = i;
					
					// Update the vehicle's tunning
					mp.trigger('addVehicleComponent', tunningComponents[selected].slot, drawable);
				}
			});
			
			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
			infoContainer.appendChild(purchaseContainer);
			purchaseContainer.appendChild(priceContainer);
			priceContainer.appendChild(itemPrice);
		}

		// Add the buttons
		let purchaseButton = document.createElement('div');
		let cancelButton = document.createElement('div');

		purchaseButton.classList.add('double-button', 'accept-button');
		cancelButton.classList.add('double-button', 'cancel-button');

		purchaseButton.textContent = i18next.t('general.purchase');
		cancelButton.textContent = i18next.t('general.back');;

		purchaseButton.onclick = (function() {
			if(drawable !== undefined) {
				mp.trigger('confirmVehicleModification', tunningComponents[selected].slot, drawable);
			}
		});

		cancelButton.onclick = (function() {
			// Remove the modified part
			mp.trigger('cancelVehicleModification');

			// Back to the home menu
			populateTunningHome();
		});

		options.appendChild(purchaseButton);
		options.appendChild(cancelButton);
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateTunningComponents(); }, 100);
	}
}
function populateTunningComponentsShowcase() {
	if (messagesLoaded) {
		let content = document.getElementById('content');
		let options = document.getElementById('options');

		while (content.firstChild) {
			content.removeChild(content.firstChild);
		}

		while (options.firstChild) {
			options.removeChild(options.firstChild);
		}

		for (let i = 0; i < tunningComponents[selected].components.length; i++) {
			let component = tunningComponents[selected].components[i];

			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let purchaseContainer = document.createElement('div');
			let priceContainer = document.createElement('div');
			let itemDescription = document.createElement('span');
			let itemPrice = document.createElement('span');

			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			purchaseContainer.classList.add('item-purchase');
			priceContainer.classList.add('item-price-container');
			itemDescription.classList.add('item-description');
			itemPrice.classList.add('item-price');

			// Add the description and price
			let description = component.desc.split(' ');
			itemDescription.textContent = i18next.t(description[0]) + ' ' + description[1];
			itemPrice.innerHTML = '<b>' + i18next.t('general.unit-price') + '</b>' + tunningComponents[selected].products + '$';

			itemContainer.onclick = (function () {
				if (drawable !== i) {
					// Check if there was any item selected
					if (drawable != undefined) {
						let previousSelected = document.getElementsByClassName('item-row')[drawable];
						previousSelected.classList.remove('active-item');
					}

					let currentSelected = document.getElementsByClassName('item-row')[i];
					currentSelected.classList.add('active-item');

					drawable = i;

					// Update the vehicle's tunning
					mp.trigger('addVehicleComponent', tunningComponents[selected].slot, drawable);
				}
			});

			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
			infoContainer.appendChild(purchaseContainer);
			purchaseContainer.appendChild(priceContainer);
			priceContainer.appendChild(itemPrice);
		}

		// Add the buttons
		// Create the exit button
		let exitButton = document.createElement('div');

		// Add the classes and text for the button
		exitButton.classList.add('single-button', 'cancel-button');
		exitButton.textContent = i18next.t('general.cancel');

		exitButton.onclick = (function () {
			// Close the menu
			mp.trigger('cancelVehicleModification');
			
			// Back to the home menu
			populateTunningHomeShowcase();
		});

		options.appendChild(exitButton);
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function () { populateTunningComponentsShowcase(); }, 100);
	}
}
function populateBurgerFlopperOrders(ordersJson, distancesJson) {
	if (messagesLoaded) {
		// Get the orders
		let burgerfloppperOrders = JSON.parse(ordersJson);
		let distances = JSON.parse(distancesJson);
		let header = document.getElementById('header');
		let content = document.getElementById('content');
		let options = document.getElementById('options');

		// Add the title to the menu
		header.textContent = i18next.t('fastfood.title');

		for (let i = 0; i < fastfoodOrders.length; i++) {
			let order = fastfoodOrders[i];

			// Calculate order's price
			let amount = order.pizzas * PRICE_PIZZA + order.hamburgers * PRICE_HAMBURGER + order.sandwitches * PRICE_SANDWICH;

			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let purchaseContainer = document.createElement('div');
			let priceContainer = document.createElement('div');
			let itemAmountContainer = document.createElement('div');
			let amountTextContainer = document.createElement('div');
			let itemDescription = document.createElement('span');
			let itemPrice = document.createElement('span');
			let itemAmount = document.createElement('span');

			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			purchaseContainer.classList.add('item-purchase');
			priceContainer.classList.add('item-price-container');
			itemAmountContainer.classList.add('item-amount-container');
			amountTextContainer.classList.add('item-amount-desc-container');
			itemDescription.classList.add('item-description');
			itemPrice.classList.add('item-price');
			itemAmount.classList.add('item-amount-description');

			// Add the text for each element
			itemDescription.textContent = i18next.t('fastfood.order-number', { value: order.id });
			itemPrice.innerHTML = '<b>' + i18next.t('fastfood.order') + '</b>' + amount + '$';
			itemAmount.innerHTML = '<b>' + i18next.t('fastfood.distance') + '</b>' + parseFloat(distances[i] / 1000).toFixed(2) + 'km';

			itemContainer.onclick = (function () {
				if (selected !== i) {
					// Check if there was any item selected
					if (selected != undefined) {
						let previousSelected = document.getElementsByClassName('item-row')[selected];
						previousSelected.classList.remove('active-item');
					}

					// Select the clicked element
					let currentSelected = document.getElementsByClassName('item-row')[i];
					currentSelected.classList.add('active-item');
					selected = i;
				}
			});

			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
			infoContainer.appendChild(purchaseContainer);
			purchaseContainer.appendChild(priceContainer);
			priceContainer.appendChild(itemPrice);
			purchaseContainer.appendChild(itemAmountContainer);
			itemAmountContainer.appendChild(amountTextContainer);
			amountTextContainer.appendChild(itemAmount);
		}

		let deliverButton = document.createElement('div');
		let cancelButton = document.createElement('div');

		deliverButton.classList.add('double-button', 'accept-button');
		cancelButton.classList.add('double-button', 'cancel-button');

		deliverButton.textContent = i18next.t('fastfood.deliver');
		cancelButton.textContent = i18next.t('general.exit');

		deliverButton.onclick = (function () {
			// Deliver the selected order
			if (selected != undefined) {
				mp.trigger('deliverFastfoodOrder', burgerflopperOrders[selected].id);
			}
		});

		cancelButton.onclick = (function () {
			// Close the menu
			mp.trigger('destroyBrowser');
		});

		options.appendChild(deliverButton);
		options.appendChild(cancelButton);

		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function () { populateFastfoodOrders(ordersJson, distancesJson); }, 100);
	}
}
function populateFastfoodOrders(ordersJson, distancesJson) {
	if(messagesLoaded) {
		// Get the orders
		let fastfoodOrders = JSON.parse(ordersJson);
		let distances = JSON.parse(distancesJson);
		let header = document.getElementById('header');
		let content = document.getElementById('content');
		let options = document.getElementById('options');
		
		// Add the title to the menu
		header.textContent = i18next.t('fastfood.title');
		
		for(let i = 0; i < fastfoodOrders.length; i++) {
			let order = fastfoodOrders[i];
			
			// Calculate order's price
			let amount = order.pizzas * PRICE_PIZZA + order.hamburgers * PRICE_HAMBURGER + order.sandwitches * PRICE_SANDWICH;
			
			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let purchaseContainer = document.createElement('div');
			let priceContainer = document.createElement('div');
			let itemAmountContainer = document.createElement('div');
			let amountTextContainer = document.createElement('div');
			let itemDescription = document.createElement('span');
			let itemPrice = document.createElement('span');
			let itemAmount = document.createElement('span');
			
			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			purchaseContainer.classList.add('item-purchase');
			priceContainer.classList.add('item-price-container');
			itemAmountContainer.classList.add('item-amount-container');
			amountTextContainer.classList.add('item-amount-desc-container');
			itemDescription.classList.add('item-description');
			itemPrice.classList.add('item-price');
			itemAmount.classList.add('item-amount-description');
			
			// Add the text for each element
			itemDescription.textContent = i18next.t('fastfood.order-number', {value: order.id});
			itemPrice.innerHTML = '<b>' + i18next.t('fastfood.order') + '</b>' + amount + '$';
			itemAmount.innerHTML = '<b>' + i18next.t('fastfood.distance') + '</b>' + parseFloat(distances[i] / 1000).toFixed(2) + 'km';
			
			itemContainer.onclick = (function() {
				if(selected !== i) {
					// Check if there was any item selected
					if(selected != undefined) {
						let previousSelected = document.getElementsByClassName('item-row')[selected];
						previousSelected.classList.remove('active-item');
					}
					
					// Select the clicked element
					let currentSelected = document.getElementsByClassName('item-row')[i];
					currentSelected.classList.add('active-item');
					selected = i;
				}
			});
			
			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
			infoContainer.appendChild(purchaseContainer);
			purchaseContainer.appendChild(priceContainer);
			priceContainer.appendChild(itemPrice);
			purchaseContainer.appendChild(itemAmountContainer);
			itemAmountContainer.appendChild(amountTextContainer);
			amountTextContainer.appendChild(itemAmount);
		}
		
		let deliverButton = document.createElement('div');
		let cancelButton = document.createElement('div');
		
		deliverButton.classList.add('double-button', 'accept-button');
		cancelButton.classList.add('double-button', 'cancel-button');
		
		deliverButton.textContent = i18next.t('fastfood.deliver');
		cancelButton.textContent = i18next.t('general.exit');
		
		deliverButton.onclick = (function() {
			// Deliver the selected order
			if(selected != undefined) {
				mp.trigger('deliverFastfoodOrder', fastfoodOrders[selected].id);
			}
		});
		
		cancelButton.onclick = (function() {
			// Close the menu
			mp.trigger('destroyBrowser');
		});
		
		options.appendChild(deliverButton);
		options.appendChild(cancelButton);
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateFastfoodOrders(ordersJson, distancesJson); }, 100);
	}
}

function populateCrimesMenu(crimesJson, selectedCrimes) {
	if(messagesLoaded) {
		// Get the container nodes
		let header = document.getElementById('header');
		let content = document.getElementById('content');
		let options = document.getElementById('options');
		
		let crimesArray = JSON.parse(crimesJson);
		header.textContent = i18next.t('crimes.title');
		selectedOptions = [];
		
		if(selectedCrimes.length > 0) {
			let crimes = JSON.parse(selectedCrimes);
			
			for(let i = 0; i < crimes.length; i++) {
				selectedOptions.push(crimes[i]);
			}
		}
		
		for(let i = 0; i < crimesArray.length; i++) {
			// Get the crime
			let crime = crimesArray[i];
			
			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let purchaseContainer = document.createElement('div');
			let priceContainer = document.createElement('div');
			let itemAmountContainer = document.createElement('div');
			let amountTextContainer = document.createElement('div');
			let itemDescription = document.createElement('span');
			let itemPrice = document.createElement('span');
			let itemAmount = document.createElement('span');
			
			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			purchaseContainer.classList.add('item-purchase');
			priceContainer.classList.add('item-price-container');
			itemAmountContainer.classList.add('item-amount-container');
			amountTextContainer.classList.add('item-amount-desc-container');
			itemDescription.classList.add('item-description');
			itemPrice.classList.add('item-price');
			itemAmount.classList.add('item-amount-description');
			
			for(let c = 0; c < selectedOptions.length; c++) {
				if(JSON.stringify(crime) === JSON.stringify(selectedOptions[c])) {
					// Mark the crime as applied
					itemContainer.classList.add('active-item');
					selectedOptions.splice(c, 1);
					selectedOptions.push(crime);
				}
			}
			
			// Add the text for each element
			itemDescription.textContent = crime.crime;
			itemPrice.innerHTML = '<b>' + i18next.t('crimes.fine') + '</b>' + crime.fine + '$';
			itemAmount.innerHTML = '<b>' + i18next.t('crimes.jail') + '</b>' + crime.jail + 'min.';
			
			itemContainer.onclick = (function() {
				if(selectedOptions.indexOf(crime) === -1) {
					// Select the clicked element
					let currentSelected = document.getElementsByClassName('item-row')[i];
					currentSelected.classList.add('active-item');
					
					// Save the selected index
					selectedOptions.push(crime);
				} else {
					// Delete the selection
					let currentSelected = document.getElementsByClassName('item-row')[i];
					currentSelected.classList.remove('active-item');
					
					// Remove the element from the array
					selectedOptions.splice(selectedOptions.indexOf(crime), 1);
				}
			});
			
			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
			infoContainer.appendChild(purchaseContainer);
			purchaseContainer.appendChild(priceContainer);
			priceContainer.appendChild(itemPrice);
			purchaseContainer.appendChild(itemAmountContainer);
			itemAmountContainer.appendChild(amountTextContainer);
			amountTextContainer.appendChild(itemAmount);
		}
		
		let applyButton = document.createElement('div');
		let cancelButton = document.createElement('div');
		
		applyButton.classList.add('double-button', 'accept-button');
		cancelButton.classList.add('double-button', 'cancel-button');
		
		applyButton.textContent = i18next.t('crimes.incriminate');
		cancelButton.textContent = i18next.t('general.exit');
		
		applyButton.onclick = (function() {
			// Apply the selected crimes
			if(selectedOptions.length > 0) {
				mp.trigger('applyCrimes', JSON.stringify(selectedOptions));
			}
		});
		
		cancelButton.onclick = (function() {
			// Close crimes menu
			mp.trigger('destroyBrowser');
		});
		
		options.appendChild(applyButton);
		options.appendChild(cancelButton);
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateCrimesMenu(crimesJson, selectedCrimes); }, 100);
	}
}

function populateCharacterList(charactersJson) {
	if(messagesLoaded) {
		// Get the container nodes
		let header = document.getElementById('header');
		let content = document.getElementById('content');
		let options = document.getElementById('options');
		
		// Get the players list
		let characters = JSON.parse(charactersJson);
		
		// Add the heading text
		header.textContent = i18next.t('character.title');
		
		for(let i = 0; i < characters.length; i++) {
			// Get the current character
			let character = characters[i];
			
			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let itemDescription = document.createElement('span');
			
			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			itemDescription.classList.add('item-description');
			
			// Add the name
			itemDescription.textContent = character;
			
			itemContainer.onclick = (function() {
				// Load the selected character
				mp.trigger('loadCharacter', character);
			});
			
			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
		}
		
		let createButton = document.createElement('div');
		let playButton = document.createElement('div');
		
		createButton.classList.add('double-button', 'accept-button');
		playButton.classList.add('double-button', 'play-button');
		
		createButton.textContent = i18next.t('character.create');
		playButton.textContent = i18next.t('general.play');
		
		createButton.onclick = (function() {
			// Show the character creation menu
			mp.trigger('showCharacterCreationMenu');
		});
		
		playButton.onclick = (function() {
			// Close the menu
			mp.trigger('loadIntoWorld');
		});
		
		options.appendChild(createButton);
		options.appendChild(playButton);
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded		
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateCharacterList(charactersJson); }, 100);
	}
}

function populateClothesShopMenu(clothesTypeArray, businessName, priceMultiplier, isPolice) {
	if(messagesLoaded) {
		// Add the business name to the header
		let header = document.getElementById('header');
		header.textContent = businessName;
		
		// Load the clothes list
		clothesTypes = JSON.parse(clothesTypeArray);
		multiplier = priceMultiplier;
		police = (isPolice.toLowerCase() === 'true');

		// Show the main menu
		populateClothesShopHome();
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateClothesShopMenu(clothesTypeArray, businessName, priceMultiplier, isPolice); }, 100);
	}
}

function populateClothesShopHome() {
	if(messagesLoaded) {
		// Get the container node
		let content = document.getElementById('content');
		let options = document.getElementById('options');
		
		selected = undefined;
		drawable = undefined;
		
		while(content.firstChild) {
			content.removeChild(content.firstChild);
		}
		
		while(options.firstChild) {
			options.removeChild(options.firstChild);
		}
		
		for(let i = 0; i < clothesTypes.length; i++) {
			// Get the current zone
			let type = clothesTypes[i];

			// Check if police and slot
			if(type.slot === 9 && !police) continue;
			
			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let itemDescription = document.createElement('span');
			
			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			itemDescription.classList.add('item-description');
			
			itemDescription.textContent = i18next.t(type.description);
			
			itemContainer.onclick = (function() {
				selected = i;
				
				// Load the clothes from the zone
				mp.trigger('getClothesByType', i);
			});
			
			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
		}
		
		let exitButton = document.createElement('div');
		
		exitButton.classList.add('single-button', 'cancel-button');
		exitButton.textContent = i18next.t('general.exit');
		
		exitButton.onclick = (function() {
			// Exit the menu
			mp.trigger('closeClothesMenu');
		});
		
		options.appendChild(exitButton);
			
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateClothesShopHome(); }, 100);
	}
}

function populateTypeClothes(typeClothesJson) {
	if(messagesLoaded) {
		// Get the container node
		let content = document.getElementById('content');
		let options = document.getElementById('options');
		
		// Get the clothes in the JSON object
		let typeClothesArray = JSON.parse(typeClothesJson);
		
		while(content.firstChild) {
			content.removeChild(content.firstChild);
		}
		
		while(options.firstChild) {
			options.removeChild(options.firstChild);
		}
		
		for(let i = 0; i < typeClothesArray.length; i++) {
			let clothes = typeClothesArray[i];
			
			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let purchaseContainer = document.createElement('div');
			let priceContainer = document.createElement('div');
			let itemAmountContainer = document.createElement('div');
			let amountTextContainer = document.createElement('div');
			let addSubstractContainer = document.createElement('div');
			let itemDescription = document.createElement('span');
			let itemPrice = document.createElement('span');
			let itemAmount = document.createElement('span');
			let itemAdd = document.createElement('span');
			let itemSubstract = document.createElement('span');
			
			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			purchaseContainer.classList.add('item-purchase');
			priceContainer.classList.add('item-price-container');
			itemAmountContainer.classList.add('item-amount-container', 'hidden');
			amountTextContainer.classList.add('item-amount-desc-container');
			addSubstractContainer.classList.add('item-add-substract-container');
			itemDescription.classList.add('item-description');
			itemPrice.classList.add('item-price');
			itemAmount.classList.add('item-amount-description');
			itemAdd.classList.add('item-adder');
			itemSubstract.classList.add('item-substract', 'hidden');
			
			itemDescription.textContent = clothes.description;
			itemPrice.innerHTML = '<b>' + i18next.t('general.price') + '</b>' + Math.round(clothes.products * multiplier) + '$';
			itemAdd.textContent = '+';
			itemSubstract.textContent = '-';
			
			itemContainer.onclick = (function() {
				if(drawable !== i) {
					
					if(drawable != undefined) {
						let previousSelected = document.getElementsByClassName('item-row')[drawable];
						let previousAmountNode = findFirstChildByClass(previousSelected, 'item-amount-container');
						previousSelected.classList.remove('active-item');
						previousAmountNode.classList.add('hidden');
					}
					
					let currentSelected = document.getElementsByClassName('item-row')[i];
					let currentAmountNode = findFirstChildByClass(currentSelected, 'item-amount-container');
					currentSelected.classList.add('active-item');
					currentAmountNode.classList.remove('hidden');
					
					purchasedAmount = 0;
					drawable = i;
					
					itemAmount.innerHTML = '<b>' + i18next.t('clothes.variation') + '</b>' + purchasedAmount;
				
					if(purchasedAmount < clothes.textures - 1) {
						// Show add button
						document.getElementsByClassName('item-adder')[drawable].classList.remove('hidden');
					} else {
						// Hide add button
						document.getElementsByClassName('item-adder')[drawable].classList.add('hidden');
					}
					document.getElementsByClassName('item-substract')[drawable].classList.add('hidden');
					
					// Replace the clothes on the character
					mp.trigger('replacePlayerClothes', drawable, purchasedAmount);
				}
			});
			
			itemAdd.onclick = (function() {
				// Get next variation
				purchasedAmount++;
				
				let adderButton = document.getElementsByClassName('item-adder')[drawable];
				let substractButton = document.getElementsByClassName('item-substract')[drawable];
				
				if(purchasedAmount == clothes.textures - 1) {
					// Maximum reached
					adderButton.classList.add('hidden');
				} else if(substractButton.classList.contains('hidden') === true) {
					// Show the button
					substractButton.classList.remove('hidden');
				}
				
				let amountSpan = document.getElementsByClassName('item-amount-description')[drawable];
				amountSpan.innerHTML = '<b>' + i18next.t('clothes.variation') + '</b>' + purchasedAmount;
				
				// Replace the clothes on the character
				mp.trigger('replacePlayerClothes', drawable, purchasedAmount);
			});
			
			itemSubstract.onclick = (function() {
				// Get previous variation
				purchasedAmount--;
				
				let adderButton = document.getElementsByClassName('item-adder')[drawable];
				let substractButton = document.getElementsByClassName('item-substract')[drawable];
				
				if(purchasedAmount == 0) {
					// Minimum reached
					substractButton.classList.add('hidden');
				} else if(adderButton.classList.contains('hidden') === true) {
					// Show the button
					adderButton.classList.remove('hidden');
				}
				
				let amountSpan = document.getElementsByClassName('item-amount-description')[drawable];
				amountSpan.innerHTML = '<b>' + i18next.t('clothes.variation') + '</b>' + purchasedAmount;
				
				// Replace the clothes on the character
				mp.trigger('replacePlayerClothes', drawable, purchasedAmount);
			});
			
			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
			infoContainer.appendChild(purchaseContainer);
			purchaseContainer.appendChild(priceContainer);
			priceContainer.appendChild(itemPrice);
			purchaseContainer.appendChild(itemAmountContainer);
			itemAmountContainer.appendChild(amountTextContainer);
			amountTextContainer.appendChild(itemAmount);
			itemAmountContainer.appendChild(addSubstractContainer);
			addSubstractContainer.appendChild(itemAdd);
			addSubstractContainer.appendChild(itemSubstract);
		}
		
		let purchaseButton = document.createElement('div');
		let cancelButton = document.createElement('div');
		
		purchaseButton.classList.add('double-button', 'accept-button');
		cancelButton.classList.add('double-button', 'cancel-button');
		
		purchaseButton.textContent = i18next.t('general.purchase');
		cancelButton.textContent = i18next.t('general.back');
		
		purchaseButton.onclick = (function() {
			if(selected != undefined) {
				mp.trigger('purchaseClothes', drawable, purchasedAmount);
			}
		});
		
		cancelButton.onclick = (function() {
			// Back to the home menu
			populateClothesShopHome();
			
			// Clear player's clothes
			mp.trigger('clearClothes', drawable);
		});
		
		options.appendChild(purchaseButton);
		options.appendChild(cancelButton);
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateTypeClothes(typeClothesJson); }, 100);
	}
}

function populateTattooMenu(tattooZoneArray, businessName, priceMultiplier) {
	if(messagesLoaded) {
		// Set the business name as header
		let header = document.getElementById('header');
		header.textContent = businessName;
		
		// Get tattoo zones
		tattooZones = JSON.parse(tattooZoneArray);
		multiplier = priceMultiplier;
		
		// Show main menu
		populateTattooHome();
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateTattooMenu(tattooZoneArray, businessName, priceMultiplier); }, 100);
	}
}

function populateTattooHome() {
	if(messagesLoaded) {
		// Get the container nodes
		let content = document.getElementById('content');
		let options = document.getElementById('options');
		
		selected = undefined;
		drawable = undefined;
		
		while(content.firstChild) {
			content.removeChild(content.firstChild);
		}
		
		while(options.firstChild) {
			options.removeChild(options.firstChild);
		}
		
		for(let i = 0; i < tattooZones.length; i++) {
			// Get the zone
			let zone = tattooZones[i];
			
			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let itemDescription = document.createElement('span');
			
			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			itemDescription.classList.add('item-description');
			
			itemDescription.textContent = i18next.t(zone);
			
			itemContainer.onclick = (function() {
				selected = i;
				
				// Load the tattoos for the zone
				mp.trigger('getZoneTattoos', i);
			});
			
			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
		}
		
		let exitButton = document.createElement('div');
		
		exitButton.classList.add('single-button', 'cancel-button');
		exitButton.textContent = i18next.t('general.exit');
		
		exitButton.onclick = (function() {
			// Exit the menu
			mp.trigger('exitTattooShop');
		});
		
		options.appendChild(exitButton);
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateTattooHome(); }, 100);
	}
}

function populateZoneTattoos(zoneTattooJson) {
	if(messagesLoaded) {
		// Get the container nodes
		let content = document.getElementById('content');
		let options = document.getElementById('options');
		
		// Get the tattoos from the zone
		let zoneTattooArray = JSON.parse(zoneTattooJson);
		
		while(content.firstChild) {
			content.removeChild(content.firstChild);
		}
		
		while(options.firstChild) {
			options.removeChild(options.firstChild);
		}
		
		for(let i = 0; i < zoneTattooArray.length; i++) {
			// Get the tattoo
			let tattoo = zoneTattooArray[i];
			
			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let purchaseContainer = document.createElement('div');
			let priceContainer = document.createElement('div');
			let itemDescription = document.createElement('span');
			let itemPrice = document.createElement('span');
			
			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			purchaseContainer.classList.add('item-purchase');
			priceContainer.classList.add('item-price-container');
			itemDescription.classList.add('item-description');
			itemPrice.classList.add('item-price');
			
			itemDescription.textContent = tattoo.name;
			itemPrice.innerHTML = '<b>' + i18next.t('general.price') + '</b>' + Math.round(tattoo.price * multiplier) + '$';
			
			itemContainer.onclick = (function() {
				if(drawable !== i) {
					
					if(drawable != undefined) {
						let previousSelected = document.getElementsByClassName('item-row')[drawable];
						previousSelected.classList.remove('active-item');
					}
					
					let currentSelected = document.getElementsByClassName('item-row')[i];
					currentSelected.classList.add('active-item');
					
					drawable = i;
					
					// Update the tattoos
					mp.trigger('addPlayerTattoo', drawable);
				}
			});
			
			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
			infoContainer.appendChild(purchaseContainer);
			purchaseContainer.appendChild(priceContainer);
			priceContainer.appendChild(itemPrice);
		}
		
		let purchaseButton = document.createElement('div');
		let cancelButton = document.createElement('div');
		
		purchaseButton.classList.add('double-button', 'accept-button');
		cancelButton.classList.add('double-button', 'cancel-button');
		
		purchaseButton.textContent = i18next.t('general.purchase');
		cancelButton.textContent = i18next.t('general.back');
		
		purchaseButton.onclick = (function() {
			if(selected != undefined) {
				mp.trigger('purchaseTattoo', selected, drawable);
			}
		});
		
		cancelButton.onclick = (function() {
			//  Back to the main menu
			populateTattooHome();
			
			// Clear the tattoos on the player
			mp.trigger('clearTattoos');
		});
		
		options.appendChild(purchaseButton);
		options.appendChild(cancelButton);
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateZoneTattoos(zoneTattooJson); }, 100);
	}
}

function populateHairdresserMenu(faceOptionsJson, selectedFaceJson, businessName) {
	if(messagesLoaded) {
		// Get the container nodes
		let faceOptions = JSON.parse(faceOptionsJson);
		let header = document.getElementById('header');
		let content = document.getElementById('content');
		let options = document.getElementById('options');
		
		header.textContent = businessName;
		selectedOptions = JSON.parse(selectedFaceJson);
		
		for(let i = 0; i < faceOptions.length; i++) {
			let face = faceOptions[i];
			
			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let purchaseContainer = document.createElement('div');
			let itemAmountContainer = document.createElement('div');
			let amountTextContainer = document.createElement('div');
			let addSubstractContainer = document.createElement('div');
			let itemDescription = document.createElement('span');
			let itemAmount = document.createElement('span');
			let itemAdd = document.createElement('span');
			let itemSubstract = document.createElement('span');
			
			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			purchaseContainer.classList.add('item-purchase');
			itemAmountContainer.classList.add('item-amount-container');
			amountTextContainer.classList.add('item-amount-desc-container');
			addSubstractContainer.classList.add('item-add-substract-container');
			itemDescription.classList.add('item-description');
			itemAmount.classList.add('item-amount-description');
			itemAdd.classList.add('item-adder');
			itemSubstract.classList.add('item-substract');
			
			if(selectedOptions[i] == face.minValue) {
				itemSubstract.classList.add('hidden');
			} else if(selectedOptions[i] == face.maxValue) {
				itemAdd.classList.add('hidden');
			}		
			
			itemDescription.textContent = i18next.t(face.desc);
			itemAmount.innerHTML = '<b>' + i18next.t('character.type') + '</b>' + selectedOptions[i];
			itemAdd.textContent = '+';
			itemSubstract.textContent = '-';
			
			itemAdd.onclick = (function() {
				selectedOptions[i]++;
				
				let adderButton = document.getElementsByClassName('item-adder')[i];
				let substractButton = document.getElementsByClassName('item-substract')[i];
				
				if(selectedOptions[i] == face.maxValue) {
					// Maximum reached
					adderButton.classList.add('hidden');
				} else if(substractButton.classList.contains('hidden') === true) {
					// Show the button
					substractButton.classList.remove('hidden');
				}
				
				let amountSpan = document.getElementsByClassName('item-amount-description')[i];
				amountSpan.innerHTML = '<b>' + i18next.t('character.type') + '</b>' + selectedOptions[i];
				
				// Update the hair
				mp.trigger('updateFacialHair', i, selectedOptions[i]);
			});
			
			itemSubstract.onclick = (function() {
				selectedOptions[i]--;
				
				let adderButton = document.getElementsByClassName('item-adder')[i];
				let substractButton = document.getElementsByClassName('item-substract')[i];
				
				if(selectedOptions[i] == face.minValue) {
					// Minimum reached
					substractButton.classList.add('hidden');
				} else if(adderButton.classList.contains('hidden') === true) {
					// Show the button
					adderButton.classList.remove('hidden');
				}
				
				let amountSpan = document.getElementsByClassName('item-amount-description')[i];
				amountSpan.innerHTML = '<b>' + i18next.t('character.type') + '</b>' + selectedOptions[i];
				
				// Update the hair
				mp.trigger('updateFacialHair', i, selectedOptions[i]);
			});
			
			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
			infoContainer.appendChild(purchaseContainer);
			purchaseContainer.appendChild(itemAmountContainer);
			itemAmountContainer.appendChild(amountTextContainer);
			amountTextContainer.appendChild(itemAmount);
			itemAmountContainer.appendChild(addSubstractContainer);
			addSubstractContainer.appendChild(itemAdd);
			addSubstractContainer.appendChild(itemSubstract);
		}
		
		let acceptButton = document.createElement('div');
		let cancelButton = document.createElement('div');
		
		acceptButton.classList.add('double-button', 'accept-button');
		cancelButton.classList.add('double-button', 'cancel-button');
		
		acceptButton.textContent = i18next.t('general.accept');
		cancelButton.textContent = i18next.t('general.exit');
		
		acceptButton.onclick = (function() {
			// Save the changes
			mp.trigger('applyHairdresserChanges');
		});
		
		cancelButton.onclick = (function() {
			// Cancel the changes
			mp.trigger('cancelHairdresserChanges');
			mp.trigger('destroyBrowser');
		});
		
		options.appendChild(acceptButton);
		options.appendChild(cancelButton);
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateHairdresserMenu(faceOptionsJson, selectedFaceJson, businessName); }, 100);
	}
}

function populateTownHallMenu(townHallOptionsJson) {
	if(messagesLoaded) {
		let townHallOptions = JSON.parse(townHallOptionsJson);
		let header = document.getElementById('header');
		let content = document.getElementById('content');
		let options = document.getElementById('options');
		
		header.textContent = i18next.t('townhall.title');
		selected = undefined;
		
		while(content.firstChild) {
			content.removeChild(content.firstChild);
		}
		
		while(options.firstChild) {
			options.removeChild(options.firstChild);
		}
		
		for(let i = 0; i < townHallOptions.length; i++) {
			let townHall = townHallOptions[i];
			
			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let purchaseContainer = document.createElement('div');
			let priceContainer = document.createElement('div');
			let itemDescription = document.createElement('span');
			let itemPrice = document.createElement('span');
			
			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			purchaseContainer.classList.add('item-purchase');
			priceContainer.classList.add('item-price-container');
			itemDescription.classList.add('item-description');
			itemPrice.classList.add('item-price');
			
			itemDescription.textContent = i18next.t(townHall.desc);
			
			if(townHall.price > 0) {
				// If there's any price, show it
				itemPrice.innerHTML = '<b>' + i18next.t('general.price') + '</b>' + townHall.price + '$';
			}
			
			itemContainer.onclick = (function() {
				if(selected !== i) {
					
					if(selected != undefined) {
						let previousSelected = document.getElementsByClassName('item-row')[selected];
						previousSelected.classList.remove('active-item');
					}
					
					let currentSelected = document.getElementsByClassName('item-row')[i];
					currentSelected.classList.add('active-item');
					
					selected = i;
					
					// Change the text in the button
					let leftButton = document.getElementsByClassName('accept-button')[0];
					leftButton.textContent = townHall.price > 0 ? i18next.t('townhall.pay') : i18next.t('townhall.check');
				}
			});
			
			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
			infoContainer.appendChild(purchaseContainer);
			purchaseContainer.appendChild(priceContainer);
			priceContainer.appendChild(itemPrice);
		}
		
		let acceptButton = document.createElement('div');
		let cancelButton = document.createElement('div');
		
		acceptButton.classList.add('double-button', 'accept-button');
		cancelButton.classList.add('double-button', 'cancel-button');
		
		// Añadimos el texto de los botones
		acceptButton.textContent = i18next.t('townhall.pay');
		cancelButton.textContent = i18next.t('general.exit');
		
		acceptButton.onclick = (function() {
			if(selected != undefined) {
				// Execute the selected operation
				mp.trigger('executeTownHallOperation', selected);
			}
		});
		
		cancelButton.onclick = (function() {
			// Close the menu
			mp.trigger('destroyBrowser');
		});
		
		options.appendChild(acceptButton);
		options.appendChild(cancelButton);
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateTownHallMenu(townHallOptionsJson); }, 100);
	}
}

function populateFinesMenu(finesJson) {
	if(messagesLoaded) {
		let finesList = JSON.parse(finesJson);
		let content = document.getElementById('content');
		let options = document.getElementById('options');
		selectedOptions = [];
		
		while(content.firstChild) {
			content.removeChild(content.firstChild);
		}
		
		while(options.firstChild) {
			options.removeChild(options.firstChild);
		}
		
		for(let i = 0; i < finesList.length; i++) {
			let fine = finesList[i];
			
			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let purchaseContainer = document.createElement('div');
			let priceContainer = document.createElement('div');
			let itemAmountContainer = document.createElement('div');
			let amountTextContainer = document.createElement('div');
			let itemDescription = document.createElement('span');
			let itemPrice = document.createElement('span');
			let itemAmount = document.createElement('span');
			
			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			purchaseContainer.classList.add('item-purchase');
			priceContainer.classList.add('item-price-container');
			itemAmountContainer.classList.add('item-amount-container');
			amountTextContainer.classList.add('item-amount-desc-container');
			itemDescription.classList.add('item-description');
			itemPrice.classList.add('item-price');
			itemAmount.classList.add('item-amount-description');
			
			itemDescription.textContent = fine.reason;
			itemPrice.innerHTML = '<b>' + i18next.t('general.amount') + '</b>' + fine.amount + '$';
			itemAmount.innerHTML = '<b>' + i18next.t('townhall.date') + '</b>' + fine.date.split('T')[0];
			
			itemContainer.onclick = (function() {
				if(selectedOptions.indexOf(fine) === -1) {
					// Mark the selected item
					let currentSelected = document.getElementsByClassName('item-row')[i];
					currentSelected.classList.add('active-item');
					
					// Save the index
					selectedOptions.push(fine);
				} else {
					// Unmark the selected item
					let currentSelected = document.getElementsByClassName('item-row')[i];
					currentSelected.classList.remove('active-item');
					
					// Remove the index
					selectedOptions.splice(selectedOptions.indexOf(fine), 1);
				}
			});
			
			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
			infoContainer.appendChild(purchaseContainer);
			purchaseContainer.appendChild(priceContainer);
			priceContainer.appendChild(itemPrice);
			purchaseContainer.appendChild(itemAmountContainer);
			itemAmountContainer.appendChild(amountTextContainer);
			amountTextContainer.appendChild(itemAmount);
		}
		
		let acceptButton = document.createElement('div');
		let cancelButton = document.createElement('div');
		
		acceptButton.classList.add('double-button', 'accept-button');
		cancelButton.classList.add('double-button', 'cancel-button');
		
		acceptButton.textContent = i18next.t('townhall.pay');
		cancelButton.textContent = i18next.t('general.back');
		
		acceptButton.onclick = (function() {
			if(selectedOptions.length > 0) {
				// Pay the selected fines
				mp.trigger('payPlayerFines', JSON.stringify(selectedOptions));
			}
		});
		
		cancelButton.onclick = (function() {
			// Back to the main menu
			mp.trigger('backTownHallIndex');
		});
		
		options.appendChild(acceptButton);
		options.appendChild(cancelButton);
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateFinesMenu(finesJson); }, 100);
	}
}

function populatePoliceControlsMenu(policeControlJson) {
	if(messagesLoaded) {
		let policeControls = JSON.parse(policeControlJson);
		let header = document.getElementById('header');
		let content = document.getElementById('content');
		let options = document.getElementById('options');
		
		// Add the header text
		header.textContent = i18next.t('police.title');
		
		for(let i = 0; i < policeControls.length; i++) {
			let control = policeControls[i];
			
			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let itemDescription = document.createElement('span');
			
			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			itemDescription.classList.add('item-description');
			
			itemDescription.textContent = control;
			
			itemContainer.onclick = (function() {
				if(selected !== i) {
					
					if(selected != undefined) {
						let previousSelected = document.getElementsByClassName('item-row')[selected];
						previousSelected.classList.remove('active-item');
					}
					
					let currentSelected = document.getElementsByClassName('item-row')[i];
					currentSelected.classList.add('active-item');
					
					selected = i;
				}
			});
			
			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
		}
		
		let acceptButton = document.createElement('div');
		let cancelButton = document.createElement('div');
		
		acceptButton.classList.add('double-button', 'accept-button');
		cancelButton.classList.add('double-button', 'cancel-button');
		
		acceptButton.textContent = i18next.t('police.load');
		cancelButton.textContent = i18next.t('general.exit');
		
		acceptButton.onclick = (function() {
			// Process the option and close the menu
			mp.trigger('proccessPoliceControlAction');
			mp.trigger('destroyBrowser');
		});
		
		cancelButton.onclick = (function() {
			// Close the menu
			mp.trigger('destroyBrowser');
		});
		
		options.appendChild(acceptButton);
		options.appendChild(cancelButton);
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populatePoliceControlsMenu(policeControlJson); }, 100);
	}
}

function populateWardrobeMenu(clothesTypeArray, isPolice) {
	if(messagesLoaded) {
		let header = document.getElementById('header');
		header.textContent = i18next.t('house.title');
		
		clothesTypes = JSON.parse(clothesTypeArray);
		police = (isPolice.toLowerCase() === 'true');
		
		populateWardrobeHome();
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateWardrobeMenu(clothesTypeArray, isPolice); }, 100);
	}
}

function populateWardrobeHome() {
	if(messagesLoaded) {
		let content = document.getElementById('content');
		let options = document.getElementById('options');
		
		selected = undefined;
		drawable = undefined;
		
		while(content.firstChild) {
			content.removeChild(content.firstChild);
		}
		
		while(options.firstChild) {
			options.removeChild(options.firstChild);
		}
		
		for(let i = 0; i < clothesTypes.length; i++) {
			let type = clothesTypes[i];

			if(clothesTypes[i].slot === 9 && !police) continue;
			
			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let itemDescription = document.createElement('span');
			
			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			itemDescription.classList.add('item-description');
			
			itemDescription.textContent = i18next.t(type.description);
			
			itemContainer.onclick = (function() {
				selected = type.slot;
				
				// Load the purchased clothes
				mp.trigger('getPlayerPurchasedClothes', selected);
			});
			
			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
		}
		
		let exitButton = document.createElement('div');
		
		exitButton.classList.add('single-button', 'cancel-button');
		exitButton.textContent = i18next.t('general.exit');
		
		exitButton.onclick = (function() {
			// Exit the menu
			mp.trigger('closeWardrobeMenu');
		});
		
		options.appendChild(exitButton);
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateWardrobeHome(); }, 100);
	}
}

function populateWardrobeClothes(typeClothesJson) {
	if(messagesLoaded) {
		let content = document.getElementById('content');
		let options = document.getElementById('options');
		
		let typeClothesArray = JSON.parse(typeClothesJson);
		
		while(content.firstChild) {
			content.removeChild(content.firstChild);
		}
		
		while(options.firstChild) {
			options.removeChild(options.firstChild);
		}
		
		for(let i = 0; i < typeClothesArray.length; i++) {
			let clothes = typeClothesArray[i];
			
			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let purchaseContainer = document.createElement('div');
			let priceContainer = document.createElement('div');
			let itemDescription = document.createElement('span');
			let itemPrice = document.createElement('span');
			
			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			purchaseContainer.classList.add('item-purchase');
			priceContainer.classList.add('item-price-container');
			itemDescription.classList.add('item-description');
			itemPrice.classList.add('item-price');
			
			itemDescription.textContent = clothes.description;
			itemPrice.innerHTML = '<b>' + i18next.t('clothes.variation') + '</b>' + clothes.texture;
			
			itemContainer.onclick = (function() {
				if(drawable !== i) {
					
					if(drawable != undefined) {
						let previousSelected = document.getElementsByClassName('item-row')[drawable];
						previousSelected.classList.remove('active-item');
					}
					
					let currentSelected = document.getElementsByClassName('item-row')[i];
					currentSelected.classList.add('active-item');
					
					drawable = i;
					
					// Update the player's clothes
					mp.trigger('previewPlayerClothes', drawable);
				}
			});
			
			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
			infoContainer.appendChild(purchaseContainer);
			purchaseContainer.appendChild(priceContainer);
			priceContainer.appendChild(itemPrice);
		}
		
		let dressButton = document.createElement('div');
		let cancelButton = document.createElement('div');
		
		dressButton.classList.add('double-button', 'accept-button');
		cancelButton.classList.add('double-button', 'cancel-button');
		
		dressButton.textContent = i18next.t('clothes.dress');
		cancelButton.textContent = i18next.t('general.back');
		
		dressButton.onclick = (function() {
			if(selected != undefined) {
				mp.trigger('changePlayerClothes', selected, drawable);
			}
		});
		
		cancelButton.onclick = (function() {
			// Back to the main menu
			populateWardrobeHome();
			
			// Clear not dressed clothes
			mp.trigger('clearWardrobeClothes', selected);
		});
		
		options.appendChild(dressButton);
		options.appendChild(cancelButton);
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateWardrobeClothes(typeClothesJson); }, 100);
	}
}

function populateContactsMenu(contactsJson, action) {
	// Check if the messages are loaded
	if(messagesLoaded) {
		// Initialize the values
		purchasedAmount = 1;
		selected = undefined;

		// Get items to show
		let contactsArray = JSON.parse(contactsJson);
		let header = document.getElementById('header');
		let content = document.getElementById('content');
		let options = document.getElementById('options');
		
		// Show business name
		header.textContent = i18next.t('telephone.contact-list');
		
		for(let i = 0; i < contactsArray.length; i++) {
			let item = contactsArray[i];
			
			let itemContainer = document.createElement('div');
			let infoContainer = document.createElement('div');
			let descContainer = document.createElement('div');
			let purchaseContainer = document.createElement('div');
			let priceContainer = document.createElement('div');
			let itemDescription = document.createElement('span');
			let itemPrice = document.createElement('span');
			
			itemContainer.classList.add('item-row');
			infoContainer.classList.add('item-content');
			descContainer.classList.add('item-header');
			purchaseContainer.classList.add('item-purchase');
			priceContainer.classList.add('item-price-container');
			itemDescription.classList.add('item-description');
			itemPrice.classList.add('item-price');
			
			itemDescription.textContent = item.contactName;
			itemPrice.innerHTML = '<b>' + item.contactNumber + '</b>';
			
			itemContainer.onclick = (function() {
				// Check if the item is not selected
				if(selected !== i) {
					// Check if there was any item selected
					if(selected != undefined) {
						let previousSelected = document.getElementsByClassName('item-row')[selected];
						previousSelected.classList.remove('active-item');
					}
					
					// Select the item
					let currentSelected = document.getElementsByClassName('item-row')[i];
					currentSelected.classList.add('active-item');
					
					// Store the item
					selected = i;
				}
			});
			
			content.appendChild(itemContainer);
			itemContainer.appendChild(infoContainer);
			infoContainer.appendChild(descContainer);
			descContainer.appendChild(itemDescription);
			infoContainer.appendChild(purchaseContainer);
			purchaseContainer.appendChild(priceContainer);
			priceContainer.appendChild(itemPrice);
		}
		
		// Add option buttons
		let cancelButton = document.createElement('div');
		
		// Add classes for the buttons
		cancelButton.classList.add('cancel-button');
		cancelButton.classList.add(parseInt(action) > 0 ? 'double-button' : 'single-button');
		
		// Add text for the buttons
		cancelButton.textContent = i18next.t('general.exit');
		
		if(parseInt(action) > 0) {
			let actionButton = document.createElement('div');
			actionButton.classList.add('double-button', 'accept-button');
			actionButton.textContent = getContactActionText(action);
	
			actionButton.onclick = (function() {
				// Check if the user purchased anything
				if(selected != undefined) {
					mp.trigger('executePhoneAction', selected);
				}
			});

			options.appendChild(actionButton);
		}
		
		cancelButton.onclick = (function() {
			// Close the purchase window
			mp.trigger('destroyBrowser');
		});
		
		options.appendChild(cancelButton);
		
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateContactsMenu(contactsJson, action); }, 100);
	}
}

function findFirstChildByClass(element, className) {
	let foundElement = undefined, found;
	function recurse(element, className, found) {
		for (let i = 0; i < element.childNodes.length && !found; i++) {
			let el = element.childNodes[i];
			let classes = el.className != undefined? el.className.split(" ") : [];
			for (let j = 0, jl = classes.length; j < jl; j++) {
				if (classes[j] == className) {
					found = true;
					foundElement = element.childNodes[i];
					break;
				}
			}
			if(found)
				break;
			recurse(element.childNodes[i], className, found);
		}
	}
	recurse(element, className, false);
	return foundElement;
}		

function getContactActionText(action) {
	let text = undefined;

	switch(parseInt(action)) {
		case 2:
			text = i18next.t('telephone.action-modify');
			break;
		case 3:
			text = i18next.t('telephone.action-delete');
			break;
		case 5:
			text = i18next.t('telephone.action-sms');
			break;
	}

	return text;
}