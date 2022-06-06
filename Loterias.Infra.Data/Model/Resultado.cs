namespace Loterias.Infra.Data.Model
{
    public class Resultado
    {
        public int Id { get; set; }
        public int ConcursoId { get; set; }
        public virtual Concurso Concurso { get; set; }
        public virtual List<ResultadoNumeroSorteado> NumeroSorteado { get; set; }
    }
}
