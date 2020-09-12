using DavWebCreator.Client.ClientModels.Browser.Stylesheets;
using DavWebCreator.Client.Models.Browser;
using DavWebCreator.Client.Models.Browser.Components;
using DavWebCreator.Client.Models.Browser.Elements;
using DavWebCreator.Client.Models.Browser.Elements.BrowserCommunication;
using DavWebCreator.Client.Models.Browser.Elements.Cards;
using DavWebCreator.Client.Models.Browser.Elements.Controls;
using DavWebCreator.Client.Models.Browser.Elements.Events;
using DavWebCreator.Client.Models.Browser.Elements.Fonts;
using DavWebCreator.Client.Models.Browser.Elements.Textboxes;
using Newtonsoft.Json;
using RAGE;
using RAGE.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BorderStyle = DavWebCreator.Client.Models.Browser.Elements.BorderStyle;
using BrowserElementAnimationType = DavWebCreator.Client.Models.Browser.Elements.BrowserElementAnimationType;
using BrowserElementType = DavWebCreator.Client.Models.Browser.Elements.BrowserElementType;
using BrowserFlexDirection = DavWebCreator.Client.Models.Browser.Elements.BrowserFlexDirection;
using BrowserText = DavWebCreator.Client.Models.Browser.Elements.BrowserText;
using BrowserTitle = DavWebCreator.Client.Models.Browser.Elements.BrowserTitle;


namespace DavWebCreator.Client
{
    public class CEFBrowserEvents : RAGE.Events.Script
    {

        private HtmlWindow BrowserWindow;
        private Browser BrowserModel;

        public CEFBrowserEvents()
        {
            Cursor.Visible = false;
            RAGE.Events.Add("INITIALIZE_CEF_BROWSER", CreateBrowserEvents);
            RAGE.Events.Add("BROWSER_ELEMENT_CLICKED_EVENT", EventTrigger);
            RAGE.Events.Add("TRIGGER_TEST", TriggerTest);
            RAGE.Events.Add("PROVIDE_ERROR", ProvideError);
            RAGE.Events.Add("CLOSE_BROWSER", CloseBrowser);
            Events.Add("UPDATE_PROGRESSBAR", UpdateProgressBar);
            // RAGE.Events.Add("CLOSE_BROWSER", CloseBrowserExecution);
            // RAGE.Events.Add("CLOSE_BROWSER_JS", CloseBrowserJS);
        }

        private void UpdateProgressBar(object[] args)
        {
            //ProvideError(args);
            Guid id;
            if (!Guid.TryParse(args[0].ToString(), out id))
                return;

            int currentValue;
            if (!int.TryParse(args[1].ToString(), out currentValue))
                return;

            bool showCurrentValue = false;
            bool.TryParse(args[2].ToString(), out showCurrentValue);

            string currentValueText = "";
            if (showCurrentValue)
                currentValueText = currentValue + "%";

            BrowserWindow.ExecuteJs($"document.getElementById(\"{id}\").setAttribute(\"style\", \"width:{currentValue}%\"); document.getElementById(\"{id}\").innerHTML=\"{currentValueText}\"");
        }

        private void TriggerTest(object[] args)
        {
            //Chat.Output(args[0].ToString());
            RAGE.Events.CallRemote("BROWSER_TEST", args[0].ToString());
        }

        public void Destroy()
        {
            if (BrowserWindow != null)
            {
                BrowserWindow.Destroy();
                BrowserWindow = null;
                BrowserModel = null;
                Cursor.Visible = false;
            }
        }

        public void ProvideError(params object[] args)
        {
            Events.CallRemote("BROWSER_TEXT", args);
        }

        public void EventTrigger(params object[] args)
        {
            RAGE.Events.CallRemote("BROWSER_TEXT", args);
            if (!Guid.TryParse(args[0].ToString(), out Guid guid))
            {
                Chat.Output("We got him: " + guid);
                return;
            }


            string returnEvent = args[1].ToString();

            List<BrowserClickEventResponse> response = JsonConvert.DeserializeObject<List<BrowserClickEventResponse>>(args[2].ToString());

            if (response == null)
                return;

            //List<bool> booleans = new List<bool>();
            //List<string> strings = new List<string>();

            //foreach (var resp in response)
            //{
            //    switch(resp.Type)
            //    {
            //        case BrowserElementType.Checkbox:
            //            if (bool.TryParse(resp.Value, out bool checkboxChecked))
            //                booleans.Add(checkboxChecked);
            //            break;
            //        case BrowserElementType.TextBox:
            //            strings.Add(resp.Value);
            //            break;
            //        default:
            //            strings.Add(resp.Value);
            //            break;
            //    }
            //}

            RAGE.Events.CallRemote(returnEvent, response);
        }


