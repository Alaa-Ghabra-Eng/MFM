using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MFM.Data;
using MFM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using MFM.Data.Migrations;
using System.Security.Claims;
using MFM.Models.ViewModels;
using MFM.BusinessEngine;
using System.Drawing;
using X.PagedList;
using System.Composition;

namespace MFM.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserServices _userServices;
        public TransactionsController(ApplicationDbContext context, IUserServices userServices)
        {
            _context = context;
            _userServices = userServices;
        }

        public List<Transaction> GetTransactions()
        {
            return _context.Transactions.Include(trx => trx.CreatorUser).ToList();
        }
        // GET: All Transactions 
        public async Task<IActionResult> GetAll(int? page, string? searchTerm)
        {
            if (searchTerm != null)
                return View(await _context.Transactions.Include(trx => trx.CreatorUser).Where(x => x.Description.Contains(searchTerm)).OrderByDescending(x => x.Created).ToPagedListAsync<Transaction>(page ?? 1, 3));
            else
                return View(await _context.Transactions.Include(trx => trx.CreatorUser).OrderByDescending(x => x.Created).ToPagedListAsync<Transaction>(page ?? 1, 6));
        }

        // GET: Transactions Per User
        public async Task<IActionResult> Get(int? page)
        {
            Console.WriteLine(_userServices.getCurrentUser().Id.ToString());
            return View(await _context.Transactions.Include(trx => trx.Category).Include(trx => trx.OuterParty).Where(trx => trx.CreatorUser.Id == _userServices.getCurrentUser().Id).OrderByDescending(x => x.Created).ToPagedListAsync<Transaction>(page ?? 1, 6));
        }
        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.Include(x => x.CreatorUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //ViewData["Categories"]  = await _context.Categories.ToListAsync(); 
            //ViewData["OuterParties"] =await  _context.OuterParties.ToListAsync();
            TransactionCreateViewModel trxView = new TransactionCreateViewModel();
            trxView.Categories = await _context.Categories.ToListAsync();
            trxView.OuterParties = await _context.OuterParties.ToListAsync();
            trxView.Budgets = await _context.Budgets.ToListAsync();
            return View(trxView);
        }
        [HttpPost]

        public async Task<IActionResult> Create(TransactionCreateViewModel trxView)
        {
            Transaction NewTransaction = new();
            NewTransaction = trxView.Transaction;
            //Check direction and set value sign accordingly 
            if ((NewTransaction.IsIncome && NewTransaction.Amount < 0) || (!NewTransaction.IsIncome && NewTransaction.Amount > 0))
                NewTransaction.Amount *= -1;

            NewTransaction.Category = await _context.Categories.FirstAsync(x => x.Id == trxView.Category.Id);
            NewTransaction.OuterParty = await _context.OuterParties.FirstAsync(x => x.Id == trxView.OuterParty.Id);

            // Budget processing
            // check for selected budget and link it
            if (trxView.Budget.Id != 0) // User did select a budget for this transaction
            {
                // check budget's current fund to decide wether to complete the transcation or not
                var selectedBudget = await _context.Budgets.FirstAsync(x => x.Id == trxView.Budget.Id);
                if (Math.Abs(NewTransaction.Amount) <= selectedBudget.CurrentFunds) // then proceed and link budget
                {
                    NewTransaction.budget = selectedBudget; // link budget
                    selectedBudget.CurrentFunds -= Math.Abs(NewTransaction.Amount); // process budget funds.
                    _context.Update(selectedBudget);
                    await _context.SaveChangesAsync();
                }
                else
                    return BadRequest("Selected Budget Fund Exceeded !!!");                   
            }
            NewTransaction.CreatorUser = await _context.AppUsers.FirstAsync(x => x.Id == _userServices.getCurrentUserID());
            NewTransaction.Created = DateTime.Now;
            _context.Add(NewTransaction);
            //Console.WriteLine(DateTime.Now.ToLongTimeString(),"-------Strart of Debug-------");
            //foreach (var entry in _context.ChangeTracker.Entries())
            //{ 
            //    Console.WriteLine(entry);
            //}
            //Console.WriteLine("-------End of Debug-------");

            await _context.SaveChangesAsync();
            return RedirectToAction("Create");
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /* [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create([Bind("Id,Amount,Description,Created")] Transaction transaction)
         {
             if (ModelState.IsValid)
             {
                 transaction.CreatorUser = await GetCurrentUserAsync();
                 _context.Add(transaction);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Get));
             }
             return View(transaction);
         }*/
        [HttpGet]
        /*public async Task<IActionResult> CreateTransaction(TransactionCreateViewModel transaction) {
            transaction.Amount = 1000;
            transaction.Category = await _context.Categories.FirstOrDefaultAsync();
            transaction.OuterParty = await _context.OuterParties.FirstOrDefaultAsync();    
            return View(transaction);
        }*/
        [HttpPost]
        public TransactionCreateViewModel CreateTransactionPost(TransactionCreateViewModel transaction)
        {
            return transaction;
        }
        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,Description,Created")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
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
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
            }
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Get));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
