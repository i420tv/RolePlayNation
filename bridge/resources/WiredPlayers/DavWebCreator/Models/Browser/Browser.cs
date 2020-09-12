using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Browsers.Models.BrowserModels;
using Browsers.Models.BrowserModels.Elements;
using DavWebCreator.Resources.Models.Browser.Elements;
using DavWebCreator.Server.Models.Browser.Components;
using DavWebCreator.Server.Models.Browser.Elements;
using DavWebCreator.Server.Models.Browser.Elements.Cards;
using DavWebCreator.Server.Models.Browser.Elements.Controls;
using DavWebCreator.Server.Models.Browser.Elements.Fonts;
using DavWebCreator.Server.Models.Browser.Elements.Textboxes;
using GTANetworkAPI;
using Newtonsoft.Json;

namespace DavWebCreator.Server.Models.Browser
{
    [Serializable]
    public class Browser : IBrowser
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public BrowserType Type { get; set; }

        public List<BrowserText> Texts { get; set; }
        public List<BrowserTextBox> TextBoxes {get; set; }
        public List<BrowserPasswordTextBox> PasswordTextBoxes { get; set; }
        public List<BrowserTitle> Titles { get; set; }
        public List<BrowserButton> Buttons { get; set; }
        public List<BrowserProgressBar> ProgressBars { get; set; }
        public List<BrowserCard> Cards { get; set; }
        public List<BrowserCheckBox> CheckBoxes { get; set; }
        public List<BrowserDropDown> DropDowns { get; set; }
        public List<BrowserContainer> Container { get; private set; }
        public BrowserBoxSelection BoxSelection { get; private set; }
        public List<BrowserButtonIcon> Icons { get; private set; }

        // Events
        public string CloseEvent { get; set; }

