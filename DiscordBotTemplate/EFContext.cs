using DiscordBotTemplate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotTemplate
{
    public class EFContext : DbContext
    {
        public DbSet<Guild> Guilds { get; set; }

        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {

        }
    }
}
