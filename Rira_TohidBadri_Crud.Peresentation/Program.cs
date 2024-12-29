using Microsoft.EntityFrameworkCore;
using Rira_TohidBadri_Crud.Presentation.Modules;
using Rira_TohidBadri_Crud.Application;
using Rira_TohidBadri_Crud.Infrastructure;
using Rira_TohidBadri_Crud.Peresentation.Handlers;
using Microsoft.AspNetCore.Rewrite;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CrudDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DbConnectionString"));
});


builder.Services.AddApplication();
builder.Services.AddExceptionHandler<ExceptionHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(_ => { });
app.UseHttpsRedirection();
app.AddPersonEndPoints();
app.Run();
