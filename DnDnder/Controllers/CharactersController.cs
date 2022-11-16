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
using System.Diagnostics;

namespace Tavern.Controllers
{
    public class CharactersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CharactersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Characters
        public async Task<IActionResult> Index()
        {
            if (_context.Character != null)
            {
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userCharacterSet = await _context.Character
                    .Where(c => c.AppUserID.Contains(userID)).ToListAsync();

                return View(userCharacterSet);

            }
            else
            {
                return Problem("Entity set 'ApplicationDbContext.Character'  is null.");
            }
        }

        // GET: Characters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Character == null)
            {
                return NotFound();
            }

            var character = await _context.Character
                .FirstOrDefaultAsync(c => c.Id
                .Equals(id));

            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // GET: Characters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Class,Level,Race,Alignment,Bio")] Character character)
        {
            character.AppUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
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
                _context.Add(character);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(character);
        }

        // GET: Characters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Character == null)
            {
                return NotFound();
            }

            var character = await _context.Character.FindAsync(id);

            if (character == null)
            {
                return NotFound();
            }
            return View(character);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Class,Level,Race,Alignment,Bio,AppUserID")] Character character)
        {
            if (id != character.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(character);
                    await _context.SaveChangesAsync();                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(character.Id))
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
            return View(character);
        }

        // GET: Characters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Character == null)
            {
                return NotFound();
            }

            var character = await _context.Character
                .FirstOrDefaultAsync(m => m.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Character == null)
            {
                return Problem("Entity set 'TavernContext.Character'  is null.");
            }
            var character = await _context.Character.FindAsync(id);
            if (character != null)
            {
                var charactersCampaigns = await _context.CampaignCharacters.Where(cc => cc.CharacterID == id).ToListAsync();
                foreach(var listing in charactersCampaigns)
                {
                    _context.Remove(listing);
                    await _context.SaveChangesAsync();
                }
                _context.Character.Remove(character);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterExists(int id)
        {
            return (_context.Character?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
