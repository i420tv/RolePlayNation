using System;
using Browsers.Models.BrowserModels;
using Browsers.Models.BrowserModels.Elements;
using DavWebCreator.Server.Models.Browser.Elements.Fonts;

namespace DavWebCreator.Server.Models.Browser.Elements.Textboxes
{

    [Serializable]
    public class BrowserPasswordTextBox : BrowserElement, IBrowserFont
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
        public bool IsRequired { get; set; }
        public short MinLength { get; set; }
        public BrowserTextAlign TextAlign { get; set; }

        public BrowserPasswordTextBox(string placeHolder, string text, string labelText, bool readOnly, bool isRequired)
            : base(BrowserElementType.Password)
        {
            this.PlaceHolder = placeHolder;
            this.Label = new BrowserText(labelText, BrowserTextAlign.center);
            this.Text = text;
            this.ReadOnly = readOnly;
            this.Bold = false;
            this.MaxLength = 50;
            this.Cursor = "pointer";
            this.TextAlign = BrowserTextAlign.center;
            this.ReadOnly = false;
            this.Width = "150px";
            this.Height = "25px";
            this.FontColor = "black";
            this.FontFamily = "Verdana";
            this.Bold = false;
            this.FontSize = "12px";
            //this.Margin = "0 0 0 0";
            //this.Padding = "0 0 0 0";
            this.IsRequired = isRequired;
            this.MinLength = 3;
        }
    }
}
