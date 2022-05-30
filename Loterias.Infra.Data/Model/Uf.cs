namespace Loterias.Infra.Data.Model
{
    public class Uf
    {
        public int Id { get; set; }
        public int IbgeId { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }  

        public Uf(int ibgeId, string nome, string sigla)
        {
            IbgeId = ibgeId;
            Nome = nome;
            Sigla = sigla;
        }
    }
}
