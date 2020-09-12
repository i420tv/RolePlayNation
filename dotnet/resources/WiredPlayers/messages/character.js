let messagesLoaded = false;
let timeout = undefined;
let characterSex = 0;

$(document).ready(function() {
	i18next.use(window.i18nextXHRBackend).init({
		backend: {
			loadPath: '../i18n/es.json'
		}
	}, function(err, t) {
		jqueryI18next.init(i18next, $, {
			optionsAttr: 'i18n-options',
			useOptionsAttr: true,
			parseDefaultValueFromContent: true
		});
		$(document).localize();
		messagesLoaded = true;
		currentStep = 0;
    });
});

function hideError() {
	$('#error').addClass('no-display');
}

function toggleMenu() {
	$('#slider').slideToggle('slow');
}

function toggleCharacterInfo() {
	if(!$("#character-customize").is(":hidden")) {
		$("#character-customize").fadeOut('slow');
	} else if(!$("#character-skins").is(":hidden")) {
		$("#character-skins").fadeOut('slow');
	}

	$('#basic-info').slideToggle('slow');
}

function toggleCharacterCustomize() {
	if(!$("#basic-info").is(":hidden")) {
		$("#basic-info").fadeOut('slow');
	}

	if(document.getElementById('character-model-default').checked) {
		// Get the skins for the default character
		mp.trigger('getDefaultSkins');
	} else {
		// Show the character customization
		$('#character-customize').slideToggle('slow');
	}
}

function populateDefaultSkins(skinJson) {
	// Get the skins from the JSON
	let skinList = JSON.parse(skinJson);

	// Get the containing element and empty the children
	let root = document.getElementById('character-skins');
	root.innerHTML = '';

	for(let i = 0; i < skinList.length; i++) {
		// Create the HTML element
		let row = document.createElement('div');
		row.innerText = skinList[i];

		row.onclick = function() {
			// Change the player model
			mp.trigger('selectDefaultCharacter', skinList[i]);
		};

		// Add the children to the parent
		root.appendChild(row);
	}

	// Show the character list
	$('#character-skins').slideToggle('slow');
}

$("div#sex").on("click", "img", function() {
	if($(this).hasClass("enabled") == false) {
		var sex = 0;
		if($("#sex-male").hasClass("enabled") == true) {
			$("#sex-male").attr("src", "../img/character/male-disabled.png");
			$("#sex-female").attr("src", "../img/character/female-enabled.png");
			$("#sex-male").removeClass("enabled");
			$("#sex-female").addClass("enabled");
			sex = 1;
		} else {
			$("#sex-male").attr("src", "../img/character/male-enabled.png");
			$("#sex-female").attr("src", "../img/character/female-disabled.png");
			$("#sex-female").removeClass("enabled");
			$("#sex-male").addClass("enabled");
		}
		mp.trigger('updatePlayerSex', sex);
	}
});

$("nav#slider ul").on("click", "li", function() {
	// check the active index
	let position = $(this).index();
	let text = $(this).text();

	// Hide the menu
	$('#slider').slideToggle('fast');

	// Check the option pressed
	$.each($("#option-panels > div"), function(index, value) {
		if(index == position) {
			$(this).removeClass("no-display");
			$("#current-option").text(text);
		} else {
			$(this).addClass("no-display");
		}
	});
});

function createCharacter() {
	if(messagesLoaded) {
		if($.trim($('#character-name').val()).length == 0) {
			$('#error-message').html(i18next.t('character.name-missing'));
			$('#error').removeClass('no-display');
		} else if($.trim($('#character-surname').val()).length == 0) {
			$('#error-message').html(i18next.t('character.surname-missing'));
			$('#error').removeClass('no-display');
		} else {
			let characterAge = $('#age').val();
			let characterName = $('#character-name').val()[0].toUpperCase() + $('#character-name').val().substr(1);
			let characterSurname = $('#character-surname').val()[0].toUpperCase() + $('#character-surname').val().substr(1);
			mp.trigger('acceptCharacterCreation', characterName.trim() + " " + characterSurname.trim(), characterAge);
		}
	} else {
		clearTimeout(timeout);
		timeout = setTimeout(function() { createCharacter(); }, 100);
	}
}

