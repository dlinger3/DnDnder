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
            ViewBag.AppUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
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


        /**
         * 
         * 
         * 
         * VISIT THIS PAGE FOR POSSIBLE SOLUTION TO THIS. NEED TO POSSIBLY ALTER ApplicationDbContext OnModelCreating() 
         * https://stackoverflow.com/questions/70236977/asp-net-core-model-with-a-collectionstring
         * 
         * 
         */
        [HttpPost]
        public async Task<ActionResult> JoinCampaign(string UserID, int CharacterID, int CampaignListingID)
        {
           if(UserID != null && CharacterID != null && CampaignListingID != null)
            {
                var CampaignListing = await _context.CampaignListing.Where(cl => cl.Id.Equals(CampaignListingID)).FirstOrDefaultAsync();
                var NewPlayer = await _context.Character.FindAsync(CharacterID);
                if (NewPlayer != null)
                {
                    if(CampaignListing.Players == null)
                    {
                        CampaignListing.Players = new List<Character>();
                        CampaignListing.Players.Add(NewPlayer);
                    }
                    else
                    {
                        CampaignListing.Players.Add(NewPlayer);
                    }
                    

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


        public async Task<ActionResult> JoinCampaignReroute(string UserID, int CampaignListingId)
        {
            if (_context.Character != null)
            {
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


    }
}
