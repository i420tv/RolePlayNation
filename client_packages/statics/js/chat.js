const bufferSize = 10;
const maxChatRows = 50;

let chatInput = undefined;
let chatContainer = undefined;
let currentMessage = undefined;
let previousMessages = [];

var chatAPI = {
	push: (message) => {
        // Check if the buffer is full
        if(chatContainer.childNodes.length === maxChatRows) {
            // Delete the oldest message
            chatContainer.removeChild(chatContainer.firstChild);
        }
    
        // Create the element
        let textElement = document.createElement('p');
        textElement.textContent = message;
        textElement.style.padding = '3px';
    
        // Add the element to the child nodes
        chatContainer.appendChild(textElement);
        chatContainer.scrollTop = chatContainer.scrollHeight;
    },	
	clear: clearChat,	
	activate: focusChat,	
	show: focusChat
};

$(document).ready(function() {
    // Retrieve the chat container and input
    chatInput = document.getElementById('chat-input');
    chatContainer = document.getElementById('chat_messages');

    // Remove the default behaviour of the UP key
    chatInput.addEventListener('keydown', function(event) {
        if(event.keyCode === 38) {
            event.preventDefault();
        }
    });

    // Handle keys for the chat input
    chatInput.addEventListener('keyup', function(event) {
        switch(event.keyCode) {
            case 13:
                // ENTER key pressed
                if(chatInput.value.trim().length > 0) {
                    // Get the message on the input
                    let message = chatInput.value.trim();
                    chatInput.value = '';

                    if(previousMessages[bufferSize - 1] !== undefined) {
                        // The buffer is full, remove the first element
                        previousMessages.splice(0, 1);
                    }

                    // Store the message into the array
                    previousMessages.push(message);
                    chatInput.value = '';

                    // Send the message or command
                    if(message.startsWith('/')) {
                        // Remove the slash
                        message = message.substring(1);

                        if(message.length > 0) {
                            // Invoke the command
                            mp.invoke("command", message);
                        }
                    } else {
                        // Send the chat message
                        mp.invoke('chatMessage', message);
                    }
                }

                // Disable the chat input
                mp.trigger('toggleChatOpen', false);

                break;
            case 27:
                // ESCAPE key pressed
                chatInput.value = '';

                // Disable the chat input
                mp.trigger('toggleChatOpen', false);
                break;
            case 38:
                // UP ARROW key pressed
                if(previousMessages.length > 0) {

                    if(currentMessage === undefined || currentMessage === previousMessages.length - 1) {
                        currentMessage = 0;
                    } else {
                        currentMessage++;
                    }

                    // Show the message on the chat
                    chatInput.value = previousMessages[currentMessage];
                }

                // Set the cursor on the last position
                chatInput.setSelectionRange(chatInput.value.length, chatInput.value.length);

                break;
            case 40:
                // DOWN ARROW key pressed
                if(previousMessages.length > 0) {

                    if(currentMessage === undefined || currentMessage === 0) {
                        currentMessage = previousMessages.length - 1;
                    } else {
                        currentMessage--;
                    }

                    // Show the message on the chat
                    chatInput.value = previousMessages[currentMessage];
                }

                // Set the cursor on the last position
                chatInput.setSelectionRange(chatInput.value.length, chatInput.value.length);

                break;
        }
    });
});

function printChatMessage(message) {
    // Check if the buffer is full
    if(chatContainer.childNodes.length === maxChatRows) {
        // Delete the oldest message
        chatContainer.removeChild(chatContainer.firstChild);
    }

    // Create the element
    let textElement = document.createElement('p');
    textElement.textContent = message;
    textElement.style.padding = '3px';

    // Add the element to the child nodes
    chatContainer.appendChild(textElement);
    chatContainer.scrollTop = chatContainer.scrollHeight;
}

function clearChat() {
    // Remove all the messages from the chat
    while(chatContainer.firstChild) {
        // Delete each message
        chatContainer.removeChild(chatContainer.firstChild);
    }
}

function focusChat() {
    // Set the focus on the chat input
    chatContainer.classList.remove('disabled');
    chatInput.disabled = false;
    chatInput.focus();
}

function disableChatInput() {
    // Disables the messages on the chat
    chatInput.disabled = true;
    chatContainer.classList.add('disabled');
}