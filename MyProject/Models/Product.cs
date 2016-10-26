using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MyProject.Models
{
    public class Product
    {
       
        public int ProductId { get; set; }
        public string ProdName { get; set; }
        public decimal ProdPrice { get; set; }
    
        public string ProdImageUrl { get; set; }
        
        public string ProdDescription { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}