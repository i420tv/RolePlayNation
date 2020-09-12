import { map } from "async";

const KEYs = {
    OPEN_CREATOR: 75/* k */,
    BIND1: 120/* 1 */,
    BIND2: 121/* 2 */,
};


let CEF_Creator = null;
let bind1 = '';
let bind2 = '';

function toggleCreator()
{
    bind1 = '';
    bind2 = '';
    if(CEF_Creator === null)
    {
        CEF_Creator = mp.browsers.new("package://BlipCreator/CEF/index.html");

        mp.keys.bind(KEYs.OPEN_CREATOR, false, pressToOpenCreator);
        mp.keys.bind(KEYs.BIND1, false, pressBind1);
        mp.keys.bind(KEYs.BIND2, false, pressBind2);

        mp.events.add("be:bind1", (data) => bind1 = data);
        mp.events.add("be:bind2", (data) => bind2 = data);
        mp.events.add("be:clear", () =>{ bind1 = ''; bind2 = '';});
        mp.events.add("be:blipCreate", (data) => { mp.events.callRemote("blipCreate", data); });
        mp.gui.chat.push("!{#ff9a16}[ BlipCreator ]!{#FFF} Creator activated, press the button to open. !{#FF0000}First time can take time to open");
    }
    else
    {
        mp.keys.unbind(KEYs.OPEN_CREATOR, false, pressToOpenCreator);
        mp.keys.unbind(KEYs.BIND1, false, pressBind1);
        mp.keys.unbind(KEYs.BIND2, false, pressBind2);

        mp.events.remove("be:bind1");
        mp.events.remove("be:bind2");
        mp.events.remove("be:clear");
        mp.events.remove("be:blipCreate");
        CEF_Creator.destroy();
        CEF_Creator = null;
        mp.gui.chat.push("!{#ff9a16}[ BlipCreator ]!{#FFF} Creator deactivated");
    }
}
/* Create Function */
function blipcreated(key){ CEF_Creator.execute(`toastr.success("Key ${key} pressed, blip created.","Success");`); }
function pressToOpenCreator()
{
    CEF_Creator.execute('toggleCreator();');
}
function pressBind1()
{
    if(bind1.length > 0) {
        mp.events.callRemote("blipCreate", bind1);
        blipcreated("1");
    }
}
function pressBind2()
{
    if(bind2.length > 0){
        mp.events.callRemote("blipCreate", bind2);
        blipcreated("2");
    } 
}

/* SHARED EVENTS */
mp.events.add("be:disableChat", ()=> mp.gui.chat.activate(false));
mp.events.add("be:enableChat", ()=> mp.gui.chat.activate(true));
mp.events.add("be:blipCreator", ()=> toggleCreator());

/** Editor */
mp.events.add("be:gotoPos", (x,y,z) =>
{
    mp.players.local.position = new mp.Vector3(x,y,z);
});
mp.events.add("be:delBlip", (id) =>
{
    mp.events.callRemote('delBlip', id);
});
var CEF_Editor = null;
mp.events.add("be:showEditor", (data) => 
{
    CEF_Editor = mp.browsers.new("package://BlipCreator/CEF/editor.html");
    CEF_Editor.execute(`showEditor('${data}');`);
});
mp.events.add("be:closeEdit", ()=>
{
    if(CEF_Editor !== null)
    {        
        CEF_Editor.destroy();
        CEF_Editor = null;
    }
});