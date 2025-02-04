﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using todo_backend.Domain.Models;
using todo_backend.Infrastructure.Interfaces;
using todo_backend.WEB.Mapping.DTOs;

namespace todo_backend.WEB.Controllers
{
    [Route("api/v1/catalogs")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        public CatalogController(ICatalogRepository catalogRepository, IMapper mapper)
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCatalog(CatalogDTO catalogDto)
        {
            Catalog createdCatalog = await _catalogRepository.CreateCatalogAsync(_mapper.Map<Catalog>(catalogDto));

            CatalogDTO createdCatalogDto = _mapper.Map<CatalogDTO>(createdCatalog);
            return CreatedAtAction(nameof(GetCatalogsById), new { id = createdCatalogDto.Id }, createdCatalogDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalog(int id)
        {
            await _catalogRepository.DeleteCatalogAsync(id);  

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCatalogs()
        {
            IList<Catalog> catalogs = await _catalogRepository.GetCatalogsAsync();

            return Ok(_mapper.Map<List<CatalogDTO>>(catalogs));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalogsById(int id)
        {
            Catalog catalog = await _catalogRepository.GetCatalogAsync(id);

            if (catalog == null)
                return NotFound();

            return Ok(_mapper.Map<CatalogDTO>(catalog));
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCatalog(int id, [Required] string title)
        {
            Catalog updatedCatalog = await _catalogRepository.UpdateCatalogAsync(id, title);
            
            if (updatedCatalog == null)
                return NotFound();

            return Ok(_mapper.Map<CatalogDTO>(updatedCatalog));
        }
    }
}
