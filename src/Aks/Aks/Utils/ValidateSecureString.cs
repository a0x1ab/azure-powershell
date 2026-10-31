using Microsoft.Azure.Commands.Aks.Properties;

using System;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.Security;
using System.Text.RegularExpressions;

namespace Microsoft.Azure.Commands.Aks.Utils
{
    public sealed class ValidateSecureString: ValidateEnumeratedArgumentsAttribute
    {
        public string RegularExpression { get; set; }

        protected override void ValidateElement(object element)
        {
            SecureString secureString = element as SecureString;
            string content = SecureStringToString(secureString);
            Regex regex = new Regex(RegularExpression);
            if (!regex.IsMatch(content))
            {
                throw new ArgumentException(string.Format(Resources.SecureStringNotValid, RegularExpression));
            }
        }

        private string SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
