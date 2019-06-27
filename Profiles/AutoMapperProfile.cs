using AutoMapper;
using System.Linq;

namespace OpenSpace.Profiles{

    public class AutoMapperProfile : AutoMapper.Profile{

        public AutoMapperProfile(){

            CreateMap<BancoDados.Apresentacao, Model.ApresentacaoModel>();

        }


    }
}