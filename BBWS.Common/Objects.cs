using System;
using System.Collections.Generic;

namespace BBWS.Common
{

    public class Credentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class ErrorFound
    {
        public string ErrorName { get; set; }
    }

    public class Requests
    {
        public int RequestId { get; set; }
        public int BankId { get; set; }
        public string BloodGroup { get; set; }
        public string BloodRh { get; set; }
        public DateTime RequestDate { get; set; }
        public int Priority { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class BadBlood
    {
        public int BadBloodBagId { get; set; }
        public int BloodBagId { get; set; }
        public DateTime ReceiveDate { get; set; }

    }

    public class ChangePassCredentials
    {
        public string Username { get; set; }
        public string OldPass { get; set; }
        public string NewPass { get; set; }

    }

    public class ForgotPassword
    {
        public string Username { get; set; }
        public string SecAnswer { get; set; }
    }

    public enum Gender
    {
        M,
        F
    }


    public class DonorDetails
    {
        public int BankId { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public Gender Gender { get; set; }
        public bool IsEmergencyDonor { get; set; }
        public int NumberOfDonations { get; set; }
        public string Occupation { get; set; }
        public string Industry { get; set; }
        public DateTime BirthDay { get; set; }
        public int Age { get; set; }
        public int DoctorId { get; set; }
        public DateTime Timestamp { get; set; }
        public DonorMedicalHistory MedicalHistory { get; set; }
        public string BloodGroup { get; set; }
        public string BloodRh { get; set; }
    }

    public class BloodBagMinimal
    {
        public string SocialSecurityNumber { get; set; }
        public string BloodBagId { get; set; }
        public DateTime DateReceived { get; set; }
        public bool HasBeenProcesed { get; set; }
        public bool ToBeThrown { get; set; }
        public string GroupAndRh { get; set; }
    }
    
    public class BloodBag
    {
        public string SocialSecurityNumber { get; set; }
        public decimal Rbc { get; set; }
        public decimal Hgb { get; set; }
        public decimal Hct { get; set; }
        public decimal Mcv { get; set; }
        public decimal Mchc { get; set; }
        public decimal Wbc { get; set; }
        public decimal Plt { get; set; }
        public decimal Vsh { get; set; }
        public decimal Tgp { get; set; }
        public string Group { get; set; }
        public string Rh { get; set; }
        public bool AnticorpsHiv { get; set; }
        public bool AnticoprsHeB { get; set; }
        public bool AnticorpsHeC { get; set; }
        public bool AnticorpsLec { get; set; }
        public bool AnticorpsSif { get; set; }
        public bool IsGoodForUse { get; set; }

    }

    public class DonorMedicalHistory
    {
        public string DiseaseName { get; set; }
        public bool IsCured { get; set; }
    }

    public class YourDetails
    {
        public string BankName { get; set; }
        public List<Doctor> DoctorsList { get; set; }
    }

    public class Doctor
    {
        public int DoctorId { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorEmail { get; set; }
        public string DoctorStampId { get; set; }
    }

    public class SearchFilterDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ssn { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
