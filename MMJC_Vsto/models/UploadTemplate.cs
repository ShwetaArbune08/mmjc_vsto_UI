using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sundeus.models
{
  public  class UploadTemplate
    {
        public int ContractTypeId { get; set; }
        public string TemplateName { get; set; }
        public string BlobPath { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public int TenantId { get; set; }

        public List<coordinates> coordinates { get; set; }

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
