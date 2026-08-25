using Ecom.API.Middleware;
using Ecom.infrastructure;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Ecom.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.InfrastructureConfiguration(builder.Configuration);
            builder.Services.AddAutoMapper(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));
            builder.Services.AddRateLimiter(options =>
            {
                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    var responseMessage = new
                    {
                        StatusCode = 429,
                        Message = "Too many requests. Please wait a moment and try again.",
                        Error = "Rate Limit Exceeded"
                    };
                    await context.HttpContext.Response.WriteAsJsonAsync(responseMessage, cancellationToken: token);
                };
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.AddFixedWindowLimiter("FixedPolicy", opt =>
                {
                    opt.PermitLimit = 3;
                    opt.Window = TimeSpan.FromSeconds(10);
                    opt.QueueLimit = 0;
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionsMiddleware>();
            app.UseSecurityHeaders(policies =>
                policies.AddDefaultSecurityHeaders()
            );

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseRateLimiter();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}