        public void CreateBrowserEvents(params object[] args)
        {
            try
            {
                this.BrowserModel = JsonConvert.DeserializeObject<Browser>(args[0].ToString());

                if (BrowserWindow != null)
                {
                    BrowserWindow.Destroy();
                    BrowserWindow = null;
                }
                BrowserWindow = new HtmlWindow(BrowserModel.Path)
                {
                    Active = false
                };
                BrowserWindow.Active = true;

                Cursor.Visible = true;

                List<IBrowserElement> allElements = new List<IBrowserElement>();

                allElements.AddRange(BrowserModel.Buttons);
                allElements.AddRange(BrowserModel.Titles);
                allElements.AddRange(BrowserModel.Texts);
                allElements.AddRange(BrowserModel.CheckBoxes);
                allElements.AddRange(BrowserModel.TextBoxes);
                allElements.AddRange(BrowserModel.Cards);
                allElements.AddRange(BrowserModel.DropDowns);
                allElements.AddRange(BrowserModel.PasswordTextBoxes);
                allElements.AddRange(BrowserModel.ProgressBars);
                allElements.AddRange(BrowserModel.Container);
                allElements.AddRange(BrowserModel.Icons);

                string backgroundColor = "";
                if (!string.IsNullOrEmpty(this.BrowserModel.BackgroundColor))
                    backgroundColor = new Stylesheet("background-color", this.BrowserModel.BackgroundColor).ToString();

                string opacity = "";
                if (!string.IsNullOrEmpty(this.BrowserModel.Opacity))
                    opacity = new Stylesheet("opacity", this.BrowserModel.Opacity).ToString();

                string scrollBarX = "";
                if (this.BrowserModel.ScrollBarX)
                    scrollBarX = new Stylesheet("overflow-x", "scroll").ToString();
                else
                    scrollBarX = new Stylesheet("overflow-x", "hidden").ToString();

                string scrollBarY = "";
                if(this.BrowserModel.ScrollBarY)
                    scrollBarY = new Stylesheet("overflow-y", "scroll").ToString();
                else
                    scrollBarY = new Stylesheet("overflow-y", "hidden").ToString();

                string margin = "";
                if (!string.IsNullOrEmpty(this.BrowserModel.Margin))
                    margin = new Stylesheet("margin", this.BrowserModel.Margin).ToString();

                string padding = "";
                if(!string.IsNullOrEmpty(this.BrowserModel.Padding))
                    padding = new Stylesheet("padding", this.BrowserModel.Padding).ToString();

                string jsToExecute = $"document.getElementById('DavWebCreator').setAttribute(\"style\",\"text-align:center;{backgroundColor}{margin}{padding}{opacity}{scrollBarX}{scrollBarY}display:block;background-color:transparent;width:{this.BrowserModel.Width};height:{this.BrowserModel.Height}\");";
                jsToExecute += $"document.getElementById('DavWebCreator').setAttribute(\"z-index\",\"130;\");";
                string align = GetContentAlignment(BrowserModel.ContentPosition);

                string browserAlignAddClass = $"document.getElementById('DavWebCreator').className +=' p-2 {align}';";
              
                switch (this.BrowserModel.Type)
                {
                    case BrowserType.Custom: // Default


                        // ProvideError(mid);
                        string mid = GetHtmlStringByElements(allElements);
                        if (!string.IsNullOrEmpty(mid))
                        {
                            BrowserWindow.ExecuteJs($"{browserAlignAddClass}{jsToExecute}document.getElementById('DavWebCreator').innerHTML = '{mid}';");
                        }
                        break;
                    //case BrowserType.YesNoDialog:
                    //    ProvideError("User Bro");
                    //    //BrowserYesNoDialog yesNoDialog = BrowserModel.YesNoDialog;
                    //    //string dialogHtml = GetYesNoDialogHtml(yesNoDialog);
                    //    //ProvideError(dialogHtml);
                    //    //BrowserWindow.ExecuteJs($"{browserAlignAddClass}{jsToExecute}document.getElementById('DavWebCreator').innerHTML = '{dialogHtml}';");
                    //    break;
                    case BrowserType.Selection:
                        string selection = GetHtmlStringByElements(allElements);
                        ProvideError(selection);
                        BrowserWindow.ExecuteJs($"document.getElementById('Content').innerHTML ='{selection}';{jsToExecute}");
                        break;
                }
            }
            catch (Exception e)
            {
                ProvideError(e);
            }
        }

        private void CloseBrowser(params object[] args)
        {
            if (!string.IsNullOrEmpty(this.BrowserModel.CloseEvent))
                Events.CallRemote(this.BrowserModel.CloseEvent);

            Destroy();
        }


