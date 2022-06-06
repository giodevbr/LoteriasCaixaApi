using Loterias.Core.Enums;

namespace Loterias.Infra.Data.Model
{
    public class ConcursoDadosBruto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public TipoConcurso TipoConcurso { get; set; }
        public string ConcursoPlanilha { get; set; }
        public string? ConcursoApi { get; set; }
        public bool Processado { get; set; }
    }
}
