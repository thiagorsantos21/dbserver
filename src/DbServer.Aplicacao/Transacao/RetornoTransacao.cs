using System;
using System.Collections.Generic;
using System.Text;

namespace DBserve.Aplicacao.Transacao
{
    public class RetornoTransacao
    {
        public string Mensagem { get; set; }


        public RetornoTransacao(string mensagem) 
        {
            this.Mensagem = mensagem;
        }
    }
}
