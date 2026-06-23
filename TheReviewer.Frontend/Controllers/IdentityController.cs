using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TheReviewer.Frontend.Models;
using TheReviewer.Logic.Models;
using TheReviewer.Logic.Services;

namespace TheReviewer.Frontend.Controllers;

public class IdentityController(ReviewerService reviewerService) : Controller
{
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        var result = reviewerService.Create(model.Email, model.Password);
        if (!result.Success || result.Reviewer is null)
        {
            var errorMessage = result.AddCreateReviewerError();
            ModelState.AddModelError(string.Empty, errorMessage);

            return View(model);
        }

        await SignInAsync(result.Reviewer);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var reviewer = reviewerService.Login(model.Email, model.Password);
        if (reviewer is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid email or password");
            return View(model);
        }

        await SignInAsync(reviewer);
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    private async Task SignInAsync(ReviewerModel reviewer)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, reviewer.Id.ToString()),
            new(ClaimTypes.Email, reviewer.Email),
            new(ClaimTypes.Name, reviewer.Name)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }
}

