using System;
using System.ComponentModel.DataAnnotations;
using Endpoints.Framework.Extensions;

namespace Endpoints.Framework.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IsMd5Attribute: ValidationAttribute
    {
        public int Length { get; private set; }

        public IsMd5Attribute(int length = 32, string ErrorMessage = "'{0}' must be md5 string") : base(ErrorMessage)
        {
            Length = length;
        }

        public override bool IsValid(object value)
        {
            string val = value as string;
            if (val.Length != Length) return false;
            return $"^[a-zA-Z0-9_-]{{{Length}}}$".Test(val);
        }
    }
}
