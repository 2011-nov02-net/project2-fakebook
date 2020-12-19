﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fakebook.RestApi.Model
{
    public static class RegularExpressions
    {
        public const string NumbersOnly = @"^[0-9]*$";
        public const string NoSpecialCharacters = @"^[A-Za-z0-9 '-,.#]*$";
        public const string NameCharacters = @"^[A-Za-z0-9 -]*$";
        public const string PasswordCharacters = @"^[A-Za-z0-9!@#$%&-_]{8,}$";
        public const string EmailCharacters = @"^[a-zA-Z]+[a-zA-Z0-9\.]*\@[a-zA-Z]+[a-zA-Z0-9]+\.([a-zA-Z]+[a-zA-Z0-9]+){0,253}";
        public const string PhoneNumberCharacters = @"^(\+[0-9]{0,3})?[ -]?[0-9]{3}[ -]?[0-9]{3}[ -]?[0-9]{4}";
    }
}
