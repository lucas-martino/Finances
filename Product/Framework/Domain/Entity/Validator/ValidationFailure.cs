namespace Framework.Domain.Entity.Validator
{
    public class ValidationFailure
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        public Severity Severity { get; set; }
    }
}