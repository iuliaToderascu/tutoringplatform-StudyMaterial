using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace TutoringPlatformBackEnd.StudyMaterial.Model
{
    public class StudyMaterialModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore] // Exclude id from serialization
        public ObjectId Id { get; set; }

        public string TutorId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string EducationLevel { get; set; }

        [Required]
        public List<string> Tags { get; set; }

        [Url]
        public string CoverImageURL { get; set; } // URL for the cover image

        [Url]
        [Required]
        public string ContentURL { get; set; }     // URL for the content
    }
}



