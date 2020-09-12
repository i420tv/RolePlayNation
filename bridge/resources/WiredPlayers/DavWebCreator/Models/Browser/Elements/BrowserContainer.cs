using System;
using System.Collections.Generic;
using Browsers.Models.BrowserModels;

namespace DavWebCreator.Server.Models.Browser.Elements
{
    [Serializable]
    public class BrowserContainer : BrowserElement
    {
        public List<Guid> Elements { get; set; }

        public BrowserContainer(Position position)
            : base(BrowserElementType.Container)
        {
            Elements = new List<Guid>();
        }

        public void AddElement(Guid elementId)
        {
            this.Elements.Add(elementId);
        }
    }
}