        public string GetStylesToAppendByBrowserFont(IBrowserFont text, bool blockElement = false)
        {
            if (text == null)
            {
                ProvideError("FFS-777: ", "IBrowserFont properties couldn't be resolved.");
                return "";
            }

            string returnVal = "";

            if (text.Bold)
            {
                Stylesheet fontWeight = new Stylesheet("font-weight", "bold");
                returnVal += fontWeight;
            }


            if (!string.IsNullOrEmpty((text.FontFamily)))
            {
                Stylesheet fontFamily = new Stylesheet("font-family", text.FontFamily);
                returnVal += fontFamily;
            }
            if (!string.IsNullOrEmpty((text.FontSize)))
            {
                Stylesheet fontSize = new Stylesheet("font-size", text.FontSize);
                returnVal += fontSize;
            }
            if (!string.IsNullOrEmpty(text.FontColor))
            {
                Stylesheet color = new Stylesheet("color", text.FontColor);
                returnVal += color;
            }
            if (!string.IsNullOrEmpty((text?.TextAlign.ToString())))
            {
                Stylesheet textAlign = new Stylesheet("text-align", text.TextAlign.ToString());
                returnVal += textAlign;
            }


            return returnVal;
        }

        public string GetHtmlStringByElements(List<IBrowserElement> elements, List<Guid> alreadyAdded = null)
        {
            if (alreadyAdded == null)
                alreadyAdded = new List<Guid>();

            StringBuilder rawHtmlBuilder = new StringBuilder();

            if (elements.Any(w=> w.Type == BrowserElementType.Card))
            {
                List<IBrowserElement> container = elements.Where(w => w.Type == BrowserElementType.Card).ToList();
                foreach (IBrowserElement element in container)
                {
                    BrowserCard browserCard = element as BrowserCard;
                    if (browserCard == null)
                    {
                        ProvideError("Error", "Browser Card is null.");
                        continue;
                    }

                    alreadyAdded.Add(browserCard.Id);
                    switch (browserCard.CardType)
                    {
                        case BrowserCardType.HeaderAndContent:

                            string cardHtml = GetHtmlToAppendByBrowserCard(browserCard);
                            foreach (IBrowserElement elem in elements.OrderBy(o => o.OrderIndex))
                            {
                                if (alreadyAdded.Contains(elem.Id))
                                    continue;

                                if (!browserCard.ChildElements.Contains(elem.Id))
                                    continue;

                                alreadyAdded.Add(elem.Id);


                                switch (elem.Type)
                                {
                                    case BrowserElementType.Card:
                                        BrowserCard card = elem as BrowserCard;
                                        string align = GetContentAlignment(card.ItemAlignment);

                                        cardHtml += $"<div class=\"p-2 {align}\">";
                                       

                                        if (card.ChildElements.Count <= 0)
                                        {
                                            cardHtml += GetHtmlStringByBrowserElement(elem);
                                            cardHtml += "</div></div></div>";
                                            continue;
                                        }


                                        cardHtml += GetHtmlStringByBrowserElement(elem);

                                        for (int i = 1; i <= card.CurrentRow; i++)
                                        {
                                            IEnumerable<IBrowserElement> childElements = elements.Where(w =>
                                            card.ChildElements.Contains(w.Id) && w.Row == i);
                                            if (!childElements.Any())
                                                continue;

                                            string itemAlign = GetContentAlignment(card.ItemAlignment);

                                            cardHtml += $"<div class=\"p-2 {itemAlign} {card.FlexWrap.ToString().Replace('_','-')}\" style=\"margin:0;padding:0;\">";
                                            foreach (IBrowserElement child in childElements.OrderBy(w => w.OrderIndex))
                                            {
                                                string childAlign = GetContentAlignment(child.ItemAlignment);

                                                cardHtml += $"<div class=\"p2 {childAlign} {child.FlexDirection.ToString().Replace('_','-')}\">";
                                                cardHtml += GetHtmlStringByBrowserElement(child);
                                                alreadyAdded.Add(child.Id);

                                                switch (child.Type)
                                                {
                                                    case BrowserElementType.Card:
                                                        cardHtml += "</div></div></div>";
                                                        break;
                                                }

                                                cardHtml += "</div>";
                                            }

                                            cardHtml += "</div></div>";
                                        }

                                        cardHtml += "</div></div>";

                                        break;
                                    default:
                                        string itemAlignment = GetContentAlignment(elem.ItemAlignment);

                                        cardHtml += $"<div class=\"p-2 {itemAlignment}\">";
                                        cardHtml += GetHtmlStringByBrowserElement(elem);
                                        cardHtml += "</div>";
                                        break;
                                }
                            }

                            rawHtmlBuilder.Append(cardHtml + "</div></div>");

                            break;
                        case BrowserCardType.HeaderDescriptionAndButtonWithIcon:


                            break;
                    }
                }
            }

            foreach (IBrowserElement element in elements.OrderBy(w => w.OrderIndex))
            {
                if (alreadyAdded.Contains(element.Id))
                    continue;

                rawHtmlBuilder.Append(GetHtmlStringByBrowserElement(element));
            }

            return rawHtmlBuilder.ToString();
        }

