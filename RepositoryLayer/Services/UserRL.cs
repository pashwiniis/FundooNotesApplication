using CommanLayer.Models1;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entites;
using RepositoryLayer.InterfacesRL;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        Context context;

        private readonly IConfiguration configuration;
        public UserRL(Context context, IConfiguration config)
        {
            this.context = context;//appcontext to for api
            this.configuration = config;//for startup file instance
        }
        /// <summary>
        /// Implemented the regestration of new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns true></returns>
        /// <returns false></returns>
        public bool Registration(UserRegestarion user)
        {
            try
            {
                User newuser = new User();
                newuser.FirstName = user.FirstName;
                newuser.LastName = user.LastName;
                newuser.Email = user.Email;
                newuser.Password = EncryptPassword(user.Password);
                context.Users.Add(newuser);
                int result = context.SaveChanges();//save all changes in database also
                if (result > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Implemented the encyription of passsword method to encrypt the password
        /// </summary>
        /// <param name="password"></param>
        /// <returns encryptedPassword></returns>
        public string EncryptPassword(string password)
        {
            try
            {
                byte[] encode = new byte[password.Length];
                encode = Encoding.UTF8.GetBytes(password);
                string encPassword = Convert.ToBase64String(encode);
                return encPassword;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// defination of the decrypted password 
        /// </summary>
        /// <param name="encryptpwd"></param>
        /// <returns decryptpwd ></returns>
        public string DecryptPassword(string encryptpwd)
        {
            try
            {
                UTF8Encoding encodepwd = new UTF8Encoding();
                Decoder Decode = encodepwd.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
                int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string decryptpwd = new String(decoded_char);
                return decryptpwd;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// implented the login method for user login
        /// </summary>
        /// <param name="userlogin"></param>
        /// <returns ClaimTokenByID></returns>
        /// /// <returns null></returns>
        public string Login(UserLogin userlogin)
        {
            try
            {
                User newuser = new User();

                newuser = context.Users.Where(x => x.Email == userlogin.Email).FirstOrDefault();
                string decPass = DecryptPassword(newuser.Password);
                var ID = newuser.Id;
                if (decPass==userlogin.Password && newuser!=null)
                {
                    return ClaimTokenByID(ID);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public string ClaimTokenByID(long Id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", Id.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        /// <summary>
        /// implemented this method because of generation of token 
        /// </summary>
        /// <param name="email"></param>
        /// <returns token></returns>
        public string GenerateJwtToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Email", email) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        /// <summary>
        /// it defines the forget method for forget password
        /// </summary>
        /// <param name="email"></param>
        /// <returns token></returns>
        /// /// <returns null></returns>
        public string ForgetPassword(string email)
        {
            try
            {
                var checkemail = context.Users.FirstOrDefault(e => e.Email == email);
                if (checkemail!=null)
                {
                    
                    var token = GenerateJwtToken(email);
                    new MSMQModel().MSMQSender(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <returns true></returns>
        /// <returns false></returns>
        public bool ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {

                if (password.Equals(confirmPassword))
                {
                    User user = context.Users.Where(e => e.Email==email).FirstOrDefault();
                    user.Password = confirmPassword;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
