using TutoringPlatformBackEnd.StudyMaterial.Model;

namespace TutoringPlatformBackEnd.StudyMaterial.Services
{
    public interface IStudyMaterialService
    {
        Task<List<StudyMaterialModel>> GetAllStudyMaterialsAsync();
        Task<StudyMaterialModel> GetStudyMaterialByIdAsync(string id);
        Task<List<StudyMaterialModel>> GetStudyMaterialsByTutorIdAsync(string tutorId);
        Task<StudyMaterialModel> CreateStudyMaterialAsync(StudyMaterialModel studyMaterial);
        Task UpdateStudyMaterialAsync(string id, StudyMaterialModel studyMaterial);
        Task DeleteStudyMaterialAsync(string id);
    }
}
