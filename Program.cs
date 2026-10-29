using david_suvarski_kt_31_23.Database;
using david_suvarski_kt_31_23.ServiceExtensions;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container.

    builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            ReferenceHandler.IgnoreCycles;
    });
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<StudentDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddServices();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
}
finally
{
    LogManager.Shutdown();
}

/*
1. Создать интерфейс, например:
public interface IGroupService
{
    Task<Group[]> GetGroupsAsync(
        CancellationToken cancellationToken = default);
}
2. Создать класс сервиса:
public class GroupService : IGroupService
{
    private readonly StudentDbContext _dbContext;

    public GroupService(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Group[]> GetGroupsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Groups.ToArrayAsync(cancellationToken);
    }
}
3. Зарегистрировать сервис в AddServices():
services.AddScoped<IGroupService, GroupService>();
4. Подключить сервис к контроллеру:
private readonly IGroupService _groupService;

public GroupsController(IGroupService groupService)
{
    _groupService = groupService;
}
И вызвать его внутри метода контроллера:
[HttpGet]
public async Task<ActionResult> GetGroupsAsync(
    CancellationToken cancellationToken)
{
    var groups = await _groupService.GetGroupsAsync(cancellationToken);

    return Ok(groups);
}
Как проверить
В терминале перейди в проект:
cd "/Users/amulinne/Documents/Учебная/ПП/david-suvarski-kt-31-23"
Проверь сборку:
dotnet build
Запусти приложение:
dotnet run
Этот терминал оставь работающим. Должна появиться строка:
Now listening on: http://localhost:5289
Открой Swagger:
http://localhost:5289/swagger/index.html
Дальше:
1. Раскрой нужный метод.
2. Нажми Try it out.
3. Заполни параметры.
4. Нажми Execute.
5. Ответ смотри в Response body.
*/