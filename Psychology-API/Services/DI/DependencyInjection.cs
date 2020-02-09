using Microsoft.Extensions.DependencyInjection;
using Psychology_API.DataServices.Contracts;
using Psychology_API.DataServices.DataServices;
using Psychology_API.Helpers;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Repositories.Contracts.GenericRepository;
using Psychology_API.Repositories.Repositories;
using Psychology_API.SeedData;
using Psychology_API.Servises.Cache;
using Psychology_API.Servises.ComputedHash;
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
            // Repository 
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IPhonebookRepository, PhonebookRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IReceptionRepository, ReceptionRepository>();
            services.AddScoped<IVacationRepository, VacationRepository>();
            services.AddScoped<ILoggerRepository, LoggerRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // DataServices
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IAuthService, AuthService>();
            //
            services.AddSingleton<CacheSettings>();
            services.AddSingleton<RabbitMQSettings>();
            services.AddSingleton(typeof(ICache<>), typeof(Cache<>));
            services.AddTransient<SeedAllData>();
            services.AddScoped<IHash, HashHmac>();
        }
    }
}