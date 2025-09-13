using System;
using Exiled.API.Features;

namespace RACommands {
    public class Plugin : Plugin<Config>
    {
        public override string Name => "RACommands";
        public override string Author => "KedArch";
        public override Version Version => new Version(1, 0, 0);

        public override void OnEnabled()
        {
        }

        public override void OnDisabled()
        {
        }
    }
}
