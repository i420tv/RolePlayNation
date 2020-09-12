let browser = null;
let player = mp.players.local;

mp.keys.bind(0x75, false, function () {
    if (browser != null) {
        browser.destroy();
        browser = null;
        mp.gui.cursor.show(false, false);
        return
    }
    if (mp.gui.cursor.visible)
        return;
    
    browser = mp.browsers.new('package://AnimMenu/Animmenu.html');
    mp.gui.cursor.show(true, true);
})


function openCity(evt, cityName) {
	var i, tabcontent, tablinks;
	tabcontent = document.getElementsByClassName("tabcontent");
	for (i = 0; i < tabcontent.length; i++) {
	  tabcontent[i].style.display = "none";
	}
	tablinks = document.getElementsByClassName("tablinks");
	for (i = 0; i < tablinks.length; i++) {
	  tablinks[i].className = tablinks[i].className.replace(" active", "");
	}
	document.getElementById(cityName).style.display = "block";
	evt.currentTarget.className += " active";
}


mp.events.add('triggerbutton', () => {
	if(player.vehicle){
		mp.gui.chat.push("You are in a vehicle!");
	}
	else{
	mp.events.callRemote("PlayAnim1");
	browser.destroy();
	browser = null;
	mp.gui.cursor.show(false, false);
	}
});

mp.events.add('triggerbutton2', () => {
	if(player.vehicle){
		mp.gui.chat.push("You are in a vehicle!");
	}
	else{
	mp.events.callRemote("CryCommand");
	browser.destroy();
	browser = null;
	mp.gui.cursor.show(false, false);
	}
});


mp.events.add('stopAnim', () => {
	if(player.vehicle){
		mp.gui.chat.push("You are in a vehicle!");
	}
	else{
	mp.events.callRemote("Stopanim");
	browser.destroy();
	browser = null;
	mp.gui.cursor.show(false, false);
	}
});


