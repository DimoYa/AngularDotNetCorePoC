using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyEdo.Core.Common;

namespace MyEdo.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," +
                          GlobalConstants.ResourceRoleName)]
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
    }
}
