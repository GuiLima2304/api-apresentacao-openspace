using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenSpace.Models;

namespace ApresentacaoController.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class ApresentacaoController:Controller{
        private readonly DbOpenSpace _context;

        public ApresentacaoController(DbOpenSpace context)
        {
            _context = context;
        }

         [HttpGet]
        public ActionResult Get(){
            var teste = _context.Apresentacao.OrderBy(p => p.Id);
            return new JsonResult(teste);
        }


        [HttpGet("{id}")]
        public ActionResult Get(int id){
            
            var getApresentacao = _context.Apresentacao.Find(id);

            return Json(getApresentacao);
        }

        [HttpPost]
        public ActionResult Post(Apresentacao value){

            using(var ctx = new DbOpenSpace()){
                
                ctx.Apresentacao.Add(new Apresentacao(){
                    Titulo = value.Titulo,
                    Descricao = value.Descricao,
                    UsuarioId = value.UsuarioId
                });

                ctx.SaveChanges();
            }

            return Ok();
         
        }

        [HttpPut("{id}")]
        public ActionResult Put(Apresentacao value){

            using(var ctx = new DbOpenSpace()){

                var tituloExistente = ctx.Apresentacao.Where(p => p.Id == value.Id).FirstOrDefault();

                if(tituloExistente != null){

                    tituloExistente.Titulo = value.Titulo;
                    ctx.SaveChanges();

                }else{
                    return NotFound();
                }
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Apresentacao value){

            using(var ctx = new DbOpenSpace()){

                var apresentacao = ctx.Apresentacao.Where(p => p.Id == value.Id).FirstOrDefault();

                ctx.Apresentacao.Remove(apresentacao);
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}