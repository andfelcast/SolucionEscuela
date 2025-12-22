using Escuela.Domain.Repositories;
using Escuela.Infrastructure.Persistence;
using Escuela.Infrastructure.Repositories;
using EscuelaWebAPI.Services.Implementation;
using EscuelaWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EscuelaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EscuelaDBConnection")));
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<ISubjectService, SubjectService>();
builder.Services.AddTransient<ITeacherService, TeacherService>();
builder.Services.AddTransient<IAuthRepository, AuthRepository>();
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient<ISubjectRepository, SubjectRepository>();
builder.Services.AddTransient<ITeacherRepository, TeacherRepository>();

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
