using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    
    public class Category
    {

        
        public int CategoryId { get; set; }
        public string CatName { get; set; }
    }
}