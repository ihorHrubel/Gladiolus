using BusinessLogicLayer.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/profiles")]
    public class UserProfilesController : ApiController
    {
        private IUserProfileService _userProfileService;
        private ModelFactory _modelFactory;
        public UserProfilesController(IUserProfileService userProfileService , ModelFactory modelFactory)
        {
            _userProfileService = userProfileService;
            _modelFactory = modelFactory;
        }
        [Route("~/api/myprofile")]
        public IHttpActionResult GetMyProfile()
        {
            try
            {
                var user = _userProfileService.GetUserProfile(User.Identity.GetUserId());
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var users = _userProfileService.GetUserProfiles();
                if (users != null)
                {
                    return Ok(users.AsQueryable());
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("{id}")]
        public IHttpActionResult Get(string id)
        {
            try
            {
                var user = _userProfileService.GetUserProfile(id);
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("~/api/myprofile")]
        [HttpPost]
        public IHttpActionResult UpdateMyProfile(UpdateUserProfileModel updateUserProfileModel)
        {
            try
            {
                if (updateUserProfileModel != null )
                {
                    var userProfileDto = _modelFactory.Parse(updateUserProfileModel);
                    if(_userProfileService.UpdateUserProfile(User.Identity.GetUserId(), userProfileDto))
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("UserProfile not updated");
                    }                    
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
