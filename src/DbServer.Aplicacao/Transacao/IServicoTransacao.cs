using DbServer.Aplicacao.Transacao.Requisicao;
using DBserver.Dominio;

namespace DBserve.Aplicacao.Transacao
{
    public interface IServicoTransacao
    {
        RespostaPadrao<RetornoTransacao> Efetuar(RequisicaoDeTransacao transacao);
    }
}
