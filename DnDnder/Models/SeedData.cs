using Tavern.Data.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tavern.Models;
using System.Diagnostics;
using System.Diagnostics;

namespace Tavern.Models
{
    public class SeedData
    {
        public static void InitializeTables(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if(context.Campaign.Any())
                {
                    return;
                }

                context.Campaign.Add(
                    new Campaign
                    {
                        CampaignName = "TableSeed",
                        WorldName = "TableSeed",
                        Details = "TableSeed",
                        AppUserID = "8e960aea-65f1-426d-8482-044fdac734c2"
                    }
                    );

                context.SaveChanges();

            }

            
        }
    }
}
