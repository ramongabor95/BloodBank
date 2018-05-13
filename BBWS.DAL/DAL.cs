using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using BBWS.Common;

namespace BBWS.DAL
{
    public class DAL
    {
        public static string GetSsnByBloodBagId(int bbid)
        {
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[GetSSNByBloodBagId]"
                };

                command.Parameters.AddWithValue("@bbid", bbid);
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    var results = command.ExecuteReader();
                    while (results.Read())
                    {
                        return results.GetValue(0).ToString();
                    }
                    sqlConnection.Close();
                }
                return "";
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went wrong with the database" + ex.Message);
            }
        }

        public static BloodBag GetBloodBagById(string bbid)
        {
            var bloodBag = new BloodBag();
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[GetBloodBagById]"
                };
                command.Parameters.AddWithValue("@bbid", bbid);
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    var results = command.ExecuteReader();
                    while (results.Read())
                    {
                        bloodBag = new BloodBag();
                        bloodBag.Rbc = Convert.ToDecimal(results.GetValue(3).ToString());
                        bloodBag.Hgb = Convert.ToDecimal(results.GetValue(4).ToString());
                        bloodBag.Hct = Convert.ToDecimal(results.GetValue(5).ToString());
                        bloodBag.Mcv = Convert.ToDecimal(results.GetValue(6).ToString());
                        bloodBag.Mchc = Convert.ToDecimal(results.GetValue(7).ToString());
                        bloodBag.Wbc = Convert.ToDecimal(results.GetValue(8).ToString());
                        bloodBag.Plt = Convert.ToDecimal(results.GetValue(9).ToString());
                        bloodBag.Vsh = Convert.ToDecimal(results.GetValue(10).ToString());
                        bloodBag.Group = results.GetValue(11).ToString();
                        bloodBag.Rh = results.GetValue(12).ToString();
                        bloodBag.AnticorpsHiv = results.GetBoolean(13);
                        bloodBag.AnticorpsHeC = results.GetBoolean(14);
                        bloodBag.AnticoprsHeB = results.GetBoolean(15);
                        bloodBag.AnticorpsLec = results.GetBoolean(16);
                        bloodBag.AnticorpsSif = results.GetBoolean(17);
                        bloodBag.Tgp = Convert.ToDecimal(results.GetValue(18).ToString());
                        return bloodBag;
                    }
                }
                return bloodBag;
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went wrong with the database" + ex.Message);
            }
        }

        public static List<BloodBagMinimal> SearchForBloodBagByDateInterval(DateTime d1, DateTime d2)
        {
            var toReturn = new List<BloodBagMinimal>();
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[GetBloodBagsByDateInterval]"
                };
                command.Parameters.AddWithValue("@d1", d1);
                command.Parameters.AddWithValue("@d2", d2);
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    var results = command.ExecuteReader();
                    while (results.Read())
                    {
                        var temp = new BloodBagMinimal
                        {
                            BloodBagId = results.GetValue(0).ToString(),
                            DateReceived = Convert.ToDateTime(results.GetValue(2).ToString()),
                            GroupAndRh = results.GetValue(3) + "/" + results.GetValue(4),
                            HasBeenProcesed = results.GetBoolean(5),
                            ToBeThrown = results.GetBoolean(6)
                        };
                        temp.SocialSecurityNumber = GetSsnByBloodBagId(Convert.ToInt32(temp.BloodBagId));
                        toReturn.Add(temp);
                    }
                    sqlConnection.Close();
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went wrong with the database" + ex.Message);
            }
        }
        public static List<BloodBagMinimal> SearchForBloodBagBySsn(string ssn)
        {
            var toReturn = new List<BloodBagMinimal>();
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[GetBloodBagsBySSN]"
                };
                command.Parameters.AddWithValue("@ssn", ssn);
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    var results = command.ExecuteReader();
                    while (results.Read())
                    {
                        var temp = new BloodBagMinimal
                        {
                            BloodBagId = results.GetValue(0).ToString(),
                            DateReceived = Convert.ToDateTime(results.GetValue(2).ToString()),
                            GroupAndRh = results.GetValue(3) + "/" + results.GetValue(4),
                            HasBeenProcesed = results.GetBoolean(5),
                            ToBeThrown = results.GetBoolean(6),
                            SocialSecurityNumber = ssn
                        };
                        toReturn.Add(temp);
                    }
                    sqlConnection.Close();
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went wrong with the database" + ex.Message);
            }
        }
        public static void UpdateBloodBagAnalysis(BloodBag bb)
        {
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[UpdateBloodBagAnalysis]"
                };

                command.Parameters.AddWithValue("@bbid", GetBloodBagIdByDonorId(GetDonorIdBySsn(bb.SocialSecurityNumber)));
                command.Parameters.AddWithValue("@rbc", bb.Rbc);
                command.Parameters.AddWithValue("@hgb", bb.Hgb);
                command.Parameters.AddWithValue("@hct", bb.Hct);
                command.Parameters.AddWithValue("@mcv", bb.Mcv);
                command.Parameters.AddWithValue("@mchc", bb.Mchc);
                command.Parameters.AddWithValue("@wbc", bb.Wbc);
                command.Parameters.AddWithValue("@plt", bb.Plt);
                command.Parameters.AddWithValue("@vsh", bb.Vsh);
                command.Parameters.AddWithValue("@group", bb.Group);
                command.Parameters.AddWithValue("@rh", bb.Rh);
                command.Parameters.AddWithValue("@hiv", bb.AnticorpsHiv);
                command.Parameters.AddWithValue("@HepB", bb.AnticoprsHeB);
                command.Parameters.AddWithValue("@HepC", bb.AnticorpsHeC);
                command.Parameters.AddWithValue("@lec", bb.AnticorpsLec);
                command.Parameters.AddWithValue("@sif", bb.AnticorpsSif);
                command.Parameters.AddWithValue("@tgp", bb.Tgp);
                command.Parameters.AddWithValue("@ok", bb.IsGoodForUse);

                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went wrong with the database" + ex.Message);
            }
        }

        public static void UpdateBloodBagAnalysisByBloodBagId(BloodBag bb, string bbid)
        {
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[UpdateBloodBagAnalysis]"
                };

                command.Parameters.AddWithValue("@bbid", bbid);
                command.Parameters.AddWithValue("@rbc", bb.Rbc);
                command.Parameters.AddWithValue("@hgb", bb.Hgb);
                command.Parameters.AddWithValue("@hct", bb.Hct);
                command.Parameters.AddWithValue("@mcv", bb.Mcv);
                command.Parameters.AddWithValue("@mchc", bb.Mchc);
                command.Parameters.AddWithValue("@wbc", bb.Wbc);
                command.Parameters.AddWithValue("@plt", bb.Plt);
                command.Parameters.AddWithValue("@vsh", bb.Vsh);
                command.Parameters.AddWithValue("@group", bb.Group);
                command.Parameters.AddWithValue("@rh", bb.Rh);
                command.Parameters.AddWithValue("@hiv", bb.AnticorpsHiv);
                command.Parameters.AddWithValue("@HepB", bb.AnticoprsHeB);
                command.Parameters.AddWithValue("@HepC", bb.AnticorpsHeC);
                command.Parameters.AddWithValue("@lec", bb.AnticorpsLec);
                command.Parameters.AddWithValue("@sif", bb.AnticorpsSif);
                command.Parameters.AddWithValue("@tgp", bb.Tgp);
                command.Parameters.AddWithValue("@ok", bb.IsGoodForUse);

                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went wrong with the database" + ex.Message);
            }
        }


        public static void DeleteBloodBagAnalysis(string ssn)
        {
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[DeleteBloodBagAnalysis]"
                };
                command.Parameters.AddWithValue("@bbid", ssn);
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went wrong with the database" + ex.Message);
            }
        }
        public static int GetBloodBagIdByDonorId(int did)
        {
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[GetBloodBagIdByDonorId]"
                };

                command.Parameters.AddWithValue("@did", did);
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    var results = command.ExecuteReader();
                    while (results.Read())
                    {
                        return int.Parse(results.GetValue(0).ToString());
                    }
                    sqlConnection.Close();
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went wrong with the database" + ex.Message);
            }
        }
        public static void AddNewBloodBagAnalysis(BloodBag bb, string bbid)
        {
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[InsertNewBloodBagAnalysis]"
                };

                command.Parameters.AddWithValue("@BloodBagId", string.IsNullOrEmpty(bbid) ? GetBloodBagIdByDonorId(GetDonorIdBySsn(bb.SocialSecurityNumber)).ToString() : bbid);
                command.Parameters.AddWithValue("@RBC", bb.Rbc);
                command.Parameters.AddWithValue("@HGB", bb.Hgb);
                command.Parameters.AddWithValue("@HCT", bb.Hct);
                command.Parameters.AddWithValue("@MCV", bb.Mcv);
                command.Parameters.AddWithValue("@MCHC", bb.Mchc);
                command.Parameters.AddWithValue("@WBC", bb.Wbc);
                command.Parameters.AddWithValue("@PLT", bb.Plt);
                command.Parameters.AddWithValue("@VSH", bb.Vsh);
                command.Parameters.AddWithValue("@Group", bb.Group);
                command.Parameters.AddWithValue("@RH", bb.Rh);
                command.Parameters.AddWithValue("@HIV", bb.AnticorpsHiv);
                command.Parameters.AddWithValue("@HepB", bb.AnticoprsHeB);
                command.Parameters.AddWithValue("@HepC", bb.AnticorpsHeC);
                command.Parameters.AddWithValue("@Leuc", bb.AnticorpsLec);
                command.Parameters.AddWithValue("@Sifi", bb.AnticorpsSif);
                command.Parameters.AddWithValue("@TGP", bb.Tgp);
                command.Parameters.AddWithValue("@ok", bb.IsGoodForUse);

                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went wrong with the database" + ex.Message);
            }
        }

        public static DonorMedicalHistory GetMedicalHistoryByDonorId(int donorId)
        {
            var toReturn = new DonorMedicalHistory();
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[GetDonorMedicalHistory]"
                };

                command.Parameters.AddWithValue("@did", donorId);

                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    var results = command.ExecuteReader();
                    while (results.Read())
                    {
                        toReturn.DiseaseName = results.GetValue(2).ToString();
                        toReturn.IsCured = Convert.ToBoolean(results.GetValue(3).ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went wrong with the database" + ex.Message);
            }
            return toReturn;
        }

        public static int GetDonorIdBySsn(string ssn)
        {
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[GetDonorIdBySSN]"
                };

                command.Parameters.AddWithValue("@ssn", ssn);
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    var results = command.ExecuteReader();
                    while (results.Read())
                    {
                        return int.Parse(results.GetValue(0).ToString());
                    }
                    sqlConnection.Close();
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went wrong with the database" + ex.Message);
            }
        }

        public static void UpdateDonor(DonorDetails dd, int donorId)
        {
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "UpdateDonor"
                };

                command.Parameters.AddWithValue("@did", donorId);
                command.Parameters.AddWithValue("@ssn", dd.SocialSecurityNumber);
                command.Parameters.AddWithValue("@first", dd.FirstName);
                command.Parameters.AddWithValue("@last", dd.LastName);
                command.Parameters.AddWithValue("@phone", dd.PhoneNumber);
                command.Parameters.AddWithValue("@city", dd.City);
                command.Parameters.AddWithValue("@country", dd.Country);
                command.Parameters.AddWithValue("@postcode", dd.PostalCode);
                command.Parameters.AddWithValue("@gender", dd.Gender.ToString());
                command.Parameters.AddWithValue("@isEm", dd.IsEmergencyDonor);
                command.Parameters.AddWithValue("@nrDon", dd.NumberOfDonations);
                command.Parameters.AddWithValue("@occ", dd.Occupation);
                command.Parameters.AddWithValue("@ind", dd.Industry);
                command.Parameters.AddWithValue("@age", dd.Age);
                command.Parameters.AddWithValue("@dob", dd.BirthDay);
                command.Parameters.AddWithValue("@group", dd.BloodGroup);
                command.Parameters.AddWithValue("@rh", dd.BloodRh);

                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                }

                if (!string.IsNullOrEmpty(dd.MedicalHistory?.DiseaseName))
                {
                    command = new SqlCommand
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "[dbo].[UpdateDonorMedicalHistory]"
                    };

                    command.Parameters.AddWithValue("@did", GetDonorIdBySsn(dd.SocialSecurityNumber));
                    command.Parameters.AddWithValue("@name", dd.MedicalHistory.DiseaseName);
                    command.Parameters.AddWithValue("@isC", dd.MedicalHistory.IsCured);

                    using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                    {
                        command.Connection = sqlConnection;
                        sqlConnection.Open();
                        command.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                }
                else
                {
                    command = new SqlCommand
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "[dbo].[DeleteDonorMedicalHistory]"
                    };

                    command.Parameters.AddWithValue("@did", GetDonorIdBySsn(dd.SocialSecurityNumber));

                    using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                    {
                        command.Connection = sqlConnection;
                        sqlConnection.Open();
                        command.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went wrong with the database" + ex.Message);
            }
        }

        public static List<DonorDetails> GetDonorsByDataFilter(SearchFilterDetails sfd)
        {
            var toReturn = new List<DonorDetails>();
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM Donors " +
                                  "WHERE SSN LIKE '%" + sfd.Ssn + "%'" +
                                  "AND FirstName LIKE '%" + sfd.FirstName + "%'" +
                                  "AND LastName LIKE '%" + sfd.LastName + "%'" +
                                  "AND Phone LIKE '%" + sfd.Phone + "%'" +
                                  "AND City LIKE '%" + sfd.City + "%'" +
                                  "AND Country LIKE '%" + sfd.Country + "%'"
                };


                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    var results = command.ExecuteReader();
                    while (results.Read())
                    {
                        var tempObj = new DonorDetails
                        {
                            BankId = int.Parse(results.GetValue(1).ToString()),
                            SocialSecurityNumber = results.GetValue(2).ToString(),
                            FirstName = results.GetValue(3).ToString(),
                            LastName = results.GetValue(4).ToString(),
                            PhoneNumber = results.GetValue(5).ToString(),
                            City = results.GetValue(6).ToString(),
                            Country = results.GetValue(7).ToString(),
                            PostalCode = results.GetValue(8).ToString(),
                            Gender = results.GetValue(9).ToString() == "M" ? Gender.M : Gender.F,
                            IsEmergencyDonor = bool.Parse(results.GetValue(10).ToString()),
                            NumberOfDonations = int.Parse(results.GetValue(11).ToString()),
                            Occupation = results.GetValue(12).ToString(),
                            Industry = results.GetValue(13).ToString(),
                            Age = int.Parse(results.GetValue(14).ToString()),
                            BirthDay = Convert.ToDateTime(results.GetValue(15).ToString()),
                            Timestamp = Convert.ToDateTime(results.GetValue(17).ToString()),
                            MedicalHistory = new DonorMedicalHistory(),
                            BloodGroup = results.GetValue(18).ToString(),
                            BloodRh = results.GetValue(19).ToString()
                        };
                        tempObj.MedicalHistory =
                            GetMedicalHistoryByDonorId(GetDonorIdBySsn(tempObj.SocialSecurityNumber));
                        toReturn.Add(tempObj);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong" + ex.Message);
            }
            return toReturn;
        }

        public static List<DonorDetails> GetDonorsByKeyword(string keyword, int key)
        {
            var toReturn = new List<DonorDetails>();
            try
            {
                var command = new SqlCommand();
                switch (key)
                {
                    case 1:
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetDonorsBySSN";
                        command.Parameters.AddWithValue("@ssn", keyword);
                        break;
                    case 2:
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetDonorsByFirstNanme";
                        command.Parameters.AddWithValue("@fname", keyword);
                        break;
                    case 3:
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetDonorsByLastNanme";
                        command.Parameters.AddWithValue("@lname", keyword);
                        break;
                    default:
                        return new List<DonorDetails>();    
                }
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    var results = command.ExecuteReader();
                    while (results.Read())
                    {
                        var tempObj = new DonorDetails
                        {
                            BankId = int.Parse(results.GetValue(1).ToString()),
                            SocialSecurityNumber = results.GetValue(2).ToString(),
                            FirstName = results.GetValue(3).ToString(),
                            LastName = results.GetValue(4).ToString(),
                            PhoneNumber = results.GetValue(5).ToString(),
                            City = results.GetValue(6).ToString(),
                            Country = results.GetValue(7).ToString(),
                            PostalCode = results.GetValue(8).ToString(),
                            Gender = results.GetValue(9).ToString() == "M" ? Gender.M : Gender.F,
                            IsEmergencyDonor = bool.Parse(results.GetValue(10).ToString()),
                            NumberOfDonations = int.Parse(results.GetValue(11).ToString()),
                            Occupation = results.GetValue(12).ToString(),
                            Industry = results.GetValue(13).ToString(),
                            Age = int.Parse(results.GetValue(14).ToString()),
                            BirthDay = Convert.ToDateTime(results.GetValue(15).ToString()),
                            Timestamp = Convert.ToDateTime(results.GetValue(17).ToString()),
                            MedicalHistory = new DonorMedicalHistory(),
                            BloodGroup = results.GetValue(18).ToString(),
                            BloodRh = results.GetValue(19).ToString()
                        };
                        tempObj.MedicalHistory =
                            GetMedicalHistoryByDonorId(GetDonorIdBySsn(tempObj.SocialSecurityNumber));
                        toReturn.Add(tempObj);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong" + ex.Message);
            }
            return toReturn;
        }

        public static void DeleteDonorBySsn(string ssn)
        {
            try
            {
                DeleteBloodBagAnalysis(ssn);
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[DeleteBloodBag]"
                };

                command.Parameters.AddWithValue("@did", GetDonorIdBySsn(ssn));

                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[DeleteDonorMedicalHistory]"
                };

                command.Parameters.AddWithValue("@did", GetDonorIdBySsn(ssn));

                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                }
               command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "DeleteDonorBySSN"
                };
                command.Parameters.AddWithValue("@ssn", ssn);
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong" + ex.Message);
            }
        }

        public static List<DonorDetails> GetDonorsByDoctorId(int doctorId)
        {
            var toReturn = new List<DonorDetails>();
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetDonorsByDoctorId"
                };
                command.Parameters.AddWithValue("@did", doctorId);
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    var results = command.ExecuteReader();
                    while (results.Read())
                    {
                        var tempObj = new DonorDetails
                        {
                            BankId = int.Parse(results.GetValue(1).ToString()),
                            SocialSecurityNumber = results.GetValue(2).ToString(),
                            FirstName = results.GetValue(3).ToString(),
                            LastName = results.GetValue(4).ToString(),
                            PhoneNumber = results.GetValue(5).ToString(),
                            City = results.GetValue(6).ToString(),
                            Country = results.GetValue(7).ToString(),
                            PostalCode = results.GetValue(8).ToString(),
                            Gender = results.GetValue(9).ToString() == "M" ? Gender.M : Gender.F,
                            IsEmergencyDonor = bool.Parse(results.GetValue(10).ToString()),
                            NumberOfDonations = int.Parse(results.GetValue(11).ToString()),
                            Occupation = results.GetValue(12).ToString(),
                            Industry = results.GetValue(13).ToString(),
                            Age = int.Parse(results.GetValue(14).ToString()),
                            BirthDay = Convert.ToDateTime(results.GetValue(15).ToString()),
                            Timestamp = Convert.ToDateTime(results.GetValue(17).ToString()),
                            MedicalHistory = new DonorMedicalHistory(),
                            BloodGroup = results.GetValue(18).ToString(),
                            BloodRh = results.GetValue(19).ToString()
                        };
                        tempObj.MedicalHistory =
                            GetMedicalHistoryByDonorId(GetDonorIdBySsn(tempObj.SocialSecurityNumber));
                        toReturn.Add(tempObj);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong" + ex.Message);
            }
            return toReturn;
        }
        public static YourDetails GetDetailsByBankId(int bankid)
        {
            var obj = new YourDetails();
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetBankNameByBankId"
                };
                command.Parameters.AddWithValue("@bankid", bankid);
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    var results = command.ExecuteReader();
                    while (results.Read())
                    {
                        obj.BankName = results.GetValue(0).ToString();
                    }
                    sqlConnection.Close();
                }

                command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetDoctorsListByBankId"
                };
                command.Parameters.AddWithValue("@bankid", bankid);
                obj.DoctorsList = new List<Doctor>();

                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    var results = command.ExecuteReader();
                    while (results.Read())
                    {
                        var d = new Doctor
                        {
                            DoctorId = int.Parse(results.GetValue(0).ToString()),
                            DoctorFirstName = results.GetValue(2).ToString(),
                            DoctorLastName = results.GetValue(3).ToString(),
                            DoctorEmail = results.GetValue(4).ToString(),
                            DoctorStampId = results.GetValue(5).ToString()
                        };

                        obj.DoctorsList.Add(d);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong!" + ex.Message);
            }
            return obj;
        }

        public static int GetBankIdByUsername(string username)
        {
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetBankIdByUsername"
                };

                command.Parameters.AddWithValue("@user", username);
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    var results = command.ExecuteReader();
                    while (results.Read())
                    {
                        return int.Parse(results.GetValue(0).ToString());
                    }
                    sqlConnection.Close();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong" + ex.Message);
            }
        }

        public static void AddNewDonor(DonorDetails dd)
        {
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "InsertNewDonor"
                };

                command.Parameters.AddWithValue("@BankId", dd.BankId);
                command.Parameters.AddWithValue("@cnp", dd.SocialSecurityNumber);
                command.Parameters.AddWithValue("@first", dd.FirstName);
                command.Parameters.AddWithValue("@last", dd.LastName);
                command.Parameters.AddWithValue("@phone", dd.PhoneNumber);
                command.Parameters.AddWithValue("@city", dd.City);
                command.Parameters.AddWithValue("@country", dd.Country);
                command.Parameters.AddWithValue("@postal", dd.PostalCode);
                command.Parameters.AddWithValue("@gender", dd.Gender.ToString());
                command.Parameters.AddWithValue("@isEmer", dd.IsEmergencyDonor);
                command.Parameters.AddWithValue("@nrOfDon", dd.NumberOfDonations);
                command.Parameters.AddWithValue("@occupation", dd.Occupation);
                command.Parameters.AddWithValue("@industry", dd.Industry);
                command.Parameters.AddWithValue("@age", dd.Age);
                command.Parameters.AddWithValue("@birth", dd.BirthDay);
                command.Parameters.AddWithValue("@doctorid", dd.DoctorId);
                command.Parameters.AddWithValue("@group", dd.BloodGroup);
                command.Parameters.AddWithValue("@rh", dd.BloodRh);

                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                if (!string.IsNullOrEmpty(dd.MedicalHistory?.DiseaseName))
                {
                    command = new SqlCommand
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "InsertNewDonorMedicalHistory"
                    };

                    command.Parameters.AddWithValue("@DonorId", GetDonorIdBySsn(dd.SocialSecurityNumber));
                    command.Parameters.AddWithValue("@name", dd.MedicalHistory.DiseaseName);
                    command.Parameters.AddWithValue("@isCured", dd.MedicalHistory.IsCured);

                    using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                    {
                        command.Connection = sqlConnection;
                        sqlConnection.Open();
                        command.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                }

                command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[InsertNewBloodBag]"
                };
                command.Parameters.AddWithValue("@DonorId", GetDonorIdBySsn(dd.SocialSecurityNumber));
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went wrong with the database" + ex.Message);
            }
        }

        public static string InsertNewBloodBag(string ssn)
        {
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[InsertAdditionalBloodBag]"
                };
                command.Parameters.AddWithValue("@did", GetDonorIdBySsn(ssn));
                var output = new SqlParameter("@bbid", SqlDbType.NChar, 20);
                {
                    output.Direction = ParameterDirection.Output;
                }
                command.Parameters.Add(output);
                var dbResponse = string.Empty;
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    dbResponse = output.Value.ToString();
                    sqlConnection.Close();
                }
                return dbResponse;
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went wrong with the database" + ex.Message);
            }
        }

        public static bool CheckIfSsnExists(string ssn)
        {
            try
            {
                var command = new SqlCommand
                {
                    CommandText = "CheckIfSsnExists",
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ssn", ssn);
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    var results = command.ExecuteReader();
                    while (results.Read())
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new FaultException("Something went wrong with the database" + ex.Message);
            }
        }
             
        public static int ForgotPassword(ForgotPassword f)
        {
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "ForgotPassword"
                };
                var username = new SqlParameter("@Username", SqlDbType.NChar, 20);
                var securityAnswer = new SqlParameter("@SecurityAnswer", SqlDbType.NChar, 20);
                var output = new SqlParameter("@Output", SqlDbType.NChar, 20);
                {
                    output.Direction = ParameterDirection.Output;
                }
                command.Parameters.AddWithValue("@Username", f.Username);
                command.Parameters.AddWithValue("@SecurityAnswer", f.SecAnswer);
                command.Parameters.Add(output);

                int dbResponse;
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    dbResponse = int.Parse(output.Value.ToString());
                    sqlConnection.Close();
                }
                return dbResponse;
            }
            catch (Exception s)
            {
                throw new FaultException("Something went wrong in the database");
            }
        }

        public static int ChangePassword(ChangePassCredentials c)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "ChangePassword"
            };

            var Username = new SqlParameter("@Username", SqlDbType.NChar, 20);
            var OldPassword = new SqlParameter("@OldPassword", SqlDbType.NChar, 20);
            var NewPassword = new SqlParameter("@NewPassword", SqlDbType.NChar, 20);
            var Output = new SqlParameter("@Output", SqlDbType.NChar, 20);
            {
                Output.Direction = ParameterDirection.Output;
            }
            command.Parameters.AddWithValue("@Username", c.Username);
            command.Parameters.AddWithValue("@OldPassword", c.OldPass);
            command.Parameters.AddWithValue("@NewPassword", c.NewPass);
            command.Parameters.Add(Output);

            int dbResponse;
            using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
            {
                command.Connection = sqlConnection;
                sqlConnection.Open();
                command.ExecuteNonQuery();
                dbResponse = int.Parse(Output.Value.ToString());
                sqlConnection.Close();
            }
            return dbResponse;
        }

        public static int Login(Credentials user)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "LoginP"
            };

            var Username = new SqlParameter("@Username", SqlDbType.NChar, 20);
            var Password = new SqlParameter("@Password", SqlDbType.NChar, 20);
            var Output = new SqlParameter("@Output", SqlDbType.NChar, 20);
            {
                Output.Direction = ParameterDirection.Output;
            };

            command.Parameters.AddWithValue("@Username", user.UserName);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.Add(Output);

            var dbResponse = 0;
            using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
            {
                command.Connection = sqlConnection;
                sqlConnection.Open();
                command.ExecuteNonQuery();
                dbResponse = int.Parse(Output.Value.ToString());
                sqlConnection.Close();
            }
            return dbResponse;
        }

        public static List<Requests> GetRequests()
        {
            try
            {
                var list = new List<Requests>();

                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetRequests"
                };
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    var results = command.ExecuteReader();
                    while (results.Read())
                    {
                        var temp = new Requests
                        {
                            RequestId = int.Parse(results.GetValue(0).ToString()),
                            BankId = int.Parse(results.GetValue(1).ToString()),
                            BloodGroup = results.GetValue(2) as string,
                            BloodRh = results.GetValue(3) as string,
                            RequestDate = Convert.ToDateTime(results.GetValue(4).ToString()),
                            Priority = int.Parse(results.GetValue(5).ToString())
                        };


                        list.Add(temp);
                    }
                    sqlConnection.Close();
                }
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static int RespondToRequest(int requestId)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "RequestProcessed"
            };

            var reqId = new SqlParameter("@RequestId", SqlDbType.Int, 20);
            var Output = new SqlParameter("@Output", SqlDbType.Int, 20);
            {
                Output.Direction = ParameterDirection.Output;
            }
            command.Parameters.AddWithValue("@RequestId", requestId);
            command.Parameters.Add(Output);

            var dbResponse = 0;
            using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
            {
                command.Connection = sqlConnection;
                sqlConnection.Open();
                command.ExecuteNonQuery();
                dbResponse = int.Parse(Output.Value.ToString());
                sqlConnection.Close();
            }
            return dbResponse;
        }

        public static Address GetAddress(int bankId)
        {
            try
            {
                var toReturn = new Address();
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetAddress"
                };
                var bId = new SqlParameter("@BankId", SqlDbType.Int, 20);
                command.Parameters.AddWithValue("@BankId", bankId);

                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    var results = command.ExecuteReader();
                    while (results.Read())
                    {
                        toReturn.Country = results.GetValue(0) as string;
                        toReturn.City = results.GetValue(1) as string;
                        toReturn.PostalCode = results.GetValue(2) as string;
                        toReturn.Phone = results.GetValue(3) as string;
                        toReturn.Email = results.GetValue(4) as string;
                    }
                    sqlConnection.Close();
                }
                return toReturn;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<BadBlood> GetBadBlood()
        {
            try
            {
                var list = new List<BadBlood>();

                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SearchForBadBlood"
                };
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    var results = command.ExecuteReader();
                    while (results.Read())
                    {
                        var temp = new BadBlood
                        {
                            BadBloodBagId = int.Parse(results.GetValue(0).ToString()),
                            BloodBagId = int.Parse(results.GetValue(1).ToString()),
                            ReceiveDate = Convert.ToDateTime(results.GetValue(2) as string)
                        };


                        list.Add(temp);
                    }
                    //dbResponse = Int32.Parse(Output.Value.ToString());
                    sqlConnection.Close();
                }
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static int ThrowBag(int bbbagId)
        {
            try
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "ThrowBadBloodBag"
                };

                var reqId = new SqlParameter("@BloodBagId", SqlDbType.Int, 20);
                var Output = new SqlParameter("@Output", SqlDbType.Int, 20);
                {
                    Output.Direction = ParameterDirection.Output;
                }
                command.Parameters.AddWithValue("@BloodBagId", bbbagId);
                command.Parameters.Add(Output);

                var dbResult = 0;
                using (var sqlConnection = new SqlConnection(DalBase.ConnectionString))
                {
                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    dbResult = int.Parse(Output.Value.ToString());
                    sqlConnection.Close();
                }

                return dbResult;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

}
