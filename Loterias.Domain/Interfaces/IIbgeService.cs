namespace Loterias.Domain.Interfaces
{
    public interface IIbgeService
    {
        Task ExecutarCargaDeUf();

        Task ExecutarCargaDeMunicipio();
    }
}