        // Stylesheet
        private int AddedElements = 0;
        public BrowserContentAlign ContentPosition { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public bool ScrollBarY { get; set; }
        public bool ScrollBarX { get; set; }
        public string BackgroundColor { get; set; }
        /// <summary>
        /// 0.0 - 1.0
        /// </summary>
        public string Opacity { get; set; }
        public string Margin { get; set; }
        public string Padding { get; set; }

        public Browser()
        {

        }

        public Browser(string title, BrowserType type, BrowserContentAlign position, string width, string height)
        {
            this.Id = Guid.NewGuid();
            this.Type = type;
            this.ContentPosition = position;
            this.Texts = new List<BrowserText>();
            this.TextBoxes = new List<BrowserTextBox>();
            this.PasswordTextBoxes = new List<BrowserPasswordTextBox>();
            this.Titles = new List<BrowserTitle>();
            this.Buttons = new List<BrowserButton>();
            this.Cards = new List<BrowserCard>();
            this.CheckBoxes = new List<BrowserCheckBox>();
            this.DropDowns = new List<BrowserDropDown>();
            this.ProgressBars = new List<BrowserProgressBar>();
            this.Container = new List<BrowserContainer>();
            this.Icons = new List<BrowserButtonIcon>();

            this.ScrollBarX = false;
            this.ScrollBarY = false;
            this.Width = width;
            this.Height = height;

            switch (type)
            {
                case BrowserType.Custom:
                    this.Path = $"package://DavWebCreator/Template.html";
                    break;
                //case BrowserType.YesNoDialog:
                //    this.Path = $"package://DavWebCreator/Template.html";
                //    //this.Path = $"package://DavWebCreator/Blank_Template.html";
               
                //    break;
                case BrowserType.Selection:
                    this.Path = $"package://DavWebCreator/Selection_Template.html";
                    break;
            }
        }

        public void AddYesNoDialog(BrowserYesNoDialog yesNoDialog)
        {
            this.AddElement(yesNoDialog.SuccessButton);
            this.AddElement(yesNoDialog.DismissButton);
            this.AddElement(yesNoDialog.Card);
        }

        public BrowserYesNoDialog GetYesNoDialog(string remoteEvent, string title, string subTitle, string text, string successButtonText, string dismissButtonText)
        {
            BrowserYesNoDialog yesNoDialog = new BrowserYesNoDialog();

            yesNoDialog.Card = new BrowserCard(BrowserCardType.HeaderAndContent, title, subTitle, text)
            {
                Width = "100%",
                Height = "240px",
                TextAlign = BrowserTextAlign.center,
                ItemAlignment = BrowserContentAlign.Center_small,
                FlexDirection = BrowserFlexDirection.flex_row,
                FlexWrap = BrowserFlexWrap.flex_wrap,
                Margin = "0px 0px 5px 1px"
            };

     
            yesNoDialog.Card.ContentTitle.Padding = "10px 0 0 0";
            yesNoDialog.Card.ContentTitle.Padding = "5px 0 0 0";

            yesNoDialog.SuccessButton = new BrowserButton(successButtonText, remoteEvent);
            yesNoDialog.SuccessButton.SetPredefinedButtonStyle(BrowserButtonStyle.Green);
            yesNoDialog.SuccessButton.Width = "150px";
            yesNoDialog.SuccessButton.Height = "40px";
            yesNoDialog.SuccessButton.TextAlign = BrowserTextAlign.center;
            yesNoDialog.SuccessButton.Margin = "0 0 0 8px";
            yesNoDialog.SuccessButton.AddReturnObject(yesNoDialog.SuccessButton, "");

            yesNoDialog.DismissButton = new BrowserButton(dismissButtonText, remoteEvent);
            yesNoDialog.DismissButton.SetPredefinedButtonStyle(BrowserButtonStyle.Red);
            yesNoDialog.DismissButton.Width = "150px";
            yesNoDialog.DismissButton.Height = "40px";
            yesNoDialog.DismissButton.TextAlign = BrowserTextAlign.center;
            yesNoDialog.DismissButton.Margin = "0 8px 0 0";
            yesNoDialog.DismissButton.AddReturnObject(yesNoDialog.DismissButton, ""); // Return Dismiss Button Text to let know which button was pressed

            yesNoDialog.Card.AddElement(yesNoDialog.Card.CardTitle.Id);
            yesNoDialog.Card.AddElement(yesNoDialog.SuccessButton.Id);
            yesNoDialog.Card.AddElement(yesNoDialog.DismissButton.Id);

            return yesNoDialog;
        }


        public void AddSelectionBoxes(string remoteEvent, string title)
        {
            this.BoxSelection = new BrowserBoxSelection();
            this.BoxSelection.Title = new BrowserTitle(title, BrowserTextAlign.center);
            this.BoxSelection.PrimaryCardButton = new BrowserButton("FirstBatton", "SUMEVENT");
            this.BoxSelection.SecondaryCardButton = new BrowserButton("SecondBatton", "SUMEVENT");
            this.Cards = new List<BrowserCard>();
        }

        

        public void OpenBrowser(Client player)
        {
            foreach (var progressBar in ProgressBars)
            {
                if (progressBar.StartTimerInstant)
                {
                    // Start timer to update progressBar
                    progressBar.UpdateCurrentValue(player);
                }
            }

            player.TriggerEvent("INITIALIZE_CEF_BROWSER", JsonConvert.SerializeObject(this));
        }


        public void AddElement(BrowserElement element)
        {
            element.OrderIndex = AddedElements++;
            //element.Position = this.Position;
            switch (element.Type)
            {
                case BrowserElementType.BrowserBoxSelection:
                    BrowserBoxSelection boxSelection = element as BrowserBoxSelection;

                    break;
                case BrowserElementType.Button:
                    BrowserButton button = element as BrowserButton;
                    Buttons.Add(button);
                    break;
                case BrowserElementType.Title:
                    BrowserTitle title = element as BrowserTitle;
                    Titles.Add(title);
                    break;
                case BrowserElementType.Text:
                    BrowserText text = element as BrowserText;
                    Texts.Add(text);
                    break;
                case BrowserElementType.TextBox:
                    BrowserTextBox textBox = element as BrowserTextBox;
                    TextBoxes.Add(textBox);
                    break;
                case BrowserElementType.Card:
                    BrowserCard card = element as BrowserCard;
                    Cards.Add(card);
                    break;
                case BrowserElementType.Password:
                    BrowserPasswordTextBox passwordTextBox = element as BrowserPasswordTextBox;
                    PasswordTextBoxes.Add(passwordTextBox);
                    break;
                //case BrowserElementType.YesNoDialog:
                //    BrowserYesNoDialog yesNoDialog = element as BrowserYesNoDialog;
                //    YesNoDialog = yesNoDialog;
                //    break;
                case BrowserElementType.Checkbox:
                    BrowserCheckBox checkBox = element as BrowserCheckBox;
                    CheckBoxes.Add(checkBox);
                    break;
                case BrowserElementType.DropDown:
                    BrowserDropDown dropDown = element as BrowserDropDown;
                    DropDowns.Add(dropDown);
                    break;
                case BrowserElementType.ProgressBar:
                    BrowserProgressBar progressBar = element as BrowserProgressBar;
                    ProgressBars.Add(progressBar);
                    break;
                case BrowserElementType.Container:
                    BrowserContainer container = element as BrowserContainer;
                    Container.Add(container);
                    break;
                case BrowserElementType.Icon:
                    BrowserButtonIcon icon = element as BrowserButtonIcon;
                    Icons.Add(icon);
                    break;
                default:
                    NAPI.Util.ConsoleOutput($"UNKNOWN ELEMENT OF TYPE {element.Type}");
                    break;
            }
          
        }

        public override string ToString()
        {
            return string.Format(this.Id + " | " + this.Path + " | " + this.Type);
        }
    }
}
