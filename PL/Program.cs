using BLL.Interfaces;
using BLL.Models;
using BLL.Service;
using DAL;
using DAL.Repositories;
using DAL.UoW;
using Microsoft.AspNetCore.Routing.Constraints;
using PI_222_2_1.Api;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddDbContext<AuctionDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBiddingService, BiddingService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ILottingService, LottingService>();


builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap["regex"] = typeof(RegexInlineRouteConstraint);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

UserApi.Serve(app);
LotApi.Serve(app);
BidApi.Serve(app);
CategoryApi.Serve(app);

app.Run();

[JsonSerializable(typeof(CreateUserDTO))]
[JsonSerializable(typeof(UserDTO))]
[JsonSerializable(typeof(LotDTO))]
[JsonSerializable(typeof(BidDTO))]
[JsonSerializable(typeof(CategoryDTO))]
[JsonSerializable(typeof(IEnumerable<UserDTO>))]
[JsonSerializable(typeof(IEnumerable<LotDTO>))]
[JsonSerializable(typeof(IEnumerable<BidDTO>))]
[JsonSerializable(typeof(IEnumerable<CategoryDTO>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}