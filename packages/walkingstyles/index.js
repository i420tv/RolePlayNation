const walkingStyles = [
    {Name: "Normal", AnimSet: null},
    {Name: "Brave", AnimSet: "move_m@brave"},
    {Name: "Confident", AnimSet: "move_m@confident"},
    {Name: "Drunk", AnimSet: "move_m@drunk@verydrunk"},
    {Name: "Fat", AnimSet: "move_m@fat@a"},
    {Name: "Gangster", AnimSet: "move_m@shadyped@a"},
    {Name: "Hurry", AnimSet: "move_m@hurry@a"},
    {Name: "Injured", AnimSet: "move_m@injured"},
    {Name: "Intimidated", AnimSet: "move_m@intimidation@1h"},
    {Name: "Quick", AnimSet: "move_m@quick"},
    {Name: "Sad", AnimSet: "move_m@sad@a"},
    {Name: "Tough", AnimSet: "move_m@tool_belt@a"}
];

mp.events.add("requestWalkingStyles", (player) => {
    player.call("receiveWalkingStyles", [JSON.stringify(walkingStyles.map(w => w.Name))]);
});

mp.events.add("setWalkingStyle", (player, styleIndex) => {
    if (styleIndex < 0 || styleIndex >= walkingStyles.length) return;
    player.data.walkingStyle = walkingStyles[styleIndex].AnimSet;
    player.outputChatBox(`Walking style set to ${walkingStyles[styleIndex].Name}.`);
});