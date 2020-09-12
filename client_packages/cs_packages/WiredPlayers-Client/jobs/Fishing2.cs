using RAGE;
using RAGE.Elements;
using System;

namespace WiredPlayers_Client.jobs
{
    class Fishing2 : Events.Script
    {
        private static int width;
        private static int height;
        private static int fishingSuccess;
        private static int fishingBarPosition;
        private static int fishingAchieveStart;
        private static int fishingBarMin;
        private static int fishingBarMax;
        private static bool movementRight;

        public static int fishingState;

        public Fishing2()
        {
            Events.Add("startPlayerFishing2", StartPlayerFishingEvent);
            Events.Add("fishingBaitTaken2", FishingBaitTakenEvent);

            movementRight = true;
            fishingAchieveStart = 0;

            // Get the game resolution
            RAGE.Game.Graphics.GetActiveScreenResolution(ref width, ref height);

            // Calculate the bar's maximum and minimum values
            fishingBarMin = width - 425;
            fishingBarMax = width - 27;
        }

        private void StartPlayerFishingEvent(object[] args)
        {
            // Start the fishing minigame
            fishingState = 1;
            Player.LocalPlayer.FreezePosition(true);
        }

        private void FishingBaitTakenEvent(object[] args)
        {
            // Start the fishing minigame
            Random random = new Random();
            fishingAchieveStart = (int)Math.Round(random.NextDouble() * 390 + fishingBarMin);
            fishingSuccess = 0;
            fishingState = 3;
        }

        public static void DrawFishingMinigame()
        {
            if (RAGE.Game.Pad.IsControlJustPressed(0, 24))
            {
                switch(fishingState)
                {
                    case 1:
                        // Start fishing
                        fishingState = 2;
                        fishingBarPosition = width - 224;
                        Events.CallRemote("startFishingTimer2");
                        break;
                    case 2:
                        // Player didn't catch any fish
                        fishingState = -1;
                        Player.LocalPlayer.FreezePosition(false);
                        Events.CallRemote("fishingCanceled2");
                        break;
                    case 3:
                        if (fishingBarPosition > fishingAchieveStart && fishingBarPosition < fishingAchieveStart + 15)
                        {
                            // Valid catch
                            fishingSuccess++;

                            if (fishingSuccess == 3)
                            {
                                // Fishing succeed
                                fishingState = -1;
                                Player.LocalPlayer.FreezePosition(false);
                                Events.CallRemote("fishingSuccess2");
                            }
                            else
                            {
                                // Generate the new bars
                                movementRight = true;
                                fishingBarPosition = width - 224;

                                Random random = new Random();
                                fishingAchieveStart = (int)Math.Round(random.NextDouble() * 390 + fishingBarMin);
                            }
                        }
                        else
                        {
                            // Player failed catching
                            fishingState = -1;
                            Player.LocalPlayer.FreezePosition(false);
                            Events.CallRemote("fishingCanceled2");
                        }
                        break;
                }

                // Don't display anything
                return;
            }

            if (fishingState == 3)
            {
                // Draw the minigame bar
                RAGE.NUI.UIResRectangle.Draw(width - 425, height - 40, 400, 25, System.Drawing.Color.FromArgb(200, 0, 0, 0));

                // Draw the success bar
                RAGE.NUI.UIResRectangle.Draw(fishingAchieveStart, height - 40, 10, 25, System.Drawing.Color.FromArgb(255, 0, 255, 0));

                // Draw the moving bar
                RAGE.NUI.UIResRectangle.Draw(fishingBarPosition, height - 41, 2, 26, System.Drawing.Color.FromArgb(255, 255, 255, 255));

                if (movementRight)
                {
                    // Move the bar to the right
                    fishingBarPosition++;

                    if (fishingBarPosition > fishingBarMax)
                    {
                        fishingBarPosition = fishingBarMax;
                        movementRight = false;
                    }
                }
                else
                {
                    // Move the bar to the left
                    fishingBarPosition--;

                    if (fishingBarPosition < fishingBarMin)
                    {
                        fishingBarPosition = fishingBarMin;
                        movementRight = true;
                    }
                }
            }
        }
    }
}
