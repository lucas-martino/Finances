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
            Assert.Equal(2018, vigencia.Ano());
            Assert.Equal(11, vigencia.Mes());
        }
    }
}