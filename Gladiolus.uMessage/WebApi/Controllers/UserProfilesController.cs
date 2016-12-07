using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/userprofiles")]
    public class UserProfilesController : BaseApiController
    {
        private IUserProfileService _userProfileService;
        public UserProfilesController(IUserProfileService userProfileService , ModelFactory modelFactory)
        {
            _userProfileService = userProfileService;
        }
        [Route("")]
        public HttpResponseMessage Get()
        {
            try
            {
                var users = _userProfileService.GetUserProfiles();
                if (users != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, users);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Users not found");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }                   
        }

        [Route("{id}")]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                var user = _userProfileService.GetUserProfile(id);
                if (user != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [Route("")]
        public HttpResponseMessage Put([FromBody] UpdateUserProfileModel updateUserProfileModel)
        {
            try
            {
                if (updateUserProfileModel != null )
                {
                    var userProfileDto = TheModelFactory.Parse(updateUserProfileModel);
                    if(_userProfileService.UpdateUserProfile(User.Identity.GetUserId(), userProfileDto))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "UserProfile not updated");
                    }                    
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User is empty");
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
