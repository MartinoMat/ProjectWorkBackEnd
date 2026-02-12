using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using ProjectWork.Server.Models;

namespace ProjectWorkServer
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Env.Load();

			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{ 
				c.SwaggerDoc("v1", new()
				{
					Title = "MediLabAPI",
					Version = "v1",
					Description = "API della WebApp MediLab",
					Contact = new OpenApiContact { Name = "Matteo Martino" }
				});

				var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});

			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
										.Replace("${DB_HOST}", Environment.GetEnvironmentVariable("DB_HOST"))
										.Replace("${DB_PORT}", Environment.GetEnvironmentVariable("DB_PORT"))
										.Replace("${DB_NAME}", Environment.GetEnvironmentVariable("DB_NAME"))
										.Replace("${DB_USER}", Environment.GetEnvironmentVariable("DB_USER"))
										.Replace("${DB_PASS}", Environment.GetEnvironmentVariable("DB_PASS"));

			builder.Services.AddDbContext<AppDbContext>(
				options => options.UseNpgsql(connectionString));

			builder.Services.AddAuthentication().AddJwtBearer(options=>
			{
				options.TokenValidationParameters= new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["Jwt:Issuer"],
					ValidAudience = builder.Configuration["Jwt:Audience"],
					IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
						System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]
						.Replace("${JWT_KEY}", Environment.GetEnvironmentVariable("JWT_KEY"))))
				};
			});

			builder.Services.AddCors();


			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

#if DEBUG
			app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
#else
			app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().WithOrigins("Inserire indirizzo"));
#endif

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
