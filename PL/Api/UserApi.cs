using BLL.Interfaces;
using BLL.Models;

namespace PI_222_2_1.Api
{
    public static class UserApi
    {
        public static void Serve(WebApplication app)
        {
            var userApi = app.MapGroup("/api/users");

            userApi.MapGet("/", (IUserService userService) =>
            {
                var users = userService.GetAll();
                return Results.Ok(users);
            });

            userApi.MapGet("/{id:int}", (int id, IUserService userService) =>
            {
                var user = userService.GetById(id);
                return user != null ? Results.Ok(user) : Results.NotFound();
            });

            userApi.MapPost("/", (CreateUserDTO dto, IUserService userService) =>
            {
                var id = userService.AddUser(dto);
                return Results.Created($"/api/users/{id}", null);
            });
        }
    }
}
