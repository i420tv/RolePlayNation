using RAGE;
using RAGE.Elements;
using Newtonsoft.Json;
using WiredPlayers_Client.globals;
using WiredPlayers_Client.model;
using System.Collections.Generic;
using System;

namespace WiredPlayers_Client.jobs
{
    class BankHeist : Events.Script
    {

        public BankHeist()
        {
            Events.Add("Hacking", HackingEvent);
           // Events.Add("EndHacking", EndHackingEvent);

           // Events.OnPlayerEnterCheckpoint += OnPlayerEnterCheckpoint;
        }

        public void HackingEvent(object [] args)
        {

            //// Disable the chat
            //Chat.Activate(false);
            //Chat.Show(false);
            // Show the tunning menu
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/Hacking.html" });
        }
    }
}
