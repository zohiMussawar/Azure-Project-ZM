using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blog1.Data;
using Blog1.Models;
using Microsoft.AspNetCore.Authorization;

namespace Blog1.Controllers
{
   
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            // var username=User.Identity.Name;
            // var articles=await _context.Articles
            //             .Where(a => a.ContributorUsername==username)
            //             .ToListAsync();


            // return View(articles);
             return View(await _context.Article.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
            var article=await _context.Articles.FindAsync(id);
            if(article== null) return NotFound();
            return View(article);
            //  if (id == null)
            // {
            //     return NotFound();
            // }

            // var article = await _context.Article
            //     .FirstOrDefaultAsync(m => m.ArticleId == id);
            // if (article == null)
            // {
            //     return NotFound();
            // }

            // return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleId,Title,Body,CreateDate,StartDate,EndDate")] Article article)
        {
            // if (!ModelState.IsValid) return View(article);
            
            //  article.ContributorUsername=User.Identity.Name;
            //     article.CreateDate=DateTime.Now;
            //     _context.Add(article);
            //     await _context.SaveChangesAsync();
            //     return RedirectToAction(nameof(Index));
             if (ModelState.IsValid)
            {
               
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(article);
          
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
             if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
            // if (id == null)
            // {
            //     return NotFound();
            // }

            // var article = await _context.Article.FindAsync(id);
            // if (article == null || article.ContributorUsername != User.Identity.Name)
            // {
            //     return Unauthorized();
            // }
            // return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,Title,Body,CreateDate,StartDate,EndDate")] Article article)
        {
             if (id != article.ArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.ArticleId))
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
            return View(article);
            // if (id != model.ArticleId)
            // {
            //     return Unauthorized();
            // }

            // if (!ModelState.IsValid) return View(model);
            // var article= await _context.Articles.FindAsync(model.ArticleId);
            // if(article==null || article.ContributorUsername !=User.Identity.Name)
            //     return Unauthorized();
            //     article.Title=model.Title;
            //     article.Body=model.Body;
            //     article.StartDate=model.StartDate;
            //     article.EndDate=model.EndDate;
            //     await _context.SaveChangesAsync();
            //     return RedirectToAction("Index");
            // {
            
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Article.FindAsync(id);
            if (article != null)
            {
                _context.Article.Remove(article);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.ArticleId == id);
        }
    }
}
