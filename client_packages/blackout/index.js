mp.game.blackout = {
    _enabled: false,

    get enabled() {
        return this._enabled;
    },

    set enabled(newState) {
        this._enabled = newState;
        for (let i = 0; i <= 16; i++) mp.game.graphics.setLightsState(i, newState);
    }
};

mp.events.add("SetBlackoutState", (newState) => {
    mp.game.blackout.enabled = newState;
});