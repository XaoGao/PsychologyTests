using Microsoft.Extensions.DependencyInjection;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Repositories.Contracts.GenericRepository;
using Psychology_API.Repositories.Repositories;
using Psychology_API.SeedData;
using Psychology_API.Servises.Cache;
using Psychology_API.Settings;

namespace Psychology_API.Servises.DI
{
    /// <summary>
    /// Статический класс, в котором содердиться метод расширения для IServiceCollection.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Регестрирует в сервисах все кастомные инъекции, вынесено в этот метод, чтобы не засорять startup класс.
        /// </summary>
        /// <param name="services"> IServiceCollection. </param>
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