function cancelCreation() {
	// Cancel character's creation
	mp.trigger('cancelCharacterCreation');
}

function showPlayerDuplicatedWarn() {
	if(messagesLoaded) {
		$('#error-message').html(i18next.t('character.name-duplicated'));
		$('#error').removeClass('no-display');
	} else {
		clearTimeout(timeout);
		timeout = setTimeout(function() { showPlayerDuplicatedWarn(); }, 100);
	}
}

function changeCharacterModel(model) {
	// Change the player's model
	mp.trigger('changePlayerModel', model);
}

function changeCharacterSex(sex) {
	if(characterSex !== sex) {
		// Change the player's sex
		characterSex = sex;
		mp.trigger('changePlayerSex', sex);

		// Change the images
		document.getElementById('sex-male').src = sex === 0 ? '../img/character/male-enabled.png' : '../img/character/male-disabled.png';
		document.getElementById('sex-female').src = sex === 1 ? '../img/character/female-enabled.png' : '../img/character/female-disabled.png';
	}
}

function updateFaceFeature() {
	// Get the data
	let dataObject = {};
	let dataKey = event.currentTarget.id;
	let dataValue = event.currentTarget.value;

	// Populate the object
	dataObject[dataKey] = parseFloat(dataValue);

	// Update the data on the client
	mp.trigger('storePlayerData', JSON.stringify(dataObject));
}

function previousCharacterValue() {
	// Initialize the value to set
	let faceValue = undefined;

	// Get the closest div
	let nodeElement = event.currentTarget.nextElementSibling;

	// Get the maximum, minimum, none values and the current value
	let values = getMaximumMinimumNoneValues(nodeElement.id);
	let current = nodeElement.innerText.includes(i18next.t('general.type')) ? parseInt(nodeElement.innerText.split(' ')[1]) : 255;

	if(current === values.min) {
		// It's the first value, check if it has a default value
		faceValue = values.none === undefined ? values.max : 255;
	} else if(current === 255) {
		// The previous value will be the maximum
		faceValue = values.max;
	} else {
		// Substract one to the value
		faceValue = current - 1;
	}

	// Update the text on the element
	nodeElement.innerText = faceValue === 255 ? i18next.t(values.none) : i18next.t('general.type-n', {value: faceValue}); 

	// Store the data and update the face
	let dataObject = {};
	dataObject[nodeElement.id] = faceValue;
	mp.trigger('storePlayerData', JSON.stringify(dataObject));
}

function nextCharacterValue() {
	// Initialize the value to set
	let faceValue = undefined;

	// Get the closest div
	let nodeElement = event.currentTarget.previousElementSibling;

	// Get the maximum, minimum, none values and the current value
	let values = getMaximumMinimumNoneValues(nodeElement.id);
	let current = nodeElement.innerText.includes(i18next.t('general.type')) ? parseInt(nodeElement.innerText.split(' ')[1]) : 255;

	if(current === values.max) {
		// It's the first value, check if it has a default value
		faceValue = values.none === undefined ? values.min : 255;
	} else if(current === 255) {
		// The next value will be the minimum
		faceValue = values.min;
	} else {
		// Substract one to the value
		faceValue = current + 1;
	}

	// Update the text on the element
	nodeElement.innerText = faceValue === 255 ? i18next.t(values.none) : i18next.t('general.type-n', {value: faceValue}); 

	// Store the data and update the face
	let dataObject = {};
	dataObject[nodeElement.id] = faceValue;
	mp.trigger('storePlayerData', JSON.stringify(dataObject));
}

function cameraPointTo(part) {
	// Change the camera pointing zone
	mp.trigger('cameraPointTo', part);
}

function rotateCharacter() {
    var rotation = parseFloat(document.getElementById('character-slider').value);
	mp.trigger('rotateCharacter', rotation);
}

