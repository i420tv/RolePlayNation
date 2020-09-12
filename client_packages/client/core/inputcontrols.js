//const localPlayer = mp.players.local;
var test = false;

global.localPlayer 	= mp.players.local;

mp.events.add('checkChatOn', () => {
    test = true;
    inputOff();
});
mp.events.add('checkChatOff', () => {
    test = false;
    inputOn();
});

mp.events.add("render", () => {

    mp.game.player.setHealthRechargeMultiplier(0.0);

    mp.game.controls.disableControlAction(2, 243, true);
	

	mp.game.controls.disableControlAction(1, 7, true);

    if (mp.players.local.vehicle) {
        mp.game.audio.setRadioToStationName("OFF");
        mp.game.audio.setUserRadioControlEnabled(false);

        var height = mp.players.local.getHeightAboveGround();
        mp.game.graphics.notify(height);
    }

    if(mp.players.local.vehicle)
    {
    	if(mp.players.local.vehicle.doesHaveWeapons())
    	{
			mp.game.controls.disableControlAction(2, 68, true);
			mp.game.controls.disableControlAction(2, 69, true);
			mp.game.controls.disableControlAction(2, 70, true);
    	}
    }


});

function inputOn() {

    mp.events.call('inputEnabled');
    mp.events.callRemote('EnableInputSS');
}

function inputOff() {

    mp.events.call('inputDisabled');
    mp.events.callRemote('DisableInputSS');
}