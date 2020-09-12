var menuEvents = ["carColorChangeToServer", "menuOkayToServer", "menuFixToServer", "Spo1", "Spo2", "Spo3", "Spo4", "Spo5", "Spo6", "Spo7", "Spo8", "Spo9", "Spo10", "Spo11", "Spo12", "Spo13", "Spo14", "Spo15", "Spo16", "Fb1", "Fb2", "Fb3", "Fb4", "Fb5", "Fb6", "Fb7", "Fb8", "Fb9", "Fb10", "Fb11", "Fb12", "Fb13", "Fb14", "Fb15", "Fb16", "Bo1", "Bo2", "Bo3", "Bo4", "Bo5", "Bsk1", "Bsk2", "Bsk3", "Bsk4", "Bsk5", "Bsk6", "Bsk7", "win1", "win2", "win3", "win4", "ems1", "ems2", "ems3", "ems4", "ems5", "br1", "br2", "br3", "br4", "su1", "su2", "su3", "su4", "su5",  "trs1", "trs2", "trs3", "trs4", "cmenuDone", "cmenuCursor", "menuTurboToServer"];


var carMenuOpen = false;
mp.events.add('cmenuActive', () =>{
	if(carMenuOpen) return 0;
	carMenu = mp.browsers.new('package://CarMenu/carmenu.html');
	mp.gui.cursor.show(true, true);
	carMenuOpen = true;
});





mp.events.add("carColorChangeToServer", (red, green, blue) =>{
	mp.events.callRemote('CmenuColor', red, green, blue);
	mp.game.ui.setNotificationTextEntry("STRING");
	mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Vehicle color changed!");
	mp.game.ui.drawNotification(true, false);
});




mp.events.add('menuFixToServer', () => {
	mp.events.callRemote('CmenuFix');
});

//Engine mods
mp.events.add('ems1', (player) => {
	mp.players.local.vehicle.setMod(11, -1);
	mp.game.ui.setNotificationTextEntry("STRING");
	mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Default engine added!");
	mp.game.ui.drawNotification(true, false);

});
mp.events.add('ems2', (player) => {
	mp.players.local.vehicle.setMod(11, 0);
	mp.game.ui.setNotificationTextEntry("STRING");
	mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Engine EMS 1 upgrade added!");
	mp.game.ui.drawNotification(true, false);
});
mp.events.add('ems3', (player) => {
	mp.players.local.vehicle.setMod(11, 1);
	mp.game.ui.setNotificationTextEntry("STRING");
	mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Engine EMS 2 upgrade added!");
	mp.game.ui.drawNotification(true, false);
});
mp.events.add('ems4', (player) => {
	mp.players.local.vehicle.setMod(11, 2);
	mp.game.ui.setNotificationTextEntry("STRING");
	mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Engine EMS 3 upgrade added!");
	mp.game.ui.drawNotification(true, false);
});
mp.events.add('ems5', (player) => {
	mp.players.local.vehicle.setMod(11, 3);
	mp.game.ui.setNotificationTextEntry("STRING");
	mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Engine EMS 4 upgrade added!");
	mp.game.ui.drawNotification(true, false);
});

//Backskirts mods
mp.events.add('Bsk1', (player) => {
	mp.players.local.vehicle.setMod(2, 1);
	mp.game.ui.setNotificationTextEntry("STRING");
	mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Back Bumper added!");
	mp.game.ui.drawNotification(true, false);

});
mp.events.add('Bsk2', (player) => {
	mp.players.local.vehicle.setMod(2, 2);
	mp.game.ui.setNotificationTextEntry("STRING");
	mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Back Bumper added!");
	mp.game.ui.drawNotification(true, false);
});
mp.events.add('Bsk3', (player) => {
	mp.players.local.vehicle.setMod(2, 3);
	mp.game.ui.setNotificationTextEntry("STRING");
	mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Back Bumper added!");
	mp.game.ui.drawNotification(true, false);
});
mp.events.add('Bsk4', (player) => {
	mp.players.local.vehicle.setMod(2, 4);
	mp.game.ui.setNotificationTextEntry("STRING");
	mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Back Bumper added!");
	mp.game.ui.drawNotification(true, false);
});
mp.events.add('Bsk5', (player) => {
	mp.players.local.vehicle.setMod(2, 5);
	mp.game.ui.setNotificationTextEntry("STRING");
	mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Back Bumper added!");
	mp.game.ui.drawNotification(true, false);

});
mp.events.add('Bsk6', (player) => {
	mp.players.local.vehicle.setMod(2, 6);
	mp.game.ui.setNotificationTextEntry("STRING");
	mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Back Bumper added!");
	mp.game.ui.drawNotification(true, false);
});
mp.events.add('Bsk7', (player) => {
	mp.players.local.vehicle.setMod(2, 7);
	mp.game.ui.setNotificationTextEntry("STRING");
	mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Back Bumper added!");
	mp.game.ui.drawNotification(true, false);
});

