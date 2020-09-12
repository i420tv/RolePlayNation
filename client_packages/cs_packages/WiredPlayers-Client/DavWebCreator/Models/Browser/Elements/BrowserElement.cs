using System;
namespace DavWebCreator.Client.Models.Browser.Elements
{
    public interface IBrowserElement
    {
        Guid Id { get; set; }
        int OrderIndex { get; set; }
        string StyleClass { get; set; }
        BrowserElementType Type { get; set; }
        BrowserElementAnimationType AnimationType { get; set; }
        Position Position { get; set; }
        BorderStyle BorderStyle { get; set; }
        string BorderWidth { get; set; }
        string BorderColor { get; set; }
        int Row { get; set; }
        BrowserFlexDirection FlexDirection { get; set; }
        BrowserContentAlign ItemAlignment { get; set; }
        string Width { get; set; }
        string Height { get; set; }
        bool ScrollBarY { get; set; }
        bool ScrollBarX { get; set; }
        bool LoadingIndicator { get; set; }
        string Cursor { get; set; }
        string Margin { get; set; }
        string Padding { get; set; }
        string BackgroundColor { get; set; }
        string Opacity { get; set; }
    }
}
