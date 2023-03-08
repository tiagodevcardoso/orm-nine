using Microsoft.AspNetCore.Mvc;
using ORM.Nine.Database.Configurations;
using ORM.Nine.Database.Models.Input;
using ORM.Nine.Database.Models.Output;

namespace ORM.Nine.Database.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EntityController : ControllerBase
    {
        private readonly IContextServices _contextServices;

        public EntityController(IContextServices contextServices)
        {
            _contextServices = contextServices;
        }

        [HttpGet(Name = "GetEntity")]
        public JsonReturn Get([FromBody] InputApiParameters _p)
        {
            return _contextServices.SelectJson(_p.Procedure, _p.Table, _p.NumberPage, _p.Search, _p.Sort, _contextServices.MountConditions(_p.Conditions.ToDictionary(x => x.Field, x => x.Value)));
        }
    }
}