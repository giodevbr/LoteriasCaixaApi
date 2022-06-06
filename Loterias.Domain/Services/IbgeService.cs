using Loterias.Core.Dtos;
using Loterias.Core.Interfaces;
using Loterias.Domain.Interfaces;
using Loterias.Infra.Data.Model;
using Loterias.Infra.Data.Repository.Interfaces;
using Loterias.Infra.Data.Rest.Ibge.Dtos;
using Loterias.Infra.Data.Rest.Ibge.Interfaces;
using Loterias.Util.Resources;

namespace Loterias.Domain.Services
{
    public class IbgeService : IIbgeService
    {
        private readonly IDomainNotification _notificacaoDeDominio;
        private readonly IIbgeApiService _ibgeConsultaService;
        private readonly IMunicipioRepository _municipioRepository;
        private readonly IUfRepository _estadoRepository;

        public IbgeService(IDomainNotification notificacaoDeDominio,
                           IIbgeApiService ibgeConsultaService,
                           IMunicipioRepository municipioRepository,
                           IUfRepository estadoRepository)
        {
            _notificacaoDeDominio = notificacaoDeDominio;
            _ibgeConsultaService = ibgeConsultaService;
            _municipioRepository = municipioRepository;
            _estadoRepository = estadoRepository;
        }

        public async Task ExecutarCargaDeUf()
        {
            if (await ExisteRegistroDeUf())
                _notificacaoDeDominio.AddNotification(StringResources.ExisteUfs);

            if (await ExisteRegistroDeMunicipio())
                _notificacaoDeDominio.AddNotification(StringResources.ExisteMunicipios);

            if (!_notificacaoDeDominio.HasNotifications())
            {
                var ufs = await _ibgeConsultaService.ConsultarUfs();

                foreach (var uf in ufs)
                {
                    var novaUf = InstanciarUf(uf);

                    await _estadoRepository.AddAsync(novaUf);
                }
            }
        }

        public async Task ExecutarCargaDeMunicipio()
        {
            if (!await ExisteRegistroDeUf())
                _notificacaoDeDominio.AddNotification(StringResources.NaoExisteUfs);

            if (await ExisteRegistroDeMunicipio())
                _notificacaoDeDominio.AddNotification(StringResources.ExisteMunicipios);

            if (!_notificacaoDeDominio.HasNotifications())
            {
                var ufs = await _estadoRepository.GetAllAsync();

                foreach (var uf in ufs)
                {
                    var municipiosDaUf = await _ibgeConsultaService.ConsultarMunicipiosPorUf(uf.IbgeId);

                    foreach (var municipio in municipiosDaUf)
                    {
                        var novoMunicipio = InstanciarMunicipio(municipio, uf.Id);

                        await _municipioRepository.AddAsync(novoMunicipio);
                    }
                }
            }
        }

        private static Uf InstanciarUf(UfDto ufDto)
        {
            ufDto.Nome = ufDto.Nome.ToUpper();

            return new Uf(ufDto.Id, ufDto.Nome, ufDto.Sigla);
        }

        private static Municipio InstanciarMunicipio(MunicipioDto municipioDto, int ufId)
        {
            municipioDto.Nome = municipioDto.Nome.ToUpper();

            return new Municipio(municipioDto.Id, municipioDto.Nome, ufId);
        }

        private async Task<bool> ExisteRegistroDeUf()
        {
            return await _estadoRepository.AnyAsync();
        }

        private async Task<bool> ExisteRegistroDeMunicipio()
        {
            return await _municipioRepository.AnyAsync();
        }
    }
}