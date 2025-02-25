using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sundeus.models
{
  
        public class Attributes : Base
        {
            public int AttributeId { get;  set; }
            public int ContractTypeId { get;  set; }
            public string Name { get;  set; }
            public string Label { get;  set; }
            public int MetaDataTypeId { get;  set; }
            public string MetaDataValues { get;  set; }
            public string DefaultValue { get;  set; }
            public bool? IsRequired { get;  set; }
            public bool? IsEditable { get;  set; }
            public bool? IsHidden { get;  set; }
            public string HelpText { get;  set; }
            public int? MaxStringLength { get;  set; }
            public int? MinValue { get;  set; }
            public int? MaxValue { get;  set; }
            public bool? IsDependent { get;  set; }
            public string DependentOn { get;  set; }
            public int? DependentOnAttributeId { get;  set; }
            public bool? IsInherited { get;  set; }
            public string InheritedContractId { get;  set; }
            public string InheritedFrom { get;  set; }
            public bool? IsDisplaySummary { get;  set; }
            public bool? IsEditablePostExecution { get;  set; }
            public bool? IsSearchEnabled { get;  set; }
            public int SequenceNo { get;  set; }
            public int? SectionId { get;  set; }
            public string SectionName { get;  set; }
            //public bool IsValid { get; private set; }
            public int UpdatedCount { get;  set; }

            //public Attributes(AttributeDto attributeDto)
            //{
            //    AttributeId = attributeDto.AttributeId;
            //}
       
    }
}
