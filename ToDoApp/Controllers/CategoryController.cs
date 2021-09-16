using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using ToDoApp.Data.Entities;
using ToDoApp.WebApi.Models.DTOs;
using ToDoApp.WebApi.Repository.Abstract;

namespace ToDoApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _CategoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository,
                                 IMapper mapper)
        {
            _CategoryRepository = categoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All Categories 
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CategoryDTO>))]
        public IActionResult GetCategories()
        {
           
            var categories = _CategoryRepository.GetAll();

            var categoriesDTO = new List<CategoryDTO>();

            foreach (var Category in categories)
                categoriesDTO.Add(_mapper.Map<CategoryDTO>(Category));

            return Ok(categoriesDTO);
        }

        /// <summary>
        /// Get Individual Category
        /// </summary>
        /// <param name="categoryId">The Id of Category</param>
        /// <returns></returns>
        ///         
        [HttpGet("{categoryId:int}", Name = "GetCategory")]
        [ProducesResponseType(200, Type = typeof(CategoryDTO))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetCategory(int categoryId)
        {
            var Category = _CategoryRepository.GetById(categoryId);

            if (Category == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CategoryDTO>(Category));
        }

        /// <summary>
        /// Create Category
        /// </summary>
        /// <param name="categoryDTO">Category</param>
        /// <returns></returns>
        ///    
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CategoryDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest();
            }

            if (_CategoryRepository.ExsistsByName(categoryDTO.Name))
            {
                ModelState.AddModelError("", "Category already exsists");
                return StatusCode(404, ModelState);
            }
                      
            Category category = _mapper.Map<Category>(categoryDTO);


            if (!_CategoryRepository.Create(category))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {category.Name} ");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategory", new { categoryId = category.Id }, category);

        }


        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="categoryId">The Id of category</param>
        /// <param name="categoryDTO">The Id of category</param>
        /// <returns></returns>
        ///  
        [HttpPatch("{categoryId:int}", Name = "UpdateCategory")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int categoryId, CategoryDTO categoryDTO)
        {
            if (categoryDTO == null || categoryId == 0)
            {
                return BadRequest(ModelState);
            }

            Category Category = _mapper.Map<Category>(categoryDTO);

            Category.Id = categoryId;

            if (!_CategoryRepository.Update(Category))
            {
                ModelState.AddModelError("", $"Something went wrong saving the record {Category.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="categoryId">The Id of category</param>
        /// <returns></returns>
        ///   
        [HttpDelete("{categoryId:int}", Name = "DeleteCategory")]
        public IActionResult Delete(int categoryId)
        {
            if (!_CategoryRepository.ExsistsById(categoryId))
            {
                return NotFound();
            }
            Category Category = (Category)_CategoryRepository.GetById(categoryId);
            if (!_CategoryRepository.Delete(Category))
            {

                ModelState.AddModelError("", $"Something went wrong deleting the record {Category}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
