using AsyncStreamsApi.Repositories;

const string AllowKnownClientsCorsPolicyName = "AllowKnownClients";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: AllowKnownClientsCorsPolicyName,
        policy =>
        {
            var knownClientsUrls = builder.Configuration.GetSection("ClientsUrls").Get<string[]>();
            policy.WithOrigins(knownClientsUrls);
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMovieRepository, MovieSqlRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(AllowKnownClientsCorsPolicyName);

app.UseAuthorization();

app.MapControllers();

app.Run();
