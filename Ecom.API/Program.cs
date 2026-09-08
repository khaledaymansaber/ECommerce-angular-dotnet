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

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policyBuilder =>
                {
                    policyBuilder.AllowAnyMethod()
                                 .AllowAnyHeader()
                                 .AllowCredentials()
                                 .WithOrigins("http://localhost:4200");
                });
            });

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
                    opt.PermitLimit = 60; // 60 طلب في الدقيقة (بمتوسط طلب كل ثانية، كافي جداً للاستخدام الطبيعي)
                    opt.Window = TimeSpan.FromMinutes(1); // الإطار الزمني دقيقة واحدة
                    opt.QueueLimit = 0; // الأفضل نخليه صفر عشان السيرفر يرفض الطلب فوراً بـ 429 بدل ما يستهلك موارد في الانتظار
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

            // إيقاف الهيدرز الأمني وإعادة التوجيه مؤقتاً أثناء التطوير المحلي بـ HTTP
            // app.UseSecurityHeaders(policies => policies.AddDefaultSecurityHeaders());
            // app.UseHttpsRedirection();

            app.UseRouting();

            // الترتيب الصحيح لـ CORS بين Routing و Authorization
            app.UseCors("CorsPolicy");

            app.UseRateLimiter();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}