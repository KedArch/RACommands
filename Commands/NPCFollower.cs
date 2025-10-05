using System;
using System.Collections.Generic;
using MEC;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Items;

namespace RACommands.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class NpcCommand : ICommand
    {
        public string Command => "npc_follower";
        public string[] Aliases => new[] { "npcf" };
        public string Description => "Spawns Class-D NPC that follows player.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count < 1)
            {
                response = "Usage: npc <player> [pre_name|$postname] [post_name]";
                return false;
            }

            Player player = Player.Get(arguments.At(0));
            if (player == null)
            {
                response = $"Player {arguments.At(0)} not found.";
                return false;
            }
            String pre = "";
            String post = "";
            if (arguments.Count == 2 || arguments.Count == 3)
            {
                if (arguments.At(1).StartsWith("$"))
                {
                    post = arguments.At(1).Substring(1);
                }
                else
                {
                    pre = arguments.At(1);
                }
            }
            if (arguments.Count == 3)
            {
                post = arguments.At(2);
            }
            Npc npc = Npc.Spawn($"{pre} {player.Nickname} {post}", player.Role, true, player.Position);
            npc.MaxHealth = 1;
            npc.Health = 1;
            Item coin = Item.Create(ItemType.Coin);
            if (!Plugin.Instance.Npcs.ContainsKey(player))
            {
                Plugin.Instance.Npcs[player] = new List<Npc>();
            }
            Plugin.Instance.Npcs[player].Add(npc);
            Timing.CallDelayed(1f, () =>
            {
                npc.AddItem(coin);
                npc.CurrentItem = coin;
                npc.Follow(player);
            });

            response = $"NPC Follower spawned near player {player.Nickname}";
            return true;
        }

        public NpcCommand()
        {
            Exiled.Events.Handlers.Player.Dying += OnPlayerDying;
            Exiled.Events.Handlers.Player.Died += OnPlayerDied;
        }

        private void OnPlayerDying(Exiled.Events.EventArgs.Player.DyingEventArgs ev)
        {
            foreach(KeyValuePair<Player, List<Npc>> entry in Plugin.Instance.Npcs)
            {
                if (ev.Player.IsNPC)
                {
                    Npc npc = (Npc)ev.Player;
                    foreach(Npc npc_entry in entry.Value)
                    {
                        if (npc == npc_entry)
                        {
                            npc.ClearInventory(true);
                        }
                    }
                }
            }
        }

        private void OnPlayerDied(Exiled.Events.EventArgs.Player.DiedEventArgs ev)
        {
            foreach(KeyValuePair<Player, List<Npc>> entry in Plugin.Instance.Npcs)
            {
                if (ev.Player.IsNPC)
                {
                    Npc npc = (Npc)ev.Player;
                    foreach(Npc npc_entry in entry.Value)
                    {
                        if (npc == npc_entry)
                        {
                            npc.Destroy();
                            Plugin.Instance.Npcs.Remove(npc);
                        }
                    }
                }
                else
                {
                    if (Plugin.Instance.Npcs.ContainsKey(ev.Player))
                    {
                        foreach(Npc npc_entry in entry.Value)
                        {
                            npc_entry.Kill(ev.Player.Nickname, "");
                        }
                    }
                }
            }
        }

        public void OnDisable()
        {
            Exiled.Events.Handlers.Player.Dying -= OnPlayerDying;
            Exiled.Events.Handlers.Player.Died -= OnPlayerDied;
        }
    }
}
