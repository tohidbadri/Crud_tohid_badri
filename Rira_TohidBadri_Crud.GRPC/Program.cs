using Microsoft.EntityFrameworkCore;
using Rira_TohidBadri_Crud.Infrastructure;
using Rira_TohidBadri_Crud.Application;
using Rira_TohidBadri_Crud.GRPC.Handlers;
using Rira_TohidBadri_Crud.GRPC.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CrudDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DbConnectionString"));
});
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policyBuilder =>
    {
        policyBuilder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5175");
    });
});

builder.Services.AddApplication();
builder.Services.AddExceptionHandler<ExceptionHandler>();


// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGrpcService<PersonService>();
app.UseExceptionHandler(_ => { });
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.Run();
