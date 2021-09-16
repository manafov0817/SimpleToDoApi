using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Data.Entities;
using ToDoApp.WebApi.Models.DTOs;

namespace ToDoApp.WebApi.Mapper
{
    public class ToDoMapping : Profile
    {
        public ToDoMapping()
        {
            CreateMap<ToDoItemDTO,ToDoItem >().ReverseMap();
            CreateMap<ToDoListDTO, ToDoList>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();

        }
    }
}
