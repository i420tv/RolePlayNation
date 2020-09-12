using Browsers.Models.BrowserModels;
using DavWebCreator.Server.Models.Browser;
using DavWebCreator.Server.Models.Browser.Elements;
using DavWebCreator.Server.Models.Browser.Elements.Cards;
using DavWebCreator.Server.Models.Browser.Elements.Controls;
using DavWebCreator.Server.Models.Browser.Elements.Fonts;
using DavWebCreator.Server.Models.Dummys;
using GTANetworkAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WiredPlayers.globals
{



    public class Profile : Script
    {
        public List<Browser> Browsers = new List<Browser>();


        //  List<VehicleDummy> vehicleDummys = new List<VehicleDummy>
        // {
        //  new VehicleDummy(2, "Financial", 200, 100, 50, 60, true),
        // new VehicleDummy(3, "Salary", 140, 20, 43, 50, false),
        // new VehicleDummy(4, "Banshee", 120, 30, 23, 45, false),
        // new VehicleDummy(5, "Glendale", 240, 70, 63, 85, true),
        // new VehicleDummy(6, "DavMobil", 240, 70, 63, 85, true),
        //  new VehicleDummy(6, "DavMobil2", 240, 70, 63, 85, true),
        //  new VehicleDummy(6, "DavMobil3", 240, 70, 63, 85, true),
        // new VehicleDummy(6, "DavMobil4", 240, 70, 63, 85, true)
        // };
        [RemoteEvent("SHOW_PROFILE")]
        [Command("profile")]
        public void CreateExampleBoxes(Client player)
        {
            

            player.SetData("BrowserOpen", 1);

            Browser browser = new Browser("pProfile", BrowserType.Custom, BrowserContentAlign.Center, "100%", "100%")
            {
                BackgroundColor = "white",
                Margin = "100px 0 0 0",
            };

            BrowserCard card = new BrowserCard(BrowserCardType.HeaderAndContent,
                "<b>" + player.GetData(EntityData.PLAYER_NAME) + " (ID: " + player.GetData(EntityData.PLAYER_SQL_ID) + ")</b>", "", "")
            {
                Width = "90%",
                Height = "80%",
                TextAlign = BrowserTextAlign.center,
                ItemAlignment = BrowserContentAlign.Center,
                Image = $"package://DavWebCreator/Images/bannerocean.png",
                ScrollBarY = true,
                FlexDirection = BrowserFlexDirection.flex_lg_row,
                FlexWrap = BrowserFlexWrap.flex_wrap
            };

            Browsers.Add(browser);

            card.CardTitle.TextAlign = BrowserTextAlign.center;
            browser.AddElement(card);
            
            #region Your Premium

            BrowserCard characterStats = new BrowserCard(BrowserCardType.HeaderDescriptionAndButtonWithIcon,
                "", "Character Stats", "")
            {
                TextAlign = BrowserTextAlign.left,
                ItemAlignment = BrowserContentAlign.Center_small,
                FlexDirection = BrowserFlexDirection.flex_lg_row,
                FlexWrap = BrowserFlexWrap.flex_wrap,
                Width = "300px",
                Height = "210px",
                Margin = "10px",
                Row = 1
            };

            characterStats.ContentTitle.TextAlign = BrowserTextAlign.center;
            characterStats.ContentTitle.Margin = "8px";

            characterStats.Image = $"package://DavWebCreator/Images/bannerocean.png";          

            BrowserButton goButton = new BrowserButton("See More", "SHOW_STATS");
            goButton.Width = "100px";
            goButton.Height = "35px";
            goButton.Margin = "15px 20px 0 0";
            goButton.SetPredefinedButtonStyle(BrowserButtonStyle.Black_Outline);

            goButton.TextAlign = BrowserTextAlign.center;
            goButton.ItemAlignment = BrowserContentAlign.Center;
            goButton.AddReturnObject(goButton, "0");
            characterStats.AddElement(goButton.Id);

            card.AddElement(characterStats.Id);

            browser.AddElement(characterStats);
            browser.AddElement(goButton);
            browser.OpenBrowser(player);
            
            #endregion
        }
    }
}
