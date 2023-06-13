using AutoMapper;
using CodeSmashWithAngular.Configurations;
using CodeSmashWithAngular.DatabaseContext;
using CodeSmashWithAngular.Dtos;
using CodeSmashWithAngular.Helpers;
using CodeSmashWithAngular.Interfaces;
using CodeSmashWithAngular.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace CodeSmashWithAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CityController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var city = _unitOfWork.cityRepository.All();
                var cityDto = _mapper.Map<IEnumerable<CityDto>>(city);

                if (cityDto.Count() == 0)
                    return StatusCode(StatusCodes.Status404NotFound, $"List of City is not available");
                //     throw new HttpStatusCodeException(HttpStatusCode.NotFound,
                //"Please check username or password");

                return StatusCode(StatusCodes.Status200OK, cityDto);
            }
            catch (Exception ex)
            {
                //throw ex;

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        [HttpPost]
        public IActionResult AddCities([FromBody] CityDto cityDto)
        {
            try
            {
                if (cityDto == null)
                    return BadRequest(StatusCodes.Status400BadRequest);

                var city = _mapper.Map<City>(cityDto);
                city.LastUpdatedOn = DateTime.UtcNow;
                city.LastUpdatedBy = 1;

                _unitOfWork.cityRepository.Add(city);
                _unitOfWork.SaveChanges();

                return StatusCode(StatusCodes.Status201Created, cityDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateCities(int id, [FromBody] CityDto cityDto)
        {
            try
            {
                var cities = _unitOfWork.cityRepository.GetById(id);
                if ((id != cityDto.Id) || (cities == null))
                    return StatusCode(StatusCodes.Status400BadRequest, $"City with Id = {id} not found");

                cities.LastUpdatedOn = DateTime.UtcNow;
                cities.LastUpdatedBy = 2;
                _mapper.Map(cityDto, cities);

                _unitOfWork.cityRepository.Update(cities);
                _unitOfWork.SaveChanges();

                return StatusCode(StatusCodes.Status201Created, cityDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCities(int id)
        {
            try
            {
                var cities = _unitOfWork.cityRepository.GetById(id);
                if (cities == null)
                    return StatusCode(StatusCodes.Status404NotFound, $"City with Id = {id} not found");


                _unitOfWork.cityRepository.Delete(cities);
                _unitOfWork.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, $"City with Id = {id} is deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

    }
}
