using System;
using System.Collections.Generic;

namespace DavWebCreator.Client.Models.Browser.Elements
{
    public class BrowserContainer : IBrowserElement
    {
        public Guid Id { get; set; }
        public int OrderIndex { get; set; }
        public BrowserElementType Type { get; set; }
        public BrowserElementAnimationType AnimationType { get; set; }
        public Position Position { get; set; }
        public BorderStyle BorderStyle { get; set; }
        public string BorderWidth { get; set; }
        public string BorderColor { get; set; }
        public BrowserFlexDirection FlexDirection { get; set; }
        public BrowserContentAlign ItemAlignment { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public bool ScrollBarY { get; set; }
        public bool ScrollBarX { get; set; }
        public bool LoadingIndicator { get; set; }
        public string StyleClass { get; set; }
        public List<IBrowserElement> Elements { get; set; }
        public string Cursor { get; set; }
        public string Margin { get; set; }
        public string Padding { get; set; }
        public string BackgroundColor { get; set; }
        public string BackGroundColor { get; set; }
        public string Opacity { get; set; }
        public int Row { get; set; }
    }
}
