using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartnersApp.dbContext;

namespace PartnersApp.Services
{
    public static class DbContextFactory
    {
        public static PartnersDBEntities Create()
        {
            return new PartnersDBEntities();
        }
    }
}
