using System.Collections.Generic;

namespace Finances.Domain.Entity
{
    public class ValidationResult
    {
        public ValidationResult(IList<ValidationFailure> errors)
        {
            Errors = errors;
        }

        public bool IsValid 
        {
            get { return Errors == null || Errors.Count == 0; }
        }

        public IList<ValidationFailure> Errors { get; private set; }
    }
}