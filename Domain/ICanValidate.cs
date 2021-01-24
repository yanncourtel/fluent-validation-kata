using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidationKata.Domain
{
    public interface ICanValidate
    {
        public ValidationResult Validate();
    }
}
