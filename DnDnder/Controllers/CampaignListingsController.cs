using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tavern.Data.Migrations;
using Tavern.Models;
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
            ViewData["CampaignId"] = new SelectList(_context.Campaign, "Id", "CampaignName");
            return View();
        }

        // POST: CampaignListings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CampaignId")] CampaignListing campaignListing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campaignListing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CampaignId"] = new SelectList(_context.Campaign, "Id", "CampaignName", campaignListing.CampaignId);
            return View(campaignListing);
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

        private bool CampaignListingExists(int id)
        {
            return _context.CampaignListing.Any(e => e.Id == id);
        }
    }
}
