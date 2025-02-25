using System;
using System.Collections.Generic;

namespace Mmjc_Vsto.models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
    }

    public class Designation
    {
        public string designation;
        public int Id { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public string role { get; set; }
    }

    public class ReportingPerson
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }

    public class AssignmentDetails
    {
        public int id { get; set; }
        public string label { get; set; }
        public int datatypeId { get; set; }
        public string datatype { get; set; }
        public bool isUnique { get; set; }
        public bool isNullable { get; set; }
        public bool isUpdatable { get; set; }
        public int assignmentId { get; set; }
        public string assignment { get; set; }
        public int subAssignmentId { get; set; }
        public string subAssignment { get; set; }
        public int documentTypeId { get; set; }
        public string documentType { get; set; }

    }
    public class ApiResponse
    {
        public string Message { get; set; }
        public SubAssignmentInfo[] Body { get; set; }
        public int Status { get; set; }
    }
    public class ApiResponseBody
    {
        public string Message { get; set; }
        public List<AssignmentDetails> Body { get; set; }
        public int Status { get; set; }
    }
    public class ApiResponseForDocument
    {
        public string Message { get; set; }
        public List<DocumentInfo> Body { get; set; }
        public int Status { get; set; }
    }
    public class SubAssignmentInfo
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int AssignmentId { get; set; }
    }

    public class DocumentInfo
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string AssignmentMasterName { get; set; }
    }


}
