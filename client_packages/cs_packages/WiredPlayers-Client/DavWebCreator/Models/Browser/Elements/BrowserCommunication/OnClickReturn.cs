using System;
using System.Collections.Generic;
using System.Text;
using DavWebCreator.Client.Models.Browser.Elements.Events;

namespace DavWebCreator.Client.Models.Browser.Elements.BrowserCommunication
{
    public class OnClickReturn
    {
        public Guid Id { get; set; }
        public string RemoteEvent { get; set; }
        public List<BrowserRemoteReturnObject> ReturnValues = new List<BrowserRemoteReturnObject>();
    }
}
