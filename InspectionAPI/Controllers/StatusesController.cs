using InspectionAPI.Data;
using InspectionAPI.Data.Entities;
using InspectionAPI.Data.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InspectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : BaseController
    {
        public StatusesController(DataContext context) : base(context) { }

        [HttpGet("GetStatuses")]
        [Authorize]
        public async Task<List<StatusViewModel>> GetStatuses()
        {
            return await _dataContext.Statuses.Select(x => new StatusViewModel()
            {
                Id = x.Id, 
                StatusOption = x.StatusOption

            }).ToListAsync();
        }

        [HttpPost("CreateStatus")]

        public async Task<IActionResult> CreateStatus( StatusViewModel model)
        {
            var status = new Status()
            {
                StatusOption = model.StatusOption
            };

            _dataContext.Statuses.Add(status);

          var result =  await _dataContext.SaveChangesAsync();

            if(result == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("GetStatus/{id}")]

        public async Task<Status> GetStatus(int id) => await _dataContext.Statuses.Where(x => x.Id == id).FirstOrDefaultAsync();

        [HttpPut("UpdateStatus")]

        public async Task<IActionResult> UpdateStatus(StatusViewModel model)
        {
            var status = await GetStatus(model.Id);
            status.StatusOption = model.StatusOption;

            _dataContext.Update(status);
            var result = await _dataContext.SaveChangesAsync();

            if(result == 0)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
