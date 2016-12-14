using BusinessLogicLayer.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/conversations")]
    public class ConversationsController : ApiController
    {
        private IConversationService _conversationService;
        private ModelFactory _modelFactory;
        public ConversationsController(IConversationService conversationService, ModelFactory modelFactory)
        {
            _conversationService = conversationService;
            _modelFactory = modelFactory;
        }

        [Route("")]
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                var conversations = _conversationService.GetConversations(User.Identity.GetUserId());
                if (conversations != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, conversations);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Conversations not found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }       
        }
        public class FormattedDbEntityValidationException : Exception
        {
            public FormattedDbEntityValidationException(DbEntityValidationException innerException) :
                base(null, innerException)
            {
            }

            public override string Message
            {
                get
                {
                    var innerException = InnerException as DbEntityValidationException;
                    if (innerException != null)
                    {
                        StringBuilder sb = new StringBuilder();

                        foreach (var failure in innerException.EntityValidationErrors)
                        {
                            sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                            foreach (var error in failure.ValidationErrors)
                            {
                                sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                                sb.AppendLine();
                            }
                        }

                        throw new DbEntityValidationException(
                            "Entity Validation Failed - errors follow:\n" +
                            sb.ToString(), innerException
                        ); 
                    }

                    return base.Message;
                }
            }
        }
    }
}
