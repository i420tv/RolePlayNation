using System.Collections.Generic;

namespace DavWebCreator.Client.Models.Browser.Elements.Events
{
    public interface IBrowserElementWithEvent : IBrowserElement
    {
        // Remote Events
        string RemoteEvent { get; set; }
        bool Enabled { get; set; }
        List<BrowserRemoteReturnObject> ReturnObjects { get; set; }
        // CSS Properties
    }
}
