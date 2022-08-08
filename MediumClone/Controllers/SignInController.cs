using MediumClone.Models;
using MediumClone.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace MediumClone.Controllers
{   
    public class SignInController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        
        private readonly SignInManager<AppUser> signInManager;

        public SignInController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;            
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region CreateOperations
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserVM user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser();
                appUser.UserName = user.UserName;
                appUser.Email = user.Email;
                appUser.FirstName = user.FirstName;
                appUser.LastName = user.LastName;                
                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                IdentityResult resultRole = await userManager.AddToRoleAsync(appUser, "User");
                if (result.Succeeded && resultRole.Succeeded)
                {

                    string emailToken = await userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    MailMessage mail = new MailMessage();
                    mail.IsBodyHtml = true;
                    mail.To.Add(user.Email);
                    mail.From = new MailAddress("ardasen.business@gmail.com", "Email Onaylama Servisi", System.Text.Encoding.UTF8);
                    mail.Subject = "Şifre Güncelleme Talebi";
                    var confirmationLink = Url.Action("VerifyEmail", "SignIn", new { emailToken, userId =appUser.Id }, Request.Scheme);
                    mail.Body = confirmationLink;
                    mail.IsBodyHtml = true;
                    SmtpClient smp = new SmtpClient();
                    smp.Credentials = new NetworkCredential("ardasen.business@gmail.com", "fgywhklxwbwpusyg");
                    smp.Port = 587;
                    smp.Host = "smtp.gmail.com";
                    smp.EnableSsl = true;
                    await smp.SendMailAsync(mail);
                    //SendEmail(appUser, emailToken);
                    // gmail uygulma şifre fgywhklxwbwpusyg
                    Microsoft.AspNetCore.Identity.SignInResult resultCheck = await signInManager.PasswordSignInAsync(appUser, user.Password, false, false);
                    return RedirectToAction("EmailVerification");//sonradan değiştiriliecek                    
                }
                else
                {
                    Error(result);
                }
            }
            return View(user);
        }
        public async Task<IActionResult> VerifyEmail(string userId,string emailToken)
        {
            AppUser user = await userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest();
           var result= await userManager.ConfirmEmailAsync(user, emailToken);
            if (result.Succeeded)
            {

            return View();
            }
            return BadRequest();
        }
        public IActionResult EmailVerification()
        {
            return View();
        }

        #endregion
        void Error(IdentityResult result)
        {
            foreach (IdentityError item in result.Errors)
            {
                ModelState.AddModelError("User Operation", $"{item.Code} - {item.Description}");
            }
        }
        public void SendEmail(AppUser user,string emailToken)
        {           
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = true;
            mail.To.Add(user.Email);
            mail.From = new MailAddress("ardasen.business@gmail.com", "Şifre Güncelleme", System.Text.Encoding.UTF8);
            mail.Subject = "Şifre Güncelleme Talebi";
            mail.Body = $"<a target=\"_blank\" href=\"https://localhost:44303/SignIn/VerifyEmail{Url.Action("VerifyEmail", "User", new { userId = user.Id, token = HttpUtility.UrlEncode(emailToken) })}\">Emailini aktivite etmek için tıklayınız</a>";
            mail.IsBodyHtml = true;
            SmtpClient smp = new SmtpClient();
            smp.Credentials = new NetworkCredential("ardasen.business@gmail.com", "Hadron1996");
            smp.Port = 587;
            smp.Host = "smtp.gmail.com";
            smp.EnableSsl = true;
            smp.Send(mail);
        }


    }
}
