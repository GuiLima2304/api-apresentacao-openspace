using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenSpace.Mapping;
using OpenSpace.BancoDados;
using AutoMapper;
using OpenSpace.Model;
using OpenSpace.Base;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using System.Diagnostics;

namespace OpenSpace.Services
{


    public class ApresentacaoService{
        private readonly DbOpenSpace _context;
        private readonly IMapper _mapper;

        public ApresentacaoService(DbOpenSpace context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<GenericResponse<List<ApresentacaoModel>>> GetAll(){
            
            IEnumerable<Apresentacao> apresentacoes = await _context
            .Apresentacao
            .Include(a => a.Usuario)
            .OrderBy(p => p.Titulo)
            .ToListAsync();
            

            IEnumerable<ApresentacaoModel> viewModelApresentacao = apresentacoes
            .Select(x => _mapper.Map<ApresentacaoModel>(x));

            GenericResponse<List<ApresentacaoModel>> response = new GenericResponse<List<ApresentacaoModel>>(viewModelApresentacao.ToList());
            return response;
        }

        public async Task<GenericResponse<List<ApresentacaoModel>>> GetAprovadas()
        {
            List<Apresentacao> apresentacoesAprovadas = await _context.Apresentacao
            .Include(p => p.Usuario)
            .Where(p => p.Aprovado)
            .ToListAsync();

            List<ApresentacaoModel> apresentacoes = apresentacoesAprovadas
            .Select(_mapper.Map<ApresentacaoModel>)
            .ToList();

            GenericResponse<List<ApresentacaoModel>> response = new GenericResponse<List<ApresentacaoModel>>(apresentacoes);

            return response;
        }


        public async Task<GenericResponse<List<ApresentacaoModel>>> GetReprovadas()
        {
            List<Apresentacao> apresentacoesReprovadas = await _context.Apresentacao
            .Include(p => p.Usuario)
            .Where(p => !p.Aprovado)
            .ToListAsync();

            List<ApresentacaoModel> apresentacoes = apresentacoesReprovadas
            .Select(_mapper.Map<ApresentacaoModel>)
            .ToList();

            GenericResponse<List<ApresentacaoModel>> response = new GenericResponse<List<ApresentacaoModel>>(apresentacoes);

            return response;
        }

        public async Task<GenericResponse<ApresentacaoModel>> GetEspecifico(int id){
            
            var iDEncontrado = await _context.Apresentacao
            .Include(p => p.Usuario)
            .FirstOrDefaultAsync(p => p.Id == id);

            var IdMapeado = _mapper.Map<ApresentacaoModel>(iDEncontrado);
            
            GenericResponse<ApresentacaoModel> response = new GenericResponse<ApresentacaoModel>(IdMapeado);

            return response;
        }

        
        public async Task<GenericResponse<ApresentacaoModel>> Post(Apresentacao value){

            
                var ctx = new DbOpenSpace();

                ctx.Apresentacao.Add(new Apresentacao(){
                    Titulo = value.Titulo,
                    Descricao = value.Descricao,
                    UsuarioId = value.UsuarioId,
                    Aprovado = value.Aprovado,
                    MotivoReprovacao = value.MotivoReprovacao,
                    DataApresentacao = value.DataApresentacao
                });

                await ctx.SaveChangesAsync();

                var NewApresentacao = _mapper.Map<ApresentacaoModel>(ctx.Apresentacao);
                GenericResponse<ApresentacaoModel> response = new GenericResponse<ApresentacaoModel>(NewApresentacao);

            return response;
         
        }


        public async Task<GenericResponse<ApresentacaoModel>> Put(int id, ApresentacaoModel model){

            
                var ctx = new DbOpenSpace();
                var IdEncontrado = ctx.Apresentacao.Where(p => p.Id == model.Id).FirstOrDefault();


                    IdEncontrado.Titulo = model.Titulo;
                    IdEncontrado.Descricao = model.Descricao;
                    IdEncontrado.UsuarioId = model.Usuario.Id;
            
                    await ctx.SaveChangesAsync();

                    var EntidadeAlterado = _mapper.Map<ApresentacaoModel>(IdEncontrado);
                    GenericResponse<ApresentacaoModel> response = new GenericResponse<ApresentacaoModel>(EntidadeAlterado);
                    return  response;
                
        }

        public async Task<GenericResponse<ApresentacaoModel>> PutStatus(int id, ApresentacaoModel model){

            var ctx = new DbOpenSpace();
            var IdProcurado = ctx.Apresentacao.Where(p => p.Id == model.Id).FirstOrDefault();

            IdProcurado.Aprovado = model.Aprovado;
            IdProcurado.MotivoReprovacao = model.MotivoReprovacao;
            IdProcurado.DataApresentacao = model.DataApresentacao;

            await ctx.SaveChangesAsync();
            var Alterado = _mapper.Map<ApresentacaoModel>(IdProcurado);
            GenericResponse<ApresentacaoModel> response = new GenericResponse<ApresentacaoModel>(Alterado);
            return response;
        }

    
        public async Task<string> Delete(Apresentacao value){

                var apresentacao = _context.Apresentacao.Where(p => p.Id == value.Id).FirstOrDefault();

                _context.Apresentacao.Remove(apresentacao);
                await _context.SaveChangesAsync();

            return  "Entidade deletada com sucesso";
        }
    }
}