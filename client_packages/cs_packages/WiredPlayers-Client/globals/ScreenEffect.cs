using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using RAGE;
using RAGE.Game;

namespace DemoClient
{
    public class ScreenEffects : Events.Script
    {
        // just collapse the list
        public static List<string> ScreenEffectList = new List<string> {
            "SwitchHUDIn",
            "SwitchHUDOut",
            "FocusIn",
            "FocusOut",
            "MinigameEndNeutral",
            "MinigameEndTrevor",
            "MinigameEndFranklin",
            "MinigameEndMichael",
            "MinigameTransitionOut",
            "MinigameTransitionIn",
            "SwitchShortNeutralIn",
            "SwitchShortFranklinIn",
            "SwitchShortTrevorIn",
            "SwitchShortMichaelIn",
            "SwitchOpenMichaelIn",
            "SwitchOpenFranklinIn",
            "SwitchOpenTrevorIn",
            "SwitchHUDMichaelOut",
            "SwitchHUDFranklinOut",
            "SwitchHUDTrevorOut",
            "SwitchShortFranklinMid",
            "SwitchShortMichaelMid",
            "SwitchShortTrevorMid",
            "DeathFailOut",
            "CamPushInNeutral",
            "CamPushInFranklin",
            "CamPushInMichael",
            "CamPushInTrevor",
            "SwitchSceneFranklin",
            "SwitchSceneTrevor",
            "SwitchSceneMichael",
            "SwitchSceneNeutral",
            "MP_Celeb_Win",
            "MP_Celeb_Win_Out",
            "MP_Celeb_Lose",
            "MP_Celeb_Lose_Out",
            "DeathFailNeutralIn",
            "DeathFailMPDark",
            "DeathFailMPIn",
            "MP_Celeb_Preload_Fade",
            "PeyoteEndOut",
            "PeyoteEndIn",
            "PeyoteIn",
            "PeyoteOut",
            "MP_race_crash",
            "SuccessFranklin",
            "SuccessTrevor",
            "SuccessMichael",
            "DrugsMichaelAliensFightIn",
            "DrugsMichaelAliensFight",
            "DrugsMichaelAliensFightOut",
            "DrugsTrevorClownsFightIn",
            "DrugsTrevorClownsFight",
            "DrugsTrevorClownsFightOut",
            "HeistCelebPass",
            "HeistCelebPassBW",
            "HeistCelebEnd",
            "HeistCelebToast",
            "MenuMGHeistIn",
            "MenuMGTournamentIn",
            "MenuMGSelectionIn",
            "ChopVision",
            "DMT_flight_intro",
            "DMT_flight",
            "DrugsDrivingIn",
            "DrugsDrivingOut",
            "SwitchOpenNeutralFIB5",
            "HeistLocate",
            "MP_job_load",
            "RaceTurbo",
            "MP_intro_logo",
            "HeistTripSkipFade",
            "MenuMGHeistOut",
            "MP_corona_switch",
            "MenuMGSelectionTint",
            "SuccessNeutral",
            "ExplosionJosh3",
            "SniperOverlay",
            "RampageOut",
            "Rampage",
            "Dont_tazeme_bro"
        };

        private static bool _screenFxEnabled = false;
        private static string _currentScreenFx;
        private static int _ticks;
        public ScreenEffects()
        {
            Events.Add("EnableWeedFx", EnableScreenFx);
            Events.Add("DisableWeedFx", DisableScreenFx);
            Events.Add("EnableScreenFx", EnableScreenFx);
            Events.Add("DisableScreenFx", DisableScreenFx);
            Events.Tick += ScreenFxSwitcher;
        }
        private static void DisableScreenFx(object[] args)
        {
            // Start new screen effect
            Graphics.StopScreenEffect("DrugsTrevorClownsFightIn");
        }

        private static void EnableScreenFx(object[] args)
        {
            
            // Start new screen effect
            Graphics.StartScreenEffect("DrugsTrevorClownsFightIn", 0, true);
        }
        private static void DisableWeedFx(object[] args)
        {
            // Start new screen effect
            Graphics.StopScreenEffect("DrugsTrevorClownsFightIn");
        }

        private static void EnableWeedFx(object[] args)
        {

            // Start new screen effect
            Graphics.StartScreenEffect("DrugsTrevorClownsFightIn", 0, true);
        }

        private void ScreenFxSwitcher(List<Events.TickNametagData> nametags)
        {
            if (!_screenFxEnabled) return;

            // prevents the firing of too many key presses
            if (_ticks == 20)
            {
                // Previous screen effect
                if (Input.IsDown((int)ConsoleKey.LeftArrow))
                {
                    SwitchScreenFx(-1);
                }

                // Next screen effect
                if (Input.IsDown((int)ConsoleKey.RightArrow))
                {
                    SwitchScreenFx(1);
                }
                _ticks = 0;
            }

            _ticks++;
        }

        public void SwitchScreenFx(int next)
        {
            if (!string.IsNullOrWhiteSpace(_currentScreenFx))
            {
                // Stop old screen effect
                Graphics.StopScreenEffect(_currentScreenFx);

                // Get the next valid screen effect, if out of range restart at index 0
                var index = GetAValidIndex(ScreenEffectList.IndexOf(_currentScreenFx), next);

                // Set new screen effect and inform client
                _currentScreenFx = ScreenEffectList[index];
                Chat.Output($"Start screen effect [{index}]: {_currentScreenFx}");

                // Start new screen effect
                Graphics.StartScreenEffect(_currentScreenFx, 0, true);
            }
            else
            {
                _currentScreenFx = ScreenEffectList[0];
                Chat.Output($"Start screen effect [0]: {_currentScreenFx}");
                Graphics.StartScreenEffect(_currentScreenFx, 0, true);
            }
        }

        // Makes sure that we don't try to access an invalid index
        public int GetAValidIndex(int currentIndex, int modifier)
        {
            var newIndex = currentIndex + modifier;

            // Jump to the end of the list if we try to access an index below first element
            if (newIndex < 0)
            {
                return ScreenEffectList.Count - 1;
            }

            // Restart at 0 if we reach end of list
            if (newIndex >= ScreenEffectList.Count)
            {
                return 0;
            }

            // return previous / next screen effect index... Depending on the modifier
            return newIndex;
        }

        private static void StopAllScreenFx()
        {
            foreach (var fx in ScreenEffectList)
            {
                Graphics.StopScreenEffect(fx);
            }
        }
    }
}
