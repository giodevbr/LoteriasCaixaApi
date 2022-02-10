namespace Loterias.Infra.Data.Rest.Caixa.Dtos
{
    public class LotoFacilMunicipioDto
    {
        public LotoFacilMunicipioDto(string municipio, string uf)
        {
            Municipio = municipio; 
            Uf = uf;
        }

        public string Municipio { get; set; }
        public string Uf { get; set; }
    }
}
