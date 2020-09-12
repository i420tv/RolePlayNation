using Browsers.Models.BrowserModels;
using DavWebCreator.Server.Models.Browser;
using DavWebCreator.Server.Models.Browser.Elements;
using DavWebCreator.Server.Models.Browser.Elements.Cards;
using DavWebCreator.Server.Models.Browser.Elements.Fonts;
using GTANetworkAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Progressbar
{
    public class ProgressLoading : Script
    {
        [Command("progress")]
        
        public static void CreateProgressBar(Client player)
        {
            player.TriggerEvent("CLOSE_BROWSER");

            Browser browser = new Browser("progressBar", BrowserType.Custom, BrowserContentAlign.Center, "100%", "100%");
            browser.Margin = "44% 0 0 0";

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

            BrowserProgressBar progressBar = new BrowserProgressBar(0, 3, 500)
            {
                ShowCurrentValue = true,
                TextAlign = BrowserTextAlign.center,
                FontColor = "black",

                ProgressBarFinishedEvent = (client, progBar) =>
                {
                    //Timer finished CurrentValue >= MaxValue (e.g. 100/100)

                    NAPI.Util.ConsoleOutput(player.Name + progBar.CurrentValue + " TIMER FINISHED");
                    player.SendChatMessage(player.Name + progBar.CurrentValue + " TIMER FINISHED");
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