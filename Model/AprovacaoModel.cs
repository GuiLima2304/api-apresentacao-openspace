using System;

namespace OpenSpace.Model
{
  public class AprovacaoModel
  {
    public bool Aprovado { get; set; }
    public DateTime Data { get; set; }
    public string MotivoReprovacao {get;set;}
  }
}