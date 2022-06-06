namespace Loterias.Infra.Data.Model
{
    public class ResultadoNumeroSorteado
    {
        public int Id { get; set; }
        public int ResultadoId { get; set; }
        public virtual Resultado Resultado { get; set; }
        public int Numero { get; set; }
    }
}
