using System.Collections.Generic;

namespace Mmjc_Vsto.models
{
    public class TaskList
    {
        public int id { get; set; }
        public string taskName { get; set; }
        public int benchmarkNumberOfDays { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }

    public class SubAssignmentList
    {
        public int id { get; set; }
        public string subAssignmentName { get; set; }
        public List<TaskList> taskList { get; set; }
    }

    public class ContractType
    {
        public int id { get; set; }
        public string assignmentName { get; set; }
        public List<SubAssignmentList> subAssignmentList { get; set; }
    }

    public class Body
    {
        public List<ContractType> data { get; set; }
        public int totalElements { get; set; }
    }
}
