using DbServer.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbServer.Aplicacao.Repositorios.Interfaces
{
    public interface IContaCorrenteRepositorio
    {
        ContaCorrente Obter(ContaCorrente contaCorrente);
        void AtualizarSaldo(ContaCorrente contaCorrente);

    }
}
