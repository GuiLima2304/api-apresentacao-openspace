using OpenSpace.Model;
using OpenSpace.BancoDados;

namespace OpenSpace.Mapping{

    public static class ApresentacaoMapping{

        public static ApresentacaoModel MapFrom(Apresentacao apresentacao)
        {
            
            return new ApresentacaoModel(){
                Id = apresentacao.Id,
                Titulo = apresentacao.Titulo,
                Descricao = apresentacao.Descricao,
                Usuario = UsuarioMapping.Mapfrom(apresentacao.Usuario)
            };


        }
    }
}