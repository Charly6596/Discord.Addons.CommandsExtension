using System.Collections.Generic;
using System.Linq;
using Discord.Commands;

namespace Discord.Addons.CommandsExtension
{
    public static class CommandServiceExtension
    {
        public static CommandServiceInfo GetCommandServiceInfo(this CommandService commandService, string command)
        {
    
            var commandInfo = commandService.Search(command).Commands.FirstOrDefault().Command;
            var module = commandInfo.Module;
            var aliases = string.Join(", ", commandInfo.Aliases);
            var parameters = string.Join(", ", commandInfo.GetCommandParameters());
            return new CommandServiceInfo(commandInfo, module, aliases, parameters);
        }


        public static Embed GetDefaultHelpEmbed(this CommandService commandService, string command, string prefix)
        {
            EmbedBuilder helpEmbedBuilder;
            var commandModules = commandService.GetModulesWithCommands();
            var moduleMatch = commandModules.FirstOrDefault(m => m.Name == command);

            if (string.IsNullOrEmpty(command))
            {
                helpEmbedBuilder = commandService.GenerateHelpCommandEmbed();
            }
            else if (moduleMatch != null)
            {
                helpEmbedBuilder = commandService.GenerateSpecificModuleHelpEmbed(moduleMatch);
            }
            else
            {
                helpEmbedBuilder = GenerateSpecificCommandHelpEmbed(commandService, command, prefix);
            }

            helpEmbedBuilder.WithFooter(GenerateUsageFooterMessage(prefix));
            return helpEmbedBuilder.Build();
        }

        private static string GenerateUsageFooterMessage(string botPrefix)
         => $"Use {botPrefix}help [command module] or {botPrefix}help [command name] for more information.";

        private static IEnumerable<ModuleInfo> GetModulesWithCommands(this CommandService commandService)
            => commandService.Modules.Where(module => module.Commands.Count > 0);

        private static EmbedBuilder GenerateSpecificCommandHelpEmbed(this CommandService commandService, string command, string prefix)
        {
            var helpEmbedBuilder = new EmbedBuilder();
            var commandSearchResult = commandService.Search(command);
            if (!commandSearchResult.IsSuccess)
            {
                helpEmbedBuilder.WithTitle("Command not found");
                return helpEmbedBuilder;
            }
            var commandInformation = commandSearchResult.Commands.FirstOrDefault().Command.GetCommandInfo(prefix);
            helpEmbedBuilder.WithDescription(commandInformation);
            return helpEmbedBuilder;
        }

        private static EmbedBuilder GenerateSpecificModuleHelpEmbed(this CommandService commandService, ModuleInfo module)
        {
            var helpEmbedBuilder = new EmbedBuilder();
            helpEmbedBuilder.AddField(module.GetModuleName(), module.GetModuleInfo());
            return helpEmbedBuilder;
        }

        private static EmbedBuilder GenerateHelpCommandEmbed(this CommandService commandService)
        {
            var helpEmbedBuilder = new EmbedBuilder();
            var commandModules = commandService.GetModulesWithCommands();
            helpEmbedBuilder.WithTitle("How can I help you?");

            foreach (var module in commandModules)
            {
                helpEmbedBuilder.AddField(module.GetModuleName(), module.GetModuleInfo());
            }
            return helpEmbedBuilder;
        }
    }
}
