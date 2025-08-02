using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartnersApp.dbContext;
using PartnersApp.Models;

namespace PartnersApp.Services
{
    public class AuthService
    {
        public bool Authenticate(string login, string password, out bool isAdmin)
        {
            using (var db = DbContextFactory.Create())
            {
                var user = db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

                if (user != null)
                {
                    isAdmin = user.IsAdmin;
                    return true;
                }

                isAdmin = false;
                return false;
            }
        }
    }
}