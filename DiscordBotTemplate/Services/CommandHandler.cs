using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBotTemplate.Models;
using DiscordBotTemplate.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBotTemplate.Services
{
    public class CommandHandler : InitializedService
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _service;
        private readonly IConfiguration _config;
        private readonly GuildRepository _guilds;

        public CommandHandler(IServiceProvider provider, DiscordSocketClient client, CommandService service, IConfiguration config, GuildRepository guilds)
        {
            _provider = provider;
            _client = client;
            _service = service;
            _config = config;
            _guilds = guilds;
        }

        public override async Task InitializeAsync(CancellationToken cancellationToken)
        {
            _client.MessageReceived += OnMessageReceived;
            _client.JoinedGuild += OnJoinedGuild;
            _client.LeftGuild += OnLeftGuild;
            _service.CommandExecuted += OnCommandExecuted;
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }

        private async Task OnJoinedGuild(SocketGuild arg)
        {
            var guildID = arg.Id;
            var guild = new Guild()
            {
                ID = guildID
            };

            _guilds.AddGuild(guild);

            await Task.CompletedTask;
        }

        private async Task OnLeftGuild(SocketGuild arg)
        {
            var guildID = arg.Id;
            var guild = _guilds.GetGuildByID(guildID);

            _guilds.DeleteGuild(guild);

            await Task.CompletedTask;
        }

        private async Task OnMessageReceived(SocketMessage arg)
        {
            if (!(arg is SocketUserMessage message)) return;
            if (message.Source != MessageSource.User) return;

            var argPos = 0;
            if (!message.HasStringPrefix(_config["prefix"], ref argPos) && !message.HasMentionPrefix(_client.CurrentUser, ref argPos)) return;

            var context = new SocketCommandContext(_client, message);
            await _service.ExecuteAsync(context, argPos, _provider);
        }

        private async Task OnCommandExecuted(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (command.IsSpecified && !result.IsSuccess) await context.Channel.SendMessageAsync($"Error: {result}");
        }


    }
}
