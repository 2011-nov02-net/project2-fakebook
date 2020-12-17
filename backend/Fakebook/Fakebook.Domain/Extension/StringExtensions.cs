using System;

namespace Fakebook.Domain.Extension
{
    /// <summary>
    /// Static class containing extension methods to be called on strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks if the string in which this is called on is null or empty.
        /// If it is, an 'ArgumentException' will be thrown
        /// </summary>
        /// <param name="target">The string that the method is being called on</param>
        /// <param name="targetName">The name of the variable that will be shown in the exception</param>
        public static void NullOrEmptyCheck(this string target, string targetName) {
            if(string.IsNullOrEmpty(target)) {
                throw new ArgumentException($"{targetName} was null.");
            }
        }
    }
}
