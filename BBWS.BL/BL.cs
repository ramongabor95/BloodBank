using System;
using System.Collections.Generic;
using System.ServiceModel;
using BBWS.Common;

namespace BBWS.BL
{
    public class BL
    {
        public static bool CheckIfSsnExists(string ssn)
        {
            try
            {
                return DAL.DAL.CheckIfSsnExists(ssn);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }
        }
        public static string InsertNewBloodBag(string ssn)
        {
            try
            {
                return DAL.DAL.InsertNewBloodBag(ssn);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }
        }
        public static List<BloodBagMinimal> SearchForBloodBagsBySsn(string ssn)
        {
            try
            {
               return DAL.DAL.SearchForBloodBagBySsn(ssn);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }
        }

        public static BloodBag GetBloodBagById(string bbid)
        {
            try
            {
                return DAL.DAL.GetBloodBagById(bbid);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }
        }
        public static List<BloodBagMinimal> SearchForBloodBagByDateInterval(DateTime d1, DateTime d2)
        {
            try
            {
                return DAL.DAL.SearchForBloodBagByDateInterval(d1, d2);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }
        }
        public static void DeleteBloodBagAnalysis(string ssn)
        {
            try
            {
                DAL.DAL.DeleteBloodBagAnalysis(ssn);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }
        }
        public static void UpdateBloodBagAnalysis(BloodBag bb)
        {
            try
            {
                DAL.DAL.UpdateBloodBagAnalysis(bb);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }
        }

        public static void UpdateBloodBagAnalysisByBloodBagId(BloodBag bb, string bbid)
        {
            try
            {
                DAL.DAL.UpdateBloodBagAnalysisByBloodBagId(bb, bbid);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }
        }
        public static void AddBloodBagAnalysys(BloodBag bb, string bbid)
        {
            try
            {
                DAL.DAL.AddNewBloodBagAnalysis(bb, bbid);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }
        }
        public static void UpdateDonor(DonorDetails dd)
        {
            try
            {
                var did = DAL.DAL.GetDonorIdBySsn(dd.SocialSecurityNumber);
                if (did != -1)
                {
                    DAL.DAL.UpdateDonor(dd, did);
                }
            }
            catch
            {
                throw new Exception("Something went wrong");
            }
        }
        public static List<DonorDetails> GetDonorsByDataFilter(SearchFilterDetails sdf)
        {
            try
            {
                return DAL.DAL.GetDonorsByDataFilter(sdf);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }
        }
        public static List<DonorDetails> GetDonorsByKeyword(string keyword, int key)
        {
            try
            {
                return DAL.DAL.GetDonorsByKeyword(keyword,key);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }
        }
        public static void DeleteDonor(string ssn)
        {
            try
            {
                DAL.DAL.DeleteDonorBySsn(ssn);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }
        }

        public static List<DonorDetails> GetDonorsByDoctorId(int doctorid)
        {
            try
            {
                return DAL.DAL.GetDonorsByDoctorId(doctorid);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }
        }
        public static YourDetails GetDetailsByBankId(int bankId)
        {
            try
            {
                return DAL.DAL.GetDetailsByBankId(bankId);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }
        }
        public static int GetBankIdByUsername(string username)
        {
            try
            {
                return DAL.DAL.GetBankIdByUsername(username);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }
        }
        public static void AddNewDonor(DonorDetails dd)
        {
            try
            {
                DAL.DAL.AddNewDonor(dd);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static int ForgotPassword(ForgotPassword fp)
        {
            try
            {
                if(!Validations.ValidateUsernames(fp.Username))
                    throw new FaultException("Invalid username format!");
                var result = DAL.DAL.ForgotPassword(fp);
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int ChangePassword(ChangePassCredentials cpc)
        {
            try
            {
                if (!Validations.ValidateUsernames(cpc.Username))
                    throw new FaultException("Invalid username format!");
                var result = DAL.DAL.ChangePassword(cpc);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int LoginToApplication(Credentials c)
        {
            try
            {
                if (!Validations.ValidateUsernames(c.UserName))
                    throw new FaultException("Invalid username format!");
                var result = DAL.DAL.Login(c);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Requests> GetRequests()
        {
            try
            {
                var result = DAL.DAL.GetRequests();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<BadBlood> GetBadBlood()
        {
            try
            {
                var result = DAL.DAL.GetBadBlood();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Address GetAddress(int bankId)
        {
            try
            {
                var result = DAL.DAL.GetAddress(bankId);
                if (string.IsNullOrEmpty(result.PostalCode))
                    throw new FaultException("Invalid bankId");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void RespondToRequest(int responseId)
        {
            try
            {
                var result = DAL.DAL.RespondToRequest(responseId);
                if (result == 0)
                    throw new FaultException("Invalid responseId"); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void ThrowBag(int bagId)
        {
            try
            {
                var result = DAL.DAL.ThrowBag(bagId);
                if (result == 0)
                    throw new FaultException("Invalid bagId");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
