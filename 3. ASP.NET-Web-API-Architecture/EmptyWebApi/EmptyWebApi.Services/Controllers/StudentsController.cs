namespace EmptyWebApi.Services.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    using EmptyWebApi.Services.Models;

    public class StudentsController : ApiController
    {
        private List<StudentDataModel> students;

        public StudentsController()
        {
            this.students = new List<StudentDataModel>()
            {
                new StudentDataModel 
                { 
                    Username = "Mincho", 
                    BirthDate = new DateTime(1994, 10, 21), 
                    Email = "mincho@abv.bg",
                    FullName = "Mincho Michov"
                },
                new StudentDataModel 
                { 
                    Username = "Michka", 
                    BirthDate = new DateTime(1995, 1, 13), 
                    Email = "micheto@abv.bg",
                    FullName = "Michka Machkava"
                },
                new StudentDataModel 
                { 
                    Username = "Mindo", 
                    BirthDate = new DateTime(1999, 5, 18), 
                    Email = "mindo@abv.bg",
                    FullName = "Mindo Mryvkata"
                },
            };
        }

        public IHttpActionResult GetStudents()
        {
            return this.Ok(this.students);
        }
    }
}