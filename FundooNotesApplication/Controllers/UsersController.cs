using BusinessLayer.InterfacesBL;
using CommanLayer.Models1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Windows.ApplicationModel.Email;
//using System.Web.Http;

namespace FundooNotesDuplicate.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IUserBL userBL;
        public UsersController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost]
        public IActionResult AddUser(UserRegestarion user)
        {
            try
            {
                if (userBL.Registration(user))
                {
                    return this.Ok(new { Success = true, message = "Registration successfull", response = user});
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Registration Unsuccessfull" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public IActionResult Login(UserLogin userlogin)
        {
                try
                {
                   string tokenstring = userBL.Login(userlogin);
                    if(tokenstring != null)
                    {
                        
                        return Ok(new { Token = tokenstring, Message = "Login successfull" });
                    }
                    else
                    {
                        return this.BadRequest(new { Success = false, message = "Invalid Username and Password" });
                    }
                }
            
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                string token = userBL.ForgetPassword(email);
                if (token!=null)
                {
                    //var lnkHref = "<a href='" + Url.Action("ResetPassword", "Account", new { email = User, code = token }, "http") + "'>Reset Password</a>";
                    ////HTML Template for Send email
                    //string subject = "Your changed password";
                    //string body = "<b>Please find the Password Reset Link. </b><br/>" + lnkHref;
                    ////Get and set the AppSettings using configuration manager.
                    //EmailManager.AppSettings(out ID, out Password, out SMTPPort, out Host);
                    ////Call send email methods.
                    //EmailManager.MessageQueue_ReceiveCompleted();
                    return Ok(new { message = "Token sent succesfully.Please check your email for password reset" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Email not registered" });
                }


            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
                var email = User.Claims.First(e => e.Type == "Email").Value;
                //var email1 = User.FindFirst(ClaimTypes.Email).Value.ToString();
                userBL.ResetPassword(email, password, confirmPassword);
                return Ok(new { message = "Password reset done succussfully" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
