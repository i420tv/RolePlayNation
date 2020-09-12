using System;
using DavWebCreator.Server.ClientModels.Browser.Elements;

namespace DavWebCreator.Server.Models.Browser.Elements.Events
{
    [Serializable]
    public class BrowserRemoteReturnObject
    {
        public Guid Id { get; set; }
        public BrowserElementType ElementType { get; set; }
        public ReturnType ReturnTypeOfRemoteEvent { get; set; }
        public string HiddenValue { get; set; }

        public BrowserRemoteReturnObject(Guid id, string hiddenValue, BrowserElementType elementType, ReturnType returnType)
        {
            this.Id = id;
            this.ReturnTypeOfRemoteEvent = returnType;
            this.ElementType = elementType;
            this.HiddenValue = hiddenValue;
        }
    }
}
