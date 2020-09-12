using System;
using DavWebCreator.Resources.Models.Browser.Elements;
using DavWebCreator.Server.Models.Browser.Elements;
using DavWebCreator.Server.Models.Browser.Elements.Cards;
using DavWebCreator.Server.Models.Browser.Elements.Controls;
using DavWebCreator.Server.Models.Browser.Elements.Fonts;

namespace DavWebCreator.Server.Models.Browser.Components
{
    [Serializable]
    public class BrowserYesNoDialog : BrowserElement, IBrowserFont
    {
        public BrowserTitle Title { get; set; }
        public BrowserTitle SubTitle { get; set; }
        public BrowserButton SuccessButton { get; set; }
        public BrowserText Text { get; set; }
        public BrowserCard Card { get; set; }
        public BrowserButton DismissButton { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public string FontColor { get; set; }
        public bool Bold { get; set; }
        public BrowserTextAlign TextAlign { get; set; }

        public BrowserYesNoDialog() : base(BrowserElementType.YesNoDialog)
        {

        }

    }
}
