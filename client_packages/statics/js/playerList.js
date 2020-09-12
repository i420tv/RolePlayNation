$(document).ready(function() {
	i18next.use(window.i18nextXHRBackend).init({
		backend: {
			loadPath: '../i18n/en.json'
		}
	}, function(err, t) {
		jqueryI18next.init(i18next, $, {
			optionsAttr: 'i18n-options',
			useOptionsAttr: true,
			parseDefaultValueFromContent: true
		});

		$(document).localize();
    });
});

function updatePlayerList(playersJson) {
	// Get the player list
	let players = JSON.parse(playersJson);

	// Update the online counter
	document.getElementById('online').textContent = i18next.t('players.online') + players.length;

	// Remove all the children
	let container = document.getElementById('player-container');

	while(container.firstChild) {
		// Remove the player
		container.removeChild(container.firstChild);
	}

	// Show all the players
	for(let i = 0; i < players.length; i++) {
		// Get the player object
		let connectedPlayer = players[i];

		// Create the container element
		let playerRow = document.createElement('div');

		// Create the children
		let playerIdColumn = document.createElement('div');
		let playerNameColumn = document.createElement('div');
		let playerPingColumn = document.createElement('div');

		// Add the classes to the elements
		playerIdColumn.classList.add('player-id');
		playerNameColumn.classList.add('player-name');
		playerPingColumn.classList.add('player-ping');

		// Add the text for each player
		playerIdColumn.textContent = connectedPlayer.playerId;
		playerNameColumn.textContent = connectedPlayer.playerName;
		playerPingColumn.textContent = connectedPlayer.playerPing + ' ms';
		
		// Add the row to the parent
		playerRow.appendChild(playerIdColumn);
		playerRow.appendChild(playerNameColumn);
		playerRow.appendChild(playerPingColumn);
		container.appendChild(playerRow);
	}
}