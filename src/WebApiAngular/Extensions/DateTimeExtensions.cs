using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiAngular.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime Truncate(this DateTime dateTime)
        {
            //if (timeSpan == TimeSpan.Zero) return dateTime; // Or could throw an ArgumentException
            if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue) return dateTime; // do not modify "guard" values
            return dateTime.AddTicks(-(dateTime.Ticks % TimeSpan.FromSeconds(1).Ticks));
        }

        public static DateTimeOffset Truncate(this DateTimeOffset dateTime)
        {
            //if (timeSpan == TimeSpan.Zero) return dateTime; // Or could throw an ArgumentException
            if (dateTime == DateTimeOffset.MinValue || dateTime == DateTimeOffset.MaxValue) return dateTime; // do not modify "guard" values
            return dateTime.AddTicks(-(dateTime.Ticks % TimeSpan.FromSeconds(1).Ticks));
        }


        public static DateTime AsUtc(this DateTime dateTime)
        {
            var dt = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

            return dt;
        }


        public static DateTime AsLocal(this DateTime dateTime)
        {
            var dt = DateTime.SpecifyKind(dateTime, DateTimeKind.Local);

            return dt;
        }

        public static DateTimeOffset AsLocal(this DateTimeOffset dateTime)
        {
            var dt = dateTime.LocalDateTime;// DateTime.SpecifyKind(dateTime, DateTimeKind.Local);

            return dt;
        }
    }
}