        public string GetHtmlStringByBrowserElement(IBrowserElement element)
        {
            try
            {
                StringBuilder rawHtmlBuilder = new StringBuilder();
                switch (element.Type)
                {
                    case BrowserElementType.Card:
                        BrowserCard card = element as BrowserCard;
                        switch (card.CardType)
                        {
                            case BrowserCardType.HeaderDescriptionAndButtonWithIcon:
                                string cardHtml2 = GetHtmlToAppendByBrowserCard(card);
                                rawHtmlBuilder.Append(cardHtml2);
                                break;
                            default:
                                string cardHtml3 = GetHtmlToAppendByBrowserCard(card);
                                rawHtmlBuilder.Append(cardHtml3);
                                break;
                        }
                        break;
                    case BrowserElementType.BrowserBoxSelection:
                        BrowserBoxSelection boxSelection = element as BrowserBoxSelection;
                        rawHtmlBuilder.Append(GetHtmlToAppendByBrowserBoxSelection(boxSelection));
                        break;
                    case BrowserElementType.Title:
                        BrowserTitle titleElement = element as BrowserTitle;
                        rawHtmlBuilder.Append(GetHtmlToAppendByBrowserTitle(titleElement));
                        break;
                    case BrowserElementType.Text:
                        BrowserText textElement = element as BrowserText;
                        rawHtmlBuilder.Append(GetHtmlToAppendByBrowserTextElement(textElement));
                        break;
                    case BrowserElementType.Checkbox:
                        BrowserCheckBox checkBoxElement = element as BrowserCheckBox;
                        rawHtmlBuilder.Append(GetHtmlToAppendByCheckBox(checkBoxElement));
                        break;
                    case BrowserElementType.Button:
                        BrowserButton browserButton = element as BrowserButton;
                        rawHtmlBuilder.Append(GetHtmlToAppendByBrowserButton(browserButton));
                        break;
                    case BrowserElementType.TextBox:
                        BrowserTextBox browserTextBox = element as BrowserTextBox;
                        rawHtmlBuilder.Append(GetHtmlToAppendByBrowserTextBox(browserTextBox));
                        break;
                    case BrowserElementType.Password:
                        BrowserPasswordTextBox browserPasswordTextBox = element as BrowserPasswordTextBox;
                        rawHtmlBuilder.Append(GetHtmlToAppendByBrowserPasswordTextBox(browserPasswordTextBox));
                        break;
                    case BrowserElementType.DropDown:
                        BrowserDropDown dropDown = element as BrowserDropDown;
                        rawHtmlBuilder.Append(GetHtmlToAppendByBrowserDropDown(dropDown));
                        break;
                    case BrowserElementType.ProgressBar:
                        BrowserProgressbar progressBar = element as BrowserProgressbar;
                        rawHtmlBuilder.Append(GetHtmlToAppendByBrowserProgressBar(progressBar));
                        break;
                    case BrowserElementType.Icon:
                        BrowserButtonIcon icon = element as BrowserButtonIcon;
                        rawHtmlBuilder.Append(GetHtmlToAppendByIcon(icon));
                        break;
                }

                return rawHtmlBuilder.ToString();
            }
            catch (Exception e)
            {
                ProvideError(e);
                return "";
            }
        }

        private string GetHtmlToAppendByBrowserBoxSelection(BrowserBoxSelection boxSelection)
        {
            string html = "<div class=\"container\">";
            html += "<div class=\"row\">";
            html += "<div class=\"col\">";


            return "";
        }

        public string GetDefaultStyleSettings(IBrowserElement browserElement, bool excludeOverflow = false, bool excludeWidthAndHeight = false)
        {
            string returner = "";
            if (!excludeWidthAndHeight)
            {
                if (!string.IsNullOrEmpty((browserElement.Width)))
                {
                    Stylesheet width = new Stylesheet("width", browserElement.Width);
                    returner += width;
                }

                if (!string.IsNullOrEmpty((browserElement.Height)))
                {
                    Stylesheet height = new Stylesheet("height", browserElement.Height);

                    returner += height;
                }
            }

            if (!string.IsNullOrEmpty((browserElement.Cursor)))
            {
                Stylesheet cursor = new Stylesheet("cursor", browserElement.Cursor);
                returner += cursor;
            }
            if (!string.IsNullOrEmpty((browserElement.Margin)))
            {
                Stylesheet margin = new Stylesheet("margin", browserElement.Margin);
                returner += margin;
            }
            if (!string.IsNullOrEmpty((browserElement.Padding)))
            {
                Stylesheet padding = new Stylesheet("padding", browserElement.Padding);
                returner += padding;
            }
            if (!string.IsNullOrEmpty(browserElement.BackgroundColor))
            {
                Stylesheet backgroundColor = new Stylesheet("background-color", browserElement.BackgroundColor);
                returner += backgroundColor;
            }
            if (!string.IsNullOrEmpty(browserElement.Opacity))
            {
                Stylesheet opacity = new Stylesheet("opacity", browserElement.Opacity);
                returner += opacity;
            }

            if (browserElement.ScrollBarX)
            {
                Stylesheet scrollBarX = new Stylesheet("overflow-x","scroll");
                returner += scrollBarX;
            }
            else
            {
                Stylesheet scrollBarX = new Stylesheet("overflow-x", "hidden");
                returner += scrollBarX;
            }

            if (!excludeOverflow)
            {
                if (browserElement.ScrollBarY)
                {
                    Stylesheet scrollBarY = new Stylesheet("overflow-y", "scroll");
                    returner += scrollBarY;
                }
                else
                {
                    Stylesheet scrollBarY = new Stylesheet("overflow-y", "hidden");
                    returner += scrollBarY;
                }
            }

            if (browserElement.BorderStyle != BorderStyle.none)
            {
                if (string.IsNullOrEmpty(browserElement.BorderWidth))
                    browserElement.BorderWidth = "1px";
                if (string.IsNullOrEmpty(browserElement.BorderColor))
                    browserElement.BorderColor = "black";

                string borderStyle = browserElement.BorderStyle.ToString();
                if (browserElement.BorderStyle == BorderStyle.doublee)
                    borderStyle = "double";

                string border = new Stylesheet("border", $"{browserElement.BorderWidth} {borderStyle} {browserElement.BorderColor}").ToString();
                returner += border;
            }
            

            return returner;
        }

