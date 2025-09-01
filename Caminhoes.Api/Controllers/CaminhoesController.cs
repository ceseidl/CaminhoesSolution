using Caminhoes.Api.Data;
using Caminhoes.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caminhoes.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaminhoesController : ControllerBase
    {
        private readonly ICaminhaoRepository _repository;
        public CaminhoesController(ICaminhaoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Caminhao>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{codigoChassi}")]
        public async Task<ActionResult<Caminhao>> GetById(string codigoChassi)
        {
            var caminhao = await _repository.GetByIdAsync(codigoChassi);
            if (caminhao == null) return NotFound();
            return Ok(caminhao);
        }

        [HttpPost]
        public async Task<ActionResult<Caminhao>> Create(Caminhao caminhao)
        {
            var created = await _repository.AddAsync(caminhao);
            return CreatedAtAction(nameof(GetById), new { codigoChassi = created.CodigoChassi }, created);
        }

        [HttpPut("{codigoChassi}")]
        public async Task<ActionResult<Caminhao>> Update(string codigoChassi, Caminhao caminhao)
        {
            var updated = await _repository.UpdateAsync(codigoChassi, caminhao);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{codigoChassi}")]
        public async Task<IActionResult> Delete(string codigoChassi)
        {
            var deleted = await _repository.DeleteAsync(codigoChassi);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
