using AutoMapper;
using OpenSpace.Model;
using System.Linq;

namespace OpenSpace.Profiles{

    public class AutoMapperProfile : Profile{

        public AutoMapperProfile(){

            CreateMap<BancoDados.Usuario, Model.UsuarioModel>()
            .ReverseMap();
           
            CreateMap<BancoDados.Apresentacao, Model.ApresentacaoModel>()
            .ReverseMap();


        }
    }
}