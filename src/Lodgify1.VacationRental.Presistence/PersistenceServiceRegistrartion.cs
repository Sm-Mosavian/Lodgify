using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Lodgify1.VacationRental.Presistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lodgify1.VacationRental.Presistence
{
    public static class PersistenceServiceRegistrartion
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<VacationRentalDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("LodgifyConnectionString"));
            });
            // options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            return services;
        }
    }
}
