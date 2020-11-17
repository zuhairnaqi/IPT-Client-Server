using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;

namespace ubit.ipt.server
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        public static List<StudentModel> studentsList = new List<StudentModel>();

        [WebMethod]
        public string getStudentsList()
        {
            return JsonConvert.SerializeObject(studentsList);
        }

        [WebMethod]
        public void addStudent()
        {
            string studentData = HttpContext.Current.Request.Params["request"];
            StudentModel student = JsonConvert.DeserializeObject<StudentModel>(studentData);
            studentsList.Add(student);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string calculatePercentage()
        {
            string request = HttpContext.Current.Request["request"];
            StudentModel studentRequest = JsonConvert.DeserializeObject<StudentModel>(request);
            decimal totalMarks = 100 * studentRequest.subjectNum;
            decimal ObtainedMarks = studentRequest.subjectsDetail.Sum(x => x.marks);
            decimal percentage = ObtainedMarks / totalMarks * 100;
            SubjectsDetail minSubject = studentRequest.subjectsDetail[0];
            SubjectsDetail maxSubject = studentRequest.subjectsDetail[0];
            for(int i = 1; i < studentRequest.subjectNum; i++)
            {
                if (studentRequest.subjectsDetail[i].marks < minSubject.marks)
                {
                    minSubject = studentRequest.subjectsDetail[i];
                }
                else if (studentRequest.subjectsDetail[i].marks > maxSubject.marks)
                {
                    maxSubject = studentRequest.subjectsDetail[i];
                }
            }
            ResponseData data = new ResponseData()
            {
                totalMarks = totalMarks,
                ObtainedMarks = ObtainedMarks,
                percentage = percentage,
                minSubject = minSubject,
                maxSubject = maxSubject,
            };
            string str = JsonConvert.SerializeObject(data);
            return str;
        }

        public class StudentModel
        {
            public string studentName { get; set; }
            public int subjectNum { get; set; }
            [JsonProperty("subjectsDetail")]
            public SubjectsDetail[] subjectsDetail { get; set; }
        }

        public class SubjectsDetail
        {
            public string name { get; set; }
            public int marks { get; set; }

        }

        public class ResponseData {
            public decimal totalMarks { get; set; }
            public decimal ObtainedMarks { get; set; }
            public decimal percentage { get; set; }
            public SubjectsDetail minSubject { get; set; }
            public SubjectsDetail maxSubject { get; set; }
        }
    }
}
