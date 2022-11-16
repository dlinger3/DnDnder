using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tavern.Models;

namespace Tavern.Data.Migrations
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Campaign>? Campaign { get; set; }

        public DbSet<Character>? Character { get; set; }

        public DbSet<CampaignListing>? CampaignListing { get; set; }

        public DbSet<CampaignCharacters>? CampaignCharacters { get; set; }

        public DbSet<Message>? Message { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<CampaignListing>().HasMany(user => user.Players)
        //        .WithMany(campaignListings => campaignListings.PlayerCampaigns)
        //        .UsingEntity<Dictionary<int, object>>(
        //        "PlayersInCampaign",
        //        listing => listing.HasOne<Character>().WithMany().HasForeignKey("PlayerCampaignsId"),
        //        listing => listing.HasOne<CampaignListing>().WithMany().HasForeignKey("PlayersId"));
        //}
    }
}