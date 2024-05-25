using Blog.Data.Context;
using Blog.Data.Repositories;
using Blog.Services.Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BlogDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("BlogDbContext"));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<BlogService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<HttpContextAccessor>();
/*
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCors", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
        policy.AllowCredentials();
    });
} );
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

