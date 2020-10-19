using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System.Diagnostics;

namespace AppInsights.EfCore.Domain
{
    public class Student
    {
        private readonly TelemetryClient telemetryClient;
        public int StudentId { get; set; }
        public string Name { get; set; }

        public Student()
        {

        }

        public Student(TelemetryClient client)
        {
            this.telemetryClient = client;
        }

        private Student(SchoolContext context)
        {
            this.telemetryClient = context.AppInsightsClient;
        }

        public string SayHello()
        {
            Activity getActivity = new Activity("Student is saying Hello");
            var telemetryOperation = this.telemetryClient.StartOperation<RequestTelemetry>(getActivity);

            this.telemetryClient.StopOperation(telemetryOperation);
            return $"Hello {Name}";
        }
    }
}