        #region Html Converter for Models
        public string GetHtmlToAppendByBrowserTitle(BrowserTitle titleElement)
        {
            string styles = GetStylesToAppendByBrowserFont(titleElement)
                + GetDefaultStyleSettings(titleElement);
            string animationClasses = GetAnimationClass(titleElement.AnimationType);

            return $"<h1 id=\"{titleElement.Id}\" class=\"{titleElement.StyleClass} {animationClasses}\" style=\"{styles}\">{titleElement.Title}</h1>";
        }
        public string GetHtmlToAppendByCheckBox(BrowserCheckBox checkBox)
        {
            string widthAndHeight = GetDefaultStyleSettings(checkBox);
            string fontSettings = GetStylesToAppendByBrowserFont(checkBox);
            string animationClasses = GetAnimationClass(checkBox.AnimationType);

            return $"<div class=\"all-divs\"><label for=\"{checkBox.Id}\" style=\"{fontSettings}\">{checkBox.Text}</label> <input id=\"{checkBox.Id}\" type=\"checkbox\"  class=\"{animationClasses}\"style=\"{fontSettings}{widthAndHeight}\" {(checkBox.IsChecked ? "checked" : "")}></div>";
        }

        public string GetYesNoDialogHtml(BrowserYesNoDialog yesNoElement)
        {
            var elements = new List<IBrowserElement>()
            {
                yesNoElement.Card,
                yesNoElement.Title,
                yesNoElement.SubTitle,
                yesNoElement.SuccessButton,
                yesNoElement.DismissButton
            };

            var html = GetHtmlStringByElements(elements);

            return html;
        }

        public string GetHtmlByBrowserCard(BrowserYesNoDialog yesNoElement)
        {
            string widthAndHeight = GetDefaultStyleSettings(yesNoElement);
            string fontSettings = GetStylesToAppendByBrowserFont(yesNoElement);
            string animationClasses = GetAnimationClass(yesNoElement.AnimationType);

            string widthAndHeightTitle = GetDefaultStyleSettings(yesNoElement.Title);
            string fontSettingsTitle = GetStylesToAppendByBrowserFont(yesNoElement.Title);
            string animationClassesTitle = GetAnimationClass(yesNoElement.Title.AnimationType);

            string widthAndHeightSubTitle = GetDefaultStyleSettings(yesNoElement.SubTitle);
            string fontSettingsSubTitle = GetStylesToAppendByBrowserFont(yesNoElement.SubTitle);
            string animationClassesSubTitle = GetAnimationClass(yesNoElement.SubTitle.AnimationType);

            string widthAndHeightText = GetDefaultStyleSettings(yesNoElement.Text);
            string fontSettingsText = GetStylesToAppendByBrowserFont(yesNoElement.Text);
            string animationClassesText = GetAnimationClass(yesNoElement.Text.AnimationType);


            return
                $"<div class=\"card\" style=\"{fontSettings}{widthAndHeight}\" class=\"{animationClasses}\"><div class=\"card-header\" class=\"{animationClassesTitle}\" style=\"{widthAndHeightTitle}{fontSettingsTitle}\">{yesNoElement.Title.Title}</div><div class=\"card-body\" style=\"text-align:center;\"><h5 class=\"card-title {animationClassesSubTitle}\" style=\"{widthAndHeightSubTitle}{fontSettingsSubTitle}\">" +
                $"{yesNoElement.SubTitle.Title}" +
                $"</h5><p class=\"card-text\" style=\"{widthAndHeightText}{fontSettingsText}\" class=\"{animationClassesText}\">" +
                $"{yesNoElement.Text.Text}" +
                $"</p>{GetHtmlToAppendByBrowserButton(yesNoElement.DismissButton)}" +
                $"{GetHtmlToAppendByBrowserButton(yesNoElement.SuccessButton)}</div></div>";
        }

