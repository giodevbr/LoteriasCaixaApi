namespace Loterias.Infra.Data.Model
{
    public class ConcursoLocalidade
    {
        public int Id { get; set; }
        public int ConcursoId { get; set; }
        public virtual Concurso Concurso { get; set; }
        public int MunicipioId { get; set; }
        public virtual Municipio Municipio { get; set; }
    }
}
