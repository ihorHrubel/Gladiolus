using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("api/zabchukcontroller")]
    public class DefaultController : ApiController
    {
        public List<string> list;
        public DefaultController()
        {
            list = new List<string>();
            list.Add("Volodya");
            list.Add("Nazar");
        }

        // GET: api/Default
        [Route("")]
        public object Get()
        {
            return list;
        }

        // POST: api/Default
        [Route("")]
        public string Post([FromBody]string value)
        {
            list.Add(value);
            return value;
            /*try
            {
                if (value != null)
                {
                    list.Add(value);
                    return Request.CreateResponse(HttpStatusCode.Created, value);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Messsages not found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException);
            }*/
        }
    }
}