        public string GetHtmlToAppendByBrowserTextElement(BrowserText textElement)
        {
            string fontSettings = GetStylesToAppendByBrowserFont(textElement);
            string widthAndHeight = GetDefaultStyleSettings(textElement);
            string animationClasses = GetAnimationClass(textElement.AnimationType);

            return $"<p id=\"{textElement.Id}\" class=\"text-break {animationClasses}\" style=\"{fontSettings}{widthAndHeight}\">{textElement.Text}</p>";
        }

        public string GetHtmlToAppendByBrowserCard(BrowserCard card)
        {
            string fontSettings = GetStylesToAppendByBrowserFont(card);
            string defaultSettings = GetDefaultStyleSettings(card,true);
            string animationClasses = GetAnimationClass(card.AnimationType);

            string exitButton = "";

            if (card.ExitButton)
            {
                exitButton = $"<button type=\"button\" class=\"close\" aria-label=\"Close\"" +
                             " onclick=\"onExitButtonClick()\"><span aria-hidden=\"true\">&times;</span></button>";
            }

            string topImage = "";
            if ((!string.IsNullOrEmpty(card.Image)))
                topImage = $"<img src=\"{card.Image}\" class=\"card-img-top\" alt=\"{card.Image}\">";

            var contentTitleStyle = GetStylesToAppendByBrowserFont(card.ContentTitle);
            var contentTitleStyles = GetDefaultStyleSettings(card.ContentTitle);
            string contentTitle = !string.IsNullOrEmpty(card.ContentTitle.Title)
                ? $"<h5 class=\"card-title\" style=\"{contentTitleStyle}{contentTitleStyles}\">{card.ContentTitle.Title}</h5>"
                : "";

            var contentStyles = GetDefaultStyleSettings(card.ContentText);
            var contentFontStyle = GetStylesToAppendByBrowserFont(card.ContentText);
            string contentText = !string.IsNullOrEmpty(card.ContentText.Text) ? $"<p class=\"card-text\" style=\"{contentFontStyle}{contentStyles}\">{card.ContentText.Text}</p>" : "";

            string flexWrap = card.FlexWrap.ToString().Replace('_','-');
            string flex = card.FlexDirection == BrowserFlexDirection.unset ? "" : card.FlexDirection.ToString().Replace('_', '-');

            string contentAlign = GetContentAlignment(card.ItemAlignment);

            var cardTitleFontStyle = GetStylesToAppendByBrowserFont(card.CardTitle);
            var cardTitleStyles = GetDefaultStyleSettings(card.CardTitle);

            string cardHeader = !string.IsNullOrEmpty(card.CardTitle.Title)
                ? $"<h5 class=\"card-header\" style=\"{cardTitleFontStyle}{cardTitleStyles}\">{card.CardTitle.Title}{exitButton}</h5>"
                : "";

            return
                $"<div class=\"card container{animationClasses}\" style=\"{defaultSettings}{fontSettings}\">{topImage}{cardHeader}{contentTitle}{contentText}<div class=\"cardContentus d-flex {flexWrap} {flex} {contentAlign}\">";
        }

