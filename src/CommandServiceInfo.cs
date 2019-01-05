using Discord.Commands;

namespace Discord.Addons.CommandsExtension
{
    public class CommandServiceInfo
    {
        public CommandInfo CommandInformation { get; set; }
        public ModuleInfo ModuleInformation{ get; set; }
        public string CommandParameters { get; set; }
        public string CommandAliases { get; set; }

        public CommandServiceInfo(CommandInfo commandInformation, ModuleInfo moduleInformation, string commandParameters, string commandAliases)
        {
            CommandInformation = commandInformation;
            ModuleInformation = moduleInformation;
            CommandParameters = commandParameters;
            CommandAliases = commandAliases;
        }
    }
}
