using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Movies.Models;
using Movies.Models.AccountViewModels;
using Movies.ViewModels;

namespace Movies.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminPanelController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Data.ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roles;

        public AdminPanelController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        Data.ApplicationDbContext context,
        RoleManager<IdentityRole> roleMan)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _roles = roleMan;
        }

        public async Task<IActionResult> Index()
        {
            var allRoles = from r in _roles.Roles select r; // spis rol, nazwa roli i id roli
            ViewBag.allRoles = allRoles;
            var rolki2 = from k in _context.UserRoles select k;  // role id, user id

            //var rolki3 = from k in _context.Roles select k;  // 
            //var rolki4 = from k in _context.Users select k;  // 
            //var useman = from um in _userManager.Users select um; // user id, user name
            //var users = _userManager.Users; // to samo co wyzej

            var contx = (from r in _roles.Roles
                        join k in _context.UserRoles
                        on r.Id equals k.RoleId
                        join u in _userManager.Users
                        on k.UserId equals u.Id
                        select new UserRoleViewModel
                        {
                            UserName = u.UserName,
                            UserId = u.Id,
                            Role = r.Name
                            
                        }).ToList();

            return View(contx);
        }

        [HttpPost]
        public async Task<ActionResult> AddRole(string userId, string roleId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddToRoleAsync(user, roleId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AddRole(string userId)
        {
            var roles = (from r in _roles.Roles select 
                        new UserRoleViewModel
                        {
                            Role = r.Name,
                            RoleId = r.Id
                           
                        }).ToList();
            ViewBag.UserId = userId;

            return View(roles);
        }



        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            

            if (await _userManager.IsInRoleAsync(user, "Admin") != true)
            {
                if (user != null)
                {
                    IdentityResult result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        ViewBag.UserDeleteError = "usunięto użytkownika" + " : " + user.UserName;
                        //return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.UserDeleteError = "nie udało się usunąć użytkownika" + " : " + user.UserName;
                        //return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                ViewBag.UserDeleteError = "nie możesz usunąć administratora";
            }
            return View("Index", _userManager.Users);
        }
    }
}