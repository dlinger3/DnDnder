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

        // GET: CampaignListings/Details/5
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

            List<Character> Players = new List<Character>();
            foreach(var player in CampaignCharactersSet)
            {
                Players.Add(
                    await _context.Character.Where(c => c.Id.Equals(player.CharacterID) && c.AppUserID.Equals(player.AppUserID)).FirstAsync());
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
            //var campaignid = (int)tempdata["campaignid"];
            //var userid = tempdata["appuserid"] as string;
            //campaignlisting newlisting = new campaignlisting()
            //{
            //    campaignid = campaignid,
            //    appuserid = userid
            //};
            //return RedirectToAction(Create(newListing));
            return View();
            //return Content("<form action='actionname' id='frmTest' method='post'><input type='hidden' name='someValue' value='" + someValue + "' /><input type='hidden' name='anotherValue' value='" + anotherValue + "' /></form><script>document.getElementById('frmTest').submit();</script>");
        }

        // POST: CampaignListings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(int id)
        //{
        //    Debug.WriteLine("Entered Create function for Campaign Listing");

        //    var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    CampaignListing newlisting = new CampaignListing()
        //    {
        //        CampaignId = id,
        //        AppUserID = userid
        //    };
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(newlisting);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    //ViewData["CampaignId"] = new SelectList(_context.Campaign, "Id", "CampaignName", campaignListing.CampaignId);
        //    return RedirectToAction(nameof(Index));
        //}


        // POST: CampaignListings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(int id, [Bind("Id,CampaignId,AppUserID")] CampaignListing campaignListing, [FromBody] Campaign campaign)
        //{
        //    Debug.WriteLine("Entered Create function for Campaign Listing, attempting to add id: " + id);
        //    CampaignListing newListing = new CampaignListing()
        //    {
        //        CampaignId = id,
        //        AppUserID = User.FindFirstValue(ClaimTypes.NameIdentifier)
        //    };

        //    if (ModelState.IsValid)
        //    {
        //        _context.CampaignListing.Add(newListing);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    //ViewData["CampaignId"] = new SelectList(_context.Campaign, "Id", "CampaignName", campaignListing.CampaignId);
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id)
        {
            Debug.WriteLine("Entered Create function for Campaign Listing, attempting to add id: " + id);
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

        // GET: CampaignListings/Edit/5
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
            ViewData["CampaignId"] = new SelectList(_context.Campaign, "Id", "CampaignName", campaignListing.CampaignId);
            return View(campaignListing);
        }

        // POST: CampaignListings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CampaignId")] CampaignListing campaignListing)
        {
            if (id != campaignListing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campaignListing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampaignListingExists(campaignListing.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CampaignId"] = new SelectList(_context.Campaign, "Id", "CampaignName", campaignListing.CampaignId);
            return View(campaignListing);
        }

        // GET: CampaignListings/Delete/5
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

        // POST: CampaignListings/Delete/5
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

            if (id == null || _context.ListingChatGroups == null)
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
            List<CampaignCharacters> ListingCharacters = await _context.CampaignCharacters.Where(clc => clc.CampaignListingID.Equals(id)).ToListAsync();
            List<Character> characters = new List<Character>();

            //Gathers all of the player characters in this listing to be stored in ViewData["Players"]
            foreach (var ListingCharacter in ListingCharacters)
            {
                
                Character character = await _context.Character
                    .Where(c => c.Id
                    .Equals(ListingCharacter.CharacterID)
                    && c.AppUserID
                    .Equals(ListingCharacter.AppUserID))
                    .FirstAsync();


                //TODO: Need to add ability for the creater (DM) of the CampaignListing to get added here as well. Ability
                //TODO: for DM to provide a name for themself needs to be added or something similar?
                if (ListingCharacter.AppUserID.Equals(User.FindFirstValue(ClaimTypes.NameIdentifier)))
                {

                    UserCharacterName = character.Name;
                }

                characters.Add(character);
            }
            
            TempData["ListingID"] = id;
            TempData["UserID"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //TODO: Add DM ability for DM to provide a name to show up in the chat members list
            // TempData["DM"] = Listing.DMName; 
            ViewData["Players"] = characters;
            TempData["ThisCharacter"] = UserCharacterName;
            //ViewBag["ListingGroupID"];
            ViewData["AllMessages"] = GroupMessages;

            return View();
        }

        public async Task<ActionResult> LeaveCampaign(string UserID, int CampaignListingId)
        {
            if (_context.Character != null)
            {
                //var userCharacterSet = await _context.Character
                //    .Where(c => c.AppUserID.Contains(userID)).ToListAsync();
                var campaignListing = await _context.CampaignListing.Where(cl => cl.Id.Equals(CampaignListingId)).FirstOrDefaultAsync();  
                var userCharacterSet = await _context.Character.Where(ch => ch.AppUserID.Contains(UserID)).ToListAsync();
                if (userCharacterSet != null && campaignListing != null)
                {
                    var playerList = campaignListing.Players;
                    if(playerList != null)
                    {
                        foreach (var player in playerList)
                        {
                            if (userCharacterSet.Contains(player))
                            {
                                _context.Remove(player);
                            }
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return View(userCharacterSet);

            }
            else
            {
                return Problem("Entity set 'ApplicationDbContext.Character'  is null.");
            }
            return View();
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
                    _context.SaveChanges();
                    

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
                //var userCharacterSet = await _context.Character
                //    .Where(c => c.AppUserID.Contains(userID)).ToListAsync();
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


        public void RollDice(string d4, string d6, string d8)
        {
            Debug.WriteLine("Input is: " + d4 +"," + d6 +","+ d8);
        }

    }
}
