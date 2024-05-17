using MongoDB.Driver;
using TutoringPlatformBackEnd.StudyMaterial.Actor;
using TutoringPlatformBackEnd.StudyMaterial.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("http://localhost:3000") // Allow requests from frontend domain
               .AllowAnyMethod() // Allow any HTTP method
               .AllowAnyHeader(); // Allow any header
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add the study material service
builder.Services.AddSingleton<IStudyMaterialService>(sp =>
{
    var connectionString = "mongodb+srv://toderascuiulia:333222456aA11@iuliascluster.0fax4nf.mongodb.net/";
    return new StudyMaterialService(connectionString);
});

builder.Services.AddSingleton<IStudyMaterialActor, StudyMaterialActor>();

// Configure MongoDB Cloud connection
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = "mongodb+srv://toderascuiulia:333222456aA11@iuliascluster.0fax4nf.mongodb.net/"; 
    return new MongoClient(connectionString);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowFrontend"); // Apply CORS policy

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
