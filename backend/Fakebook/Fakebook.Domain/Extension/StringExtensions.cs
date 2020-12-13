using System;

namespace Fakebook.Domain.Extension
{
    public static class StringExtensions
    {
        public static void NullOrEmptyCheck(this string target, string targetName) {
            if(string.IsNullOrEmpty(target)) {
                throw new ArgumentException($"{targetName} was null.");
            }
        }
    }
}