//Nitrous mods
mp.events.add('Bo1', (player) => {
	mp.players.local.vehicle.setMod(40, -1);
	mp.game.ui.setNotificationTextEntry("STRING");
	mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "No Nitrous added!");
	mp.game.ui.drawNotification(true, false);

});
mp.events.add('Bo2', (player) => {
	mp.players.local.vehicle.setMod(40, 0);
	mp.game.ui.setNotificationTextEntry("STRING");
	mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "20% Nitrous added!");
	mp.game.ui.drawNotification(true, false);
});
mp.events.add('Bo3', (player) => {
	mp.players.local.vehicle.setMod(40, 1);
	mp.game.ui.setNotificationTextEntry("STRING");
	mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "60% Nitrous added!");
	mp.game.ui.drawNotification(true, false);
});
mp.events.add('Bo4', (player) => {
	mp.players.local.vehicle.setMod(40, 2);
	mp.game.ui.setNotificationTextEntry("STRING");
	mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "100% Nitrous added!");
	mp.game.ui.drawNotification(true, false);
});
mp.events.add('Bo5', (player) => {
	mp.players.local.vehicle.setMod(40, 3);
	mp.game.ui.setNotificationTextEntry("STRING");
	mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Ram Boost added!");
	mp.game.ui.drawNotification(true, false);
});
	//Front Bumper mods
	mp.events.add('Fb1', (player) => {
		mp.players.local.vehicle.setMod(1, 1);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Front Bumper added!");
		mp.game.ui.drawNotification(true, false);

	});
	mp.events.add('Fb2', (player) => {
		mp.players.local.vehicle.setMod(1, 2);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Front Bumper added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Fb3', (player) => {
		mp.players.local.vehicle.setMod(1, 3);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Front Bumper added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Fb4', (player) => {
		mp.players.local.vehicle.setMod(1, 4);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Front Bumper added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Fb5', (player) => {
		mp.players.local.vehicle.setMod(1, 5);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Front Bumper added!");
		mp.game.ui.drawNotification(true, false);

	});
	mp.events.add('Fb6', (player) => {
		mp.players.local.vehicle.setMod(1, 6);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Front Bumper added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Fb7', (player) => {
		mp.players.local.vehicle.setMod(1, 7);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Front Bumper added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Fb8', (player) => {
		mp.players.local.vehicle.setMod(1, 8);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Front Bumper added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Fb9', (player) => {
		mp.players.local.vehicle.setMod(1, 9);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Front Bumper added!");
		mp.game.ui.drawNotification(true, false);

	});
	mp.events.add('Fb10', (player) => {
		mp.players.local.vehicle.setMod(1, 10);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Front Bumper added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Fb11', (player) => {
		mp.players.local.vehicle.setMod(1, 11);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Front Bumper added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Fb12', (player) => {
		mp.players.local.vehicle.setMod(1, 12);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Front Bumper added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Fb13', (player) => {
		mp.players.local.vehicle.setMod(1, 13);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Front Bumper added!");
		mp.game.ui.drawNotification(true, false);

	});
	mp.events.add('Fb14', (player) => {
		mp.players.local.vehicle.setMod(1, 14);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Front Bumper added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Fb15', (player) => {
		mp.players.local.vehicle.setMod(1, 15);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Front Bumper added!");
		mp.game.ui.drawNotification(true, false);

	});
	mp.events.add('Fb16', (player) => {
		mp.players.local.vehicle.setMod(1, 16);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Front Bumper added!");
		mp.game.ui.drawNotification(true, false);
	});

	//Side skirts mods
	mp.events.add('Side1', (player) => {
		mp.players.local.vehicle.setMod(3, 1);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Side Skirts added!");
		mp.game.ui.drawNotification(true, false);

	});
	mp.events.add('Side2', (player) => {
		mp.players.local.vehicle.setMod(3, 2);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Side Skirts added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Side3', (player) => {
		mp.players.local.vehicle.setMod(3, 3);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Side Skirts added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Side4', (player) => {
		mp.players.local.vehicle.setMod(3, 4);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Side Skirts added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Side5', (player) => {
		mp.players.local.vehicle.setMod(3, 5);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Side Skirts added!");
		mp.game.ui.drawNotification(true, false);

	});
	mp.events.add('Side6', (player) => {
		mp.players.local.vehicle.setMod(3, 6);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Side Skirts added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Side7', (player) => {
		mp.players.local.vehicle.setMod(3, 7);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Side Skirts added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Side8', (player) => {
		mp.players.local.vehicle.setMod(3, 8);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Side Skirts added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Side9', (player) => {
		mp.players.local.vehicle.setMod(3, 9);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Side Skirts added!");
		mp.game.ui.drawNotification(true, false);

	});
	mp.events.add('Side10', (player) => {
		mp.players.local.vehicle.setMod(3, 10);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Side Skirts added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Side11', (player) => {
		mp.players.local.vehicle.setMod(3, 11);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Side Skirts added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Side12', (player) => {
		mp.players.local.vehicle.setMod(3, 12);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Side Skirts added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Side13', (player) => {
		mp.players.local.vehicle.setMod(3, 13);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Side Skirts added!");
		mp.game.ui.drawNotification(true, false);

	});
	mp.events.add('Side14', (player) => {
		mp.players.local.vehicle.setMod(3, 14);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Side Skirts added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Side15', (player) => {
		mp.players.local.vehicle.setMod(3, 15);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Side Skirts added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Side16', (player) => {
		mp.players.local.vehicle.setMod(3, 16);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Side Skirts added!");
		mp.game.ui.drawNotification(true, false);
	});

	//Spoiler mods
	mp.events.add('Spo1', (player) => {
		mp.players.local.vehicle.setMod(0, 1);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Spoiler added!");
		mp.game.ui.drawNotification(true, false);

	});
	mp.events.add('Spo2', (player) => {
		mp.players.local.vehicle.setMod(0, 2);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Spoiler added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Spo3', (player) => {
		mp.players.local.vehicle.setMod(0, 3);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Spoiler added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Spo4', (player) => {
		mp.players.local.vehicle.setMod(0, 4);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Spoiler added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Spo5', (player) => {
		mp.players.local.vehicle.setMod(0, 5);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Spoiler added!");
		mp.game.ui.drawNotification(true, false);

	});
	mp.events.add('Spo6', (player) => {
		mp.players.local.vehicle.setMod(0, 6);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Spoiler added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Spo7', (player) => {
		mp.players.local.vehicle.setMod(0, 7);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Spoiler added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Spo8', (player) => {
		mp.players.local.vehicle.setMod(0, 8);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Spoiler added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Spo9', (player) => {
		mp.players.local.vehicle.setMod(0, 9);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Spoiler added!");
		mp.game.ui.drawNotification(true, false);

	});
	mp.events.add('Spo10', (player) => {
		mp.players.local.vehicle.setMod(0, 10);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Spoiler added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Spo11', (player) => {
		mp.players.local.vehicle.setMod(0, 11);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Spoiler added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Spo12', (player) => {
		mp.players.local.vehicle.setMod(0, 12);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Spoiler added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Spo13', (player) => {
		mp.players.local.vehicle.setMod(0, 13);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Spoiler added!");
		mp.game.ui.drawNotification(true, false);

	});
	mp.events.add('Spo14', (player) => {
		mp.players.local.vehicle.setMod(0, 14);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Spoiler added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Spo15', (player) => {
		mp.players.local.vehicle.setMod(0, 15);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Spoiler added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('Spo16', (player) => {
		mp.players.local.vehicle.setMod(0, 16);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Spoiler added!");
		mp.game.ui.drawNotification(true, false);
	});

	//Brakes mods
	mp.events.add('br1', () => {
		mp.players.local.vehicle.setMod(12, -1);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Standard Brakes added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('br2', () => {
		mp.players.local.vehicle.setMod(12, 0);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Street Brakes added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('br3', () => {
		mp.players.local.vehicle.setMod(12, 1);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Sport Brakes added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('br4', () => {
		mp.players.local.vehicle.setMod(12, 2);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Race Brakes added!");
		mp.game.ui.drawNotification(true, false);
	});


	//Suspension mods
	mp.events.add('su1', () => {
		mp.players.local.vehicle.setMod(15, -1);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Standard Suspension added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('su2', () => {
		mp.players.local.vehicle.setMod(15, 0);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Lower Suspension added!");
		mp.game.ui.drawNotification(true, false);
	});

	mp.events.add('su3', () => {
		mp.players.local.vehicle.setMod(15, 1);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Street Suspension added!");
		mp.game.ui.drawNotification(true, false);
	});

	mp.events.add('su4', () => {
		mp.players.local.vehicle.setMod(15, 2);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Sport Suspension added!");
		mp.game.ui.drawNotification(true, false);
	});

	mp.events.add('su5', () => {
		mp.players.local.vehicle.setMod(15, 3);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Race Suspension added!");
		mp.game.ui.drawNotification(true, false);
	});


	//Turbo
	mp.events.add('menuTurboToServer', () => {
		mp.players.local.vehicle.setMod(18, 0);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Turbo tuning added!");
		mp.game.ui.drawNotification(true, false);
	});

	//Tranmission
	mp.events.add('trs1', () => {
		mp.players.local.vehicle.setMod(13, -1);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Standard Transmission added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('trs2', () => {
		mp.players.local.vehicle.setMod(13, 0);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Street Transmission added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('trs3', () => {
		mp.players.local.vehicle.setMod(13, 1);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Sport Transmission added!");
		mp.game.ui.drawNotification(true, false);
	});
	mp.events.add('trs4', () => {
		mp.players.local.vehicle.setMod(13, 2);
		mp.game.ui.setNotificationTextEntry("STRING");
		mp.game.ui.setNotificationMessage("CHAR_CARSITE3", "CHAR_CARSITE3", false, 4, "Vehicle tuner", "Race Transmission added!");
		mp.game.ui.drawNotification(true, false);
	});



	mp.events.add('cmenuDone', () => {
		carMenu.destroy();
		carMenuOpen = false;
		mp.gui.cursor.show(false, false);
	})

	mp.events.add('cmenuCursor', () => {
		mp.gui.cursor.show(true, true);
	});

	mp.events.add("menuOkayToServer", () => {
		mp.events.callRemote('CmenuOkay');
	});





