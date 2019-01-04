# Discord.Net.Addons.CommandsExtension
An extension of Discord.Net.Commands, mainly to get information about your commands as string, ideally to build a help command. 
Comes with a extension of the [CommandService](https://discord.foxbot.me/latest/api/Discord.Commands.CommandService.html) to build a help command easily!
<p align="center">
  <img src="https://thumbs.gfycat.com/ImpossibleIllustriousIaerismetalmark-small.gif">
</p>

## How can I add the package to my project?

This package is uploaded to NuGet:
- [Discord.Addons.CommandsExtensions](https://www.nuget.org/packages/Discord.Addons.CommandsExtension/)

## Main features
- Auto-generated embed to display a help command.
- Show your own prefix in the embed.
- Search modules.
- Search commands.
- Display a module icon in the generated help embed.
- Multiple extensions to get formatted data from your commands and modules as `string`, to build your own help command.

## How to use the auto-generated help embed

If you're looking for an auto-generated embed for your help command, and you don't care about how it looks, this is what you're looking for!

### With Dependency Injection
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
### If you've followed Peter [.Net Framework tutorials](https://www.youtube.com/watch?v=BwjNGq8FXLU&list=PLwmVCZVHfSkGCAs01wc74JkZVH0S8yLo_)
If you've followed Peter .Net Framework tutorials, then most likely you don't have the Dependency Injection setted up. In this case, you have to add a global variable, in your `Global.cs` class, to reference the `CommandService`:
```cs
using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordTutorialBot
{
    internal static class Global
    {
        internal static DiscordSocketClient Client { get; set; }
        internal static ulong MessageIdToTrack { get; set; }
        //Add your variable at the bottom
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
