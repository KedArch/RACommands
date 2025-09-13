using System.ComponentModel;
using System.Collections.Generic;
using Exiled.API.Interfaces;

namespace RACommands
{
    public class Translations : ITranslation
    {
        [Description("String to add before NPC name.")]
        public List<string> NpcPreTitle { get; set; } = new()
        {
          ""
        };
        [Description("String to add after NPC name.")]
        public List<string> NpcPostTitle { get; set; } = new()
        {
          ""
        };
    }
}
