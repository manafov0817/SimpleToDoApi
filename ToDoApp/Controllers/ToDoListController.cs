using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ToDoApp.Data.Entities;
using ToDoApp.WebApi.Models.DTOs;
using ToDoApp.WebApi.Repository.Abstract;

namespace ToDoApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IMapper _mapper;

        public ToDoListController(IToDoListRepository toDoListRepository,
                                 IMapper mapper)
        {
            _toDoListRepository = toDoListRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All Lists 
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ToDoListDTO>))]
        public IActionResult GetToDoLists()
        {
            var toDoLists = _toDoListRepository.GetAll();

            var toDoListsDTO = new List<ToDoListDTO>();

            foreach (var toDoList in toDoLists)
                toDoListsDTO.Add(_mapper.Map<ToDoListDTO>(toDoList));

            return Ok(toDoListsDTO);
        }

        /// <summary>
        /// Get Individual List
        /// </summary>
        /// <param name="toDoListId">The Id of List</param>
        /// <returns></returns>
        ///         
        [HttpGet("{toDoListId:int}", Name = "GetToDoList")]
        [ProducesResponseType(200, Type = typeof(ToDoListDTO))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetToDoList(int toDoListId)
        {
            var toDoList = _toDoListRepository.GetById(toDoListId);

            if (toDoList == null)
                return NotFound();

            return Ok(_mapper.Map<ToDoListDTO>(toDoList));
        }

        /// <summary>
        /// Create List
        /// </summary>
        /// <param name="toDoListDTO">The Id of List</param>
        /// <returns></returns>
        ///    
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ToDoListDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] ToDoListDTO toDoListDTO)
        {
            if (toDoListDTO == null)
            {
                return BadRequest();
            }

            if (_toDoListRepository.ExsistsByTitle(toDoListDTO.Title))
            {
                ModelState.AddModelError("", "List already exsists");
                return StatusCode(404, ModelState);
            }

            ToDoList toDoList = _mapper.Map<ToDoList>(toDoListDTO);

            toDoList.CreateDate = DateTime.Now;

            if (!_toDoListRepository.Create(toDoList))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {toDoList.Title} ");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetToDoList", new { toDoListId = toDoList.Id }, toDoList);

        }

        /// <summary>
        /// Update List
        /// </summary>
        /// <param name="toDoListId">The Id of List</param>
        /// <param name="toDoListDTO">The Id of List</param>
        /// <returns></returns>
        ///  
        [HttpPatch("{toDoListId:int}", Name = "UpdateToDoList")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int toDoListId, ToDoListDTO toDoListDTO)
        {
            if (toDoListDTO == null || toDoListId == 0)
            {
                return BadRequest(ModelState);
            }

            ToDoList toDoList = _mapper.Map<ToDoList>(toDoListDTO);

            toDoList.Id = toDoListId;

            if (!_toDoListRepository.Update(toDoList))
            {
                ModelState.AddModelError("", $"Something went wrong saving the record {toDoList.Title}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        /// <summary>
        /// Delete List
        /// </summary>
        /// <param name="toDoListId">The Id of List</param>
        /// <returns></returns>
        ///   
        [HttpDelete("{toDoListId:int}", Name = "DeleteToDoList")]
        public IActionResult Delete(int toDoListId)
        {
            if (!_toDoListRepository.ExsistsById(toDoListId))
            {
                return NotFound();
            }

            ToDoList toDoList = _toDoListRepository.GetById(toDoListId);
            if (!_toDoListRepository.Delete(toDoList))
            {

                ModelState.AddModelError("", $"Something went wrong deleting the record {toDoList}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
