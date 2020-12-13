using System;

namespace Fakebook.Domain.Extension
{
    public static class ObjectExtensions
    {
        public static void NullCheck(this object target, string targetName) {
            if(target is null) {
                throw new ArgumentException($"{targetName} was null.");
            }
        }
    }
}
