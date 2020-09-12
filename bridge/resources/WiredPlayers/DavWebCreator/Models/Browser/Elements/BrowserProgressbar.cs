using Browsers.Models.BrowserModels;
using DavWebCreator.Resources.Models.Browser.Elements;
using GTANetworkAPI;
using System;
using System.Timers;
using DavWebCreator.Server.Models.Browser.Elements.Fonts;
using Newtonsoft.Json;

namespace DavWebCreator.Server.Models.Browser.Elements
{
    [Serializable]
    public class BrowserProgressBar : BrowserElementWithEvent, IBrowserFont
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public int CurrentValue { get; set; }
        public int MilleSecondsProgressInterval { get; set; }
        public int ProgressStep { get; set; }
        public bool ShowCurrentValue { get; set; }
        public bool StartTimerInstant { get; set; }
        public string ElapsedRemoteEvent { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public string FontColor { get; set; }
        public bool Bold { get; set; }
        public BrowserTextAlign TextAlign { get; set; }
        public BrowserContentAlign ItemInlineAlignment { get; set; }

        private Client player;
        private Timer timer;


        public delegate void ProgressBarFinished(Client player, BrowserProgressBar progressBar);
        [JsonIgnore]
        public ProgressBarFinished ProgressBarFinishedEvent;

        public BrowserProgressBar(int currentValue, int progressStep, int millisecondsProgressInterval, bool startTimerInstant = true)
                : base(BrowserElementType.ProgressBar, "")
        {
            this.Width = "300px";
            this.Height = "25px";
            this.MinValue = 0;
            this.MaxValue = 100;
            this.CurrentValue = currentValue;
            this.MilleSecondsProgressInterval = millisecondsProgressInterval;
            this.SetPredefinedProgressBarStyle(BrowserProgressBarStyle.Blue_Striped);
            this.ProgressStep = progressStep;
            this.StartTimerInstant = startTimerInstant;
            this.ItemInlineAlignment = BrowserContentAlign.Start;
        }

        public void UpdateCurrentValue(Client player)
        {
            this.player = player;
            if (timer == null)
            {
                timer = new Timer
                {
                    Interval = MilleSecondsProgressInterval,
                    AutoReset = true
                };
                timer.Elapsed += Callback;
            }

            if(StartTimerInstant)
                timer.Start();
        }

        private void Callback(object sender, ElapsedEventArgs e)
        {

            if (CurrentValue >= MaxValue)
            {
                timer.Stop();
                if (ProgressBarFinishedEvent == null)
                {
                    NAPI.Util.ConsoleOutput("NO FINISHED EVENT DECLARED FOR PROGRESS BAR");
                    return;
                }

                ProgressBarFinishedEvent.Invoke(player, this);
                return;
            }

            CurrentValue += ProgressStep;
            UpdateProgressBarValue(Id, CurrentValue, ShowCurrentValue);
        }

        public void UpdateProgressBarValue(Guid id, int currentValue, bool showCurrentvalue)
        {
            player.TriggerEvent("UPDATE_PROGRESSBAR", Id, CurrentValue, ShowCurrentValue);
        }

        public void SetPredefinedProgressBarStyle(BrowserProgressBarStyle style)
        {
            switch (style)
            {
                case BrowserProgressBarStyle.Blue_Striped:
                    this.StyleClass = "progress-bar-striped";
                    break;
                case BrowserProgressBarStyle.Green_Striped:
                    this.StyleClass = "progress-bar-striped bg-success";
                    break;
                case BrowserProgressBarStyle.Orange_Striped:
                    this.StyleClass = "progress-bar-striped bg-warning";
                    break;
                case BrowserProgressBarStyle.Red_Striped:
                    this.StyleClass = "progress-bar-striped bg-danger";
                    break;
                case BrowserProgressBarStyle.Blue:
                    //this.StyleClass = "";
                    break;
                case BrowserProgressBarStyle.Green:
                    this.StyleClass = "bg-success";
                    break;
                case BrowserProgressBarStyle.Orange:
                    this.StyleClass = "bg-warning";
                    break;
                case BrowserProgressBarStyle.Red:
                    this.StyleClass = "bg-danger";
                    break;
            }
        }
    }


    public enum BrowserProgressBarStyle
    {
        Blue,
        Green,
        Light_Blue,
        Orange,
        Red,
        Blue_Striped,
        Green_Striped,
        Light_Blue_Striped,
        Orange_Striped,
        Red_Striped,

    }
}
