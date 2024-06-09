using APIMovieReview.Data;
using APIMovieReview.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace APIMovieReview.Config
{
    public class MovieReviewAPIApplication
    {
        readonly WebApplicationBuilder _builder;
        readonly WebApplication _app;
        public MovieReviewAPIApplication(string[] args)
        {
            _builder = WebApplication.CreateBuilder(args);

            ConfigureServices();

            _app = _builder.Build();

            ConfigureMiddlewares();
        }

        private void ConfigureServices()
        {

            _builder.Services.AddDbContext<DatabaseContext>(options =>
       options.UseSqlServer(_builder.Configuration.GetConnectionString("MovieReviewAPIContext") ?? throw new InvalidOperationException("Connection string 'Movie_Review_APIContext' not found.")));

            _builder.Services.AddAuthorization();

            // Add services to the container.
            _builder.Services.AddControllers();
            
            ConfigureSwagger();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            _builder.Services.AddEndpointsApiExplorer();
            _builder.Services.AddSwaggerGen();
            _builder.Services.AddIdentityApiEndpoints<CustomUser>()
                .AddEntityFrameworkStores<DatabaseContext>();
        }

        private void ConfigureMiddlewares()
        {
            // Configure the HTTP request pipeline.
            if (_app.Environment.IsDevelopment())
            {
                _app.UseSwagger();
                _app.UseSwaggerUI();
            }

            _app.MapIdentityApi<CustomUser>();

            _app.UseHttpsRedirection();

            _app.UseAuthentication();

            _app.UseAuthorization();


            _app.MapControllers();
        }
        private void ConfigureSwagger()
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            _builder.Services.AddEndpointsApiExplorer();
            _builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        public void Run()
        {
            _app.Run();
        }
    }
}