using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mmjc_Vsto.models
{
    public class TemplateDto
    {
        public int Id { get; set; } // Primary Key, Auto Increment

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; } = false;

        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public int AssignmentMasterId { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string DocumentId { get; set; }

        public int DocTypeId { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        public bool Status { get; set; } = true;

        public float Version { get; set; }
    }
    public class coordinates
    {
        //  public int TemplateId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int PageNumber { get; set; }
        public int Number { get; set; }

    }

}
