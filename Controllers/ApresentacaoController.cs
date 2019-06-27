using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenSpace.Mapping;
using OpenSpace.BancoDados;
using AutoMapper;
using OpenSpace.Model;



namespace ApresentacaoController.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class ApresentacaoController:Controller{
        private readonly DbOpenSpace _context;
        private readonly IMapper mapper;

        public ApresentacaoController(DbOpenSpace context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

         [HttpGet]
        public ActionResult Get(){
            IEnumerable<Apresentacao> apresentacoes = _context
            .Apresentacao
            .Include(a => a.Usuario)
            .OrderBy(p => p.Titulo)
            .ToList();

            IEnumerable<ApresentacaoModel> viewModelApresentacao = apresentacoes
            .Select(x => mapper.Map<ApresentacaoModel>(x));

            return Json(viewModelApresentacao);
        }


        [HttpGet("{id}")]
        public ActionResult Get(int id){
            
            var getApresentacao = _context.Apresentacao.Find(id);

            return Ok(getApresentacao);
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
                    tituloExistente.Descricao = value.Descricao;
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