function getMaximumMinimumNoneValues(elementId) {
	// Create the return object
	let values = {'max': undefined, 'min': 0, 'none': undefined};

	switch(elementId) {
		case 'hairModel':
			values.max = characterSex === 0 ? 73 : 77;
			break;
		case 'beardModel':
			values.max = 28;
			values.none = 'character.no-beard';
			break;
		case 'eyebrowsModel':
			values.max = 33;
			break;
		case 'chestModel':
			values.max = 17;
			values.none = 'character.no-hair';
			break;
		case 'blemishesModel':
			values.max = 23;
			values.none = 'character.no-blemishes';
			break;
		case 'ageingModel':
			values.max = 14;
			values.none = 'character.no-ageing';
			break;
		case 'complexionModel':
			values.max = 11;
			values.none = 'character.no-complexion';
			break;
		case 'sundamageModel':
			values.max = 10;
			values.none = 'character.no-sundamage';
			break;
		case 'frecklesModel':
			values.max = 17;
			values.none = 'character.no-freckles';
			break;
		case 'makeupModel':
			values.max = 74;
			values.none = 'character.no-makeup';
			break;		
		case 'blushModel':
			values.max = 6;
			values.none = 'character.no-blush';
			break;
		case 'lipstickModel':
			values.max = 9;
			values.none = 'character.no-lipstick';
			break;
		default:
			values.max = 45;
			break;
	}

	return values;
}

$('.btn-number').click(function(e){
    e.preventDefault();
    
    fieldName = $(this).attr('data-field');
    type      = $(this).attr('data-type');
    var input = $("input[name='"+fieldName+"']");
    var currentVal = parseInt(input.val());
    if (!isNaN(currentVal)) {
        if(type == 'minus') {
            
            if(currentVal > input.attr('min')) {
                input.val(currentVal - 1).change();
            } 
            if(parseInt(input.val()) == input.attr('min')) {
                $(this).attr('disabled', true);
            }

        } else if(type == 'plus') {

            if(currentVal < input.attr('max')) {
                input.val(currentVal + 1).change();
            }
            if(parseInt(input.val()) == input.attr('max')) {
                $(this).attr('disabled', true);
            }

        }
    } else {
        input.val(0);
    }
});
$('.input-number').focusin(function(){
   $(this).data('oldValue', $(this).val());
});
$('.input-number').change(function() {
    
    minValue =  parseInt($(this).attr('min'));
    maxValue =  parseInt($(this).attr('max'));
    valueCurrent = parseInt($(this).val());
    
    name = $(this).attr('name');
    if(valueCurrent >= minValue) {
        $(".btn-number[data-type='minus'][data-field='"+name+"']").removeAttr('disabled')
    } else {
        $(this).val($(this).data('oldValue'));
    }
    if(valueCurrent <= maxValue) {
        $(".btn-number[data-type='plus'][data-field='"+name+"']").removeAttr('disabled')
    } else {
        $(this).val($(this).data('oldValue'));
    }
    
    
});
$(".input-number").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 190]) !== -1 ||
             // Allow: Ctrl+A
            (e.keyCode == 65 && e.ctrlKey === true) || 
             // Allow: home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39)) {
                 // let it happen, don't do anything
                 return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
	});
	

/* Composure/Psycho */

$(".btn-age").on("click", function() {
	var $button = $(this);

	if ($button.attr('data-dir') == 'minus') {
		if ($('#age').val() > 12) {
			$('#age').val(parseInt($('#age').val()) - 1);	
			if($('#age').val() < 90) {
				$('#btn-age-plus').attr('disabled', false);
			}
		} else {
			$button.attr('disabled', true);
		}
	} else if ($button.attr('data-dir') == 'plus') {
		if ($('#age').val() < 90) {
			$('#age').val(parseInt($('#age').val()) + 1);	
			if($('#age').val() > 12) {
				$('#btn-age-minus').attr('disabled', false);
			}
		} else {
			$button.attr('disabled', true);
		}
	}
});