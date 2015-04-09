using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FvckYeahBubbleTea.Models
{
    public class TeaContext : DbContext
    {
        public TeaContext()
            : base("name=DefaultConnection")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Friend> Friends { get; set; }
        public DbSet<BaseTea> BaseTeas { get; set; }
        public DbSet<Flavor> Flavors { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<FinalOrder> FinalOrders { get; set; }
    }
}