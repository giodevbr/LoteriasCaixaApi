using Loterias.Infra.Data.Model;
using Loterias.Infra.Data.Repository.Interfaces;

namespace Loterias.Infra.Data.Repository.Repositories
{
    public class MunicipioRepository : BaseRepository<Municipio>, IMunicipioRepository
    {
        public MunicipioRepository(AppDbContext context) : base(context)
        {
        }
    }
}
