using RAGE;
using RAGE.Elements;
using WiredPlayers_Client.globals;
using System.Linq;

namespace WiredPlayers_Client.weapons
{
    class Weapons : Events.Script
    {
        private Blip weaponBlip = null;

        public Weapons()
        {
            RAGE.Nametags.Enabled = false;            

            Events.Add("makePlayerReload", MakePlayerReloadEvent);
            Events.Add("getPlayerWeapons", GetPlayerWeaponsEvent);
            Events.Add("showWeaponCheckpoint", ShowWeaponCheckpointEvent);
            Events.Add("deleteWeaponCheckpoint", DeleteWeaponCheckpointEvent);

            Events.OnPlayerWeaponShot += OnPlayerWeaponShotEvent;
        }

        public static bool IsValidWeapon(int weapon)
        {
            return Constants.VALID_WEAPONS.Where(w => RAGE.Game.Misc.GetHashKey(w) == (uint)weapon).Count() > 0;
        }

        private void MakePlayerReloadEvent(object[] args)
        {
            // Reload the weapon
            Player.LocalPlayer.TaskReloadWeapon(true);
        }

        private void GetPlayerWeaponsEvent(object[] args)
        {
            string callback = args[0].ToString();
        }

        private void ShowWeaponCheckpointEvent(object[] args)
        {
            // Get the variables from the array
            Vector3 position = (Vector3)args[0];

            // Set the checkpoint with the crates
            weaponBlip = new Blip(1, position, string.Empty, 1, 1);
        }

        private void DeleteWeaponCheckpointEvent(object[] args)
        {
            // Delete the checkpoint on the map
            weaponBlip.Destroy();
            weaponBlip = null;
        }

        private void OnPlayerWeaponShotEvent(Vector3 targetPos, Player target, Events.CancelEventArgs cancel)
        {
            // Calculate the weapon the player is holding
            uint weaponHash = RAGE.Game.Weapon.GetSelectedPedWeapon(Player.LocalPlayer.Handle);

            // Get the bullets remaining
            int bullets = Player.LocalPlayer.GetAmmoInWeapon(weaponHash);

            // Update the weapon's bullet amount
            Events.CallRemote("updateWeaponBullets", bullets);
        }
    }
}
