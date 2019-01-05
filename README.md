# Discord.Net.Addons.CommandsExtension

An extension of Discord.Net.Commands, mainly to get information about your commands as string, ideally to build a help command. 
Comes with a extension of the [CommandService](https://discord.foxbot.me/latest/api/Discord.Commands.CommandService.html) to build a help command easily!

## Getting Started

This package is uploaded to NuGet:
- [Discord.Addons.CommandsExtensions](https://www.nuget.org/packages/Discord.Addons.CommandsExtension/)
- Alternatively, you can download the project and reference it to your project.

### Prerequisites

First of all, you need a C# discord bot, using the `Discord.Net version >= 2.0`.
If you don't have one yet, check out some tutorials here to get started with Discord bots!
- [C# bot tutorials](https://www.youtube.com/channel/UCmfZ6FWTHZjPrPP3dWQ1bHg/playlists)

### Using the default help embed

If you're looking for an auto-generated embed for your help command, and you don't care about how it looks, this is what you're looking for!

All you need to do is add the `CommandService` to your dependency container if you haven't it yet, and inject it into the Command Module where the help command is.

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
            var botPrefix = ">"; //replace this with your own prefix.
            var helpEmbed = _commandService.GetDefaultHelpEmbed(command, botPrefix);
            await Context.Channel.SendMessageAsync(embed: helpEmbed);
        }
    }
}
```

However, as much as we want to encourage the use of the Dependency Injection pattern, it's not easy to understand for starters, or there might be someone with a project, in a point where setting up DI would take a lot of work.
If you've followed [Peter .Net Framework tutorials](https://www.youtube.com/watch?v=BwjNGq8FXLU&list=PLwmVCZVHfSkGCAs01wc74JkZVH0S8yLo_), then most likely you don't have the Dependency Injection setted up. I will use his tutorials as a reference to explain how to set this up without Dependency Injection.
You have to add a global variable, in your `Global.cs` class, to reference the `CommandService`:

```cs
using Discord.Commands;
using System;

namespace DiscordTutorialBot
{
    internal static class Global
    {
        internal static CommandService commandService { get; set;}
    }
}
```

The next step, is use that variable to reference your commnad service, you can do that right where you initialize the `CommandService`, in the `CommandHandler` class, in the `InitializeAsync` method:
```cs
public async Task InitializeAsync(DiscordSocketClient client)
{
  _client = client;
  _service = new CommandService();
  Global.commandService = _service;
  await _service.AddModulesAsync(Assembly.GetEntryAssembly());
  _client.MessageReceived += HandleCommandAsync;
}
```
After that, the `CommandService` will be accesible from your help command with `Global.commandService`.

```cs
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Addons.CommandsExtension;

namespace MyBot.Modules
{
    public class HelpModule : ModuleBase
    {      

        [Command("help"), Alias("assist"), Summary("Shows help menu.")]
        public async Task Help([Remainder] string command = null)
        {
            var botPrefix = ">"; //replace this with your own prefix.
            var helpEmbed = Global.commandService.GetDefaultHelpEmbed(command, botPrefix);
            await Context.Channel.SendMessageAsync(embed: helpEmbed);
        }
    }
}
```
After that, you're ready to go!
<p align="center">
  <img src="https://thumbs.gfycat.com/ImpossibleIllustriousIaerismetalmark-small.gif">
</p>

## Authors

* **Charly6596** - _CommandService extensions and default embed generation_ - [Github profile](https://github.com/Charly6596) - Discord: Charly#7094

See also the list of [contributors](https://github.com/Charly6596/Discord.Addons.CommandsExtension/graphs/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* [Discord-BOT-tutorial](https://discord.gg/cGhEZuk) discord server
* [Peter](https://github.com/petrspelos) and his [tutorials](https://www.youtube.com/channel/UCmfZ6FWTHZjPrPP3dWQ1bHg/playlists)
* [C# Discord Bot Common Issues GitHub repository](https://github.com/discord-bot-tutorial/common-issues)
