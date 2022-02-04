using CommanLayer.Models1;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.InterfacesRL
{
    public interface IUserRL
    {
        public bool Registration(UserRegestarion user);
        public string Login(UserLogin userLogin);
        public string EncryptPassword(string password);
        public string DecryptPassword(string encryptpwd);
        public string GenerateJwtToken(string email);
        public string ForgetPassword(string email);
        public bool ResetPassword(string email, string password, string confirmPassword);
    }
}
