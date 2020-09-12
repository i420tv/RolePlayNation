using DavWebCreator.Server.Models.Browser.Elements.Fonts;
using System;
using DavWebCreator.Server.Models.Browser.Elements;

namespace Browsers.Models.BrowserModels.Elements
{
    [Serializable]
    public class BrowserTextBox : BrowserElement, IBrowserFont
    {
        public string PlaceHolder { get; set; }
        public BrowserText Label { get; set; }      
        public string Text { get; set; }
        public bool ReadOnly { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public string FontColor { get; set; }
        public bool Bold { get; set; }
        public short MaxLength { get; set; }
        public BrowserTextAlign TextAlign { get; set; }

        public BrowserTextBox(string placeHolder, string text, string labelText, bool readOnly, BrowserElementType elementType = BrowserElementType.TextBox)
            :base(elementType)
        {
            this.PlaceHolder = placeHolder;
            this.Label = new BrowserText(labelText, BrowserTextAlign.center);
            this.Text = text;
            this.ReadOnly = readOnly;
            this.Bold = false;
            this.MaxLength = 50;
            //this.Width = "120px";
            //this.Height = "30px";
            this.Cursor = "pointer";
            this.TextAlign = BrowserTextAlign.center;
            this.ReadOnly = readOnly;
            this.Width = "150px";
            this.Height = "25px";
            this.FontColor = "black";
            this.FontFamily = "Verdana";
            this.Bold = false;
            this.FontSize = "12px";
            this.Margin = "0 0 0 0";
            this.Padding = "0 0 0 0";
        }
    }
}
