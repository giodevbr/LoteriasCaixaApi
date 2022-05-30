namespace Loterias.Infra.Data.Model
{
    public class Municipio
    {
        public int Id { get; set; }
        public int IbgeId { get; set; }
        public string Nome { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado Estado { get; set; } 

        public Municipio(int ibgeId, string nome, int estadoId)
        {
            IbgeId = ibgeId;
            Nome = nome;
            EstadoId = estadoId;
        }
    }
}
