using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("Regions")] //Or [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllAsync(); //This returns Domain Regions
            var regionsDTOs = mapper.Map<List<Models.DTOs.Region>>(regions); //Use AutoMapper to convert to DTO
            return Ok(regionsDTOs);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetAsync")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);

            if(region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTOs.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTOs.AddRegionRequest addRegionRequest)
        {
            //Convert request to domain model
            var region = new Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population,
            };

            //Pass details to Repository
            region = await regionRepository.AddAsync(region);

            //Converst back to DTO
            var regionDTO = new Models.DTOs.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
            };

            return CreatedAtAction(nameof(GetAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]Guid id, [FromBody ]Models.DTOs.UpdateRegionRequest updateRegionRequest)
        {
            //Convert DTO to domain model
            var region = new Region()
            {
                Name = updateRegionRequest.Name,
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Long = updateRegionRequest.Long,
                Lat = updateRegionRequest.Lat,
                Population = updateRegionRequest.Population
            };

            //Update using repository

            region = await regionRepository.UpdateAsync(id, region);

            //if null -> NotFound

            if(region == null) { return NotFound(); }

            //Convert domain back to DTO

            var regionDTO = mapper.Map<Region>(region);

            //Return ok

            return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            //Get region from db
            var region = await regionRepository.DeleteAsync(id);

            //if null -> NotFound
            if (region == null) { return NotFound(); }

            //Convert response to DTO

            var regionDTO = mapper.Map<Models.DTOs.Region>(region);

            //rETURN ok response

            return Ok(regionDTO);
        }
    }
}
