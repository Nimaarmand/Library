using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Constants.Commons
{
    public static class Messages
    {
        public static string Success(string? message = null)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "عملیات با موفقیت انجام شد.";
            }
            else
            {
                return $" {message} با موفقیت انجام شد.";
            }
        }

        public static string Error(string? message = null)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "خطا در انجام عملیات رخ داده است.";
            }
            else
            {
                return $"خطا: {message}";
            }
        }

        public static string Info(string? message = null)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "اطلاعات: عملیات با موفقیت انجام شد.";
            }
            else
            {
                return $"اطلاعات: {message}";
            }
        }
        public static string Repetitive(string? message = null)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "اطلاعات تکراری است.";
            }
            else
            {
                return $" {message} بررسی انجام شد.";
            }
        }
        public static string Warning(string? message = null)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "هشدار: عملیات با مشکلی مواجه شده است.";
            }
            else
            {
                return $"هشدار: {message}";
            }
        }

        public static string Custom(string message)
        {
            return $" {message} ";
        }
    }
}
