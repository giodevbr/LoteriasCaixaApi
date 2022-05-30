namespace Loterias.Infra.Data.Model
{
    public class Estado
    {
        public int Id { get; set; }
        public int IbgeId { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }  

        public Estado(int ibgeId, string nome, string sigla)
        {
            IbgeId = ibgeId;
            Nome = nome;
            Sigla = sigla;
        }
    }
}
