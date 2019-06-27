using OpenSpace.BancoDados;

namespace OpenSpace.Model{

    public class ApresentacaoModel{

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public UsuarioModel Usuario { get; set; }

    }


}