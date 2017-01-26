using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class StudentController : ApiController
    {
        private ODbContext db = new ODbContext();

        // GET api/Student
        public IEnumerable<Student> GetStudents()
        {
            return db.students.AsEnumerable();
        }

        // GET api/Student/5
        public Student GetStudent(int id)
        {
            Student student = db.students.Find(id);
            if (student == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return student;
        }

        // PUT api/Student/5
        public HttpResponseMessage PutStudent(int id, Student student)
        {
            if (ModelState.IsValid && id == student.id)
            {
                db.Entry(student).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Student
        public HttpResponseMessage PostStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                db.students.Add(student);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, student);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = student.id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Student/5
        public HttpResponseMessage DeleteStudent(int id)
        {
            Student student = db.students.Find(id);
            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.students.Remove(student);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, student);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}