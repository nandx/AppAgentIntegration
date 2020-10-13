using System;

namespace AppAgentIntegration.Model
{
    public class AgentDataNew
    {
        public String PayeeID { set; get; }
        public String Name { set; get; }
        public DateTime? JoinDate { set; get; }
        public String Title { set; get; }
        public String EmailAddress { set; get; }
        public String Address { set; get; }
        public String RecruitBy { set; get; }
        public String RecruiterName { set; get; }
        public String AMCode { set; get; }
        public String AMName { set; get; }
        public String SAMCode { set; get; }
        public String SAMName { set; get; }
        public String ADCode { set; get; }
        public String ADNAme { set; get; }
        
        public String GAOfficeCode { set; get; }
        public String GAOfficeName { set; get; }
        public String No_Identitas { set; get; }
        public String LicenseID { set; get; }
        public String Phone { set; get; }
        public String EmployeeStatus { set; get; }
        public String TerminationComments { set; get; }
        public String AccountNo { set; get; }
        public String BankCode { set; get; }
       
        
        public DateTime? ExpiryDate { set; get; }
        public DateTime? TerminationDate { set; get; }
        public DateTime? CreatedDate { set; get; }
        public DateTime? UpdatedDate { set; get; }
        public DateTime? DeletedDate { set; get; }
    }
}