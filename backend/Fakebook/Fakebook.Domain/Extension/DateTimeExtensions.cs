using System;

namespace Fakebook.Domain.Extension
{
    public static class DateTimeExtensions
    {
        public static void EnforcePast(this DateTime datetime) {
            if(!datetime.InPast()) {
                throw new ArgumentException($"{nameof(datetime)} is not in the past");
            }
        }

        public static bool InPast(this DateTime time) {
            return time <= DateTime.Now;
        }
    }
}
