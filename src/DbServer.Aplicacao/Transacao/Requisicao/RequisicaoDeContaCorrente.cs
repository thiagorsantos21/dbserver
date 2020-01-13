using System;
using System.Collections.Generic;
using System.Text;

namespace DbServer.Aplicacao.Transacao.Requisicao
{
    public class RequisicaoDeContaCorrente
    {
        public string Agencia { get; set; }
        public string Numero { get; set; }
        public string Digito { get; set; }
    }
}
