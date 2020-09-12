using System;
using System.Collections.Generic;
using DavWebCreator.Client.Models.Browser.Components;
using DavWebCreator.Client.Models.Browser.Elements;
using DavWebCreator.Client.Models.Browser.Elements.Cards;
using DavWebCreator.Client.Models.Browser.Elements.Controls;
using DavWebCreator.Client.Models.Browser.Elements.Textboxes;


namespace DavWebCreator.Client.Models.Browser
{
    public class Browser : IBrowser
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public BrowserType Type { get; set; }
        public BrowserContentAlign ContentPosition { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Margin { get; set; }
        public string Padding { get; set; }
        public string BackgroundColor { get; set; }
        public string Opacity { get; set; }
        public bool ScrollBarY { get; set; }
        public bool ScrollBarX { get; set; }
        public string CloseEvent { get; set; }
        public List<BrowserText> Texts { get; set; }
        public List<BrowserTextBox> TextBoxes { get; set; }
        public List<BrowserPasswordTextBox> PasswordTextBoxes { get; set; }
        public List<BrowserTitle> Titles { get; set; }
        public List<BrowserButton> Buttons { get; set; }
        public List<BrowserCard> Cards { get; set; }
        public List<BrowserCheckBox> CheckBoxes { get; set; }
        public List<BrowserDropDown> DropDowns { get; set; }
        public List<BrowserProgressbar> ProgressBars { get; set; }
        public List<BrowserButtonIcon> Icons { get; set; }
        public List<BrowserContainer> Container { get; set; }
    }
}
