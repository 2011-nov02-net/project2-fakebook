using System;

namespace Fakebook.Domain.Extension
{
    /// <summary>
    /// Extensions made to be callable on the base .NET/C# object
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Method that checks if a given object is null. 
        /// If it is, an 'ArgumentException' will be thrown, to be caught later.
        /// </summary>
        /// <param name="target">The object that the method is being called on</param>
        /// <param name="targetName">The name of the object that will be shown in the error message</param>
        public static void NullCheck(this object target, string targetName) {
            if(target is null) {
                throw new ArgumentException($"{targetName} was null.");
            }
        }
    }
}
