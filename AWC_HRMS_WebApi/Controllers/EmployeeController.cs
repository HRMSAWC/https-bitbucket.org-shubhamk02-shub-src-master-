using AWC_HRMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata;
using Dapper;
using Microsoft.EntityFrameworkCore;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AWC_HRMS_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private IEnumerable<EmployeeMaster> getEmployeeDetails;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;

        }

        //// GET: api/<EmployeeController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<EmployeeController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<EmployeeController>
        //[NonAction]
        //[HttpPost]
        //public IActionResult Employee([FromBody] EmpModel ObjClass)
        //{
        //    var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
        //    IConfiguration configuration = builder.Build();
        //    string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
        //    int i;

        //    if (ObjClass == null)
        //    {
        //        return BadRequest();

        //    }
        //    else
        //    {
        //        using (SqlConnection con = new SqlConnection(constring))
        //        {
        //            con.Open();
        //            SqlCommand check = new SqlCommand("Usp_GetEmployeeDetails", con);
        //            check.CommandType = CommandType.StoredProcedure;
        //            check.Parameters.AddWithValue("@Employee_Id", ObjClass.EmpId);
        //            //check.Parameters.AddWithValue("@Password", ObjClass.Password);
        //            using (var reader = check.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    i = Convert.ToInt32(reader["Result"]);
        //                    if (i == 0)
        //                    {
        //                        return NotFound();
        //                    }
        //                    else
        //                    {
        //                        HttpContext.Session.SetString("CUser", Convert.ToString(ObjClass.EmpId));
        //                        return Ok();

        //                    }

        //                }
        //            }

        //        }

        //    }

        //             return Ok();
        //    }

        
        [HttpGet]
        public async Task<IEnumerable<EmployeeMaster>> GetEmployeeDetails([FromBody] EmpModel objClass)
        {
            var employeeDetails = new List<EmployeeMaster>();
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (SqlConnection con = new SqlConnection(constring))
            { 
                con.Open();
                var procedureName = "Usp_GetEmployeeDetails";
                var parameters = new DynamicParameters();
                parameters.Add("@Employee_Id", objClass.EmpId, DbType.String, ParameterDirection.Input);
               
                var result= await con.QueryAsync<EmployeeMaster>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                if(result!=null)
                {
                    List<EmployeeMaster> item=result.Cast<EmployeeMaster>().ToList();
                    if(item.Count>0)
                    {
                        return item;
                    }
                }
            }
            return getEmployeeDetails;
                
        }
    }

        

        // PUT api/<EmployeeController>/5
       // [HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<EmployeeController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}


    
}
