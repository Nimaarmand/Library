using Domain.Entities.Commons;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.Books
{
    public class Book:BaseEntity
    {      
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }    
        public string ISBN { get; set; } 
        public int Pages { get; set; } 
        public string Publisher { get; set; }  
        public string Language { get; set; }  
        public bool IsAvailable { get; set; }        
        public DateTime PublicationDate { get; set; }  

        public long BookCategoriesId { get; set; }
        public BookCategories BookCategories { get; set; }
    }
}
