using RAGE;
using RAGE.Elements;
using System;

namespace WiredPlayers_Client.jobs
{
    class Garbage : Events.Script
    {
        private Blip garbageBlip = null;
        private Checkpoint garbageCheckpoint = null;

        public Garbage()
        {
            Events.Add("showGarbageCheckPoint", ShowGarbageCheckPointEvent);
            Events.Add("deleteGarbageCheckPoint", DeleteGarbageCheckPointEvent);

            Events.OnPlayerEnterCheckpoint += OnPlayerEnterCheckpoint;
        }

        private void ShowGarbageCheckPointEvent(object[] args)
        {
            // Get the variables from the arguments
            Vector3 checkpoint = (Vector3)args[0];
            Vector3 nextCheckpoint = (Vector3)args[1];
            uint checkpointType = Convert.ToUInt32(args[2]);

            if (garbageBlip == null)
            {
                // Create a checkpoint and a blip on the map
                garbageBlip = new Blip(1, checkpoint, string.Empty, 1, 1);
                garbageCheckpoint = new Checkpoint(checkpointType, checkpoint, 2.5f, nextCheckpoint, new RGBA(198, 40, 40, 200));
            }
            else
            {
                // Update checkpoint and blip
                garbageBlip.Position = checkpoint;
                garbageCheckpoint.Position = checkpoint;
                garbageCheckpoint.Direction = nextCheckpoint;
                garbageCheckpoint.Model = checkpointType;
            }
        }

        private void DeleteGarbageCheckPointEvent(object[] args)
        {
            // Destroy the checkpoint
            garbageCheckpoint.Destroy();
            garbageCheckpoint = null;

            // Destroy the blip on the map
            garbageBlip.Destroy();
            garbageBlip = null;
        }

        private void OnPlayerEnterCheckpoint(Checkpoint checkpoint, Events.CancelEventArgs cancel)
        {
            if (checkpoint == garbageCheckpoint && Player.LocalPlayer.Vehicle != null)
            {
                // Get the next checkpoint
                Events.CallRemote("garbageCheckpointEntered");
            }
        }
    }
}
