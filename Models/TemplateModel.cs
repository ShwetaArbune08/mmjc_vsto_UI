using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmjc_Vsto.models
{
    public class TemplateModel
    {
        public int TemplateId { get;  set; }
        public int ContractTypeId { get;  set; }
        public string TemplateName { get;  set; }
        public string Description { get;  set; }
        public string TemplatePath { get; set; }
    }
}
