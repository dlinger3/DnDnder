using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tavern.Data.Migrations;
using Tavern.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Tavern.Controllers
{
    public class CampaignListingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CampaignListingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CampaignListings
        public async Task<IActionResult> Index()
        {
            //Stores values needed for determine if/how the user is assocaited with a campaign listing
            var UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.AppUserID = UserID;
            ViewBag.ListingsMap = await _context.CampaignCharacters
                .Where(uID => uID.AppUserID.Equals(UserID))
                .ToDictionaryAsync(
                        //Dicitonary Key
                        k => k.AppUserID +"--" +k.CampaignListingID.ToString(), 
                        //Dictionary Value
                        v => (int)v.CampaignListingID);

            var applicationDbContext = _context.CampaignListing.Include(c => c.Campaign);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CampaignListings/Details/id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CampaignListing == null)
            {
                return NotFound();
            }

            var campaignListing = await _context.CampaignListing
                .Include(c => c.Campaign)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campaignListing == null)
            {
                return NotFound();
            }

            var CampaignCharactersSet = await _context.CampaignCharacters
                .Where(cl => cl.CampaignListingID.Equals(id)).ToListAsync();
            Campaign campaign = campaignListing.Campaign;
            List<Character> Players = new List<Character>();
            var UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            TempData["ThisUsersID"] = UserID;
            TempData["UserIsInCampaign"] = false;
            //User is the DM and owns the campaign listing
            if (campaign.AppUserID.Equals(UserID))
            {
                TempData["UserIsInCampaign"] = true;
            }
            foreach (var player in CampaignCharactersSet)
            {
                Character nextCharacter = await _context.Character.Where(c => c.Id.Equals(player.CharacterID)).FirstAsync();
                if(nextCharacter.AppUserID.Equals(UserID))
                {
                    TempData["UserIsInCampaign"] = true;
                }
                Players.Add(nextCharacter);
            }
            if(Players.Any())
            {
                
                ViewData["CharacterList"] = Players;
            }
            return View(campaignListing);
        }

        // GET: CampaignListings/Create
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id)
        {
            CampaignListing newListing = new CampaignListing()
            {
                CampaignId = id,
                AppUserID = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            if (ModelState.IsValid)
            {
                _context.CampaignListing.Add(newListing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CampaignId"] = new SelectList(_context.Campaign, "Id", "CampaignName", campaignListing.CampaignId);
            return RedirectToAction(nameof(Index));
        }

        // GET: CampaignListings/Edit/id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CampaignListing == null)
            {
                return NotFound();
            }

            var campaignListing = await _context.CampaignListing.FindAsync(id);
            if (campaignListing == null)
            {
                return NotFound();
            }
            List<CampaignCharacters> ListingCharacters = await _context.CampaignCharacters.Where(cl => cl.CampaignListingID.Equals(id)).ToListAsync();
            List<Character> characters = new List<Character>();
            foreach(var c in ListingCharacters)
            {
                Character nextCharacter = await _context.Character.FindAsync(c.CharacterID);
                characters.Add(nextCharacter);
            }
            ViewData["Characters"] = characters;
            ViewData["CampaignCharacters"] = ListingCharacters;
            ViewData["CampaignId"] = new SelectList(_context.Campaign, "Id", "CampaignName", campaignListing.CampaignId);
            return View(campaignListing);
        }

        // POST: CampaignListings/Edit/id
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind("Id,CampaignId")] CampaignListing campaignListing
        public async Task<IActionResult> Edit(int ListingID, int CharacterID)
        {
            var listingCharacterRecord = await _context.CampaignCharacters.Where(cc => cc.CampaignListingID == ListingID && cc.CharacterID == CharacterID).ToListAsync();
            foreach(var character in listingCharacterRecord)
            {
                _context.CampaignCharacters.Remove(character);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Edit", new {id = ListingID });
        }

        // GET: CampaignListings/Delete/id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CampaignListing == null)
            {
                return NotFound();
            }

            var campaignListing = await _context.CampaignListing
                .Include(c => c.Campaign)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campaignListing == null)
            {
                return NotFound();
            }

            return View(campaignListing);
        }

        // POST: CampaignListings/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CampaignListing == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CampaignListing'  is null.");
            }
            var campaignListing = await _context.CampaignListing.FindAsync(id);
            if (campaignListing != null)
            {
                var listingCharacters = await _context.CampaignCharacters.Where(lc => lc.CampaignListingID.Equals(id)).ToListAsync();
                foreach(var character in listingCharacters)
                {
                    _context.CampaignCharacters.Remove(character);
                    await _context.SaveChangesAsync();
                }
                var listingMessages = await _context.Message.Where(m => m.CampaignListingID == id).ToListAsync();
                foreach (var message in listingMessages)
                {
                    _context.Message.Remove(message);
                    await _context.SaveChangesAsync();
                }
                _context.CampaignListing.Remove(campaignListing);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
        [HttpGet]
        public async Task<ActionResult> GroupMessages(int? id)
        {
            //Used to set the character name of the current user to TempData["ThisPlayer"]. This is used to display who post a message in the listings chat. 
            string UserCharacterName ="";

            if (id == null)
            {
                return NotFound();
            }
            List<Message> AllMessages = new List<Message>();

            //LINQ (query) to the DB for all ListingChatGroups for the given id 
            var GroupMessages = await _context.Message
                .Where(cg => cg.CampaignListingID
                .Equals(id))
                .ToListAsync();
            

            CampaignListing Listing = await _context.CampaignListing.FindAsync(id);
            Campaign campaign = await _context.Campaign.FindAsync(Listing.CampaignId);
            List<CampaignCharacters> ListingCharacters = await _context.CampaignCharacters.Where(clc => clc.CampaignListingID.Equals(id)).ToListAsync();
            List<Character> characters = new List<Character>();
            var UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Gathers all of the player characters in this listing to be stored in ViewData["Players"]
            foreach (var ListingCharacter in ListingCharacters)
            {
                Character character = await _context.Character
                    .Where(c => c.Id
                    .Equals(ListingCharacter.CharacterID)
                    && c.AppUserID
                    .Equals(ListingCharacter.AppUserID))
                    .FirstAsync();
                if (ListingCharacter.AppUserID.Equals(UserID))
                {

                    UserCharacterName = character.Name;
                }

                characters.Add(character);
            }
            if (campaign.AppUserID.Equals(UserID))
            {
                UserCharacterName = "Dungeon Master";
            }

            TempData["ListingID"] = id;
            TempData["UserID"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            TempData["SendersCharacter"] = UserCharacterName;
            //TODO: Add DM ability for DM to provide a name to show up in the chat members list
            // TempData["DM"] = Listing.DMName; 
            TempData["Players"] = characters;
            TempData["CampaignName"] = campaign.CampaignName;
            TempData["AllMessages"] = GroupMessages;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LeaveCampaign(int? id)
        {

            if (id == null || _context.CampaignListing == null)
            {
                return NotFound();
            }

            var campaignListing = await _context.CampaignListing
                .Include(c => c.Campaign)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campaignListing == null)
            {
                return NotFound();
            }
            TempData["CampaignName"] = campaignListing.Campaign.CampaignName;
            return View(campaignListing);
        }

        /// <param name = "id"> argument is the campaign listing id. 
        [HttpPost, ActionName("LeaveCampaign")]
        public async Task<ActionResult> LeaveCampaignConfirmed(int id)
        {
            if (_context.CampaignCharacters != null)
            {
                var UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var CampaignCharacterSet = await _context.CampaignCharacters.Where(cC => cC.CampaignListingID.Equals(id)).ToListAsync();
                foreach(var character in CampaignCharacterSet)
                {
                    if(character.AppUserID.Equals(UserID))
                    {
                        _context.Remove(character);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else
            {
                return Problem("Entity set 'ApplicationDbContext.Character'  is null.");
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<ActionResult> JoinCampaign(string UserID, int CharaID, int ListingID)
        {
           if(UserID != null && CharaID != null && ListingID != null)
            {
                var CampaignListing = await _context.CampaignListing.FindAsync(ListingID);
                var NewPlayer = await _context.Character.FindAsync(CharaID);
                if (NewPlayer != null && CampaignListing != null)
                {
                    CampaignCharacters NewCampaignCharacter = new CampaignCharacters()
                    {
                        CampaignListingID = ListingID,
                        CharacterID = CharaID,
                        AppUserID = UserID
                    };
                    
                    _context.CampaignCharacters.Add(NewCampaignCharacter);
                    await _context.SaveChangesAsync();

                    var UpdatedListing = await _context.CampaignListing.ToListAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
                
            }
            else
            {
                return NotFound();
            }
        }


        public async Task<ActionResult> JoinCampaignReroute(int CampaignListingId)
        {
            if (_context.Character != null)
            {
                var UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userCharacterSet = await _context.Character.Where(ch => ch.AppUserID.Contains(UserID)).ToListAsync();
                TempData["CampaignListingID"] = CampaignListingId;
                return View(userCharacterSet);

            }
            else
            {
                return Problem("Entity set 'ApplicationDbContext.Character'  is null.");
            }
            return View();
        }


        private bool CampaignListingExists(int id)
        {
            return _context.CampaignListing.Any(e => e.Id == id);
        }

    }
}
