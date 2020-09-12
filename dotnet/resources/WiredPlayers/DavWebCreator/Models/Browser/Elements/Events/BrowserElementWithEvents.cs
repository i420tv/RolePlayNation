using Browsers.Models.BrowserModels;
using Browsers.Models.BrowserModels.Elements;
using System;
using System.Collections.Generic;
using DavWebCreator.Server.ClientModels.Browser.Elements;
using DavWebCreator.Server.Models.Browser.Elements;
using DavWebCreator.Server.Models.Browser.Elements.Events;

namespace DavWebCreator.Resources.Models.Browser.Elements
{
    [Serializable]
    public class BrowserElementWithEvent : BrowserElement
    {
        // Remote Events
       // public string Title { get; set; }
        public string RemoteEvent { get; set; }
        public bool Enabled { get; set; }

        public List<BrowserRemoteReturnObject> ReturnObjects { get; set; }

        public BrowserElementWithEvent(BrowserElementType type, string remoteEvent)
            : base(type)
        {
            this.RemoteEvent = remoteEvent;
            this.Enabled = true;
            this.ReturnObjects = new List<BrowserRemoteReturnObject>();
        }
        
        /// <summary>
        /// The passed elements will be later returned to the remote event, including the current value of the DOM (HTML) Element.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="returnType"></param>
        public void AddReturnObject(BrowserElement element, string hiddenValue = "", ReturnType returnType = ReturnType.Text)
        {
            this.ReturnObjects.Add(new BrowserRemoteReturnObject(element.Id, hiddenValue, element.Type, returnType));
        }
    }
}
