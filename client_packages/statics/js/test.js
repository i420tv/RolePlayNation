let questions = [];
let answers = [];
let answeredQuestions = [];
let currentApplicationStep = 0;

$(document).ready(function() {
	i18next.use(window.i18nextXHRBackend).init({
		backend: {
			loadPath: '../i18n/en.json'
		}
	}, function(err, t) {
        jqueryI18next.init(i18next, $);
		$(document).localize();
	});
});

function initializeApplication(questionJson, answersJson) {
    // Get the questions and the answers
    questions = JSON.parse(questionJson);
    answers = JSON.parse(answersJson);

    // Populate the first question
    populateQuestions();
}

function nextQuestion() {
    // Increase the index
    currentApplicationStep++;

    // Populate the next question
    populateQuestions();
}

function previousQuestion() {
    // Check if it's the first question
    if(currentApplicationStep === 0) return;

    // Decrease the index
    currentApplicationStep--;

    // Populate the previous question
    populateQuestions();
}

function populateQuestions() {
    // Clear the question and answer, if any
    let questionContainer = document.getElementById('question');
    let answerContainer = document.getElementById('answers');

    while(answerContainer.firstChild) {
        // Remove each answer
        answerContainer.removeChild(answerContainer.firstChild);
    }

    // Get the question and answers from the JSON string
    let currentQuestion = questions[currentApplicationStep];
    let currentAnswers = answers.filter(a => a.question === currentQuestion.id);
    
    // Search for the question in the answered array
    let answered = answeredQuestions.find(q => q.question === currentQuestion.id);

    // Populate the question and answers
    questionContainer.textContent = currentQuestion.text;

    for(let i = 0; i < currentAnswers.length; i++) {
        // Get the answer
        let answer = currentAnswers[i];

        // Create the element to store the answer
        let answerRow = document.createElement('div');
        answerRow.textContent = answer.text;

        if(answered !== undefined && answer.id === answered.answer) {
            // Check as selected
            answerRow.classList.add('selected');
        }

        answerRow.onclick = (event) => {
            // Check if the row has been selected
            if(event.srcElement.classList.contains('selected')) return;

            // Remove the selection on all the children
            answerContainer.childNodes.forEach(n => n.classList.remove('selected'));

            // Make the clicked element selected
            event.srcElement.classList.add('selected');

            // Add the answer to the list
            let index = answeredQuestions.findIndex(a => a.question === currentQuestion.id);

            if(index !== -1) {
                // The question has been answered, change the answer
                answeredQuestions[index].answer = answer.id;
            } else {
                // Create a new answer to the question
                let questionAnswer = { 'question': currentQuestion.id, 'answer': answer.id };
                answeredQuestions.push(questionAnswer);
            }

            if(questions.length === answeredQuestions.length) {
                // Check if all the answers are answered
                finishButton.classList.add('accept');
                finishButton.classList.remove('disabled-accept');
            }
        };

        // Add the answer to the container
        answerContainer.appendChild(answerRow);
    }

    // Get the buttons
    let acceptButton = document.getElementById('next');
    let finishButton = document.getElementById('finish');
    let cancelButton = document.getElementById('previous');
    
    if(currentApplicationStep === 0) {
        // It's the first question, disable the back button
        cancelButton.classList.add('disabled-cancel');
        cancelButton.classList.remove('cancel');
    } else {
        // Enable the button
        cancelButton.classList.add('cancel');
        cancelButton.classList.remove('disabled-cancel');
    }

    if(currentApplicationStep === questions.length - 1) {
        // Enable the submission
        acceptButton.classList.add('hidden');
        finishButton.classList.remove('hidden');

        if(questions.length !== answeredQuestions.length) {
            // There are questions pending to answer
            finishButton.classList.add('disabled-accept');
            finishButton.classList.remove('accept');
        } else {
            // All the questions have been answered
            finishButton.classList.add('accept');
            finishButton.classList.remove('disabled-accept');
        }
    } else {
        // Enable the next question
        finishButton.classList.add('hidden');
        acceptButton.classList.remove('hidden');
    }
} 

function submitApplication() {
    // Check if all questions are submitted
    if(document.getElementById('finish').classList.contains('disabled-accept')) return;

    // Send the answers to the server
    mp.trigger('submitApplication', JSON.stringify(answeredQuestions));
}

function showApplicationMistakes(mistakes) {
    // Show the mistakes
    document.getElementById('mistakes').textContent = i18next.t('application.mistakes') + mistakes;

    // Swap the panels
    document.getElementById('failure').classList.remove('hidden');
    document.getElementById('application').classList.add('hidden');
}