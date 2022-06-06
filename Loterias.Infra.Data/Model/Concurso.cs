using Loterias.Core.Enums;

namespace Loterias.Infra.Data.Model
{
    public class Concurso
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public TipoConcurso TipoConcurso { get; set; }
        public DateTime DataSorteio { get; set; }
        public StatusConcurso StatusConcurso { get; set; }
        public Decimal ValorArrecadado { get; set; }
        public Decimal ValorAcumulado { get; set; }
        public Decimal ValorSorteado { get; set; }
        public virtual List<Premiacao> Premiacao { get; set; }
        public virtual Resultado Resultado { get; set; }
        public virtual List<PremiacaoLocalidade> PremiacaoLocalidade { get; set; }
        public virtual ConcursoLocalidade ConcursoLocalidade { get; set; }
    }
}
