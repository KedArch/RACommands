using System;
using UnityEngine;
using CommandSystem;
using Exiled.API.Features;

namespace RACommands.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class TeleportCoordsCommand : ICommand
    {
        public string Command { get; } = "teleport_coords";
        public string[] Aliases { get; } = new[] { "tpc" };
        public string Description { get; } = "Teleports player to coordinates.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count < 4)
            {
                response = "Usage: teleport <player> <x> <y> <z>";
                return false;
            }

            Player player = Player.Get(arguments.At(0));
            if (player == null)
            {
                response = $"Player {arguments.At(0)} not found.";
                return false;
            }

            if (!float.TryParse(arguments.At(1), out float x) ||
                !float.TryParse(arguments.At(2), out float y) ||
                !float.TryParse(arguments.At(3), out float z))
            {
                response = "Coordinates must be floats.";
                return false;
            }

            player.Position = new Vector3(x, y, z);
            response = $"Player {player.Nickname} was teleported to coordinates ({x}, {y}, {z}).";
            return true;
        }
    }
}
