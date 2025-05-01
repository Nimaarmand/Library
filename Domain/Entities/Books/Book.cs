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
    public class Book : BaseEntity
    {
        /// <summary>
        /// نام کتاب
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// نویسنده کتاب
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// موضوع کتاب
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// شماره شناسایی بین‌المللی کتاب (ISBN)
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// تعداد صفحات کتاب
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// ناشر کتاب
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// زبان کتاب
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// مشخص می‌کند که آیا کتاب در دسترس است یا خیر
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// تاریخ انتشار کتاب
        /// </summary>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// شناسه دسته‌بندی کتاب
        /// </summary>
        public long BookCategoriesId { get; set; }

        /// <summary>
        /// شی مرتبط به دسته‌بندی کتاب‌ها
        /// </summary>
        public BookCategories BookCategories { get; set; }
    }
}
