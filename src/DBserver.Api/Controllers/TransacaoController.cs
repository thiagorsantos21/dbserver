using System.Web.Http.Description;
using DBserve.Aplicacao.Transacao;
using DbServer.Aplicacao.Transacao.Requisicao;
using DBserver.Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBserver.Api.Controllers
{
    [Route("api/transacao")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly IServicoTransacao _servico;

        public TransacaoController(IServicoTransacao servico)
        {
            _servico = servico;
        }

       
        [HttpPost]
        [ResponseType(typeof(RequisicaoDeTransacao))]
        [ProducesResponseType(200, Type = typeof(RetornoTransacao))]
        [ProducesResponseType(400, Type = typeof(RespostaErroPadrao))]
        [ProducesResponseType(401, Type = typeof(RespostaErroPadrao))]
        [ProducesResponseType(403, Type = typeof(RespostaErroPadrao))]
        public ActionResult Post (RequisicaoDeTransacao requisicao)
        {
            var resultado = _servico.Efetuar(requisicao);

            if (!resultado.Sucesso)
            {
                return StatusCode(resultado.Erro.StatusCode, new RespostaErroPadrao(resultado.Erro));
            }

            return Ok(resultado.Objeto);
        }


       

    }

}