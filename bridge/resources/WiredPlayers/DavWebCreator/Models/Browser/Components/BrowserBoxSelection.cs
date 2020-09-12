using System;
using System.Collections.Generic;
using Browsers.Models.BrowserModels;
using DavWebCreator.Server.Models.Browser.Elements;
using DavWebCreator.Server.Models.Browser.Elements.Cards;
using DavWebCreator.Server.Models.Browser.Elements.Controls;

namespace DavWebCreator.Server.Models.Browser.Components
{
    /// <summary>
    /// Not imlemented yet
    /// </summary>
    [Serializable]
    public class BrowserBoxSelection : BrowserElement
    {
        public BrowserTitle Title { get; set; }
        public List<BrowserCard> Cards { get; set; }
        public BrowserButton PrimaryCardButton { get; set; }
        public BrowserButton SecondaryCardButton { get; set; }
        public BrowserBoxSelection() : base( BrowserElementType.BrowserBoxSelection)
        {
            this.Cards = new List<BrowserCard>();
        }

        public void AddCard(string cardTitle, string cardContent)
        {
            var card = new BrowserCard(BrowserCardType.HeaderDescriptionAndButtonWithIcon, "", cardTitle, cardContent);

            this.Cards.Add(card);
        }
    }
}
