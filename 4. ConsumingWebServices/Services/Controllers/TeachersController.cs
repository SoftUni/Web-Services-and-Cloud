using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Services.Controllers
{
    using System.Web;

    using Services.Models;
    using Services.Repositories;

    public class TeachersController : ApiController
    {
        private IRepository<Student> repository =
            new XmlStudentsRepository(HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/students.xml"));

        // GET: api/Teachers
        public IEnumerable<Student> Get()
        {
            var students = this.repository.GetAll();
            return students;
        }

        // GET: api/Teachers/5
        public Student Get(int id)
        {
            var student = this.repository.GetById(id);
            return student;
        }

        // POST: api/Teachers
        public IHttpActionResult Post([FromBody]Student newStudent)
        {
            var student = this.repository.Add(newStudent);

            return this.Created(new Uri(Url.Link("api", new { id = student.Id })), student);
        }

        // PUT: api/Teachers/5
        public IHttpActionResult Put(int id, [FromBody]string firstName, [FromBody]string lastName)
        {
            var student = this.repository.GetById(id);
            student.FirstName = firstName;
            student.LastName = lastName;
            this.repository.Update(student);

            return this.Ok(student);
        }

        // DELETE: api/Teachers/5
        public IHttpActionResult Delete(int id)
        {
            var student = this.repository.GetById(id);
            this.repository.Delete(student);
            return this.Ok();
        }
    }
}
