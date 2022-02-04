using BusinessLayer.InterfacesBL;
using CommanLayer.Models1;
using RepositoryLayer.InterfacesRL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.services1
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public bool Registration(UserRegestarion user)
        {
            try
            {
                return userRL.Registration(user);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Login(UserLogin userlogin)
        {
            try
            {
                return userRL.Login(userlogin);
            }
            catch (Exception)
            {

                throw;
            }
        }
       
        public string GenerateJwtToken(string email)
        {
            try
            {
                return userRL.GenerateJwtToken(email);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string ForgetPassword(string email)
        {
            try
            {
                return userRL.ForgetPassword(email);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {
                return userRL.ResetPassword(email, password, confirmPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
