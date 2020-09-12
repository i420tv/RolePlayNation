// Copyright © 2019 by CommanderDonkey
// Read README.md for using

let speedo = mp.browsers.new("package://speedometer/index.html");
let showed = false;
let player = mp.players.local;

global.localPlayer 	= mp.players.local;

let gas = 0;
let carhp = 100;


mp.events.add('getFuel', (player, args) => {
	
	gas = `${ args }` * 2;


})




mp.events.add('render', () =>
{
	const vehicle = localPlayer.vehicle;

	if (player.vehicle && player.vehicle.getPedInSeat(-1) === player.handle)
	{

		if(showed === false)
		{
			speedo.execute("showSpeedo();");
			showed = true;
		}

		let vehhp1 = player.vehicle.getHealth() / 10 + 6;
		let vehhp = (vehhp1).toFixed(0);

		let vel1 = player.vehicle.getSpeed() * 2.236936;
		let vel = (vel1).toFixed(0);

		speedo.execute(`update(${vel}, ${gas}, ${vehhp});`);

		mp.game.graphics.notify(gas);
	}
	else
	{
		if(showed)
		{
			speedo.execute("hideSpeedo();");
			showed = false;
		}
	}
	
	if(vehicle){
		if(mp.game.vehicle.isThisModelACar(vehicle.model))
		{
			const roll = vehicle.getRoll();

			if(vehicle.isInAir() || roll > 75.0 || roll < -75.0)
			{
				mp.game.controls.disableControlAction(0, 59, true);
				mp.game.controls.disableControlAction(0, 60, true);
				mp.game.controls.disableControlAction(2, 59, true);
				mp.game.controls.disableControlAction(2, 60, true);
			}
		}
	}
});