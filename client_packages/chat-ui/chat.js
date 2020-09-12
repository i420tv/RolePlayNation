mp.events.add('SendToChat', (player, message) => {

    if (player.getVariable(player.remoteId) != null) {
        let playerName = player.getVariable(player.remoteId);

        mp.gui.chat.push(`${playerName} (${player.remoteId}): ${message}`);
    }
    else {

        if (player != mp.players.local) {
            mp.gui.chat.push(`Player ${player.remoteId}: ${message}`);
        }
    }

    if (player == mp.players.local)
    {
        mp.gui.chat.push(`${player.name} says: ${message}`);
    }

});
