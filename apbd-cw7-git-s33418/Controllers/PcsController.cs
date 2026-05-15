using apbd_cw7_git_s33418.DTO;
using apbd_cw7_git_s33418.Service;
using Microsoft.AspNetCore.Mvc;

namespace apbd_cw7_git_s33418.Controllers
{
    [ApiController]
    [Route("api/pcs")]
    public class PcsController : ControllerBase
    {
        private readonly IProductService _service;

        public PcsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetComputers>>> GetComputers()
        {
            var computers = await _service.GetAllAsync();
            return Ok(computers);
        }

        [HttpGet("{id:int}/components")]
        public async Task<ActionResult<List<GetComponentsByID>>> GetComponentsById(int id)
        {
            var components = await _service.GetComponentsByPcIdAsync(id);
            if (components is null)
            {
                return NotFound();
            }

            return Ok(components);
        }

        [HttpPost]
        public async Task<ActionResult<PostPcAnswer>> PostPc([FromBody] PostPcRequest request)
        {
            if (request is null)
            {
                return BadRequest("Request body is required.");
            }

            try
            {
                var createdPc = await _service.CreatePCAsync(request);
                return Created($"/api/pcs/{createdPc.Id}", createdPc);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GetComputers>> PutPc(int id, [FromBody] PutPcRequest request)
        {
            if (request is null)
            {
                return BadRequest("Request body is required.");
            }

            try
            {
                var updatedPc = await _service.UpdatePCAsync(id, request);
                if (updatedPc is null)
                {
                    return NotFound();
                }

                return Ok(updatedPc);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePc(int id)
        {
            var deleted = await _service.DeletePCAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
