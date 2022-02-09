using System.Linq;
using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    [HttpGet("v1/categories")]
    public async Task<IActionResult> GetAsync(
        [FromServices] BlogDataContext context)
    {
        try
        {
            var categories = await context.Categories.ToListAsync();
            return Ok( new ResultViewModel<List<Category>>(categories));
        }
        catch 
        {
            return StatusCode(500, new ResultViewModel<Category>("05x04 - Falha interna no servidor."));
        }
    }

    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] BlogDataContext context,
        [FromRoute] int id)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null) 
                return NotFound( new ResultViewModel<Category>("Conteúdo não encontrado."));
            
            return Ok(new ResultViewModel<Category>(category));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Category>("05x04 - Falha interna no servidor."));
        }
    }

    [HttpPost("v1/categories")]
    public async Task<IActionResult> PostAsync(
        [FromServices] BlogDataContext context,
        [FromBody] EditorCategoryViewModel model)
    {
        //if (model.Id != 0) model.Id = 0;
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<Category>>(ModelState.GetErrors()));
        try
        {
            Category category = new()
            {
                Id = 0,
                Name = model.Name,
                Slug = model.Slug,
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            var newModel = await context.Categories.FirstOrDefaultAsync(x => x == category);

            return Created($"v1/categories/{newModel?.Id}", new ResultViewModel<Category>(newModel));
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500,
                new ResultViewModel<Category>("Não foi possível incluir a catgoria."));
        }

        catch
        {
            return StatusCode(500,
                new ResultViewModel<Category>("Não foi possível incluir a catgoria."));
        }
    }

    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromServices] BlogDataContext context,
        [FromBody] EditorCategoryViewModel model)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null) return NotFound(new ResultViewModel<Category>("Conteúdo não encontrador."));

            category.Name = model.Name;
            category.Slug = model.Slug;
            context.Categories.Update(category);

            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Category>(category));
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500,
                new ResultViewModel<Category>("Não foi possível incluir a catgoria."));
        }

        catch
        {
            return StatusCode(500,
                new ResultViewModel<Category>("Não foi possível incluir a catgoria."));
        }
    }

    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] BlogDataContext context)
    {
        try
        {

            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null) return NotFound();

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return Ok(category);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Não foi possível excluir a catgoria.");
        }
    }

}