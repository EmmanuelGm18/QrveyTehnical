using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace TimeTrackingApi._0.Models
{
    public class Project
    {
        private string _rojectId;
        private string _name;
        private Time _totalSpentTime;
        private DateTime _creationDate;

        public Project()
        {

        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProjectId { get => _rojectId; set => _rojectId = value; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get => _name; set => _name = value; }

        public Time TotalSpentTime { get => _totalSpentTime; set => _totalSpentTime = value; }

        [JsonIgnore]
        public DateTime CreationDate { get => _creationDate; set => _creationDate = value; }
    }
}
