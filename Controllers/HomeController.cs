using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Blog1.Models;
using Blog1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Blog1.Controllers;

public class HomeController : Controller
{
private readonly ApplicationDbContext _context;
public HomeController(ApplicationDbContext context){
    _context=context;
   
}
public async Task<IActionResult> Index(){

    
var today =DateTime.Now;
var articles=await _context.Article
        .Where(articles=>articles.StartDate<=today && articles.EndDate>=today)
        .OrderByDescending(a => a.CreateDate)
        .ToListAsync();
        return View(articles);
}
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
            [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDashboard()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

// Handle Admin Dashboard
// Like Approve Candidates
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ApproveContributor(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.Status = "Approved";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("AdminDashboard");
        }
// Here is assign the role to the Candidates
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateRole(string id, string role)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
               user.Role = role;
                user.IsApproved=true;
                await _context.SaveChangesAsync();
            
            }
            return RedirectToAction("AdminDashboard");
        }
}

