using Finances.Domain.Entity;
using Xunit;

namespace Test.Finances.Domain.Entity
{
    public class VigenciaTest
    {
        [Fact]
        public void UsuarioSenhaCorretoRetornaUsuario()
        {
            //Given
            Vigencia vigencia = new Vigencia();

            //When
            vigencia.Referencia = 201811;

            //Then
            Assert.Equal(vigencia.Ano(), 2018);
            Assert.Equal(vigencia.Mes(), 11);
        }
    }
}