        public string GetHtmlToAppendByBrowserCardForHeaderWithContentAndButtons(BrowserCard card)
        {
            string fontSettings = GetStylesToAppendByBrowserFont(card);
            string defaultSettings = GetDefaultStyleSettings(card);
            string animationClasses = GetAnimationClass(card.AnimationType);


            string topImage = "";
            if ((!string.IsNullOrEmpty(card.Image)))
            {
               topImage = $"<img src=\"{card.Image}\" class=\"card-img-top\" alt=\"{card.Image}\">";
            }
            
            return $"<div class=\"card {animationClasses}\" style=\"{defaultSettings}\">{topImage}<div class=\"card-body\"><h5 style=\"{fontSettings}\" class=\"card-title {fontSettings}\">{card.ContentTitle}</h5><p class=\"card-text\" style=\"{fontSettings}\">" +
                   $"{card.ContentText}</p>";
        }
        public string GetContentAlignment(BrowserContentAlign align)
        {
            string contentAlign = "";

            switch (align)
            {
                case BrowserContentAlign.Start:
                    contentAlign = "d-flex justify-content-start";
                    break;
                case BrowserContentAlign.Center:
                    contentAlign = "d-flex justify-content-center";
                    break;
                case BrowserContentAlign.End:
                    contentAlign = "d-flex justify-content-end";
                    break;
                case BrowserContentAlign.Around:
                    contentAlign = "d-flex justify-content-around";
                    break;
                case BrowserContentAlign.Between:
                    contentAlign = "d-flex justify-content-between";
                    break;
                case BrowserContentAlign.Start_large:
                    contentAlign = "d-flex justify-content-lg-start";
                    break;
                case BrowserContentAlign.End_large:
                    contentAlign = "d-flex justify-content-lg-end";
                    break;
                case BrowserContentAlign.Center_large:
                    contentAlign = "d-flex justify-content-lg-center";
                    break;
                case BrowserContentAlign.Around_large:
                    contentAlign = "d-flex justify-content-lg-around";
                    break;
                case BrowserContentAlign.Between_large:
                    contentAlign = "d-flex justify-content-lg-between";
                    break;
                case BrowserContentAlign.Start_xl:
                    contentAlign = "d-flex justify-content-xl-start";
                    break;
                case BrowserContentAlign.End_xl:
                    contentAlign = "d-flex justify-content-xl-end";
                    break;                                 
                case BrowserContentAlign.Center_xl:        
                    contentAlign = "d-flex justify-content-xl-center";
                    break;                                 
                case BrowserContentAlign.Around_xl:        
                    contentAlign = "d-flex justify-content-xl-around";
                    break;                                 
                case BrowserContentAlign.Between_xl:       
                    contentAlign = "d-flex justify-content-xl-between";
                    break;
                case BrowserContentAlign.Start_medium:
                    contentAlign = "d-flex justify-content-md-start";
                    break;
                case BrowserContentAlign.End_medium:
                    contentAlign = "d-flex justify-content-md-end";
                    break;
                case BrowserContentAlign.Center_medium:
                    contentAlign = "d-flex justify-content-md-center";
                    break;
                case BrowserContentAlign.Around_medium:
                    contentAlign = "d-flex justify-content-md-around";
                    break;
                case BrowserContentAlign.Between_medium:
                    contentAlign = "d-flex justify-content-md-between";
                    break;
                case BrowserContentAlign.Start_small:
                    contentAlign = "d-flex justify-content-sm-start";
                    break;
                case BrowserContentAlign.End_small:
                    contentAlign = "d-flex justify-content-sm-end";
                    break;
                case BrowserContentAlign.Center_small:
                    contentAlign = "d-flex justify-content-sm-center";
                    break;
                case BrowserContentAlign.Around_small:
                    contentAlign = "d-flex justify-content-sm-around";
                    break;
                case BrowserContentAlign.Between_small:
                    contentAlign = "d-flex justify-content-sm-between";
                    break;
            }

            return contentAlign;
        }
        public string GetHtmlToAppendByIcon(BrowserButtonIcon icon)
        {
            string fontSettings = GetStylesToAppendByBrowserFont(icon);
            string defaultSettings = GetDefaultStyleSettings(icon);
            string animationClasses = GetAnimationClass(icon.AnimationType);

            OnClickReturn onClickEvent = new OnClickReturn()
            {
                RemoteEvent = icon.RemoteEvent,
                ReturnValues = icon.ReturnObjects,
                Id = icon.Id
            };

            string enabled = icon.Enabled ? $"onclick = btnClickEventByElement([{ JsonConvert.SerializeObject(onClickEvent)}])" : "disabled";

            return $"<i title=\"{icon.Text}\" style=\"{defaultSettings}\" class=\"{icon.StyleClass} {animationClasses}\"" +
                    $"{enabled}></i>";
        }

        public string GetHtmlToAppendByBrowserTextBox(BrowserTextBox textBox)
        {
            string fontSettings = GetStylesToAppendByBrowserFont(textBox);
            string settings = GetDefaultStyleSettings(textBox);

            string labelFontSettings = GetStylesToAppendByBrowserFont(textBox.Label);

            return $"<label for=\"{textBox.Id}\" style=\"{labelFontSettings}\">{textBox.Label.Text}<input type=\"text\" class=\"form-control\" id=\"{textBox.Id}\" MaxLength=\"{textBox.MaxLength}\" placeholder=\"{textBox.PlaceHolder}\" value=\"{textBox.Text}\" style=\"{fontSettings}{settings}\" {(textBox.ReadOnly ? "readonly" : "")}></label>";
        }

        public string GetHtmlToAppendByBrowserPasswordTextBox(BrowserPasswordTextBox passwordTextBox)
        {
            string fontSettings = GetStylesToAppendByBrowserFont(passwordTextBox);
            string settings = GetDefaultStyleSettings(passwordTextBox);
            string animationClasses = GetAnimationClass(passwordTextBox.AnimationType);

            string labelFontSettings = GetStylesToAppendByBrowserFont(passwordTextBox.Label);

            return $"<label for=\"{passwordTextBox.Id}\" style=\"{labelFontSettings}\">{passwordTextBox.Label.Text}<input type=\"password\" class=\"form-control {animationClasses}\" id=\"{passwordTextBox.Id}\" MaxLength=\"{passwordTextBox.MaxLength}\" placeholder=\"{passwordTextBox.PlaceHolder}\" value=\"{passwordTextBox.Text}\" style=\"{fontSettings}{settings}\" minLength=\"{passwordTextBox.MinLength}\" {(passwordTextBox.ReadOnly ? "readonly" : "")} {(passwordTextBox.IsRequired ? "required" : "")}></label>";
        }

