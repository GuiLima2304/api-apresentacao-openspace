using System;
using OpenSpace.Model;
using OpenSpace.BancoDados;

namespace OpenSpace.Mapping{

    public static class UsuarioMapping{

        public static UsuarioModel Mapfrom(Usuario usuario){
            
            return new UsuarioModel(){
               Id =  usuario.Id,
               Nome = usuario.Nome
            };
        }
    }
}