using System;
using System.Collections.Generic;
using DavWebCreator.Client.Models.Browser.Components;
using DavWebCreator.Client.Models.Browser.Elements;
using DavWebCreator.Client.Models.Browser.Elements.Cards;
using DavWebCreator.Client.Models.Browser.Elements.Controls;
using DavWebCreator.Client.Models.Browser.Elements.Textboxes;

namespace DavWebCreator.Client.Models.Browser
{
    public interface IBrowser
    {
        Guid Id { get; set; }
        string Path { get; set; }
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
        List<BrowserButtonIcon> Icons { get; set; }
        bool ScrollBarY { get; set; }
        bool ScrollBarX { get; set; }
    }
}
