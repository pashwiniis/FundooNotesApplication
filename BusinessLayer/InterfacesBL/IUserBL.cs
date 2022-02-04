using CommanLayer.Models1;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.InterfacesBL
{
    public interface IUserBL
    {
        public bool Registration(UserRegestarion user);
        public string Login(UserLogin userlogin);
        public string GenerateJwtToken(string email);
        public string ForgetPassword(string email);
        public bool ResetPassword(string email, string password, string confirmPassword);
    }
}
