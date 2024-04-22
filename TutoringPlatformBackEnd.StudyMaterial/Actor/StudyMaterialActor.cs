using TutoringPlatformBackEnd.StudyMaterial.Model;
using TutoringPlatformBackEnd.StudyMaterial.Services;

namespace TutoringPlatformBackEnd.StudyMaterial.Actor
{
    public class StudyMaterialActor : IStudyMaterialActor
    {
        private readonly IServiceProvider _serviceProvider;

        public StudyMaterialActor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<List<StudyMaterialModel>> GetAllStudyMaterialsAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var studyMaterialService = scope.ServiceProvider.GetRequiredService<IStudyMaterialService>();
                return await studyMaterialService.GetAllStudyMaterialsAsync();
            }
        }

        public async Task<StudyMaterialModel> GetStudyMaterialByIdAsync(string id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var studyMaterialService = scope.ServiceProvider.GetRequiredService<IStudyMaterialService>();
                return await studyMaterialService.GetStudyMaterialByIdAsync(id);
            }
        }

        public async Task<List<StudyMaterialModel>> GetStudyMaterialsByTutorIdAsync(string tutorId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var studyMaterialService = scope.ServiceProvider.GetRequiredService<IStudyMaterialService>();
                return await studyMaterialService.GetStudyMaterialsByTutorIdAsync(tutorId);
            }
        }

        public async Task<StudyMaterialModel> CreateStudyMaterialAsync(StudyMaterialModel studyMaterial)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var studyMaterialService = scope.ServiceProvider.GetRequiredService<IStudyMaterialService>();
                return await studyMaterialService.CreateStudyMaterialAsync(studyMaterial);
            }
        }

        public async Task UpdateStudyMaterialAsync(string id, StudyMaterialModel studyMaterial)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var studyMaterialService = scope.ServiceProvider.GetRequiredService<IStudyMaterialService>();
                await studyMaterialService.UpdateStudyMaterialAsync(id, studyMaterial);
            }
        }

        public async Task DeleteStudyMaterialAsync(string id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var studyMaterialService = scope.ServiceProvider.GetRequiredService<IStudyMaterialService>();
                await studyMaterialService.DeleteStudyMaterialAsync(id);
            }
        }
    }
}
