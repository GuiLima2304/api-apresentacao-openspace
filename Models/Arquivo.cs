using System;
using System.Collections.Generic;

namespace OpenSpace.Models
{
    public partial class Arquivo
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public int ApresentacaoId { get; set; }

        public virtual Apresentacao Apresentacao { get; set; }
    }
}
