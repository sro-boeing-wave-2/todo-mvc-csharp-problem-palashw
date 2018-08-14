using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GoogleKeepDB.Model
{
    public class Keep
    {
        public ObjectId Id { get; set; }
        [BsonElement("keepID")]
        public int keepID { get; set; }
        [Required]
        [BsonElement("title")]
        public string title { get; set; }
        [BsonElement("plainText")]
        public string plainText { get; set; }
        [BsonElement("checklist")]
        public List<Checklist> checklist { get; set; }
        [BsonElement("label")]
        public string label { get; set; }
        [BsonElement("isPinned")]
        public bool isPinned { get; set; }
    }

    public class Checklist
    {
        public int checklistID { get; set; }
        public string item { get; set; }
    }
}
