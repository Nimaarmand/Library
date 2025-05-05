using Domain.Entities.Commons;
using Domain.Entities.Reservations;
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
   

    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTime PublicationDate { get; set; }
        // ارتباط مستقیم با دسته‌بندی کتاب
        public long BookCategoriesId { get; set; }
        public virtual BookCategories BookCategories { get; set; }
        // ارتباط مستقیم با رزروها
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        /// <summary>
        /// ارتباط با کتاب های تحویل داده شده
        /// </summary>
        public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
    }

}

   

    

