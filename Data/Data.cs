using Microsoft.EntityFrameworkCore;
using ReceitaWSAPI.Models;

namespace ReceitaWSAPI.Data
{
    public class ReceitaWSContext : DbContext
    {
        public ReceitaWSContext(DbContextOptions<ReceitaWSContext> options)
            : base(options)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; }
    }
}
