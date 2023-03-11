using Microsoft.AspNetCore.Mvc;
using ORM.Nine.Database.Configurations;
using ORM.Nine.Database.Models.Input;
using ORM.Nine.Database.Models.Output;

namespace ORM.Nine.Database.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class EntitiesController : ControllerBase
    {
        private IContextServices _contextServices = new ContextServices();

        [HttpGet]
        public JsonReturn Get([FromBody] InputApiParameters inputApiParameters)
        {
            return _contextServices.SelectJson(
                inputApiParameters.Table, 
                inputApiParameters.NumberPage, 
                inputApiParameters.Search, 
                inputApiParameters.Sort, 
                _contextServices.MountConditions(inputApiParameters.Conditions.ToDictionary(x => x.Field, x => x.Value)));
        }

        [HttpPost]
        public JsonReturn Post([FromBody] InputApiParametersPost inputApiParametersPost)
        {
            return _contextServices.InsertJson(
                inputApiParametersPost.Table,
                inputApiParametersPost.Properties);
        }

        [HttpPut]
        public JsonReturn Put([FromBody] InputApiParametersPut inputApiParametersPut)
        {
            return _contextServices.UpdateJson(
                inputApiParametersPut.Table,
                inputApiParametersPut.IdPrimaryKey,
                inputApiParametersPut.Properties);
        }

        [HttpDelete]
        public JsonReturn Delete([FromBody] InputApiParametersDelete inputApiParametersDelete)
        {
            return _contextServices.DeleteJson(
                inputApiParametersDelete.Table,
                inputApiParametersDelete.IdPrimaryKey,
                inputApiParametersDelete.IdsPrimaryKeys);
        }
    }
}