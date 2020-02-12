using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.DataServices
{
    public class AnamnesisService : IAnamnesisService
    {
        private readonly IAnamnesisRepository _anamnesisRepository;

        public AnamnesisService(IAnamnesisRepository anamnesisRepository)
        {
            _anamnesisRepository = anamnesisRepository;
        }
        public async Task<Anamnesis> CreateAnamnesisAsync(int doctorId, int patientId, Anamnesis anamnesis)
        {
            return await _anamnesisRepository.CreateAnamnesisRepositoryAsync(doctorId, patientId, anamnesis);
        }

        public async Task<IEnumerable<Anamnesis>> GetAnamnesesAsync(int patientId)
        {
            return await _anamnesisRepository.GetAnamnesesRepositoryAsync(patientId);
        }
    }
}