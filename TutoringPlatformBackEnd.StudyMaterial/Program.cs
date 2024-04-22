using MongoDB.Driver;
using TutoringPlatformBackEnd.StudyMaterial.Actor;
using TutoringPlatformBackEnd.StudyMaterial.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add the study material service
builder.Services.AddScoped<IStudyMaterialService, StudyMaterialService>();
builder.Services.AddSingleton<IStudyMaterialActor, StudyMaterialActor>();

// Configure MongoDB connection
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = "mongodb://localhost:27017";
    return new MongoClient(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
