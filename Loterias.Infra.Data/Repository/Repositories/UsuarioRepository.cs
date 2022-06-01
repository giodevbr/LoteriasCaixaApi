using Loterias.Infra.Data.Model;
using Loterias.Infra.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Loterias.Infra.Data.Repository.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObterPorEmail(string email)
        { 
            return await _context.Usuario.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> EmailEmUso(string email)
        {
            return await _context.Usuario.Where(x => x.Email == email).AnyAsync();
        }
    }
}
