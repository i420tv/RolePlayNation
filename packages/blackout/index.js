// Toggle blackout mode with /toggleblackout (Serverside)
mp.events.addCommand("toggleblackout", (player) => {
    mp.world.blackout.enabled = !mp.world.blackout.enabled;
    player.outputChatBox(`Blackout ${mp.world.blackout.enabled ? `enabled` : `disabled`}.`);
});

mp.world.blackout = {
    _enabled: false,

    get enabled() {
        return this._enabled;
    },

    set enabled(newState) {
        this._enabled = newState;
        mp.players.call("SetBlackoutState", [this._enabled]);
    }
};

mp.events.add("playerReady", (player) => {
    player.call("SetBlackoutState", [mp.world.blackout.enabled]);
});