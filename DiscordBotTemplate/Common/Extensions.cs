using Discord;
using Discord.WebSocket;
using DiscordBotTemplate.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTemplate.Common
{
    public static class Extensions
    {
        public static async Task<IMessage> SendSuccessAsync(this ISocketMessageChannel channel, string title, string description)
        {
            var embed = new EmbedBuilder()
                .WithColor(Colors.Success)
                .WithTitle(title)
                .WithDescription(description)
                .Build();

            var message = await channel.SendMessageAsync(embed: embed);

            return message;
        }

        public static async Task<IMessage> SendWarningAsync(this ISocketMessageChannel channel, string title, string description)
        {
            var embed = new EmbedBuilder()
                .WithColor(Colors.Warning)
                .WithTitle(title)
                .WithDescription(description)
                .Build();

            var message = await channel.SendMessageAsync(embed: embed);

            return message;
        }

        public static async Task<IMessage> SendDangerAsync(this ISocketMessageChannel channel, string title, string description)
        {
            var embed = new EmbedBuilder()
                .WithColor(Colors.Danger)
                .WithTitle(title)
                .WithDescription(description)
                .Build();

            var message = await channel.SendMessageAsync(embed: embed);

            return message;
        }

        public static async Task<IMessage> SendInfoAsync(this ISocketMessageChannel channel, string title, string description)
        {
            var embed = new EmbedBuilder()
                .WithColor(Colors.Info)
                .WithTitle(title)
                .WithDescription(description)
                .Build();

            var message = await channel.SendMessageAsync(embed: embed);

            return message;
        }
    }
}
