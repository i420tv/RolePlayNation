const maxDistance = 15 * 15;
const width = 0.03;
const height = 0.0065;
const border = 0.001;
const color = [255, 255, 255, 255];

mp.nametags.enabled = true;

mp.events.add('playerRenamed', (id, fakeName) => {
    mp.players.atRemoteId(id).name = fakeName;
})

mp.events.add('render', (nametags) => {
    const graphics = mp.game.graphics;
    const screenRes = graphics.getScreenResolution(0, 0);


    nametags.forEach(nametag => {
        let [player, x, y, distance] = nametag;

        let playerName = "";

        let localPlayer = mp.players.localPlayer;

        var givenName = localPlayer.getVariable(player.remoteId);

        if (givenName != null) {

            playerName = givenName;
        }
        else {
            playerName = "( " + player.remoteId + "wooof )";
        }

        const position = player.position;

        if (distance <= maxDistance) {
            let scale = (distance / maxDistance);
            if (scale < 0.6) scale = 0.6;
            y -= scale * (0.005 * (screenRes.y / 1080));

            mp.game.graphics.drawText(playerName, [position.x, position.y, position.z + 1.2], { font: 4, color: [255, 255, 255, 255], scale: [0.42, 0.42], outline: true })
        }
    })
})