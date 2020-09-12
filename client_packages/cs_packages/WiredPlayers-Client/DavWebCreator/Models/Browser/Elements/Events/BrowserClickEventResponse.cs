using System;

namespace DavWebCreator.Client.Models.Browser.Elements.Events
{
    [Serializable]
    public class BrowserClickEventResponse
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public string HiddenValue { get; set; }
        public BrowserElementType Type { get; set; }
    }
}
