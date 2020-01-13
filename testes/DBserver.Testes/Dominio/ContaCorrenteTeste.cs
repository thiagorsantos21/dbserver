using DbServer.Dominio.Modelo;
using Xunit;
namespace DBserver.Teste.Dominio
{
    public class ContaCorrenteTeste
    {

        [Fact]
        public void Deve_Retornar_Sucesso_Ao_Creditar_Valor() 
        {
            var contaCorrente = new ContaCorrente("0001", "123456", "7")
            {
                Saldo = 100
            };

            contaCorrente.Creditar(50);

            Assert.Equal(150, contaCorrente.Saldo);
        
        }

        [Fact]
        public void Deve_Retornar_Sucesso_Ao_Debitar_Valor()
        {
            var contaCorrente = new ContaCorrente("0001", "123456", "7")
            {
                Saldo = 100
            };

            contaCorrente.Debitar(63.28M);

            Assert.Equal(36.72M, contaCorrente.Saldo);

        }

        [Fact]
        public void Deve_Retornar_Sucesso_Ao_Obter_Id()
        {
            var contaCorrente = new ContaCorrente("0001", "123456", "7")
            {
                Id = System.Guid.NewGuid(),
                Saldo = 100
            };

            Assert.True(contaCorrente.ContaValida());
        }

        [Fact]
        public void Deve_Retornar_Sucesso_Ao_Obter_Id_Vazio()
        {
            var contaCorrente = new ContaCorrente("0001", "123456", "7")
            {
                Saldo = 100
            };

            Assert.False(contaCorrente.ContaValida());
        }
    }
}
