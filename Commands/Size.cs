using System;
using UnityEngine;
using CommandSystem;
using Exiled.API.Features;

namespace RACommands.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SizeCommand : ICommand
    {
        public string Command { get; } = "size";
        public string[] Aliases { get; } = new[] { "tpc" };
        public string Description { get; } = "Resizes player x y z to given scale.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count < 4)
            {
                response = "Usage: size <player> <x> <y> <z>";
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

            player.Scale = new Vector3(x, y, z);
            response = $"Player {player.Nickname} was resized ({x}, {y}, {z}).";
            return true;
        }
    }
}
