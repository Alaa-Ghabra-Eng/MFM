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
using MFM.Models.ViewModels;
using X.PagedList;
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
        public async Task<IActionResult> Index(int? page)
        {
            //return View(await _context.OuterParties.Include(party => party.Category).ToListAsync());
            return View(await _context.OuterParties.Include(party => party.Category).ToPagedListAsync<OuterParty>(page ?? 1, 6));
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
            OuterPartyCreateViewModel model= new OuterPartyCreateViewModel();
            model.Categories = _context.Categories.ToList();
            return View(model);
        }

        // POST: OuterParties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OuterPartyCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.outerParty.CreatorUser = await _context.AppUsers.FirstAsync(x => x.Id == _userServices.getCurrentUserID());
                model.outerParty.Category = await _context.Categories.FirstAsync(x => x.Id == model.outerParty.Category.Id);
                _context.Add(model.outerParty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
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
