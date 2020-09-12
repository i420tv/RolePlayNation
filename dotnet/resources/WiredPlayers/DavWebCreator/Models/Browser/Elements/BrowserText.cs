using DavWebCreator.Server.Models.Browser.Elements.Fonts;

namespace DavWebCreator.Server.Models.Browser.Elements
{
    public class BrowserText : BrowserElement, IBrowserFont
    {
        public string Text { get; set; }
        public string FontSize { get; set; }
        public string FontFamily { get; set; }
        public bool Bold { get; set; }
        public string FontColor { get; set; }
        public BrowserTextAlign TextAlign { get; set; }
        

        public BrowserText(string text, BrowserTextAlign textAlign)
            : base(BrowserElementType.Text)
        {
            this.Text = text;
            this.TextAlign = textAlign;
        }
    }
}
