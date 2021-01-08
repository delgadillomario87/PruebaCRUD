using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using APITesting.Models;
using Newtonsoft.Json;

namespace APITesting.Controllers
{
    [RoutePrefix("api/PersonController")]
    public class PersonController : ApiController
    {
        BDModel bd = new BDModel();

        [HttpGet]
        [Route("getPerson/{personId}")]
        public HttpResponseMessage getPerson(int personId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                if (personId == 0)
                {
                    var person = bd.Person.Select
                    (x => new
                    {
                        x.personId,
                        x.name,
                        x.lastName,
                        x.Gender.genderId,
                        x.Gender.description
                    }).ToList();
                    if (person.Count > 0)
                    {
                        var OkResponse = new { code = HttpStatusCode.OK, message = "OK", response = person };
                        response.Content = new StringContent(JsonConvert.SerializeObject(OkResponse), Encoding.UTF8, "application/json");
                    }
                    else
                    {
                        var FailResponse = new { code = HttpStatusCode.NoContent, message = "No se encontraron datos", response = person };
                        response.Content = new StringContent(JsonConvert.SerializeObject(FailResponse), Encoding.UTF8, "application/json");
                    }
                }
                else
                {
                    var person = bd.Person.Select
                     (x => new
                     {
                         x.personId,
                         x.name,
                         x.lastName,
                         x.Gender
                     }).Where(x => x.personId == personId).ToList();
                    if (person.Count > 0)
                    {
                        var OkResponse = new { code = HttpStatusCode.OK, message = "OK", response = person };
                        response.Content = new StringContent(JsonConvert.SerializeObject(OkResponse), Encoding.UTF8, "application/json");
                    }
                    else
                    {
                        var FailResponse = new { code = HttpStatusCode.NoContent, message = "No se encontraron datos", response = person };
                        response.Content = new StringContent(JsonConvert.SerializeObject(FailResponse), Encoding.UTF8, "application/json");
                    }
                }
            }
            catch (Exception ex)
            {
                var BadResponse = new { code = HttpStatusCode.BadRequest, message = ex.Message, response = "null" };
                response.Content = new StringContent(JsonConvert.SerializeObject(BadResponse), Encoding.UTF8, "application/json");
            }
            return response;
        }
        [HttpPost]
        [Route("savePerson")]
        public HttpResponseMessage savePerson(Person p)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                if (!ModelState.IsValid)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }

                if (bd.Person.ToList().Where(x => x.personId == p.personId).ToList().Count == 0)
                {
                    bd.Person.Add(p);
                    bd.SaveChanges();
                    var OkResponse = new
                        {code = HttpStatusCode.Created, message = "Registro añadido con exito", response = p};
                    response.Content = new StringContent(JsonConvert.SerializeObject(OkResponse), Encoding.UTF8,
                        "application/json");
                    return response;
                }
                else
                {
                    var person = bd.Person.Find(p.personId);
                    bd.Entry(person).CurrentValues.SetValues(p);
                    bd.Entry(person).State = EntityState.Modified;
                    bd.SaveChanges();

                    var OkResponse = new
                    {
                        code = HttpStatusCode.OK, message = "Registro actualizado con exito",
                        response = new {p.personId, p.name, p.lastName, p.genderId}
                    };
                    response.Content = new StringContent(JsonConvert.SerializeObject(OkResponse), Encoding.UTF8,
                        "application/json");
                }
            }
            catch (Exception ex)
            {
                var BadResponse = new {code = HttpStatusCode.BadRequest, message = ex.Message, response = "null"};
                response.Content = new StringContent(JsonConvert.SerializeObject(BadResponse), Encoding.UTF8,
                    "application/json");
            }
            return response;
        }

        [HttpGet]
        [Route("getGender")]
        public HttpResponseMessage getGender()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                var gender = bd.Gender.Select
                (x => new
                {
                    x.genderId,
                    x.description
                }).ToList();
                if (gender.Count > 0)
                {
                    var OkResponse = new { code = HttpStatusCode.OK, message = "OK", response = gender };
                    response.Content = new StringContent(JsonConvert.SerializeObject(OkResponse), Encoding.UTF8, "application/json");
                }
                else
                {
                    var FailResponse = new { code = HttpStatusCode.NoContent, message = "No se encontraron datos", response = gender };
                    response.Content = new StringContent(JsonConvert.SerializeObject(FailResponse), Encoding.UTF8, "application/json");
                }

            }
            catch (Exception ex)
            {
                var BadResponse = new { code = HttpStatusCode.BadRequest, message = ex.Message, response = "null" };
                response.Content = new StringContent(JsonConvert.SerializeObject(BadResponse), Encoding.UTF8, "application/json");
            }
            return response;
        }
    }
}
