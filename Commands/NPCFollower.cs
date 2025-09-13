using System;
using UnityEngine;
using MEC;
using PlayerRoles;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events;
using Random = System.Random;

namespace RACommands.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class NpcCommand : ICommand
    {
        public string Command => "npc_follower";
        public string[] Aliases => new[] { "npcf" };
        public string Description => "Spawns Class-D NPC that follows player.";

        private static Translations Translations => Plugin.Instance.Translation;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count < 1)
            {
                response = "Usage: npc <player>";
                return false;
            }

            Player player = Player.Get(arguments.At(0));
            if (player == null)
            {
                response = $"Player {arguments.At(0)} not found.";
                return false;
            }
            Random random = new Random();
            string pre = Translations.NpcPreTitle[random.Next(Translations.NpcPreTitle.Count)];
            string post = Translations.NpcPostTitle[random.Next(Translations.NpcPostTitle.Count)];
            Npc npc = Npc.Spawn($"{pre} {player.Nickname} {post}", RoleTypeId.ClassD, true, player.Position);
            npc.MaxHealth = 1;
            npc.Health = 1;
            Timing.CallDelayed(1f, () =>
            {
                npc.Follow(player);
            });

            response = $"NPC Follower spawned near player {player.Nickname}";
            return true;
        }
    }
}
