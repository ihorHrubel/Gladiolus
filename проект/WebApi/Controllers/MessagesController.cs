using BusinessLogicLayer.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Hubs;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/messages")]
    public class MessagesController : ApiControllerWithHub<ChatHub>
    {
        private IMessageService _messageService;
        private ModelFactory _modelFactory;
        public MessagesController(IMessageService messageService , ModelFactory modelFactory)
        {
            _messageService = messageService;
            _modelFactory = modelFactory;
        }

        [Route("{conversationid}")]
        public HttpResponseMessage Get(string conversationId)
        {
            try
            {
                var messages = _messageService.GetMessages(conversationId);
                if (messages != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,messages);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Messages not found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

       

        [Route("{id}")]
        public HttpResponseMessage Delete(string id)
        {
            try
            {
                if (_messageService.DeleteMessage(id))
                {
                   
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
