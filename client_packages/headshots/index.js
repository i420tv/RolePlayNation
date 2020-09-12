const localPlayer = mp.players.local;

mp.events.add("render", () => {
    if (localPlayer.hasBeenDamagedByAnyPed()) {
        if (localPlayer.getLastDamageBone(0) === 31086) {
            let lastHitBy = null;

            mp.players.forEachInStreamRange((player) => {
                if (localPlayer.hasBeenDamagedBy(player.handle, true)) lastHitBy = player;
            });

            if (lastHitBy) {
                let pos = localPlayer.position;
                mp.game.gameplay.shootSingleBulletBetweenCoords(pos.x, pos.y, pos.z, pos.x, pos.y, pos.z + 0.055, 10000, false, lastHitBy.weapon, lastHitBy.handle, false, false, 9000.0);
            }
        }

        localPlayer.clearLastDamage();
    }
});