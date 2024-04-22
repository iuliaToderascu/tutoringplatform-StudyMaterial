
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using TutoringPlatformBackEnd.StudyMaterial.Model;
using TutoringPlatformBackEnd.StudyMaterial.Services;

namespace TutoringPlatformBackEnd.StudyMaterial.Services
{
    public class StudyMaterialService : IStudyMaterialService
    {
        private readonly IMongoCollection<StudyMaterialModel> _studyMaterialCollection;

        public StudyMaterialService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("StudyMaterial");
            _studyMaterialCollection = database.GetCollection<StudyMaterialModel>("TutoringPlatform");
        }

        public async Task<List<StudyMaterialModel>> GetAllStudyMaterialsAsync()
        {
            return await _studyMaterialCollection.Find(_ => true).ToListAsync();
        }

        public async Task<StudyMaterialModel> GetStudyMaterialByIdAsync(string id)
        {
            return await _studyMaterialCollection.Find(s => s.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<List<StudyMaterialModel>> GetStudyMaterialsByTutorIdAsync(string tutorId)
        {
            return await _studyMaterialCollection.Find(s => s.TutorId == tutorId).ToListAsync();
        }

        public async Task<StudyMaterialModel> CreateStudyMaterialAsync(StudyMaterialModel studyMaterial)
        {
            await _studyMaterialCollection.InsertOneAsync(studyMaterial);
            return studyMaterial;
        }

        public async Task UpdateStudyMaterialAsync(string id, StudyMaterialModel studyMaterial)
        {
            var objectId = ObjectId.Parse(id);
            await _studyMaterialCollection.ReplaceOneAsync(s => s.Id == objectId, studyMaterial);
        }

        public async Task DeleteStudyMaterialAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            await _studyMaterialCollection.DeleteOneAsync(s => s.Id == objectId);
        }
    }
}
