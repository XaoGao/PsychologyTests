using Microsoft.Extensions.DependencyInjection;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Repositories.Contracts.GenericRepository;
using Psychology_API.Repositories.Repositories;
using Psychology_API.SeedData;
using Psychology_API.Servises.Cache;
using Psychology_API.Settings;

namespace Psychology_API.Servises.DI
{
    // TODO: Вынести сюда все зависимости из класса startup
    public static class DependencyInjection
    {
        public static void AddAllServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPhonebookRepository, PhonebookRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSingleton<CacheSettings>();
            services.AddSingleton(typeof(ICache<>), typeof(Cache<>));
            services.AddTransient<SeedAllData>();
        }
    }
}