using Microsoft.AspNetCore.Mvc;
using Models.Request;
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
    [Route("changing")]
    public class ChangingOwnerDataController : Controller
    {
        private IChangingOwnerDataService _changingOwnerDataService;

        public ChangingOwnerDataController(IChangingOwnerDataService changingOwnerDataService)
        {
            _changingOwnerDataService = changingOwnerDataService;
        }

        [HttpPost]
        [Route("owner_data")]
        [OwnerAuthorization]
        public IActionResult ChangeOwnerData(ChangingOwnerData ownerData)
        {
            int id;
            RegistrationResponse response = null;
            if (HttpContext.Request.Cookies.TryGetValue("currentOwner", out string value))
            {
                if (int.TryParse(value, out id))
                {
                    response = _changingOwnerDataService.ChangeData(ownerData, id);
                }
            }

            if(response == null)
            {
                return BadRequest(new { message = "Не удалось найти ифнормацию"});
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("owner_password")]
        [OwnerAuthorization]
        public IActionResult ChangeOwnerPassword(ChangingOwnerPassword ownerData)
        {
            int id;
            RegistrationResponse response = null;
            if (HttpContext.Request.Cookies.TryGetValue("currentOwner", out string value))
            {
                if (int.TryParse(value, out id))
                {
                    response = _changingOwnerDataService.ChangePassword(ownerData, id);
                }
            }

            if (response == null)
            {
                return BadRequest(new { message = "Не удалось найти ифнормацию" });
            }

            return Ok(response);
        }
    }
}
