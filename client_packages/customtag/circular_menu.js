global.raycast_id = -1;
global.raycast_type = "";
function getLookingAtEntity() {
	let player = mp.players.local
	var startPosition = player.getBoneCoords(12844, 0.5, 0, 0);
	var resolution = mp.game.graphics.getScreenActiveResolution(1, 1);
	var secondPoint = mp.game.graphics.screen2dToWorld3d([resolution.x / 2, resolution.y / 2, 2 | 4 | 8]);
	if (secondPoint == undefined) return null;

	startPosition.z -= 0.3;
	var result = mp.raycasting.testPointToPoint(startPosition, secondPoint, player, 2 | 4 | 8 /*| 16*/);

	if (typeof result !== 'undefined') {
		if (typeof result.entity.type === 'undefined') return null;
		
		var entPos = result.entity.position;
		var lPos = player.position;
		if (mp.game.gameplay.getDistanceBetweenCoords(entPos.x, entPos.y, entPos.z, lPos.x, lPos.y, lPos.z, true) > 8) return null;
		return result.entity;
	}
	return null;
}


mp.events.add(
{
	"render" : () => {
		const player = mp.players.local;
		if(global.auth === true && global.interfaceStatus === true) {
			let remoteId = -1;
			let entityType = "";
			
			let entity = getLookingAtEntity();
			if (entity !== null) {
				if (entity.type == "player") {
					if (entity.model == 1885233650 || entity.model == 2627665880) {
						remoteId = entity.remoteId
						entityType = entity.type
						if(entity.getVariable("invisible") !== true) {
							mp.game.graphics.drawText("E", [entity.position.x, entity.position.y, entity.position.z], {
								font: 0,
								color: [255, 255, 255, 185],
								scale: [0.4, 0.4],
								outline: true
							});
						}
					}
				} else {
					if (entity.type == "vehicle") {
						remoteId = entity.remoteId
						entityType = entity.type
						mp.game.graphics.drawText("E", [entity.position.x, entity.position.y, entity.position.z], {
							font: 0,
							color: [255, 255, 255, 185],
							scale: [0.4, 0.4],
							outline: true
						});
					}
				}
			}
			
			if(global.raycast_id !== remoteId || global.raycast_type !== entityType) {
				global.raycast_id = remoteId;
				mp.gui.execute(`window.setCircularEntity(${remoteId}, '${entityType}');`);
			}
		}
	},
	"cicularClick" : () =>
	{
		mp.events.callRemote("cicularClick");
	},	
	"vehicleLock" : (vehicle_id) =>
	{
		mp.events.callRemote("vehicleLock", vehicle_id);
	},	
	"setCircularFaction" : (faction) =>
	{
		mp.gui.emit("setCircularFaction", faction);		
	},
	"circular_money" : (player_id) =>
	{
		mp.events.callRemote("circular_money", player_id);
	},
	"circular_business" : (player_id) =>
	{
		mp.events.callRemote("circular_business", player_id);
	},
	"circular_vehicle" : (player_id) =>
	{
		mp.events.callRemote("circular_vehicle", player_id);
	},
	"circular_house" : (player_id) =>
	{
		mp.events.callRemote("circular_house", player_id);
	},
	"circular_friend" : (player_id) =>
	{
		mp.events.callRemote("circular_friend", player_id);
	},
	"circular_identity" : (player_id) =>
	{
		mp.events.callRemote("circular_identity", player_id);
	},
	"circular_passport" : (player_id) =>
	{
		mp.events.callRemote("circular_passport", player_id);
	},
	"circular_weapon" : (player_id) =>
	{
		mp.events.callRemote("circular_weapon", player_id);
	},
	"circular_driving" : (player_id) =>
	{
		mp.events.callRemote("circular_driving", player_id);
	},
	"circular_trade" : (player_id) =>
	{
		mp.events.callRemote("circular_trade", player_id);
	},
	"circular_cuff" : (player_id) =>
	{
		mp.events.callRemote("circular_cuff", player_id);
	},
	"circular_lead" : (player_id) =>
	{
		mp.events.callRemote("circular_lead", player_id);
	},
	"circular_cput" : (player_id) =>
	{
		mp.events.callRemote("circular_cput", player_id);
	},
	"circular_eject" : (player_id) =>
	{
		mp.events.callRemote("circular_eject", player_id);
	},
	"circular_takelic" : (player_id) =>
	{
		mp.events.callRemote("circular_takelic", player_id);
	},
	"circular_take" : (player_id) =>
	{
		mp.events.callRemote("circular_take", player_id);
	},
	"circular_frisk" : (player_id) =>
	{
		mp.events.callRemote("circular_frisk", player_id);
	},
	"circular_arrest" : (player_id) =>
	{
		mp.events.callRemote("circular_arrest", player_id);
	},
	"circular_lic_weapon" : (player_id) =>
	{
		mp.events.callRemote("circular_lic_weapon", player_id);
	},
	"circular_clear" : (player_id) =>
	{
		mp.events.callRemote("circular_clear", player_id);
	},
	"circular_invite" : (player_id) =>
	{
		mp.events.callRemote("circular_invite", player_id);
	},
	"circular_giveplus" : (player_id) =>
	{
		mp.events.callRemote("circular_giveplus", player_id);
	},
	"circular_giveminus" : (player_id) =>
	{
		mp.events.callRemote("circular_giveminus", player_id);
	},
	"circular_repr" : (player_id) =>
	{
		mp.events.callRemote("circular_repr", player_id);
	},
	"circular_uninvite" : (player_id) =>
	{
		mp.events.callRemote("circular_uninvite", player_id);
	}		
});








