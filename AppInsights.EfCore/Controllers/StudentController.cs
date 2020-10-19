using AppInsights.EfCore.Domain;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AppInsights.EfCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly TelemetryClient client;
        private readonly SchoolContext schoolContext;

        public StudentController(TelemetryClient client, SchoolContext context)
        {
            this.client = client;
            this.schoolContext = context;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            Activity getActivity = new Activity("Get Student Called");
            var telemetryOperation = this.client.StartOperation<RequestTelemetry>(getActivity);
            var student = this.schoolContext.Students.Where(x => x.Name == "Mayank").FirstOrDefault();
            if (student == null)
            {
                student = new Student(client);
                student.Name = "Mayank";
                student.StudentId = 1;
                this.schoolContext.Students.Add(student);
                await this.schoolContext.SaveChangesAsync();
            }

            this.client.StopOperation(telemetryOperation);
            return student.SayHello();
        }
    }
}
