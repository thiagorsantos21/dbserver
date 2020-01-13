using System.Collections.Generic;

namespace DBserver.Dominio
{
    public class RespostaErroPadrao
    {
        public List<string> Erros { get; set; }

        public RespostaErroPadrao(RespostaErro respostaErro) 
        {
            this.Erros = respostaErro.Mensagens;
        }
    }
}
