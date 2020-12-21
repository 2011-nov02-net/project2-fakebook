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

        public static void EnforceIsBefore(this DateTime datetime, DateTime target) {
            if (datetime < target) {
                throw new ArgumentException($"{nameof(datetime)} is greater than {target}.");
            }
        }

        public static bool InPast(this DateTime time) {
            return time <= DateTime.Now;
        }
    }
}
