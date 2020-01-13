using DbServer.Dominio.Modelo;

namespace DBserve.Aplicacao.Transacao
{
    public interface IServicoMovimentacao
    {
        void Debitar(ContaCorrente contaCorrente, decimal valor);
        void Creditar(ContaCorrente contaCorrente, decimal valor);
    }
}
