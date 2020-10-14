using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace TimeTrackingApi._0.Models
{
    public class User
    {
        private string _id;
        private string _name;
        private string _userName;
        private string _surName;

        public User()
        {
            _id = string.Empty;
            _name = string.Empty;
            _userName = string.Empty;
            _surName = string.Empty;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get => _id; set => _id = value; }

        [Required]
        [StringLength(10, MinimumLength = 4)]
        public string UserName { get => _userName; set => _userName = value; }

        [StringLength(20, MinimumLength = 1)]
        [Required]
        public string Name { get => _name; set => _name = value; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string SurName { get => _surName; set => _surName = value; }

        [JsonIgnore]
        public DateTime CreationDate { get; set; }

    }
}
