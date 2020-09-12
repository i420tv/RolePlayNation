using System;

namespace DavWebCreator.Client.Models.Browser.Elements.Events
{
    [Serializable]
    public class BrowserRemoteReturnObject
    {
        public Guid Id { get; set; }
        public BrowserElementType ElementType { get; set; }
        public string HiddenValue { get; set; }
        public ReturnType ReturnTypeOfRemoteEvent { get; set; }
    }
}
