const localPlayer = mp.players.local;
let radioKillTimer = null;

mp.game.audio.setUserRadioControlEnabled(true);

mp.events.add("disableVehicleRadio", () => {
	if (radioKillTimer == null) {
		radioKillTimer = setInterval(() => {
			if (localPlayer.isSittingInAnyVehicle()) {
				mp.game.audio.setRadioToStationName("OFF");
				clearInterval(radioKillTimer);
				radioKillTimer = null;
				return;
			}
		}, 100);
	}
});