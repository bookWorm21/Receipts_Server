using Microsoft.AspNetCore.Mvc;
using Models.Response;
using Receipts_Server.Helpers.Attributes;
using Receipts_Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receipts_Server.Controllers
{
    [ApiController]
    [Route("info")]
    public class InfoOwnerController : Controller
    {
        private IOwnerInfoService _ownerInfoService;

        public InfoOwnerController(IOwnerInfoService ownerInfoService)
        {
            _ownerInfoService = ownerInfoService;
        }

        [HttpGet]
        [Route("get_owner_info")]
        [OwnerAuthorization]
        public IActionResult GetOwnerInfo()
        {
            int id;
            OwnerInfo owner = null;
            if (HttpContext.Request.Cookies.TryGetValue("currentOwner", out string value))
            {
                if (int.TryParse(value, out id))
                {
                    owner = _ownerInfoService.GetOwnerInfo(id);
                }
            }

            if(owner == null)
            {
                return BadRequest(new { message = "Не удалось найти ифнормацию"});
            }

            return Ok(owner);
        }

        [HttpGet]
        [Route("get_owner_properties")]
        [OwnerAuthorization]
        public IActionResult GetOwnerPropeties()
        {
            int id;
            PropertiesInfo[] properties = null;
            if (HttpContext.Request.Cookies.TryGetValue("currentOwner", out string value))
            {
                if (int.TryParse(value, out id))
                {
                    properties = _ownerInfoService.GetOwnerProperties(id);
                }
            }

            if (properties == null)
            {
                return BadRequest(new { message = "Не удалось найти ифнормацию" });
            }

            return Ok(properties);
        }
    }
}