using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace TimeTrackingApi._0.Models
{
    public class SpentTimeRegistry
    {
        private string _userId;
        private Time _spentTime;
        private string _idTimeSpentRegistry;
        private string _taskId;
        private DateTime _creationDate;
        private string detail;

        public SpentTimeRegistry()
        {

        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TimeSpentRegistryId { get => _idTimeSpentRegistry; set => _idTimeSpentRegistry = value; }

        [Required]
        public Time SpentTime { get => _spentTime; set => _spentTime = value; }

        [Required]
        public string Detail { get => detail; set => detail = value; }

        [JsonIgnore]
        public string TaskId { get => _taskId; set => _taskId = value; }

        [JsonIgnore]
        public string UserId { get => _userId; set => _userId = value; }

        [JsonIgnore]
        public DateTime CreationDate { get => _creationDate; set => _creationDate = value; }


    }
}
