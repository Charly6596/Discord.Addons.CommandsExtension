# Discord.Net.Addons.CommandsExtension
An extension of Discord.Net.Commands, mainly to get information about your commands as string, ideally to build a help command. 
Comes with a extension of the [CommandService](https://discord.foxbot.me/latest/api/Discord.Commands.CommandService.html) to build a help command easily!
<p align="center">
  <img src="https://thumbs.gfycat.com/ImpossibleIllustriousIaerismetalmark-small.gif">
</p>

## How can I add the package to my project?

You can add this package to your project using the .NET CLI:
```
dotnet add package Discord.Addons.CommandsExtension --version 1.0.0
```
Alternatively, you can add it to your project using any NuGet package manager. Here's the link to NuGet: https://www.nuget.org/packages/Discord.Addons.CommandsExtension/1.0.0

## Main features
- Auto-generated embed to display a help command.
- Show your own prefix in the embed.
- Search modules.
- Search commands.
- Display a module icon in the generated help embed.
- Multiple extensions to get formatted data from your commands and modules as `string`, to build your own help command.

## How to use the auto-generated help embed

If you're looking for an auto-generated embed for your help command, and you don't care about how it looks, this is what you're looking for!

```cs
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Addons.CommandsExtension;

namespace MyBot.Modules
{
    public class HelpModule : ModuleBase
    {
        private readonly CommandService _commandService;

        public HelpModule(CommandService commandService)
        {
            _commandService = commandService;
        }

        [Command("help"), Alias("assist"), Summary("Shows help menu.")]
        public async Task Help([Remainder] string command = null)
        {
            var botPrefix = ">";
            var helpEmbed = _commandService.GetDefaultHelpEmbed(command, botPrefix);
            await Context.Channel.SendMessageAsync(embed: helpEmbed);
        }
    }
}
```
##### Note: You need to setup dependency injection, adding your `CommandService` to the container

## Add emotes near the module name
The generated embed uses the `Remarks` attribute to get the emote
```cs
[Name("Fun")]
[Remarks("🤠")]
public class FunModule : ModuleBase
{
    //your commands here
}
```

## That's cool! And can I use my own embed format?

## **🚧👷 Under construction 👷🚧**