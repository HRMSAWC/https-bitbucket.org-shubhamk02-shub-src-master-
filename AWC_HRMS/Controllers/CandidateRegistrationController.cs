using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AWC_HRMS.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace AWC_HRMS_Web.Controllers
{
    
    public class CandidateRegistrationController : Controller
    {
        // string connection = "data source= DESKTOP-B6EBP3L; initial catalog= AWC_HRMS; integrated security=True";

        

        [NonAction]
        public string FetchCandidateEducation(int canid)
        {
            var retval3 = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            using (SqlConnection con = new SqlConnection(constring))
            {
                int a = canid;
                con.Open();
                SqlCommand check = new SqlCommand("Usp_FetchEducationDetails", con);
                check.CommandType = CommandType.StoredProcedure;
                check.Parameters.AddWithValue("@candidateId", a);

                using (var reader = check.ExecuteReader())
                {

                    if (reader.HasRows)
                    {

                        do
                        {

                            while (reader.Read())
                                if (reader.GetString(1) != null)
                                {
                                    //Console.WriteLine(reader.GetString(1));
                                    retval3 = reader.GetString(1) + "|" + reader.GetString(2) + "|" + reader.GetString(3) + "|" + 
                                        reader.GetString(4) + "|" + reader.GetString(5) + "|" + reader.GetString(6) + "|" + 
                                        reader.GetString(7) + "|" + reader.GetString(8) + "|" + reader.GetString(9) + "|" +
                                        reader.GetString(10) + "|" + reader.GetString(11) + "|" + reader.GetString(12) + "|" +
                                        reader.GetString(14) + "|" + reader.GetString(15) + "|" + reader.GetString(16) + "|" +
                                        reader.GetString(17) + "|" + reader.GetString(18) + "|" + reader.GetString(19) + "|" +
                                        reader.GetString(20) + "|" + reader.GetString(21) + "|" + reader.GetString(22) + "|" +
                                        reader.GetString(23) + "|" + reader.GetString(24) + "|" + reader.GetString(25) + "|" +
                                        reader.GetString(26) + "|" + reader.GetString(27);
                                }


                        }
                        while (reader.NextResult());

                        reader.Close();
                        con.Close();

                    }
                    return retval3;
                }
            }
        }
        [NonAction]
        public string FetchCandidateDetails(int canid)
        {
            var retval2 = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            using (SqlConnection con = new SqlConnection(constring))
            {
                int a = canid;
                con.Open();
                SqlCommand check = new SqlCommand("Usp_FetchCandidateDetails", con);
                check.CommandType = CommandType.StoredProcedure;
                check.Parameters.AddWithValue("@candidateId", a);

                using (var reader = check.ExecuteReader())
                {

                    if (reader.HasRows)
                    {

                        do
                        {

                            while (reader.Read())
                                if (reader.GetString(1) != null)
                                {
                                    //Console.WriteLine(reader.GetString(1));
                                    retval2 = reader.GetString(1) + "|" + reader.GetString(2) + "|" + reader.GetString(3) + "|"
                                        + reader.GetString(4) + "|" + reader.GetString(5) + "|" + reader.GetString(6) + "|"
                                        + reader.GetString(7) + "|" + reader.GetDateTime(8) + "|" + reader.GetString(9) + "|"
                                        + reader.GetString(10) + "|" + reader.GetString(11) + "|" + reader.GetInt32(12) + "|"
                                        + reader.GetInt32(13) + "|" + reader.GetString(14) + "|" + reader.GetString(15) + "|"
                                        + reader.GetInt32(16) + "|" + reader.GetInt32(17) + "|" + reader.GetString(18) + "|"
                                        + reader.GetString(19) + "|" + reader.GetString(20) + "|" + reader.GetString(21) + "|"
                                        + reader.GetString(23) + "|" + reader.GetString(24) + "|" + reader.GetString(25) + "|"
                                        + reader.GetString(26) + "|" + reader.GetString(27) + "|" + reader.GetString(28) + "|"
                                        + reader.GetString(29);
                                }


                        }
                        while (reader.NextResult());

                        reader.Close();
                        con.Close();

                    }
                    return retval2;
                }
            }
        }

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;

        public CandidateRegistrationController(Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        public IActionResult CandidateRegisterationDetail( int id)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                string Bretval2 = this.FetchCandidateDetails(id);
                ViewBag.Vals2 = Bretval2;
                ViewBag.myst = id;
                return View();
                //return RedirectToAction("CandidateRegisterationDetail", new { canid = id });

            }


      
        }

        [HttpPost]
        public IActionResult CandidateRegisterationDetail(CandidateMaster candidate, IFormFile file,int  canid)
        {
            ViewData["btn"] = "Save";
            int a = canid;
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            //string FN = Path.GetFileName(file.FileName);
            //string path = Path.Combine("wwwroot/CandidateImages", FN);
            //using (FileStream fs = System.IO.File.Create(path))
            //{
            //    file.CopyTo(fs);
            //    fs.Flush();
            //}
            using (SqlConnection con = new SqlConnection(constring))
            {

                if (ViewData["btn"] == "Save")
                // if (Convert.ToInt32(candidate.CandidateId) > 0)
                {


                    con.Open();
                    SqlCommand conCommand = new SqlCommand("Usp_SaveCandidateDetails", con);
                    conCommand.CommandType = CommandType.StoredProcedure;
                    conCommand.Parameters.AddWithValue("@ActionType", "Insert");
                    conCommand.Parameters.AddWithValue("@candidateId", "");
                    conCommand.Parameters.AddWithValue("@candidatename", candidate.CandidateName);
                    conCommand.Parameters.AddWithValue("@gender", candidate.Gender);
                    conCommand.Parameters.AddWithValue("@marital_status", candidate.MaritalStatus);
                    conCommand.Parameters.AddWithValue("@dob", candidate.Dob);
                    conCommand.Parameters.AddWithValue("@candidatecontactnumber", candidate.CandidateContactNumber);
                    conCommand.Parameters.AddWithValue("@fathername", candidate.FatherName);
                    conCommand.Parameters.AddWithValue("@candidate_emailid", candidate.CandidateEmailId);
                    conCommand.Parameters.AddWithValue("@candidate_image", "");
                    conCommand.Parameters.AddWithValue("@myvar", "");
                
                    var reader = conCommand.ExecuteReader();

                    if (reader.HasRows)
                    {


                        while (reader.Read())
                        {

                            ViewBag.myst = reader.GetInt32(0);
                            //ViewBag.myst = reader.GetString(0);

                        }
                        ViewData["reason"] = "Data has been inserted";
                        
                        return View();

                    }

                }
                else
                {

                    con.Open();

                    SqlCommand conCommand = new SqlCommand("Usp_SaveCandidateDetails", con);
                    conCommand.CommandType = CommandType.StoredProcedure;
                    conCommand.Parameters.AddWithValue("@ActionType", "Update");
                    conCommand.Parameters.AddWithValue("@candidateId", candidate.CandidateId);
                    conCommand.Parameters.AddWithValue("@candidatename", candidate.CandidateName);
                    conCommand.Parameters.AddWithValue("@gender", candidate.Gender);
                    conCommand.Parameters.AddWithValue("@marital_status", candidate.MaritalStatus);
                    conCommand.Parameters.AddWithValue("@dob", candidate.Dob);
                    conCommand.Parameters.AddWithValue("@candidatecontactnumber", candidate.CandidateContactNumber);
                    conCommand.Parameters.AddWithValue("@fathername", candidate.FatherName);
                    conCommand.Parameters.AddWithValue("@candidate_emailid", candidate.CandidateEmailId);
                    conCommand.Parameters.AddWithValue("@candidate_image", "");
                    conCommand.Parameters.AddWithValue("@myvar", "");
                    SqlDataAdapter da = new SqlDataAdapter(conCommand);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    con.Close();
                }
            }
            
            ViewData["reason"] = "Data has been inserted";
            ViewBag.myst = canid;
            return View();

        }


        public IActionResult CandidateRegisterationDetailNext(int id)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                string Bretval2 = this.FetchCandidateDetails(id);
                ViewBag.Vals2 = Bretval2;
                ViewBag.myst = id;
                return View();
                //return RedirectToAction("CandidateRegisterationDetail", new { canid = id });

            }

        }

        [HttpPost]
        public IActionResult CandidateRegisterationDetailNext(CandidateMaster candidate, IFormFile file, int canid)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            using (SqlConnection con = new SqlConnection(constring))
            {
                int a = canid;
                con.Open();
                SqlCommand check = new SqlCommand("Usp_updatecandidatedetails", con);
                check.CommandType = CommandType.StoredProcedure;
                check.Parameters.AddWithValue("@candidateId", candidate.CandidateId);
                check.Parameters.AddWithValue("@EmergencyContactName", candidate.EmergencyContactName);
                check.Parameters.AddWithValue("@EmergencyContactNo", candidate.EmergencyContactNo);
                check.Parameters.AddWithValue("@EmergencyContactRelation", candidate.EmergencyContactRelation);

                check.ExecuteNonQuery();

                con.Close();

            }
            ViewData["reason"] = "Data has been inserted";
            ViewBag.myst = candidate.CandidateId;
            return View();
        }



        public IActionResult CandidateRegisterationDetailAddress(int id)
        {

            CandidateMaster emp = new CandidateMaster();
            emp.stateMasters = State();
            
            //StateId = 27;
            emp.cityMasters = City(0);
            if (id == 0)
            {
                return View(emp);
                //return View();
            }
            else
            {
                string Bretval2 = this.FetchCandidateDetails(id);
                ViewBag.Vals2 = Bretval2;
                ViewBag.myst = id;
                return View(emp);
                //return View();
                //return RedirectToAction("CandidateRegisterationDetail", new { canid = id });

            }

        }

      
        [HttpPost]
        public IActionResult CandidateRegisterationDetailAddress(CandidateMaster candidate, IFormFile file, int canid)
        {
           
            ViewData["btn"] = "Save";
            int a = canid;

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
          
            using (SqlConnection con = new SqlConnection(constring))
            {

                if (ViewData["btn"] == "Save")
                {
                    try
                    {
                        con.Open();
                        SqlCommand conCommand = new SqlCommand("UspCandidateAddressDetails", con);
                        conCommand.CommandType = CommandType.StoredProcedure;
                        //conCommand.Parameters.AddWithValue("@ActionType", "Update");
                        conCommand.Parameters.AddWithValue("@Candidate_Id", candidate.CandidateId);
                        conCommand.Parameters.AddWithValue("@Permanent_Address", candidate.PermanentAddress);
                        conCommand.Parameters.AddWithValue("@Permanent_State_ID", candidate.PermanentStateId);
                        conCommand.Parameters.AddWithValue("@Permanent_City_ID", candidate.PermanentCityId);
                        conCommand.Parameters.AddWithValue("@Permanent_PinCode", candidate.PermanentPinCode);
                        conCommand.Parameters.AddWithValue("@Current_Address", candidate.CurrentAddress);
                        conCommand.Parameters.AddWithValue("@Current_State_ID", candidate.CurrentStateId);
                        conCommand.Parameters.AddWithValue("@Current_City_ID", candidate.CurrentCityId);
                        conCommand.Parameters.AddWithValue("@Current_PinCode", candidate.CurrentPinCode);
                        conCommand.ExecuteNonQuery();
                        con.Close();
                        
                    }

                    catch (Exception ex)
                    {
                        return View("DatabaseError");
                        //ex.Message.ToString();
                    }
                }
                else
                {


                }
            }

            ViewData["reason"] = "Data has been inserted";
            ViewBag.myst = candidate.CandidateId;

            CandidateMaster emp = new CandidateMaster();
            emp.stateMasters = State();
          
            return View(emp);
         
        }

        public IActionResult CandidateRegisterationDetailQualification(int id)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                string Bretval2 = this.FetchCandidateEducation(id);
                ViewBag.Vals2 = Bretval2;
                ViewBag.myst = id;
                return View();
                //return RedirectToAction("CandidateRegisterationDetail", new { canid = id });

            }

        }

        [HttpPost]
        public IActionResult CandidateRegisterationDetailQualification(CandidateEducation candidate, IFormFile file, int canid)
        {
            ViewData["btn"] = "Save";

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            //string FN = Path.GetFileName(file.FileName);
            //string path = Path.Combine("wwwroot/CandidateImages", FN);
            //using (FileStream fs = System.IO.File.Create(path))
            //{
            //    file.CopyTo(fs);
            //    fs.Flush();
            //}
            using (SqlConnection con = new SqlConnection(constring))
            {
                int a = canid;

                if (ViewData["btn"] == "Save")
         
                {

                    try
                    {
                        con.Open();

                        SqlCommand conCommand = new SqlCommand("UspPGEducation", con);
                        conCommand.CommandType = CommandType.StoredProcedure;
                        conCommand.Parameters.AddWithValue("@Action", "Insert");
                        conCommand.Parameters.AddWithValue("@Candidate_Id", candidate.CandidateId);
                        conCommand.Parameters.AddWithValue("@PG_College_Name", candidate.PgCollegeName);
                        conCommand.Parameters.AddWithValue("@PG_University", candidate.PgUniversity);
                        conCommand.Parameters.AddWithValue("@PG_Course", candidate.PgStream);
                        conCommand.Parameters.AddWithValue("@PG_Year_of_Passing", candidate.PgYearOfPassing);
                        conCommand.Parameters.AddWithValue("@PG_PercentageCGPA", candidate.PgPercentageCgpa);
                        //conCommand.Parameters.AddWithValue("@PG_MarkSheet", candidate.PgMarkSheet);
                        //conCommand.Parameters.AddWithValue("@PG_DegreePassing_Certificate", FN);
                        conCommand.ExecuteNonQuery();

                        con.Close();
                       
                        ViewData["reason"] = "Data has been inserted";
                    }

                    catch (Exception ex)
                    {
                        return View("DatabaseError");
                        //ex.Message.ToString();
                    }


                }
                else
                {

                    con.Open();

                    SqlCommand conCommand = new SqlCommand("Usp_SaveCandidateDetails", con);
                    conCommand.CommandType = CommandType.StoredProcedure;
                    conCommand.Parameters.AddWithValue("@Action", "Update");
             
                    conCommand.Parameters.AddWithValue("@Candidate_Id", candidate.CandidateId);
                    conCommand.Parameters.AddWithValue("@PG_College_Name", candidate.PgCollegeName);
                    conCommand.Parameters.AddWithValue("@PG_University", candidate.PgUniversity);
                    conCommand.Parameters.AddWithValue("@PG_Course", candidate.PgCourse);
                    conCommand.Parameters.AddWithValue("@PG_Year_of_Passing", candidate.PgYearOfPassing);
                    conCommand.Parameters.AddWithValue("@PG_PercentageCGPA", candidate.PgPercentageCgpa);
                    //conCommand.Parameters.AddWithValue("@PG_MarkSheet", candidate.PgMarkSheet);
                    //conCommand.Parameters.AddWithValue("@PG_DegreePassing_Certificate", candidate.PgDegreePassingCertificate);
                    //conCommand.Parameters.AddWithValue("@candidate_image", FN);
                    SqlDataAdapter da = new SqlDataAdapter(conCommand);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    con.Close();

                    
                }
            }
            ViewData["reason"] = "Data has been inserted";
            ViewBag.myst = candidate.CandidateId;
            return View();
        }

        public IActionResult CandidateRegisterationDetail_QualificationUG(int id)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {

                string Bretval2 = this.FetchCandidateEducation(id);
                ViewBag.Vals2 = Bretval2;
                ViewBag.myst = id;
                return View();
                //return RedirectToAction("CandidateRegisterationDetail", new { canid = id });

            }

        }
        [HttpPost]
        public IActionResult CandidateRegisterationDetail_QualificationUG(CandidateEducation candidate, IFormFile file, int canid)
        {
            ViewData["btn"] = "Save";
            int a=  canid;
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            //string FN = Path.GetFileName(file.FileName);
            //string path = Path.Combine("wwwroot/CandidateImages", FN);
            //using (FileStream fs = System.IO.File.Create(path))
            //{
            //    file.CopyTo(fs);
            //    fs.Flush();
            //}

         
            using (SqlConnection con = new SqlConnection(constring))
            {

                if (ViewData["btn"] == "Save")
               
                {
                    try
                    {
                        con.Open();
                         SqlCommand conCommand = new SqlCommand("UspUGEducation", con);
                        conCommand.CommandType = CommandType.StoredProcedure;
                        conCommand.Parameters.AddWithValue("@Action", "Update");
                        conCommand.Parameters.AddWithValue("@Candidate_Id", candidate.CandidateId);
                        conCommand.Parameters.AddWithValue("@UG_College_Name", candidate.UgCollegeName);
                        conCommand.Parameters.AddWithValue("@UG_University", candidate.UgUniversity);
                        conCommand.Parameters.AddWithValue("@UG_Course", candidate.UgStream);
                        conCommand.Parameters.AddWithValue("@UG_Year_of_Passing", candidate.UgYearOfPassing);
                        conCommand.Parameters.AddWithValue("@UG_PercentageCGPA", candidate.UgPercentageCgpa);
                        //conCommand.Parameters.AddWithValue("@UG_MarkSheet", FN);
                        //conCommand.Parameters.AddWithValue("@UG_DegreePassing_Certificate", FN);
                        
                        conCommand.ExecuteNonQuery();
                        con.Close();
                        
                       
                    }

                    catch (Exception ex)
                    {
                         return View("DatabaseError");
                        //ex.Message.ToString();
                    }


                }
                else
                {

                }
            }

            ViewData["reason"] = "Data has been inserted";
            ViewBag.myst = candidate.CandidateId;
            return View();
        }

        public IActionResult CandidateRegisterationDetail_Qualification12( int id)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                
                string Bretval2 = this.FetchCandidateEducation(id);
                ViewBag.Vals2 = Bretval2;
                ViewBag.myst = id;
                return View();
                //return RedirectToAction("CandidateRegisterationDetail", new { canid = id });

            }

        }

        [HttpPost]
        public IActionResult CandidateRegisterationDetail_Qualification12(CandidateEducation candidate, IFormFile file, int canid)
        {
            ViewData["btn"] = "Save";

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            using (SqlConnection con = new SqlConnection(constring))
            {
                try
                {
                    int a = canid;

                    if (ViewData["btn"] == "Save")
                    // if (Convert.ToInt32(candidate.CandidateId) > 0)
                    {


                        con.Open();
                        SqlCommand conCommand = new SqlCommand("Usp12Education", con);
                        conCommand.CommandType = CommandType.StoredProcedure;
                        conCommand.Parameters.AddWithValue("@Action", "Update");
                        conCommand.Parameters.AddWithValue("@Candidate_Id", candidate.CandidateId);
                        conCommand.Parameters.AddWithValue("@12_Schoole_Name", candidate._12SchoolName);
                        conCommand.Parameters.AddWithValue("@12_Board_Name", candidate._12BoardName);
                        conCommand.Parameters.AddWithValue("@12_Course", candidate._12Stream);
                        conCommand.Parameters.AddWithValue("@12_Year_of_Passing", candidate._12YearOfPassing);
                        conCommand.Parameters.AddWithValue("@12_PercentageCGPA", candidate._12PercentageCgpa);
                        //conCommand.Parameters.AddWithValue("@12_MarkSheet", Marksheet);
                        //conCommand.Parameters.AddWithValue("@12_DegreePassing_Certificate", Degree);
                       
                        conCommand.ExecuteNonQuery();
                        con.Close();
                        
                    }
                    else
                    {


                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }



            }
            ViewData["reason"] = "Data has been inserted";
            ViewBag.myst = candidate.CandidateId;
            return View();
        }
        public IActionResult CandidateRegisterationDetail_Qualification10(int id)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                string Bretval2 = this.FetchCandidateEducation(id);
                
                ViewBag.Vals2 = Bretval2;
                ViewBag.myst = id;
                return View();
                //return RedirectToAction("CandidateRegisterationDetail", new { canid = id });

            }

        }
        [HttpPost]
        public IActionResult CandidateRegisterationDetail_Qualification10(CandidateEducation candidate, IFormFile file, int canid)
        {
            ViewData["btn"] = "Save";
            int a = canid;
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            using (SqlConnection con = new SqlConnection(constring))
            {
                try
                {

                    if (ViewData["btn"] == "Save")
                    // if (Convert.ToInt32(candidate.CandidateId) > 0)
                    {


                        con.Open();
                        SqlCommand conCommand = new SqlCommand("Usp10Education", con);
                        conCommand.CommandType = CommandType.StoredProcedure;
                        conCommand.Parameters.AddWithValue("@Action", "Update");
                        conCommand.Parameters.AddWithValue("@Candidate_Id", candidate.CandidateId);
                        conCommand.Parameters.AddWithValue("@10_Schoole_Name", candidate._10SchoolName);
                        conCommand.Parameters.AddWithValue("@10_Board_Name", candidate._10BoardName);
                        conCommand.Parameters.AddWithValue("@10_Course", candidate._10Stream);
                        conCommand.Parameters.AddWithValue("@10_Year_of_Passing", candidate._10YearOfPassing);
                        conCommand.Parameters.AddWithValue("@10_PercentageCGPA", candidate._10PercentageCgpa);
                        //conCommand.Parameters.AddWithValue("@10_MarkSheet", Marksheet);
                        //conCommand.Parameters.AddWithValue("@10_DegreePassing_Certificate", Degree);
                
                        conCommand.ExecuteNonQuery();
                        con.Close();
                      
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }



            }
            ViewData["reason"] = "Data has been inserted";
            ViewBag.myst = candidate.CandidateId;
            return View();
        }



        public IActionResult UploadFile(int id)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                string Bretval2 = this.FetchCandidateDetails(id);
                ViewBag.Vals2 = Bretval2;
                ViewBag.myst = id;
                return View();
                //return RedirectToAction("CandidateRegisterationDetail", new { canid = id });

            }

        }
        [HttpPost]
        public IActionResult UploadFile(IFormFileCollection file)
        {

            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.WebRootPath, "wwwroot/CandidateImages");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }


           // string path = Path.Combine("wwwroot/CandidateImages");

            List<string> uploadedFiles = new List<string>();

            foreach (IFormFile postedFile in file)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);

                }
            }



            //ViewBag.myst = candidate.CandidateId;
            return View();


        }


        //  End Here Testing

       

        private static List<StateMaster> State()
        {
            List<StateMaster> Statelist = new List<StateMaster>();
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "UspGetState";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter _sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataSet _dtSet = new DataSet();
                    _sqlDataAdapter.Fill(_dtSet);

                    con.Open();

                    for (int i = 0; i < _dtSet.Tables[0].Rows.Count; i++)
                    {
                        Statelist.Add(new StateMaster
                        {
                            StateId = (int)_dtSet.Tables[0].Rows[i]["State_Id"],
                            StateName = _dtSet.Tables[0].Rows[i]["State_Name"].ToString()
                        });
                    }


                    con.Close();
                }
            }

            return Statelist;
        }

        private static List<CityMaster> City(int StateId)
        {
            //StateId = 27;
            List<CityMaster> Citylist = new List<CityMaster>();
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "UspGetCity";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@State_Id", StateId);
                    SqlDataAdapter _sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataSet _dtSet = new DataSet();
                    _sqlDataAdapter.Fill(_dtSet);

                    con.Open();

                    for (int i = 0; i < _dtSet.Tables[0].Rows.Count; i++)
                    {
                        Citylist.Add(new CityMaster
                        {
                            CityId = (int)_dtSet.Tables[0].Rows[i]["City_Id"],
                            CityName = _dtSet.Tables[0].Rows[i]["City_Name"].ToString()
                        });
                    }

                    con.Close();
                }
            }

            return Citylist;
        }



        [HttpPost]
        public JsonResult GetCity(int StateId)
        {
            List<CityMaster> Citylist = new List<CityMaster>();
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "UspGetCity";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@State_Id", StateId);
                    SqlDataAdapter _sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataSet _dtSet = new DataSet();
                    _sqlDataAdapter.Fill(_dtSet);

                    con.Open();

                    for (int i = 0; i < _dtSet.Tables[0].Rows.Count; i++)
                    {
                        Citylist.Add(new CityMaster
                        {
                            CityId = (int)_dtSet.Tables[0].Rows[i]["City_Id"],
                            CityName = _dtSet.Tables[0].Rows[i]["City_Name"].ToString()
                        });
                    }

                    con.Close();
                }
            }

            return Json(Citylist);
        }

        public IActionResult CandidateRegisterationDetail_Employement()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CandidateRegisterationDetail_Employement(CandidateEmployement candidate, IFormFile file)
        {
            ViewData["btn"] = "Save";

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");



            using (SqlConnection con = new SqlConnection(constring))
            {
                try
                {

                    if (ViewData["btn"] == "Save")
                    // if (Convert.ToInt32(candidate.CandidateId) > 0)
                    {


                        con.Open();
                        SqlCommand conCommand = new SqlCommand("UspEmployeement", con);
                        conCommand.CommandType = CommandType.StoredProcedure;

                        conCommand.Parameters.AddWithValue("@Candidate_Id", candidate.CandidateId);
                        conCommand.Parameters.AddWithValue("@FresherExperienced", candidate.FresherExperienced);
                        conCommand.Parameters.AddWithValue("@Total_Experienced_Year", candidate.TotalExperiencedYear);
                        conCommand.Parameters.AddWithValue("@Total_Experienced_Month", candidate.TotalExperiencedMonth);
                        conCommand.Parameters.AddWithValue("@No_of_Company", candidate.NoOfCompany);



                        conCommand.ExecuteNonQuery();
                        con.Close();



                    }
                    else
                    {

                        //con.Open();

                        //SqlCommand conCommand = new SqlCommand("Usp10Education", con);
                        //conCommand.CommandType = CommandType.StoredProcedure;
                        //conCommand.Parameters.AddWithValue("@Action", "Update");
                        //conCommand.Parameters.AddWithValue("@Candidate_Id", candidate.CandidateId);
                        //conCommand.Parameters.AddWithValue("@10_Schoole_Name", candidate._10SchoolName);
                        //conCommand.Parameters.AddWithValue("@10_Board_Name", candidate._10BoardName);
                        //conCommand.Parameters.AddWithValue("@10_Course", candidate._10Course);
                        //conCommand.Parameters.AddWithValue("@10_Year_of_Passing", candidate._10YearOfPassing);
                        //conCommand.Parameters.AddWithValue("@10_PercentageCGPA", candidate._10PercentageCgpa);


                        //SqlDataAdapter da = new SqlDataAdapter(conCommand);
                        //DataSet ds = new DataSet();
                        //da.Fill(ds);
                        //con.Close();


                    }
                }
                catch (Exception ex)
                {
                    return View("DatabaseError");
                }



            }


            ViewData["reason"] = "Employement Details inserted successfully!!";
            return View();
        }



        public IActionResult CandidateRegisterationDetail_DocumentImage(int canid)
        {
            ViewBag.myst = canid;
            return View();
        }

        [HttpPost]
        public IActionResult CandidateRegisterationDetail_DocumentImage(CandidateEducation candidate, IFormFile file1, IFormFile file2, IFormFile file3, IFormFile file4, IFormFile file5, IFormFile file6, IFormFile file7, IFormFile file8, IFormFile file9, IFormFile file10, IFormFile file11)
        {
            ViewData["btn"] = "Save";

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            string FN = "";
            string FN1 = "";
            string FN2 = "";
            string FN3 = "";
            string FN4 = "";
            string FN5 = "";
            string FN6 = "";
            string FN7 = "";
            string FN8 = "";
            string FN9 = "";
            string FN10 = "";

            if (file1 == null)
            {
                FN = Path.GetFileName("Null");
            }
            else
            {
                FN = Path.GetFileName(file1.FileName);
                string path = Path.Combine("wwwroot/CandidateImages", FN);
                using (FileStream fs = System.IO.File.Create(path))
                {
                    file1.CopyTo(fs);
                    fs.Flush();
                }

            }

            if (file2 == null)
            {
                FN1 = Path.GetFileName("Null");
            }

            else
            {

                FN1 = Path.GetFileName(file2.FileName);
                string path1 = Path.Combine("wwwroot/CandidateImages", FN1);
                using (FileStream fs = System.IO.File.Create(path1))
                {
                    file2.CopyTo(fs);
                    fs.Flush();
                }
            }

            if (file3 == null)
            {
                FN2 = Path.GetFileName("Null");
            }
            else
            {


                FN2 = Path.GetFileName(file3.FileName);
                string path2 = Path.Combine("wwwroot/CandidateImages", FN2);
                using (FileStream fs = System.IO.File.Create(path2))
                {
                    file3.CopyTo(fs);
                    fs.Flush();
                }
            }
            if (file4 == null)
            {
                FN3 = Path.GetFileName("Null");
            }
            else
            {
                FN3 = Path.GetFileName(file4.FileName);
                string path3 = Path.Combine("wwwroot/CandidateImages", FN3);
                using (FileStream fs = System.IO.File.Create(path3))
                {
                    file4.CopyTo(fs);
                    fs.Flush();
                }
            }

            if (file5 == null)
            {
                FN4 = Path.GetFileName("Null");
            }
            else
            {
                FN4 = Path.GetFileName(file5.FileName);
                string path4 = Path.Combine("wwwroot/CandidateImages", FN4);
                using (FileStream fs = System.IO.File.Create(path4))
                {
                    file5.CopyTo(fs);
                    fs.Flush();
                }
            }



            if (file6 == null)
            {
                FN5 = Path.GetFileName("Null");
            }
            else
            {
                FN5 = Path.GetFileName(file6.FileName);
                string path5 = Path.Combine("wwwroot/CandidateImages", FN5);
                using (FileStream fs = System.IO.File.Create(path5))
                {
                    file6.CopyTo(fs);
                    fs.Flush();
                }
            }
            if (file7 == null)
            {
                FN6 = Path.GetFileName("Null");
            }
            else
            {

                FN6 = Path.GetFileName(file7.FileName);
                string path6 = Path.Combine("wwwroot/CandidateImages", FN6);
                using (FileStream fs = System.IO.File.Create(path6))
                {
                    file7.CopyTo(fs);
                    fs.Flush();
                }

            }

            if (file8 == null)
            {
                FN7 = Path.GetFileName("Null");
            }
            else
            {
                FN7 = Path.GetFileName(file8.FileName);
                string path7 = Path.Combine("wwwroot/CandidateImages", FN7);
                using (FileStream fs = System.IO.File.Create(path7))
                {
                    file8.CopyTo(fs);
                    fs.Flush();
                }
            }

            if (file9 == null)
            {
                FN8 = Path.GetFileName("Null");
            }
            FN8 = Path.GetFileName(file9.FileName);
            string path8 = Path.Combine("wwwroot/CandidateImages", FN8);
            using (FileStream fs = System.IO.File.Create(path8))
            {
                file9.CopyTo(fs);
                fs.Flush();
            }
            if (file10 == null)
            {
                FN9 = Path.GetFileName("Null");
            }
            else
            {

                FN9 = Path.GetFileName(file10.FileName);
                string path9 = Path.Combine("wwwroot/CandidateImages", FN9);
                using (FileStream fs = System.IO.File.Create(path8))
                {
                    file10.CopyTo(fs);
                    fs.Flush();
                }

            }
            if (file11 == null)
            {
                FN10 = Path.GetFileName("Null");
            }
            else
            {

                FN10 = Path.GetFileName(file11.FileName);
                string path10 = Path.Combine("wwwroot/CandidateImages", FN10);
                using (FileStream fs = System.IO.File.Create(path8))
                {
                    file11.CopyTo(fs);
                    fs.Flush();
                }
            }



            using (SqlConnection con = new SqlConnection(constring))
            {
                try
                {

                    if (ViewData["btn"] == "Save")
                    // if (Convert.ToInt32(candidate.CandidateId) > 0)
                    {


                        con.Open();
                        SqlCommand conCommand = new SqlCommand("UspUploaded", con);
                        conCommand.CommandType = CommandType.StoredProcedure;
                        conCommand.Parameters.AddWithValue("@Candidate_Id", candidate.CandidateId);
                        conCommand.Parameters.AddWithValue("@10_MarkSheet", FN);
                        conCommand.Parameters.AddWithValue("@10_Degree", FN1);
                        conCommand.Parameters.AddWithValue("@12_MarkSheet", FN2);
                        conCommand.Parameters.AddWithValue("@12_Degree", FN3);
                        conCommand.Parameters.AddWithValue("@UG_MarkSheet", FN4);
                        conCommand.Parameters.AddWithValue("@UG_Degree", FN5);
                        conCommand.Parameters.AddWithValue("@PG_MarkSheet", FN6);
                        conCommand.Parameters.AddWithValue("@PG_Degree", FN7);
                        conCommand.Parameters.AddWithValue("@Aadhar_Image", FN8);
                        conCommand.Parameters.AddWithValue("@PanCard_Image", FN9);
                        conCommand.Parameters.AddWithValue("@Candidate_Image", FN10);
                        conCommand.ExecuteNonQuery();
                        con.Close();





                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    return View("DatabaseError");
                }



            }
            ViewBag.myst = candidate.CandidateId;
            ViewData["reason"] = "Image has been uploaded Successfully";

            return View();
        }


        public ActionResult AllCandidateDetails(int id)
        {
            var retval0 = "";
            var retval1 = "";
            var retval2 = "";

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (SqlConnection con = new SqlConnection(constring))
            {
                //var id = HttpContext.Session.GetString("CUser");
                con.Open();
                SqlCommand check = new SqlCommand("Usp_FetchAllCandidateData", con);
                check.CommandType = CommandType.StoredProcedure;
                check.Parameters.AddWithValue("@candidateID", "1076");

                using (var reader = check.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())// Iterate through Candidate Master Table
                            if (reader.GetString(1) != null)
                            {
                                //Console.WriteLine(reader.GetString(1));
                                retval0 = reader.GetInt32(0) + "|" + reader.GetString(1) + "|" + reader.GetString(2) + "|" + reader.GetString(3) + "|" +
                                    reader.GetString(4) + "|" + reader.GetString(5) + "|" + reader.GetString(6) + "|" +
                                    reader.GetString(7) + "|" + reader.GetDateTime(8) + "|" + reader.GetString(9) + "|" +
                                    reader.GetString(10) + "|" + reader.GetString(11) + "|" + reader.GetInt32(12) + "|" +
                                    reader.GetString(13) + "|" + reader.GetInt32(14) + "|" + reader.GetString(15) + "|" +
                                    reader.GetString(16) + "|" + reader.GetString(17) + "|" + reader.GetInt32(18) + "|" +
                                    reader.GetString(19) + "|" + reader.GetInt32(20) + "|" + reader.GetString(21) + "|" +
                                    reader.GetString(22) + "|" + reader.GetString(23) + "|" + reader.GetString(24) + "|" +
                                    reader.GetString(25) + "|" + reader.GetString(26) + "|" + reader.GetString(27) + "|" +
                                    reader.GetString(28) + "|" + reader.GetString(33) + "|" + reader.GetString(34) + "|" +
                                    reader.GetString(35) + "|" + reader.GetString(36) + "|" + reader.GetString(37);
                            }

                        reader.NextResult();

                        while (reader.Read())// Iterate through Candidate Education Table
                            if (reader.GetInt32(0) != null)
                            {
                                //Console.WriteLine(reader.GetString(1));
                                retval1 = reader.GetInt32(0) + "|" + reader.GetString(1) + "|" + reader.GetString(2) + "|" + reader.GetString(3) + "|" +
                                    reader.GetString(4) + "|" + reader.GetString(5) + "|" + reader.GetString(6) + "|" +
                                    reader.GetString(7) + "|" + reader.GetString(8) + "|" + reader.GetString(9) + "|" +
                                    reader.GetString(10) + "|" + reader.GetString(11) + "|" + reader.GetString(12) + "|" +
                                    reader.GetString(13) + "|" + reader.GetString(14) + "|" + reader.GetString(15) + "|" +
                                    reader.GetString(16) + "|" + reader.GetString(17) + "|" + reader.GetString(18) + "|" +
                                    reader.GetString(19) + "|" + reader.GetString(20) + "|" + reader.GetString(21) + "|" +
                                    reader.GetString(22) + "|" + reader.GetString(23) + "|" + reader.GetString(24) + "|" +
                                    reader.GetString(25) + "|" + reader.GetString(26) + "|" + reader.GetString(27) + "|" +
                                    reader.GetString(28);

                            }

                        reader.NextResult();

                        while (reader.Read())// Iterate through Candidate Employement Table
                            if (reader.GetInt32(0) != null)
                            {
                                //Console.WriteLine(reader.GetString(1));
                                retval2 = reader.GetInt32(0) + "|" + reader.GetString(1) + "|" + reader.GetInt32(2) + "|" + reader.GetInt32(3) + "|" + reader.GetInt32(4);

                            }

                    }
                }
            }

            @ViewBag.Vals0 = retval0;
            @ViewBag.Vals1 = retval1;
            @ViewBag.Vals2 = retval2;
            return View();
        }


    }
}
