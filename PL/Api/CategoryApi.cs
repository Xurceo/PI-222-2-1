using BLL.Interfaces;
using BLL.Models;

namespace PI_222_2_1.Api
{
    public static class CategoryApi
    {
        public static void Serve(WebApplication app)
        {
            var categoryApi = app.MapGroup("/api/categories");

            // Получить всех пользователей
            categoryApi.MapGet("/", (ICategoryService categoryService) =>
            {
                var users = categoryService.GetAll();
                return Results.Ok(users);
            });

            // Получить пользователя по id
            categoryApi.MapGet("/{id:int}", (int id, ICategoryService categoryService) =>
            {
                var user = categoryService.GetById(id);

                if (user == null)
                {
                    return Results.NotFound();
                }
                return user != null ? Results.Ok(user) : Results.NotFound();
            });

            // Создать пользователя
            categoryApi.MapPost("/", (CategoryDTO dto, ICategoryService categoryService) =>
            {
                var id = categoryService.AddCategory(dto);
                return Results.Created($"/api/categories/{id}", dto);
            });
        }
    }
}
