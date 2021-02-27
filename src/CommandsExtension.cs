using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord.Commands;

namespace Discord.Addons.CommandsExtension
{
    public static class CommandsExtension
    {
        public static string GetModuleInfo(this ModuleInfo module)
        {
            var moduleCommands = string.Join(", ", module.Commands.Select(c => c.MainName()));
            var sb = new StringBuilder()
                .AppendLine(moduleCommands);
            return sb.ToString();
        }

        /// <summary>
        /// Attach the remarks before the module name.
        /// Useful to add an emote
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public static string GetModuleName(this ModuleInfo module)
        {
            return module.Remarks != null ? $"{module.Remarks} {module.Name}" : module.Name;
        }

        /// <summary>
        /// Returns a string ready to show in a help command,
        /// having the command name, module name, summary, usage
        /// and aliases names, including the prefix
        /// </summary>
        public static string GetCommandInfo(this CommandInfo command, string prefix)
        {
            var aliases = string.Join(", ", command.Aliases);
            var module = command.Module.Name;
            var parameters = string.Join(", ", command.GetCommandParameters());
            var name = command.MainName();
            var summary = command.Summary;
            var sb = new StringBuilder()
                .AppendLine($"**Command name**: {name}")
                .AppendLine($"**Module**: {module}")
                .AppendLine($"**Summary**: {summary}")
                .AppendLine($"**Usage**: {prefix}{name} {parameters}")
                .Append($"**Aliases**: {aliases}");
            return sb.ToString();
        }

        /// <summary>
        /// Returns a collection with formatted command parameters,
        /// Optional parameter names will be enclosed with <>,
        /// while mandatory names, with []
        /// </summary>
        public static IEnumerable<string> GetCommandParameters(this CommandInfo command)
        {
            var parameters = command.Parameters;
            var optionalTemplate = "<{0}>";
            var mandatoryTemplate = "[{0}]";
            List<string> parametersFormated = new List<string>();

            foreach (var parameter in parameters)
            {
                if (parameter.IsOptional)
                    parametersFormated.Add(String.Format(optionalTemplate, parameter.Name));
                else
                    parametersFormated.Add(String.Format(mandatoryTemplate, parameter.Name));
            }

            return parametersFormated;
        }

        /// <summary>
        /// Returns the command name with the group name attached.
        /// If there is no group, will return the command name.
        /// Warning: if there is a NameAttribute in the command, its will be returned instead of the command name,
        /// it's not warranteed to be an executable name
        /// </summary>
        [Obsolete("Deprecated. Use MainName instead.")]
        public static string GetCommandNameWithGroup(this CommandInfo commandInfo)
        {
            return commandInfo.Module.Group != null ? $"{commandInfo.Module.Group} {commandInfo.Name}" : commandInfo.Name;
        }

        /// <summary>
        /// Returns the main command name used to execute this command.
        /// If the command is in a module with a GroupAttribute, the group name will be included in the command name.
        /// If there is an AliasAttribute defined before the CommandAttribute, the first alias will be used
        /// </summary>
        public static string MainName(this CommandInfo commandInfo) => commandInfo.Aliases.First();
    }
}
