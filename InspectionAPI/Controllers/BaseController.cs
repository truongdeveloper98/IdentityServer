using InspectionAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InspectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly DataContext _dataContext;

        public BaseController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
