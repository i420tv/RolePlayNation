mp.events.add("toggleIndicator", (player, indicatorID) => {
    let vehicle = player.vehicle;
    if (vehicle && player.seat == -1) {
        switch (indicatorID) {
            // Right
            case 0:
                vehicle.data.IndicatorRight = !vehicle.data.IndicatorRight;
            break;

            // Left
            case 1:
                vehicle.data.IndicatorLeft = !vehicle.data.IndicatorLeft;
            break;
        }
    }
});