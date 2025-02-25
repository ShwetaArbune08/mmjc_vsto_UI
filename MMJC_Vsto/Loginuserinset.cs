using Sundeus.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sundeus
{
    public static class Loginuserinset
    {
        public static string url { set; get; }

        public static string username { set; get; }
        public static int SelectedContracttype { set; get; }
        public static int tenantId{ set; get; }
        public static string SelectedContracttypename { set; get; }
        public static List<ContractType> LstComboitem { set; get; }
        public static List<TemplateModel> LstTemplate { set; get; }
        public static List<Clauses> LstClauses { set; get; }
        public static string templatename { set; get; }
        public static string templatesummary { set; get; }

        public static List<Attributes> Attributes { get; set; }
        public static List<coordinates> coordinates { get; set; }
        public static int signnumber { get; set; }

        public static bool IsLoggedIn { get; set; }


        public static string clauseName { get; set; }
        public static string clauseDescription { get; set; }
        public static string clauseText { get; set; }
        public static int clauseLanguage_Id { get; set; }
        public static bool clauseIsMandatory { get; set; }
        public static bool clauseIsTracked { get; set; }
        public static int clauseContractTypeId { get; set; }
        public static int clauseTemplateId { get; set; }
        public static string clauseContractTypeName { get; set; }
        public static string clauseTemplateName { get; set; }
        public static List<Clauses>  clauseListdragdrop { get; set; }
        public static int? seletedtemplateid { set; get; }
        public static int? seletedcontractId { set; get; }

    }
}
