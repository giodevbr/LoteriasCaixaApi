namespace Loterias.Infra.Data.Model
{
    public class Municipio
    {
        public int Id { get; set; }
        public int IbgeId { get; set; }
        public string Nome { get; set; }
        public int UfId { get; set; }
        public virtual Uf Uf { get; set; }

        public Municipio(int ibgeId, string nome, int ufId)
        {
            IbgeId = ibgeId;
            Nome = nome;
            UfId = ufId;
        }
    }
}
