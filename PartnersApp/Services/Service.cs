using PartnersApp.dbContext;
using PartnersApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PartnersApp.Services
{
    public class Service : IDisposable
    {
        private PartnersDBEntities _db = new PartnersDBEntities();

        public bool Authenticate(string login, string password, out bool isAdmin)
        {
            var user = _db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
            isAdmin = user?.IsAdmin ?? false;
            return user != null;
        }

        public void Dispose() => _db?.Dispose();
    }
}