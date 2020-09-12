let chat =
{
    size: 0,
    container: null,
    input: null,
    enabled: false,
    active: true,
    fadeTimer: null,
    inputHistory: [],
    inputIndex: -1,
};

function chatHeight(size)
{
    $("#chat_messages").css("height", `${size*2.0}rem`);
}

function chatSize(size)
{
    $("html").css("font-size", `${size}px`);
}

function chatFadeOut(time)
{
    if(chat.input != null) return ;
    
    if(chat.fadeTimer != null) clearTimeout(chat.fadeTimer);

    chat.fadeTimer = setTimeout(function() {
        chat.fadeTimer = null;

        $("#chat_messages").fadeTo(1000, 0.00);
    }, time);				
}

function enableChatInput(enable)
{
    if(chat.active == false && enable == true) return ;
    
    if(enable != (chat.input != null))
    {
        mp.invoke("focus", enable);

        if (enable)
        {
            chat.input = $("#chat").append(`
                <div id="input_form">
                    <div>
                        <input id="chat_msg" type="text" spellcheck="false" />
                    </div>
                </div>
            `).children(":last");

            $("#chat_msg").focus();

            $("#chat_messages").addClass("scrollable");
            $("#chat_messages").fadeTo(0, 1.0);

            if(chat.fadeTimer != null) 
            {
                clearTimeout(chat.fadeTimer);
                chat.fadeTimer = null;
            }

            chat.inputIndex = -1;
        } 
        else
        {
            $("#input_form").fadeOut(0, function()
            {
                chat.input.remove();
                chat.input = null;
                $("#chat_messages").removeClass("scrollable");
                chatFadeOut(5000);
            });
        }
    }
}

function putInCaht(text)
{
    let current_val = $("#chat_msg").val();
    

    if(!current_val) $("#chat_msg").focus().val(text);
    else $("#chat_msg").focus().val(text+current_val);
}

var chatAPI =
{
    push: (text) =>
    {
        $("#chat_messages").fadeTo(0, 1.0);
        

        let colorPositions = [];
        let colors = [];
        let chatElement = "<li>";

        for (let i = 0; i < text.length; i++) {
            let colorCheck = `${text[i]}${text[i + 1]}${text[i + 2]}`;

            if (colorCheck === "!{#") {
                colorPositions.push(i);
            }
        }

        colorPositions.forEach(el => {
            let sub = text.slice(el, -1);
            colors.push(sub.slice(3, 9));
        });

        colorPositions.forEach((el, i) => {
            let sub = text.slice(colorPositions[i] + 10, colorPositions[i + 1]);
            chatElement += `<span style='color: ${colors[i]}'>${sub}</span>`;
        });

        chatElement += "</li>";

        if (chatElement === "<li></li>") {
            chat.container.prepend("<li>" + text + "</li>");
        } else {
            chat.container.prepend(chatElement);
        }

        chat.size++;

        if (chat.size >= 50)
        {
            chat.container.children(":last").remove();
        }

        chatFadeOut(10000);
    },
    
    clear: () =>
    {
        chat.container.html("");
    },
    
    activate: (toggle) =>
    {
        if(toggle == false && chat.input != null) 
        {
            enableChatInput(false);
        }
            
        chat.active = toggle;
    },
    
    show: (toggle) =>
    {
        if(toggle) 	$("#chat").show();
        else		$("#chat").hide();
        
        chat.active = toggle;
    },

    fontsize: (size) =>
    {
        $("html").css("font-size", `${size}px`);
    },
};

$(document).ready(function()
{
    chat.container = $("#chat ul#chat_messages");
    
    $(".ui_element").show();

    chatAPI.show(false);

    chatAPI.push("Welcome to Vital RP!");

    chatHeight(11);

    $("body").keyup(function(event)
    {
        if (event.which == 84 && chat.input == null && chat.active == true)
        {
            enableChatInput(true);
            event.preventDefault();
        } 
        else if (event.which == 13 && chat.input != null)
        {
            var value = $("#chat_msg").val();

            if(value.length > 0) 
            {
                if(chat.inputHistory[0] != value) chat.inputHistory.unshift(value);

                if(chat.inputHistory.length > 50) chat.inputHistory.splice(chat.inputHistory.length-1, 1);

                if(value[0] == "/")
                {
                    value = value.substr(1);

                    if(value.length > 0) mp.invoke("command", value);
                }
                else
                {
                    mp.invoke("chatMessage", value);
                }
            }

            $("#chat_messages").scrollTop(0);

            enableChatInput(false);
        }
        else if(event.which == 38 && chat.input != null) // arrow up
        {
            let index = chat.inputIndex + 1;

            if(chat.inputHistory[index]) 
            {
                $("#chat_msg").focus().val(chat.inputHistory[index]);
                chat.inputIndex = index;
            }
        }
        else if(event.which == 40 && chat.input != null) // arrow down
        {
            let index = chat.inputIndex - 1;

            if(chat.inputHistory[index]) 
            {
                $("#chat_msg").focus().val(chat.inputHistory[index]);
                chat.inputIndex = index;
            }
            else
            {
                if(chat.inputIndex != -1)
                {
                    $("#chat_msg").focus().val("");
                    chat.inputIndex = -1;
                }
            }
        }
    });
});	