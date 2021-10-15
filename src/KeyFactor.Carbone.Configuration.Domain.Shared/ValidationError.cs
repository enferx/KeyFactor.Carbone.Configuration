using System;
using System.Collections.Generic;
using System.Text;

namespace KeyFactor.Carbone.Configuration
{
    public class ValidationError
    {
        public string Message { get; set; }

        public IReadOnlyList<string> MemberNames { get; set; }

        public ValidationError() { }

        public ValidationError(string message, IReadOnlyList<string> memberNames)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            MemberNames = memberNames ?? throw new ArgumentNullException(nameof(memberNames));
        }
    }
}
