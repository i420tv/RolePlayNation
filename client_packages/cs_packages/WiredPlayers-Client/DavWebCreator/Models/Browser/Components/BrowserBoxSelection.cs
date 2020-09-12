using System;
using System.Collections.Generic;
using DavWebCreator.Client.Models.Browser.Elements;
using DavWebCreator.Client.Models.Browser.Elements.Cards;
using DavWebCreator.Client.Models.Browser.Elements.Controls;

namespace DavWebCreator.Client.Models.Browser.Components
{
    public class BrowserBoxSelection : IBrowserElement
    {
        public BrowserCard Card { get; set; }
        public BrowserTitle Title { get; set; }
        public List<BrowserCard> Cards { get; set; }
        public List<BrowserButton> Buttons { get; set; }
        public Guid Id { get; set; }
        public int OrderIndex { get; set; }
        public string StyleClass { get; set; }
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
        public string Cursor { get; set; }
        public string Margin { get; set; }
        public string Padding { get; set; }
        public string BackgroundColor { get; set; }
        public string Opacity { get; set; }
        public int Row { get; set; }
    }
}
