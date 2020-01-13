using Xunit;
using Moq;
using System;
using DBserve.Aplicacao.Transacao;
using DbServer.Aplicacao.Repositorios.Interfaces;
using DbServer.Aplicacao.Transacao.Requisicao;
using DbServer.Dominio.Modelo;

namespace DBserver.Teste.Aplicacao
{
    public class ServicoTransacaoTeste
    {
        private ServicoTransacao _servicoTransacao;
        private Mock<IContaCorrenteRepositorio> _contaCorrenteRepositorio;
        private ServicoMovimentacao _movimentacao;

        public ServicoTransacaoTeste()
        {
            _contaCorrenteRepositorio = new Mock<IContaCorrenteRepositorio>();
            _movimentacao = new ServicoMovimentacao(_contaCorrenteRepositorio.Object);
            _servicoTransacao = new ServicoTransacao(_contaCorrenteRepositorio.Object, _movimentacao);

        }

        [Fact]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Parametro_Valor_For_Zero()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao();
            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }


        [Fact]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Parametro_ContaCorrente_Origem_E_Destino_Forem_Nulas()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M
            };
            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }

        [Fact]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Parametro_ContaCorrente_Origem_For_Nula()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Destino = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456", Digito = "7" },

            };
            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }

        [Fact]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Parametro_ContaCorrente_Destino_For_Nula()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Origem = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456", Digito = "7" },
            };
            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }

        [Fact]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Parametro_ContaCorrente_Origem_E_Destino_Forem_Vazios()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Origem = new RequisicaoDeContaCorrente(),
                Destino = new RequisicaoDeContaCorrente()
            };
            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }

        [Fact]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Parametro_ContaCorrente_Origem_E_Destino_Nao_Possuirem_Numero_Valido()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Origem = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "A123456", Digito = "7" },
                Destino = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456Z", Digito = "7" }
            };
            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }

        [Fact]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Parametro_ContaCorrente_Origem_Nao_Possuir_Numero_Valido()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Origem = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "A123456", Digito = "7" },
                Destino = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456", Digito = "7" }
            };
            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }

        [Fact]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Parametro_ContaCorrente_Destino_Nao_Possuir_Numero_Valido()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Origem = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456", Digito = "7" },
                Destino = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456Z", Digito = "7" }
            };
            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }

        [Fact]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Nao_Encontar_ContaCorrente_Origem()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Origem = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456", Digito = "7" },
                Destino = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "654321", Digito = "0" }
            };

            
            _contaCorrenteRepositorio.Setup(x => x.Obter(It.Is<ContaCorrente>(x=> x.Numero == "123456"))).Returns((ContaCorrente)null);
            _contaCorrenteRepositorio.Setup(x => x.Obter(It.Is<ContaCorrente>(x=> x.Numero == "654321"))).Returns(new ContaCorrente("0001","654321","0") { Id = Guid.NewGuid(), Saldo = 200M });

            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }


        [Fact]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Nao_Encontar_ContaCorrente_Destino()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Origem = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456", Digito = "7" },
                Destino = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "654321", Digito = "0" }
            };


            _contaCorrenteRepositorio.Setup(x => x.Obter(It.Is<ContaCorrente>(x => x.Numero == "123456"))).Returns(new ContaCorrente("0001", "123456", "7") { Id = Guid.NewGuid(), Saldo = 500M });
            _contaCorrenteRepositorio.Setup(x => x.Obter(It.Is<ContaCorrente>(x => x.Numero == "654321"))).Returns((ContaCorrente)null);

            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }

        [Fact]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Der_Alguma_Exececao_AO_Persistir_Saldo()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Origem = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456", Digito = "7" },
                Destino = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "654321", Digito = "0" }
            };


            _contaCorrenteRepositorio.Setup(x => x.Obter(It.Is<ContaCorrente>(x => x.Numero == "123456"))).Returns(new ContaCorrente("0001", "123456", "7") { Id = Guid.NewGuid(), Saldo = 500M });
            _contaCorrenteRepositorio.Setup(x => x.Obter(It.Is<ContaCorrente>(x => x.Numero == "654321"))).Returns(new ContaCorrente("0001", "654321", "0") { Id = Guid.NewGuid(), Saldo = 200M });

            _contaCorrenteRepositorio.Setup(x => x.AtualizarSaldo(It.IsAny<ContaCorrente>())).Throws(new Exception());


            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }

        [Fact]
        public void Deve_Retornar_Sucesso_Como_Verdadeiro_Efetuar_Transacao()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Origem = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456", Digito = "7" },
                Destino = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "654321", Digito = "0" }
            };


            _contaCorrenteRepositorio.Setup(x => x.Obter(It.Is<ContaCorrente>(x => x.Numero == "123456"))).Returns(new ContaCorrente("0001", "123456", "7") { Id = Guid.NewGuid(), Saldo = 500M });
            _contaCorrenteRepositorio.Setup(x => x.Obter(It.Is<ContaCorrente>(x => x.Numero == "654321"))).Returns(new ContaCorrente("0001", "654321", "0") { Id = Guid.NewGuid(), Saldo = 200M });

            _contaCorrenteRepositorio.Setup(x => x.AtualizarSaldo(It.Is<ContaCorrente>(x => x.Numero == "123456")));
            _contaCorrenteRepositorio.Setup(x => x.AtualizarSaldo(It.Is<ContaCorrente>(x => x.Numero == "654321")));

            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.True(response.Sucesso);
        }
    }
}
