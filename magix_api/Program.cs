using magix_api.Services.PlayerService;
using magix_api.Services.GameService;
using magix_api.Services.DeckService;
using magix_api.Services.GameOptionsService;
using magix_api.Repositories;
using magix_api.Data;
using magix_api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSqlite<MagixContext>("Data Source=magix.db");
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
builder.Services.AddSingleton<IFactionsRepo, FactionsRepo>();
builder.Services.AddScoped<IGameOptionsRepo, GameOptionsRepo>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IDeckRepository, DeckRepository>();

// Add Custom Services
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IDeckService, DeckService>();
builder.Services.AddScoped<IGameOptionsService, GameOptionsService>();

var app = builder.Build();

app.Use((context, next) =>
{
    context.Request.EnableBuffering();
    return next();
});

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
