using System;
using System.Collections.Generic;
using System.Text;
using DavWebCreator.Client.Models.Browser.Elements.Events;

namespace DavWebCreator.Client.Models.Browser.Elements.Controls
{
    public class BrowserImage : IBrowserElementWithEvent
    {
        public Guid Id { get; set; }
        public int OrderIndex { get; set; }
        public string StyleClass { get; set; }
        public BrowserElementType Type { get; set; }
        public BrowserElementAnimationType AnimationType { get; set; }
        public Position Position { get; set; }
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
        public string RemoteEvent { get; set; }
        public bool Enabled { get; set; }
        public List<BrowserRemoteReturnObject> ReturnObjects { get; set; }
        public BorderStyle BorderStyle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string BorderWidth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string BorderColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
