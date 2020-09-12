using Browsers.Models.BrowserModels;
using DavWebCreator.Server.Models.Browser;
using DavWebCreator.Server.Models.Browser.Elements;
using DavWebCreator.Server.Models.Browser.Elements.Cards;
using DavWebCreator.Server.Models.Browser.Elements.Fonts;
using GTANetworkAPI;
using WiredPlayers.model;
using WiredPlayers.database;
using WiredPlayers.globals;
using WiredPlayers.messages.information;
using WiredPlayers.messages.general;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WiredPlayers.globals
{
    public class InitCef : Script
    {
        public static InitCef instance;

        public InitCef()
        {
            instance = this;
        }

        public void CreateExampleProgressBar(Client player)
        {
            Browser browser = new Browser("ExampleBrowser", BrowserType.Custom, BrowserContentAlign.Center, "100%", "100%");
            browser.Margin = "55% 0 0 0";

            // Container
           /* BrowserCard card = new BrowserCard(BrowserCardType.HeaderAndContent,
                "Example Progress-bar", "Sir,", "This progress-bar triggers a function and starts automatically.")
            {
                Width = "600px",
                Height = "200px",
                TextAlign = BrowserTextAlign.center,
                ItemAlignment = BrowserContentAlign.Center,
                FlexDirection = BrowserFlexDirection.flex_column,
                FlexWrap = BrowserFlexWrap.flex_wrap,
                Margin = "0px 0px 5px 1px"
            };
            */

            BrowserProgressBar progressBar = new BrowserProgressBar(95, 1, 1000)
            {
                ShowCurrentValue = true,
                TextAlign = BrowserTextAlign.center,
                FontColor = "black",

                ProgressBarFinishedEvent = (client, progBar) =>
                {
                    //Timer finished CurrentValue >= MaxValue (e.g. 100/100)
                    
                    player.TriggerEvent("CLOSE_BROWSER");

                }
            };

            progressBar.SetPredefinedProgressBarStyle(BrowserProgressBarStyle.Blue_Striped);

            // Add the progress-bar to the card, that the card includes it in the view.
            //card.AddElement(progressBar.Id);

            // Add the progress-bar to the browser elements.
            browser.AddElement(progressBar);

            // Add the card to the browser elements.
            //browser.AddElement(card);

            browser.OpenBrowser(player);
            player.TriggerEvent("enableMouse");

        }
    }
}