using Finances.Domain.Entity.Validator;

namespace Finances.Domain.Entity
{
    public class Usuario : FinancesDomainEntity
    {
        public Usuario()
        {
            Validator = new UsuarioValidator();
        }

        public string Nome { get; set; }
        private string login;
        public string Login
        {
            get
            {
                if (!string.IsNullOrEmpty(login))
                    return login.ToLower();

                return "";
            }
            set
            {
                if (!string.IsNullOrEmpty(login))
                    login = value.ToUpper();
                else
                    login = value;
            }
        }
        public string Senha { get; set; }
    }
}