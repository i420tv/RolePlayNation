using System;

namespace DavWebCreator.Server.Models.Browser.Elements.Controls
{
    [Serializable]
    public class BrowserDropDownValue
    {
        public Guid Id { get; set; }
        public string HiddenValue { get; set; }
        public string Value { get; set; }

        public BrowserDropDownValue(string value, string hiddenValue)
        {
            this.Id = Guid.NewGuid();
            this.HiddenValue = hiddenValue;
            this.Value = value;
        }
    }
}
