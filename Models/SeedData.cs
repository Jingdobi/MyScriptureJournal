using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyScriptureJournal.Data;
using System;
using System.Linq;

namespace RazorPagesScripture.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MyScriptureJournalContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MyScriptureJournalContext>>()))
            {
                // Look for any movies.
                if (context.Scripture.Any())
                {
                    return;   // DB has been seeded
                }

                context.Scripture.AddRange(
                    new Scripture
                    {
                        Book = "Moroni",
                        AddDate = DateTime.Now, 
                        Chapter = 5,
                        Verse = 8,
                        Notes = "This is my favorite scripture"
                    },

                    new Scripture
                    {
                        Book = "Genesis",
                        AddDate = DateTime.Now,
                        Chapter = 20,
                        Verse = 20,
                        Notes = "This is my second favorite scripture"
                    },

                    new Scripture
                    {
                        Book = "Mormon",
                        AddDate = DateTime.Now,
                        Chapter = 10,
                        Verse = 5,
                        Notes = "This is my third favorite scripture"
                    },

                    new Scripture
                    {
                        Book = "Jacob",
                        AddDate = DateTime.Now,
                        Chapter = 5,
                        Verse = 63,
                        Notes = "This is my fourth favorite scripture"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}