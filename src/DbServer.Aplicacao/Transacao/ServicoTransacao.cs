using DbServer.Aplicacao.Repositorios.Interfaces;
using DbServer.Aplicacao.Transacao.Requisicao;
using DbServer.Aplicacao.Transacao.Validacao;
using DbServer.Dominio.Excecoes;
using DbServer.Dominio.Modelo;
using DBserver.Dominio;
using FluentValidation.Results;
using System;

namespace DBserve.Aplicacao.Transacao
{
    public class ServicoTransacao : IServicoTransacao
    {
        private readonly IContaCorrenteRepositorio _contaCorrenteRepositorio;
        private readonly IServicoMovimentacao _movimentacao;
        

        public ServicoTransacao(IContaCorrenteRepositorio contaCorrenteRepositorio, IServicoMovimentacao movimentacao) 
        {
            _contaCorrenteRepositorio = contaCorrenteRepositorio;
            _movimentacao = movimentacao;
        }

        public RespostaPadrao<RetornoTransacao> Efetuar (RequisicaoDeTransacao transacao)
        {
            RespostaPadrao<RetornoTransacao> resultado = new RespostaPadrao<RetornoTransacao>();
            try
            {
                var validacao = ValidarObjetoDeRequisicao(transacao);
                if (!validacao.IsValid)
                {
                    resultado.ConfigurarResposta(validacao);
                }
                else
                {
                    var contaOrigem = new ContaCorrente(transacao.Origem.Agencia,transacao.Origem.Numero,transacao.Origem.Digito);
                    var contaDestino = new ContaCorrente(transacao.Destino.Agencia, transacao.Destino.Numero, transacao.Destino.Digito);

                    contaOrigem = ValidarContaCorrente(contaOrigem,"origem");
                    contaDestino = ValidarContaCorrente(contaDestino,"destino");

                    _movimentacao.Debitar(contaOrigem,transacao.Valor);
                    _movimentacao.Creditar(contaDestino, transacao.Valor);

                    resultado.ConfigurarResposta(true,new RetornoTransacao("Transação concluída com sucesso."));
                }
            }
            catch (ExcecaoDeDominio ex)
            {
                resultado.ConfigurarResposta(ex);
            }
            catch (Exception ex)
            {
                resultado.ConfigurarResposta(ex);
            }

            return resultado;
        }

        private ValidationResult ValidarObjetoDeRequisicao(RequisicaoDeTransacao transacao)
        {
            var validador = new ValidadorRequisicaoDeTransacao();
            var validacao = validador.Validate(transacao);

            return validacao;
        }

        private ContaCorrente ValidarContaCorrente(ContaCorrente contaCorrente, string tipoConta = "origem") 
        {
            var resultado = _contaCorrenteRepositorio.Obter(contaCorrente);

            if (resultado == null)
            {
                throw new ExcecaoDeDominio($"conta de {tipoConta} é inválida");
            }

            return resultado;
        }

    }
}
