using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MFM.Data;
using MFM.Models;
using System.Drawing.Printing;
using MFM.BusinessEngine;

namespace MFM.Controllers
{
    public class OuterPartiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserServices _userServices;

        public OuterPartiesController(ApplicationDbContext context, IUserServices userServices)
        {
            _context = context;
            _userServices = userServices;
        }

        // GET: OuterParties
        public async Task<IActionResult> Index()
        {
              return View(await _context.OuterParties.ToListAsync());
        }

        // GET: OuterParties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OuterParties == null)
            {
                return NotFound();
            }

            var outerParty = await _context.OuterParties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (outerParty == null)
            {
                return NotFound();
            }

            return View(outerParty);
        }

        // GET: OuterParties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OuterParties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] OuterParty outerParty)
        {
            if (ModelState.IsValid)
            {
                outerParty.CreatorUser = await _context.AppUsers.FirstAsync(x => x.Id == _userServices.getCurrentUserID());
                _context.Add(outerParty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(outerParty);
        }

        // GET: OuterParties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OuterParties == null)
            {
                return NotFound();
            }

            var outerParty = await _context.OuterParties.FindAsync(id);
            if (outerParty == null)
            {
                return NotFound();
            }
            return View(outerParty);
        }

        // POST: OuterParties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] OuterParty outerParty)
        {
            if (id != outerParty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(outerParty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OuterPartyExists(outerParty.Id))
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
            return View(outerParty);
        }

        // GET: OuterParties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OuterParties == null)
            {
                return NotFound();
            }

            var outerParty = await _context.OuterParties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (outerParty == null)
            {
                return NotFound();
            }

            return View(outerParty);
        }

        // POST: OuterParties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OuterParties == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OuterParties'  is null.");
            }
            var outerParty = await _context.OuterParties.FindAsync(id);
            if (outerParty != null)
            {
                _context.OuterParties.Remove(outerParty);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OuterPartyExists(int id)
        {
          return _context.OuterParties.Any(e => e.Id == id);
        }
    }
}
