using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using RazorPagesScripture.Models;

namespace MyScriptureJournal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public IList<Scripture> Scripture { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList Books { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ScriptureBook { get; set; }

        public string DateSort { get; set; }

        public string BookSort { get; set; }

 /*       public async Task OnGetAsync(string sortOrder)
        {
           
        }*/
        public async Task OnGetAsync(string sortOrder)
        {
            IQueryable<string> bookQuery = from m in _context.Scripture
                                            orderby m.Book
                                            select m.Book;

            var notes = from m in _context.Scripture
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                notes = notes.Where(s => s.Notes.Contains(SearchString));
            }
            if(!string.IsNullOrEmpty(ScriptureBook))
            {
                notes = notes.Where(x => x.Book == ScriptureBook);
            }
            Books = new SelectList(await bookQuery.Distinct().ToListAsync());
            Scripture = await notes.ToListAsync();

             BookSort = String.IsNullOrEmpty(sortOrder) ? "book_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<Scripture> dateIQ = from d in _context.Scripture
                                           select d;
            switch (sortOrder)
            {
                case "book_desc":
                    dateIQ = dateIQ.OrderByDescending(d => d.Book);
                    break;
                case "date_desc":
                    dateIQ = dateIQ.OrderByDescending(d => d.AddDate);
                    break;
            }
            Scripture = await dateIQ.AsNoTracking().ToListAsync();
        }
    }
}
