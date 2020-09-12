using System;

namespace DavWebCreator.Server.Models.Browser.Elements
{
    [Serializable]
    public enum BrowserElementType
    {
        TextBox = 1,
        Button = 2,
        Checkbox = 3,
        DropDown = 4,
        MultiDropDown = 5,
        Grid = 5,
        Title = 6,
        SubTitle = 7,
        Text = 8,
        Image = 9,
        Container = 10,
        Card = 11,
        Password = 20,
        YesNoDialog = 21,
        ProgressBar = 22,
        BrowserBoxSelection = 23,
        Icon = 24
    }
}
