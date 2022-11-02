using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tavern.Data.Migrations;
using Tavern.Models;
using System.Diagnostics;
using System.Text.Json;



namespace Tavern.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ChatController(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            //TODO: REMOVE commented code if not needed
            //var messages = _context.Message
            //   .Where(m => m.CampaignListingID.Equals(id)).ToList();

            //return new ObjectResult(new { data = messages, status = "success" });
            return View();
        }

        //[HttpGet("ListingChapGroupID")]
        //public IEnumerable<Message> GetMessageByID(int ChatGroupID)
        //{
        //    return _context.ListingChatGroups.Where(lm => lm.ListingChapGroupID.Equals(ChatGroupID));
        //}

        [HttpPost]
        //public async Task<IActionResult> Create(int id, [FromBody] Message message)
        public async Task<IActionResult> Create(int id, [FromBody] Message message)
        {
            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Message new_message = new Message
            {
                SentBy = message.SentBy,
                MessageText = message.MessageText,
                CampaignListingID = message.CampaignListingID,
            };
            if (ModelState.IsValid)
            {
                Debug.WriteLine("Message Model state was valid");
                await _context.Message.AddAsync(new_message);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                Debug.WriteLine("Message Model state was not valid");
            }
            return NotFound();
            //TODO: REMOVE THIS IF Pusher IS NOT USED
            //var options = new PusherOptions
            //{
            //    Cluster = "PUSHER_APP_CLUSTER",
            //    Encrypted = true
            //};
            //var pusher = new Pusher(
            //    "PUSHER_APP_ID",
            //    "PUSHER_APP_KEY",
            //    "PUSHER_APP_SECRET",
            //    options
            //);
            //var result = pusher.TriggerAsync(
            //    "private-" + message,
            //    "new_message",
            //new { new_message },
            //new TriggerOptions() { SocketId = message.SocketId });

            //return new ObjectResult(new { status = "success", data = new_message });
        }

        [HttpGet]
        public IActionResult GetAllMessages(int? id)
        {
            var messages = _context.Message
                .Where(m => m.CampaignListingID.Equals(id)).ToList();

            string JsonOutput = JsonSerializer.Serialize(messages);


            return new JsonResult(Ok(new { data = JsonOutput, status = "success" }));
        }
    }
}
