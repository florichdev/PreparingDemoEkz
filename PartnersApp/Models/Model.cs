using System;

namespace PartnersApp.Models
{
    public class Partner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string Director { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Rating { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Sale
    {
        public int Id { get; set; }
        public int PartnerId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}