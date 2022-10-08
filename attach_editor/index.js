const fs = require('fs');

const objectsList = [
	'prop_fishing_rod_01',
	'prop_ld_fireaxe'
];

const bodyParts = [
 	{
		 name: 'Skel root',
		 id: 0
	},

	{
		name: 'Right hand',
		id: 57005
	},

	{
		name: 'Left hand',
		id: 18905
	},

	{
		name: 'Head',
		id: 12844
	}
];

mp.events.addCommand('attach', (player, _, object, body) => {
	let len = objectsList.length; 

	if(object == undefined) {
		player.outputChatBox('!{#ff0000}/attach [ID object] [ID body part]');

		let msg = '';

		for(let i = 0; i < len; i++) {
			msg +=  '(' +i+ ')'+ objectsList[i]+ ' | ';
		}
		player.outputChatBox(msg);
		return;
	}

	let id = parseInt(object);
	if(id < 0 || id > len) {
		return;
	}

	let lenBody = bodyParts.length;

	if(body == undefined) {
		let msg = '';

		for(let i = 0; i < lenBody; i++) {
			msg += '(' +i+ ')'+ bodyParts[i].name+ ' | ';
		}
		player.outputChatBox(msg);
		return;
	}

	let bodyID = parseInt(body);
	if(bodyID < 0 || bodyID > lenBody) {
		return;
	}

	player.call("attachObject", [ objectsList[id], bodyParts[bodyID].id ]);
});

mp.events.add('finishAttach', (player, object) => {

	let objectJSON = JSON.parse(object);
	let text = `{ ${objectJSON.object}, ${objectJSON.body}, ${objectJSON.x.toFixed(4)}, ${objectJSON.y.toFixed(4)}, ${objectJSON.z.toFixed(4)}, ${objectJSON.rx.toFixed(4)}, ${objectJSON.ry.toFixed(4)}, ${objectJSON.rz.toFixed(4)} },\r\n`;
	
	player.outputChatBox(text);

	fs.appendFile('./attachments.txt', text, err => {

		if (err) {
		  console.error(err)
		  return
		}
	});
});