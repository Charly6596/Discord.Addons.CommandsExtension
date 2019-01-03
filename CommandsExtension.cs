using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord.Commands;

namespace Discord.Net.Addons.CommandsExtension
{
    public static class CommandsExtension
    {
        public static string GetModuleInfo(this ModuleInfo module)
        {
            var moduleCommands = string.Join(", ", module.Commands.Select(c => c.GetCommandNameWithGroup()));
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

        public static string GetCommandInfo(this CommandInfo command, string prefix)
        {
            var aliases = string.Join(", ", command.Aliases);
            var module = command.Module.Name;
            var parameters = string.Join(", ", command.GetCommandParameters());
            var name = command.GetCommandNameWithGroup();
            var summary = command.Summary;
            var sb = new StringBuilder()
                .AppendLine($"**Command name**: {name}")
                .AppendLine($"**Module**: {module}")
                .AppendLine($"**Summary**: {summary}")
                .AppendLine($"**Usage**: {prefix}{name} {parameters}")
                .Append($"**Aliases**: {aliases}");
            return sb.ToString();
        }


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
        /// </summary>
        public static string GetCommandNameWithGroup(this CommandInfo commandInfo)
        {
            return commandInfo.Module.Group != null ? $"{commandInfo.Module.Group} {commandInfo.Name}" : commandInfo.Name;
        }
    }
}
