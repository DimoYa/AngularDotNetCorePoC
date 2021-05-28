using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyEdo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEdo.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," +
                          GlobalConstants.ResourceRoleName)]
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
    }
}
