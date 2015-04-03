namespace EmptyWebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    using EmptyWebApi.Models;

    public class StudentsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetStudent()
        {
            var students = new List<StudentDataModel>()
            {
                new StudentDataModel
                    {
                        Name = "Picho Penchev",
                        BirthDate = new DateTime(1992, 2, 13),
                        Username = "Pichop"
                    },
                    new StudentDataModel {Name = "Picho Penchev", BirthDate = new DateTime(1992, 2, 13), Username = "Pichop" },
                    new StudentDataModel {Name = "Michka Michekva", BirthDate = new DateTime(1995, 5, 23), Username = "Micheto" }
            };

            return this.Ok(students);
        }
    }
}