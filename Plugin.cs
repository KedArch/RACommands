using System;
using System.Collections.Generic;
using Exiled.API.Features;

namespace RACommands {
    public class Plugin : Plugin<Config>
    {
        public override string Name => "RACommands";
        public override string Author => "KedArch";
        public override Version Version => new Version(1, 0, 0);

        public static Plugin Instance;
        private RACommands.Commands.NpcCommand npcCommand;
        public Dictionary<Player, List<Npc>> Npcs { get; private set; }
        public override void OnEnabled()
        {
          Instance = this;
          Npcs = new Dictionary<Player, List<Npc>>();
          npcCommand = new RACommands.Commands.NpcCommand();
        }

        public override void OnDisabled()
        {
          npcCommand.OnDisable();
          Instance = null;
        }
    }
}
