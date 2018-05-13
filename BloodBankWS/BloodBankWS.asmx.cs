using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;
using System.Web.Services;
using BBWS.BL;
using BBWS.Common;

namespace BloodBankWS
{
    /// <inheritdoc />
    /// <summary>
    /// Summary description for BloodBankWS
    /// </summary>
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BloodBankWS : WebService
    {
        [WebMethod]
        public bool CheckIfSsnExists(string ssn)
        {
            try
            {
                return BL.CheckIfSsnExists(ssn);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public string InsertNewBloodBag(string ssn)
        {
            try
            {
                return BL.InsertNewBloodBag(ssn);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public List<BloodBagMinimal> SearchForBloodBagByDateInterval(DateTime d1, DateTime d2)
        {
            try
            {
                return BL.SearchForBloodBagByDateInterval(d1, d2);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public BloodBag GetBloodBagById(string bbid)
        {
            try
            {
                return BL.GetBloodBagById(bbid);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public List<BloodBagMinimal> SearchForBloodBagsBySsn(string ssn)
        {
            try
            {
                return BL.SearchForBloodBagsBySsn(ssn);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public void DeleteBloodBagAnalysis(string ssn)
        {
            try
            {
                BL.DeleteBloodBagAnalysis(ssn);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        [WebMethod]
        public void UpdateBloodBagAnalysis(BloodBag bb)
        {
            try
            {
                BL.UpdateBloodBagAnalysis(bb);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public void UpdateBloodBagAnalysisByBloodBagId(BloodBag bb, string bbid)
        {
            try
            {
                BL.UpdateBloodBagAnalysisByBloodBagId(bb, bbid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [WebMethod]
        public void AddNewBloodBagAnalysis(BloodBag bb, string bbid)
        {
            try
            {
                BL.AddBloodBagAnalysys(bb, bbid);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public void AddNewDonor(DonorDetails dd, string username, string password)
        {
            try
            {
                BL.AddNewDonor(dd);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public void DeleteDonor(string ssn, string username, string password)
        {
            try
            {
                BL.DeleteDonor(ssn);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public List<DonorDetails> GetDonorsByDoctorId(int dd, string username, string password)
        {
            try
            {
               return BL.GetDonorsByDoctorId(dd);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public List<DonorDetails> GetDonorsByKeyword(string keyword, int key, string username, string password)
        {
            try
            {
                return BL.GetDonorsByKeyword(keyword, key);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public void UpdateDonor(DonorDetails dd, string username, string password)
        {
            try
            {
                BL.UpdateDonor(dd);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public List<DonorDetails> GetDonorsByDataFilter(SearchFilterDetails sdf, string username, string password)
        {
            try
            {
                return BL.GetDonorsByDataFilter(sdf);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public int GetBankIdByUsername(string username, string u, string p)
        {
            try
            {
                return BL.GetBankIdByUsername(username);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }
        }

        [WebMethod]
        public YourDetails GetDetailsByBankId(int bankid, string u, string p)
        {
            try
            {
                return BL.GetDetailsByBankId(bankid);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }
        }

        [WebMethod]
        public int ForgotYourPassword(ForgotPassword fp, string username, string password)
        {
            try
            {
                return BL.ForgotPassword(fp);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public int ChangePassword(ChangePassCredentials cpc, string username, string password)
        {
            try
            {
                return BL.ChangePassword(cpc);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public int Login(Credentials cpc, string username, string password)
        {
            try
            {
                return BL.LoginToApplication(cpc);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public List<Requests> GetRequests(string username, string password)
        {
            try
            {
                return BL.GetRequests();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public List<BadBlood> GetBadBlood(string username, string password)
        {
            try
            {
                return BL.GetBadBlood();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public Address GetAddress(int bankId, string username, string password)
        {
            try
            {
                return BL.GetAddress(bankId);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public void RespondToRequest(int requestId, string username, string password)
        {
            try
            {
                BL.RespondToRequest(requestId);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public void ThrowBag(int bagId, string username, string password)
        {
            try
            {
                BL.ThrowBag(bagId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
