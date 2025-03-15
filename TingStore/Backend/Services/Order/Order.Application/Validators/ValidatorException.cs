using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Order.Application.Validators
{
    public class ValidatorException : Exception
    {
        public IDictionary<string, string[]> Error { get;  }
        public ValidatorException()
        {
            Error = new Dictionary<string, string[]>();
        }

        public ValidatorException(IEnumerable<ValidationFailure> failure)
        {
            Error = failure.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
    }
}
