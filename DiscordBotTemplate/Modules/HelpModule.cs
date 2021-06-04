using Discord;
using Discord.Commands;
using DiscordBotTemplate.Classes;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTemplate.Modules
{
    public class HelpModule : ModuleBase
    {
        private readonly CommandService _commands;
        private readonly IConfiguration _config;

        public HelpModule(CommandService commands, IConfiguration config)
        {
            _commands = commands;
            _config = config;
        }

        [Command("help")]
        [Summary("Lists all of the commands available for this bot.")]
        public async Task Help()
        {
            EmbedBuilder builder = new EmbedBuilder
            {
                Title = "List of all Bot Commands",
                Description = $"[Invite this bot to your Discord server!]({ _config["botInviteLink"]})",
                Author = new EmbedAuthorBuilder().WithName("Template Bot").WithIconUrl(Context.Client.CurrentUser.GetAvatarUrl() ?? Context.Client.CurrentUser.GetDefaultAvatarUrl()),
                Footer = new EmbedFooterBuilder().WithText("Developed by Munch"),
                Timestamp = DateTime.Now,
                Color = Colors.Info
            };

            foreach (var command in _commands.Commands.OrderBy(c => c.Name))
                AddCommand(command, builder);

            await ReplyAsync("", embed: builder.Build());
        }

        private void AddCommand(CommandInfo command, EmbedBuilder builder)
        {
            builder.AddField(f =>
            {
                f.Name = $"{_config["prefix"]}{command.Name}";
                f.Value = command.Summary ?? "No summary available.";

                if (command.Parameters.Count > 0)
                {
                    foreach (var parameter in command.Parameters)
                        f.Name += $" <{parameter.Name}>";
                }
            });
        }
    }
}
