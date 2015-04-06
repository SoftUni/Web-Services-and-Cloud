namespace Services.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;

    using Services.Models;
    using Services.Repositories;

    public class StudentsController : ApiController
    {
        private IRepository<Student> repository =
            new XmlStudentsRepository(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/students.xml"));

        [HttpGet]
        public IEnumerable<Student> GetAll()
        {
            var students = this.repository.GetAll();
            return students;
        }

        [HttpGet]
        public Student GetSingle(int id)
        {
            var student = this.repository.GetById(id);
            return student;
        }

        [HttpPost]
        public HttpResponseMessage PostStudent(Student newStudent)
        {
            var student = this.repository.Add(newStudent);
            var response = Request.CreateResponse<Student>(HttpStatusCode.Created, student);
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = student.Id }));
            return response;
        }
    }
}