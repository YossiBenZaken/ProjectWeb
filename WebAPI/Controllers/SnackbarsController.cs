using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnackbarsController : ControllerBase
    {
        private readonly ISnackbarService _snackbarService;

        public SnackbarsController(ISnackbarService snackbarService)
        {
            _snackbarService = snackbarService;
        }

        // GET: api/Snackbars
        [HttpGet]
        public async Task<IActionResult> GetSnackbar()
        {
            var result = await _snackbarService.GetSnackbars();
            if (result.Count == 0)
            {
                return NoContent();
            }
            return Ok(result);

        }
        // GET: api/Snackbars/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSnackbar(int id)
        {
            var snackbar = await _snackbarService.GetSnackbar(id);
            if (snackbar is null) return NotFound();
            return Ok(snackbar);
        }
        // PUT: api/Snackbars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSnackbar(int id, Snackbar snackbar)
        {
            if (id != snackbar.id) return BadRequest();
            var result = await _snackbarService.GetSnackbar(id);
            if (result is null)
            {
                return NotFound();
            }
            await _snackbarService.updateSnackbar(snackbar);


            return NoContent();
        }
        // POST: api/Snackbars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostSnackbar(Snackbar snackbar)
        {
            await _snackbarService.createSnackbar(snackbar);
            return CreatedAtAction("GetSnackbar", new { id = snackbar.id }, snackbar);
        }
        // DELETE: api/Snackbars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSnackbar(int id)
        {
            var snackbar = await _snackbarService.GetSnackbar(id);
            if (snackbar is null) return NotFound();
            await _snackbarService.deleteSnackbar(id);
            return NoContent();
        }


    }
}
