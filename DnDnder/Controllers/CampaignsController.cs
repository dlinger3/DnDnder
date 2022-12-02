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
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Tavern.Controllers
{
    public class CampaignsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CampaignsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Campaigns
        public async Task<IActionResult> Index()
        {
            if (_context.Campaign != null)
            {
                var userID =  User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userCampaignSet = await _context.Campaign
                    .Where(c => c.AppUserID.Contains(userID)).ToListAsync();
                Dictionary<int, ListedCampaign> listedCampaigns = new Dictionary<int, ListedCampaign>();
                if(userCampaignSet != null)
                {
                    foreach(var campaign in userCampaignSet)
                    {
                        var listing = await _context.CampaignListing.Where(cl => cl.CampaignId.Equals(campaign.Id)).FirstOrDefaultAsync();
                        if(listing != null)
                        {
                            ListedCampaign campaignListing = new ListedCampaign(campaign.Id, true, listing.Id);
                            listedCampaigns.Add(campaign.Id, campaignListing);
                        }
                    }
                }
                ViewData["listedCampaigns"] = listedCampaigns;
                return View(userCampaignSet);
            }
            else
            {
                return Problem("Entity set 'ApplicationDbContext.Campaign'  is null.");
            }
        }

        // GET: Campaigns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Campaign == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaign
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // GET: Campaigns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Campaigns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CampaignName,WorldName,Details,CampaignImg")] Campaign campaign)
        {
            try
            {
                campaign.AppUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                if (errors.Any())
                {
                    errors = errors.ToList();
                    foreach (var error in errors)
                    {
                        Debug.WriteLine(error);
                        Debug.WriteLine(error.ErrorMessage);
                    }
                }
                if (ModelState.IsValid)
                {
                    Debug.WriteLine("Model State was valid...");
                    _context.Add(campaign);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(InvalidDataException ide)
            {
                Debug.WriteLine(ide.StackTrace);
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(campaign);
        }

        // GET: Campaigns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Campaign == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaign.FindAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }
            return View(campaign);
        }

        // POST: Campaigns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CampaignName,WorldName,Details,AppUserID,CampaignImg")] Campaign campaign)
        {
            if (id != campaign.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campaign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampaignExists(campaign.Id))
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
            return View(campaign);
        }

        // GET: Campaigns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Campaign == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaign
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // POST: Campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Campaign == null)
            {
                return Problem("Entity set 'TavernContext.Campaign'  is null.");
            }
            var campaignListing = await _context.CampaignListing.Where(cl => cl.CampaignId == id && cl.AppUserID == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();
            if(campaignListing != null)
            {
                foreach(var listing in campaignListing)
                {
                    _context.CampaignListing.Remove(listing);
                }
            }

            var campaign = await _context.Campaign.FindAsync(id);
            if (campaign != null)
            {
                _context.Campaign.Remove(campaign);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ListCampaign(int id)
        {
            Debug.WriteLine("In ListCampaign in Campaigns Controller");

            TempData["CampaignID"] = id;
            TempData["AppUserID"] = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //List<CampaignListing> ListOfCampaigns = await _context.CampaignListing.ToListAsync();
            return RedirectToAction("Create", "CampaignListings");
        }
        public async Task<IActionResult> DelistCampaign(int listingID)
        {
            if (_context.CampaignListing == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CampaignListing'  is null.");
            }
            var campaignListing = await _context.CampaignListing.FindAsync(listingID);
            if (campaignListing != null)
            {
                var listingCharacters = await _context.CampaignCharacters.Where(lc => lc.CampaignListingID.Equals(listingID)).ToListAsync();
                foreach (var character in listingCharacters)
                {
                    _context.CampaignCharacters.Remove(character);
                    await _context.SaveChangesAsync();
                }
                var listingMessages = await _context.Message.Where(m => m.CampaignListingID == listingID).ToListAsync();
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
        private bool CampaignExists(int id)
        {
            return (_context.Campaign?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
