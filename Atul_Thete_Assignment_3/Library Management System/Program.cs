using Library_Management_System.Service;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cosmos DB configuration
var containerNames = new Dictionary<string, string>
{
    { "Book", builder.Configuration["CosmosDb:BookContainerName"] },
    { "Member", builder.Configuration["CosmosDb:MemberContainerName"] },
    { "Issue", builder.Configuration["CosmosDb:IssueContainerName"] }
};

builder.Services.AddSingleton<ICosmosDbService>(InitializeCosmosClientInstanceAsync(builder.Configuration.GetSection("CosmosDb"), containerNames).GetAwaiter().GetResult());

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

static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection, Dictionary<string, string> containerNames)
{
    string databaseName = configurationSection["DatabaseName"];
    string account = configurationSection["Account"];
    string key = configurationSection["Key"];
    CosmosClient client = new CosmosClient(account, key);
    CosmosDbService cosmosDbService = new CosmosDbService(client, databaseName, containerNames);

    DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);

    foreach (var containerName in containerNames.Values)
    {
        await database.Database.CreateContainerIfNotExistsAsync(containerName, "/uId");
    }

    return cosmosDbService;
}
