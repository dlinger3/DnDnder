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
using Microsoft.AspNetCore.Identity;

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
            return _context.Campaign != null ?
                        View(await _context.Campaign.ToListAsync()) :
                        Problem("Entity set 'TavernContext.Campaign'  is null.");
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

        //if prefix did not work change the bind to Bind["CampaignName,WorldName,Details"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CampaignName,WorldName,Details,AppUserID,AppUser")] Campaign campaign)
        {
            try
            {
                //TODO: Need to implement a better way to assign the ID. Try reasearching user sessions for asp.net core 6
                string UserEmail = User.Identity.Name;              
                var AppUser = from user in _context.Users
                              where user.Email == UserEmail
                              select user.Id;
                campaign.AppUserID = AppUser.Single();
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,campaignName,worldName,details")] Campaign campaign)
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
            var campaign = await _context.Campaign.FindAsync(id);
            if (campaign != null)
            {
                _context.Campaign.Remove(campaign);
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
