using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleKeepDB.Model
{
    public class Keep
    {
        [Key]
        public int keepID { get; set; }
        [Required]
        public string title { get; set; }
        public string plainText { get; set; }
        public List<Checklist> checklist { get; set; }
        public string label { get; set; }
        public bool isPinned { get; set; }
    }

    public class Checklist
    {
        public int checklistID { get; set; }
        public string item { get; set; }
    }
}
