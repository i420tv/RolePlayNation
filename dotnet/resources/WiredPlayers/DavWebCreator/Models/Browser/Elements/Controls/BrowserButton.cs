using System;
using Browsers.Models.BrowserModels;
using DavWebCreator.Resources.Models.Browser.Elements;
using DavWebCreator.Server.Models.Browser.Elements.Fonts;

namespace DavWebCreator.Server.Models.Browser.Elements.Controls
{
    /// <summary>
    /// Checkout the following link to see the available style classes.
    /// https://getbootstrap.com/docs/4.0/components/buttons/
    /// </summary>
    [Serializable]
    public class BrowserButton : BrowserElementWithEvent, IBrowserFont
    {
        public string FontFamily { get; set; }
        public string FontColor { get; set; }
        public BrowserTextAlign TextAlign { get; set; }
        public string FontSize { get; set; }
        public bool Bold { get; set; }
        public string Text { get; set; }

        public BrowserButton(string text, string remoteEvent)
            :base(BrowserElementType.Button, remoteEvent)
        {
            this.Text = text;
            this.TextAlign = BrowserTextAlign.center;
            this.Width = "100%";
            this.Height = "60px";
            this.Cursor = "pointer";
            this.SetPredefinedButtonStyle(BrowserButtonStyle.Blue);
        }

        public void SetPredefinedButtonStyle(BrowserButtonStyle style)
        {
            switch (style)
            {
                case BrowserButtonStyle.Blue:
                    this.StyleClass = " btn btn-primary";
                    break;
                case BrowserButtonStyle.Grey:
                    this.StyleClass = " btn btn-secondary";
                    break;
                case BrowserButtonStyle.Green:
                    this.StyleClass = " btn btn-success";
                    break;
                case BrowserButtonStyle.Red:
                    this.StyleClass = " btn btn-danger";
                    break;
                case BrowserButtonStyle.Yellow:
                    this.StyleClass = " btn btn-warning";
                    break;
                case BrowserButtonStyle.LightBlue:
                    this.StyleClass = " btn btn-info";
                    break;
                case BrowserButtonStyle.White:
                    this.StyleClass = " btn btn-light";
                    break;
                case BrowserButtonStyle.Black:
                    this.StyleClass = " btn btn-dark";
                    break;
                case BrowserButtonStyle.Link:
                    this.StyleClass = " btn btn-link";
                    break;
                case BrowserButtonStyle.Blue_Outline:
                    this.StyleClass = " btn btn-outline-primary";
                    break;
                case BrowserButtonStyle.Grey_Outline:
                    this.StyleClass = " btn btn-outline-secondary";
                    break;
                case BrowserButtonStyle.Green_Outline:
                    this.StyleClass = " btn btn-outline-success";
                    break;
                case BrowserButtonStyle.Red_Outline:
                    this.StyleClass = " btn btn-outline-danger";
                    break;
                case BrowserButtonStyle.Yellow_Outline:
                    this.StyleClass = " btn btn-outline-warning";
                    break;
                case BrowserButtonStyle.LightBlue_Outline:
                    this.StyleClass = " btn btn-outline-info";
                    break;
                case BrowserButtonStyle.White_Outline:
                    this.StyleClass = " btn btn-outline-light";
                    break;
                case BrowserButtonStyle.Black_Outline:
                    this.StyleClass = " btn btn-outline-dark";
                    break;
            }
        }
    }
}
