using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sundeus.models
{
   public class Clauses
    {

        public int ClauseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Language_Id { get; set; }
        public string Text { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsTracked { get; set; }
        public bool IsDeleted { get; set; }
        public int ContractTypeId { get; set; }
        public int TemplateId { get; set; }
        public string ContractTypeName { get; set; }
        public string TemplateName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
