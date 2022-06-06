namespace Loterias.Infra.Data.Model
{
    public class Premiacao
    {
        public int Id { get; set; }

        public int ConcursoId { get; set; }
        public virtual Concurso Concurso { get; set; }  
        public int Pontos { get; set; }
        public int Ganhadores { get; set; }
        public decimal ValorPagoPorGanhador { get; set; }
        public decimal ValorPagoTotal { get; set; }
    }
}
