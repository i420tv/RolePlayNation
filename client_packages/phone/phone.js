let browser = null;

mp.keys.bind(72, false, function () {
    if (browser != null) {
        browser.destroy();
        browser = null;
        mp.gui.cursor.show(false, false);
        return
    }
    if (mp.gui.cursor.visible)
        return;
    
    browser = mp.browsers.new('package://phone/mobile.html');
    browser.execute(`let name = "${mp.players.local.name}"`)
    mp.gui.cursor.show(true, true);
})
