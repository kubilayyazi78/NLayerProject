using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayerProject.Core.Models;
using NLayerProject.Core.Services;
using NLayerProject.Web.ApiService;
using NLayerProject.Web.DTOs;
using NLayerProject.Web.Filters;

namespace NLayerProject.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly CategoryApiService _categoryApiService;

        public CategoriesController(ICategoryService categoryService, IMapper mapper, CategoryApiService categoryApiService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Index()
        {

            // var categories = await _categoryService.GetAllAsync();

            var categories = await _categoryApiService.GetAllAsync();

            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return View(categoriesDto);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            await _categoryApiService.AddAsync(categoryDto);
            //await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));

            //return View(_mapper.Map<CategoryDto>(newCategory));

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Update(int id)
        {
            // var category = await _categoryService.GetEntityAsync(id);

            var category = await _categoryApiService.GetByIdAsync(id);

            return View(_mapper.Map<CategoryDto>(category));
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            //  _categoryService.Update(_mapper.Map<Category>(categoryDto));

            await _categoryApiService.Update(categoryDto);

            return RedirectToAction("Index");
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        public async Task<IActionResult> Delete(int id)
        {
            //var category = _categoryService.GetEntityAsync(id).Result;

            // _categoryService.Remove(category);

           

            await _categoryApiService.Remove(id);

            return RedirectToAction("Index");

        }


    }
}
