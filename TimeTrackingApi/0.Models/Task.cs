using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace TimeTrackingApi._0.Models
{
    public class Task
    {
        private string _taksId;
        private string _title;
        private Time _originalStimate;
        private ICollection<SpentTimeRegistry> _spentTime;
        private DateTime _creationDate;

        public Task()
        {
            _spentTime = new List<SpentTimeRegistry>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TaskId { get => _taksId; set => _taksId = value; }

        [MaxLength(50)]
        public string Title { get => _title; set => _title = value; }

        [EnumDataType(typeof(Status))]
        public Status TaskStatus { get; set; }

        [JsonIgnore]
        public string ProjectId { get; set; }

        [JsonIgnore]
        public string AsignedUserId { get; set; }

        [Required]
        public Time OriginalStimate { get => _originalStimate; set => _originalStimate = value; }

        public ICollection<SpentTimeRegistry> SpentTime { get => _spentTime; set => _spentTime = value; }

        [JsonIgnore]
        public DateTime CreationDate { get => _creationDate; set => _creationDate = value; }
    }

    public enum Status
    {
        Created,
        InProgress,
        Closed,        
    }
}