        public string GetAnimationClass(BrowserElementAnimationType type)
        {
            string animationClasses = "";

            switch (type)
            {
                case BrowserElementAnimationType.HeartBeat:
                    animationClasses = "heart animated css";
                    break;
                case BrowserElementAnimationType.Rotation:
                    animationClasses = "rotator animation";
                    break;
            }

            return animationClasses;
        }

        public string GetHtmlToAppendByBrowserProgressBar(BrowserProgressbar progressBar)
        {
            string styles = GetStylesToAppendByBrowserFont(progressBar);
            string defaultStyle = GetDefaultStyleSettings(progressBar, excludeWidthAndHeight: true);
            string animations = GetAnimationClass(progressBar.AnimationType);

            string progressValue = "";

            if (progressBar.ShowCurrentValue)
                progressValue = progressBar.CurrentValue + "%";

            
            return $"<div class=\"progress {GetContentAlignment(progressBar.ItemInlineAlignment)}\" style=\"{styles}{defaultStyle}width:{progressBar.Width};height:{progressBar.Height};\" class=\"{animations}\"><div id=\"{progressBar.Id}\" class=\"progress-bar {progressBar.StyleClass}\" role=\"progressbar\" style=\"{defaultStyle}{styles}width: {progressBar.CurrentValue}%\" aria-valuenow=\"{progressBar.CurrentValue}\" aria-valuemin=\"{progressBar.MinValue}\" aria-valuemax=\"{progressBar.MaxValue}\">{progressValue}</div></div>";
        }
        public string GetHtmlToAppendByBrowserDropDown(BrowserDropDown dropDown)
        {
            string fontSettingsLabel = GetStylesToAppendByBrowserFont(dropDown.Label);
            string settingsLabel = GetDefaultStyleSettings(dropDown.Label);
            string labelAnimations = GetAnimationClass(dropDown.Label.AnimationType);

            string fontSettings = GetStylesToAppendByBrowserFont(dropDown);
            string settings = GetDefaultStyleSettings(dropDown);
            string animationClasses = GetAnimationClass(dropDown.AnimationType);

            string dropDownOptions = "";
            foreach (BrowserDropDownValue ddV in dropDown.Values)
            {
                dropDownOptions += $"<li id=\"{ddV.Id}\" class=\"{labelAnimations}\" style=\"{fontSettingsLabel}{settingsLabel}\"" +
                                   $"onclick=selectionChanged(\"{ddV.Id}\",\"{dropDown.Id}\") data-value=\"{ddV.HiddenValue}\">{ddV.Value}</li>"; //\"
            }

            string dd = "<div class=\"Wrapper\">" +
                        "<div class=\"selectorinio\" onclick=\"show();\">" +
                        $"<div id=\"{dropDown.Id}\" class=\"label {animationClasses}\" style=\"{settings}{fontSettings}\">{dropDown.Label.Text}</div>" +
                        "<b class=\"button\">▾</b></div>" +
                        "<div id=\"options\" class=\"hidden\">" +
                        "<ul>" + dropDownOptions + "</ul></div></div>";

            return dd;
        }

        public string GetHtmlToAppendByBrowserButton(BrowserButton button)
        {
            string fontSettings = GetStylesToAppendByBrowserFont(button);
            string widthAndHeight = GetDefaultStyleSettings(button);

            OnClickReturn onClickEvent = new OnClickReturn()
            {
                RemoteEvent = button.RemoteEvent,
                ReturnValues = button.ReturnObjects,
                Id = button.Id
            };

            string animationClasses = GetAnimationClass(button.AnimationType);

            string enabled = button.Enabled ? $"onclick=btnClickEventByElement([{JsonConvert.SerializeObject(onClickEvent)}])" : "disabled";

            string outa = $"<input id=\"{button.Id}\" type=\"button\" class=\"{button.StyleClass} {animationClasses}\" value=\"{button.Text}\"" +
                          $"style=\"{fontSettings}{widthAndHeight}\" {enabled}>";

            return outa;
        }


        private string GetHtmlValueById(Guid id)
        {
            return $"document.getElementById(\"{id}\").value";
        }

        public string GetTextAlignByPosition(BrowserTextAlign position)
        {
            Stylesheet block = new Stylesheet("display", "block");

            string alignValue = "center";

            switch (position)
            {
                case BrowserTextAlign.center:
                    alignValue = "center";
                    break;
                case BrowserTextAlign.left:
                    alignValue = "left";
                    break;
                case BrowserTextAlign.right:
                    alignValue = "right";
                    break;
            }
            Stylesheet textAlign = new Stylesheet("text-align", alignValue);

            return block + " " + textAlign;
        }
        #endregion


    }
}
