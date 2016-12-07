using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/messages")]
    public class MessagesController : ApiController
    {
        private IMessageService _messageService;
        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
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

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Create([FromBody] MessageDTO messageDto)
        {
            try
            {
                if (messageDto != null)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, _messageService.CreateMessage(messageDto));
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Messsages not found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException);
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
