using Microsoft.AspNetCore.Mvc;
using OpenSpace.BancoDados;
using OpenSpace.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using OpenSpace.Services;

namespace OpenSpace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ApresentacaoController:Controller{
    
        private readonly ApresentacaoService _service;
        
        public ApresentacaoController(ApresentacaoService service)
        {
            _service = service;
        }


            ///<summary>
            ///Mostra todas as apresentacoes
            ///</summary>
            ///<result></result>

         [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _service.GetAll());
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }


            ///<summary>
            ///Mostra os aprovados
            ///</summary>
            ///<result></result>

        [HttpGet("aprovadas")]
        public async Task<ActionResult> GetAprovados()
        {
            try
            {
                return Ok(await _service.GetAprovadas());
            }
            catch(System.Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }


            ///<summary>
            ///Mostra os reprovados
            ///</summary>
            ///<result></result>

        [HttpGet("reprovadas")]
        public async Task<ActionResult> GetReprovadas()
        {
            try
            {
                return Ok(await _service.GetReprovadas());
            }
            catch(System.Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }


            ///<summary>
            ///Mostra uma apresentacao especifica
            ///</summary>
            ///<result></result>

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEspecifico(int id){
            
            try
            {
                
                return Ok(await _service.GetEspecifico(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }


            ///<summary>
            ///Insere uma nova apresentacao
            ///</summary>
            ///<result></result>

        [HttpPost]
        public ActionResult Post(Apresentacao value){

           try
           {
               return Ok(_service.Post(value));
           }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }


            ///<summary>
            ///Edita uma apresentacao
            ///</summary>
            ///<result></result>

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]ApresentacaoModel model)
        {
            try
            {
               
                return Ok(await _service.Put(id, model));
                
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }


            ///<summary>
            ///Edita uma aprovacao/reprovacao
            ///</summary>
            ///<result></result>

        [HttpPut("status/{id}")]
        public async Task<ActionResult> PutStatus(int id, [FromBody]ApresentacaoModel model)
        {
            try
            {
                if (model.Aprovado)
                {
                    if (model.DataApresentacao < DateTime.Now)
                        return BadRequest();
                    return Ok(await _service.Put(id, model));
                }
                else
                {
                    if (model.MotivoReprovacao == String.Empty)
                        return BadRequest();
                    return Ok(await _service.Put(id, model));
                }
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }


            ///<summary>
            ///Deleta uma apresentacao
            ///</summary>
            ///<result></result>

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Apresentacao value){

            try{
                return Ok(await _service.Delete(value));
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }
}