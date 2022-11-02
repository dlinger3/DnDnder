using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tavern.Data.Migrations;
using Tavern.Models;

namespace Tavern.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        public ChatHub(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        //public async Task SendMessage(Message message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", message);
        //}

        public async Task SendMessage(string userCharacter, string message, string ListingID)
        {
            await Clients.Group("Group--" + ListingID).SendAsync("ReceiveMessage", userCharacter, message);
        }

        public async Task AddUserToGroup(string ListingID, string UserID)
        {
            //TODO: This list is returning without any values for some reason. Need to investigate why. It should have 1 value. 
            var ListingUsers = await _context.CampaignCharacters
                .Where(cl => cl.CampaignListingID
                .Equals(int.Parse(ListingID))).ToListAsync();

            foreach(var user in ListingUsers)
            {
                if(user.AppUserID.Equals(UserID))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, "Group--" + ListingID);
                }
            }
            
        }
        

    }
   

}
