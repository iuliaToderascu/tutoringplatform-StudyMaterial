﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace TutoringPlatformBackEnd.StudyMaterial.Model
{
    public class StudyMaterialModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore] // Exclude id from serialization
        public ObjectId Id { get; set; }

        public string TutorId { get; set; }
        public string Title { get; set; }
        public string EducationLevel { get; set; }
        public List<string> Tags { get; set; }

        public byte[] Content { get; set; }

        public byte[] CoverImage { get; set; }
    }
}



