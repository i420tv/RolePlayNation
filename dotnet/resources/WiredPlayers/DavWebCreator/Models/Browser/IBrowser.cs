using System;
using System.Collections.Generic;
using Browsers.Models.BrowserModels;
using Browsers.Models.BrowserModels.Elements;
using DavWebCreator.Server.Models.Browser.Components;
using DavWebCreator.Server.Models.Browser.Elements;
using DavWebCreator.Server.Models.Browser.Elements.Cards;
using DavWebCreator.Server.Models.Browser.Elements.Controls;
using DavWebCreator.Server.Models.Browser.Elements.Textboxes;

namespace DavWebCreator.Server.Models.Browser
{
    public interface IBrowser
    {
        Guid Id { get; set; }
        string Path { get; set; }
        BrowserType Type { get; set; }
        BrowserContentAlign ContentPosition { get; set; }
        string Width { get; set; }
        string Height { get; set; }
        List<BrowserText> Texts { get; set; }
        List<BrowserTextBox> TextBoxes { get; set; }
        List<BrowserPasswordTextBox> PasswordTextBoxes { get; set; }
        List<BrowserTitle> Titles { get; set; }
        List<BrowserButton> Buttons { get; set; }
        List<BrowserCard> Cards { get; set; }
        List<BrowserCheckBox> CheckBoxes { get; set; }
        List<BrowserButtonIcon> Icons { get; }
    }
}
