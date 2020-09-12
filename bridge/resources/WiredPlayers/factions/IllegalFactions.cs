using GTANetworkAPI;
using WiredPlayers.model;
using WiredPlayers.globals;
using WiredPlayers.chat;
using WiredPlayers.business;
using WiredPlayers.database;
using WiredPlayers.messages.error;
using WiredPlayers.messages.information;
using WiredPlayers.messages.general;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace WiredPlayers.factions
{
    public class IllegalFactions : Script
    {
        public static List<ChannelModel> channelList;
        public static List<FactionWarningModel> factionWarningList;
        public static List<PermissionModel> permissionList;
        public IllegalFactions()
        {
            // Initialize the required fields
            factionWarningList = new List<FactionWarningModel>();
        }
        private bool HasUserCommandPermission(Client player, string command, string option = "")
        {
            bool hasPermission = false;
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            //hehsdaw
            foreach (PermissionModel permission in permissionList)
            {
                if (permission.playerId == playerId && command == permission.command)
                {
                    // We check whether it's a command option or just the command
                    if (option == string.Empty || option == permission.option)
                    {
                        hasPermission = true;
                        break;
                    }
                }
            }

            return hasPermission;
        }
        public static string GetPlayerFactionRank(Client player)
        {
            string rankString = string.Empty;
            int faction = player.GetData(EntityData.PLAYER_FACTION);
            int rank = player.GetData(EntityData.PLAYER_RANK);

            // Get the player faction
            FactionModel factionModel = Constants.FACTION_RANK_LIST.Where(fact => fact.faction == faction && fact.rank == rank).FirstOrDefault();

            return factionModel == null ? string.Empty : (player.GetData(EntityData.PLAYER_SEX) == Constants.SEX_MALE ? factionModel.descriptionMale : factionModel.descriptionFemale);
        }

        [Command(Commands.COM_LAB, Commands.HLP_LAB_COMMAND, GreedyArg = true)]
        public void BusinessCommand(Client player, string args)
        {
            if (player.GetData(EntityData.PLAYER_FACTION) == Constants.ILLEGAL_FACTION_ONE)
            {
                if(player.GetData(EntityData.PLAYER_RANK) > 7)
                {
                    if (args.Trim().Length > 0)
                    {
                        BusinessModel business = new BusinessModel();
                        string[] arguments = args.Split(' ');
                        string message = string.Empty;
                        switch (arguments[0].ToLower())
                        {
                            case Commands.ARG_INFO:
                                break;
                            case Commands.ARG_CREATE:
                                if (player.GetData(EntityData.PLAYER_FACTION) == Constants.ILLEGAL_FACTION_ONE)
                                {
                                    if (arguments.Length == 3)
                                    {
                                        // We get the business type
                                        if (int.TryParse(arguments[1], out int type) && (arguments[2] == Commands.ARG_INNER || arguments[2] == Commands.ARG_OUTER))
                                        {
                                            business.type = 28;
                                            business.ipl = arguments[2] == Commands.ARG_INNER ? Business.GetBusinessTypeIpl(type) : string.Empty;
                                            business.position = new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 1.0f);
                                            business.dimension = arguments[2] == Commands.ARG_INNER ? player.Dimension : 0;
                                            business.multiplier = 3.0f;
                                            business.owner = EntityData.PLAYER_FACTION;
                                            business.locked = false;
                                            business.name = GenRes.business;

                                            Task.Factory.StartNew(() =>
                                            {
                                                NAPI.Task.Run(() =>
                                                {
                                                // Get the id from the business
                                                business.id = Database.AddNewBusiness(business);

                                                    if (arguments[2] == Commands.ARG_INNER)
                                                    {
                                                    // The business has a label to enter
                                                    business.businessLabel = NAPI.TextLabel.CreateTextLabel(business.name, business.position, 5.0f, 0.75f, 4, new Color(255, 255, 255), false, business.dimension);
                                                    }

                                                // Add the business to the list
                                                Business.businessList.Add(business);
                                                });
                                            });
                                        }
                                        else
                                        {
                                            player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_LAB_CREATE_COMMAND);
                                        }
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_LAB_CREATE_COMMAND);
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}
