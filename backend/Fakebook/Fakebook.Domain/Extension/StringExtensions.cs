using System;
using System.Text.RegularExpressions;

using Fakebook.RestApi.Model;

namespace Fakebook.Domain.Extension
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string target) {
            return string.IsNullOrEmpty(target);
        }

        public static void NullOrEmptyCheck(this string target, string targetName) {
            if(target.IsNullOrEmpty()) {
                throw new ArgumentException($"{targetName} was null.");
            }
        }

        public static void EnforceNameCharacters(this string target, string targetName) {
            target.NullOrEmptyCheck(targetName);

            var regex = new Regex(RegularExpressions.NameCharacters);

            if(!regex.IsMatch(target)) {
                throw new ArgumentException($"{targetName} can only contain name characters.");
            }
        }

        public static void EnforceNoSpecialCharacters(this string target, string targetName) {
            target.NullOrEmptyCheck(targetName);

            var regex = new Regex(RegularExpressions.NoSpecialCharacters);

            if(!regex.IsMatch(target)) {
                throw new ArgumentException($"{targetName} cannot have special characters.");
            }
        }

        public static void EnforceEmailCharacters(this string target, string targetName) {
            target.NullOrEmptyCheck(targetName);

            var regex = new Regex(RegularExpressions.EmailCharacters);

            if (!regex.IsMatch(target)) {
                throw new ArgumentException($"{targetName} is not a valid email.");
            }
        }

        public static void EnforcePhoneNumberCharacters(this string target, string targetName) {
            target.NullOrEmptyCheck(targetName);

            var regex = new Regex(RegularExpressions.PhoneNumberCharacters);

            if (!regex.IsMatch(target)) {
                throw new ArgumentException($"{targetName} isn't a valid phone number.");
            }
        }
    }
}
