using RAGE;
using RAGE.Elements;
using WiredPlayers_Client.model;
using WiredPlayers_Client.globals;
using WiredPlayers_Client.character;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace WiredPlayers_Client.account
{
    class Register : Events.Script
    {
        public Register()
        {
            Events.Add("showRegisterWindow", ShowRegisterWindowEvent);
            Events.Add("showApplicationTest", ShowApplicationTestEvent);
            Events.Add("submitApplication", SubmitApplicationEvent);
            Events.Add("failedApplication", FailedApplicationEvent);
            Events.Add("retryApplication", RetryApplicationEvent);
            Events.Add("clearApplication", ClearApplicationEvent);
            Events.Add("createPlayerAccount", CreatePlayerAccountEvent);
        }

        private void ShowRegisterWindowEvent(object[] args)
        {
            // Create register window
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/register.html" });
        }

        private void ShowApplicationTestEvent(object[] args)
        {
            // Destroy the current window
            Browser.DestroyBrowserEvent(null);

            // Create the application window
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/application.html", "initializeApplication", args[0].ToString(), args[1].ToString() });
        }

        private void SubmitApplicationEvent(object[] args)
        {
            // Get the answers
            Dictionary<int, int> questionsAnswers = new Dictionary<int, int>();
            List<TestModel> answers = JsonConvert.DeserializeObject<List<TestModel>>(args[0].ToString());

            foreach (TestModel testModel in answers)
            {
                // Add the question and answer to the dictionary
                questionsAnswers.Add(testModel.question, testModel.answer);
            }

            // Send the answers to the server
            Events.CallRemote("submitApplication", JsonConvert.SerializeObject(questionsAnswers));
        }

        private void FailedApplicationEvent(object[] args)
        {
            // Get the mistakes
            int mistakes = Convert.ToInt32(args[0]);

            // Show the mistakes
            Browser.ExecuteFunctionEvent(new object[] { "showApplicationMistakes", mistakes });
        }

        private void RetryApplicationEvent(object[] args)
        {
            // Create a new application form
            Events.CallRemote("loadApplication");
        }

        private void ClearApplicationEvent(object[] args)
        {
            // Unfreeze the player
            Player.LocalPlayer.FreezePosition(true);

            // Show the message on the panel
            Browser.DestroyBrowserEvent(null);

            

        }

        private void CreatePlayerAccountEvent(object[] args)
        {
            // Get the password from the array
            string password = args[0].ToString();

            // Create login window
            Events.CallRemote("registerAccount", password);

            // Destroy the current window             
            Browser.DestroyBrowserEvent(null);
        }
    }
}

