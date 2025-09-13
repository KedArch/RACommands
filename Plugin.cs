using System;
using Exiled.API.Features;

namespace RACommands {
    public class Plugin : Plugin<Config, Translations>
    {
        public override string Name => "RACommands";
        public override string Author => "KedArch";
        public override Version Version => new Version(1, 0, 0);

        public static Plugin Instance;

        public override void OnEnabled()
        {
          Instance = this;
        }

        public override void OnDisabled()
        {
          Instance = null;
        }
    }
}
