using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRentMessageBrokerConsumer1.Infrastructure.Persistence
{
    public static class DatabaseInitializer
    {
        public static void MigrateDatabase<T>(T context) where T : DbContext
        {
            try
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    Console.WriteLine("Aplicando migrations...");
                    context.Database.Migrate();
                    Console.WriteLine("Migrations aplicadas com sucesso.");
                }                    
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao aplicar migrations: " + ex.Message);
                // Logar o erro ou tomar outras ações necessárias
            }
        }
    }
}
