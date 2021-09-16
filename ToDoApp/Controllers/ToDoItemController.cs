using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ToDoApp.Data.Entities;
using ToDoApp.WebApi.Models.DTOs;
using ToDoApp.WebApi.Repository.Abstract;

namespace ToDoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoItemRepository _toDoItemRepository;
        private readonly IMapper _mapper;

        public ToDoItemController(IToDoItemRepository toDoItemRepository,
                                 IMapper mapper)
        {
            _toDoItemRepository = toDoItemRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All Tasks 
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ToDoItemDTO>))]
        public IActionResult GetToDoItems()
        {
            var toDoItems = _toDoItemRepository.GetAll();

            var toDoItemsDTO = new List<ToDoItemDTO>();

            foreach (var toDoItem in toDoItems)
                toDoItemsDTO.Add(_mapper.Map<ToDoItemDTO>(toDoItem));

            return Ok(toDoItemsDTO);
        }

        /// <summary>
        /// Get Individual Task
        /// </summary>
        /// <param name="taskId">The Id of Task</param>
        /// <returns></returns>
        ///         
        [HttpGet("{taskId:int}", Name = "GetToDoItem")]
        [ProducesResponseType(200, Type = typeof(ToDoItemDTO))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetToDoItem(int taskId)
        {
            var toDoItem = _toDoItemRepository.GetById(taskId);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ToDoItemDTO>(toDoItem));
        }

        /// <summary>
        /// Create Task
        /// </summary>
        /// <param name="taskDTO">The Id of Task</param>
        /// <returns></returns>
        ///    
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ToDoItemDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] ToDoItemDTO taskDTO)
        {
            if (taskDTO == null)
            {
                return BadRequest();
            }

            if (_toDoItemRepository.ExsistsByTitle(taskDTO.Title))
            {
                ModelState.AddModelError("", "Task already exsists");
                return StatusCode(404, ModelState);
            }

            if (!_toDoItemRepository.CategoryIdExsists(taskDTO.CategoryId))
            {
                ModelState.AddModelError("", "Category does not exsists");
                return StatusCode(404, ModelState);
            }

            ToDoItem toDoItem = _mapper.Map<ToDoItem>(taskDTO);

            toDoItem.CreateDate = DateTime.Now;

            if (!_toDoItemRepository.Create(toDoItem))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {toDoItem.Title} ");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetToDoItem", new { taskId = toDoItem.Id }, toDoItem);

        }


        /// <summary>
        /// Update Task
        /// </summary>
        /// <param name="taskId">The Id of Task</param>
        /// <param name="taskDTO">The Id of Task</param>
        /// <returns></returns>
        ///  
        [HttpPatch("{taskId:int}", Name = "UpdateToDoItem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int taskId, ToDoItemDTO taskDTO)
        {
            if (taskDTO == null || taskId == 0)
            {
                return BadRequest(ModelState);
            }

            ToDoItem toDoItem = _mapper.Map<ToDoItem>(taskDTO);

            toDoItem.Id = taskId;

            if (!_toDoItemRepository.Update(toDoItem))
            {
                ModelState.AddModelError("", $"Something went wrong saving the record {toDoItem.Title}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        /// <summary>
        /// Delete Task
        /// </summary>
        /// <param name="taskId">The Id of Task</param>
        /// <returns></returns>
        ///   
        [HttpDelete("{taskId:int}", Name = "DeleteToDoItem")]
        public IActionResult Delete(int taskId)
        {
            if (!_toDoItemRepository.ExsistsById(taskId))
            {
                return NotFound();
            }
            ToDoItem toDoItem = (ToDoItem)_toDoItemRepository.GetById(taskId);
            if (!_toDoItemRepository.Delete(toDoItem))
            {

                ModelState.AddModelError("", $"Something went wrong deleting the record {toDoItem}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
