namespace DavWebCreator.Client.Models.Browser.Elements.Fonts
{
    public interface IBrowserFont
    {
        string FontFamily { get; set; }
        string FontSize { get; set; }
        string FontColor { get; set; }
        bool Bold { get; set; }
        BrowserTextAlign TextAlign { get; set; }
    }
}
