using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyProject.Models
{
    public class MyDatabaseContext:DbContext
    {
        public MyDatabaseContext() : base("MyFirstConnection")
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public  DbSet<Product> Products { get; set; }

       
    }
}