using magix_api.Services.PlayerService;
using magix_api.Services.GameService;
using magix_api.Services.DeckService;
using magix_api.Repositories;
using magix_api.Data;
using Microsoft.EntityFrameworkCore;
using magix_api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MagixContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// Add Custom Repositories
builder.Services.AddScoped<IDeckRepository, DeckRepository>();

// Add Custom Services
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IDeckService, DeckService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
