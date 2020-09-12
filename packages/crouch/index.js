mp.events.add("playerDeath", (player) => {
    player.data.isCrouched = false;
});

mp.events.add("toggleCrouch", (player) => {
    if (player.data.isCrouched === undefined) {
        player.data.isCrouched = true;
    } else {
        player.data.isCrouched = !player.data.isCrouched;
    }
});