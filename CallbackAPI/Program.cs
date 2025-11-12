using CallbackAPI.Controllers; // <-- so we can reference the controller type
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CallbackAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Listen on all interfaces so LAN can reach it
            builder.WebHost.UseUrls("http://0.0.0.0:7002");

            // Ensure your controllers assembly is loaded for discovery
            builder.Services
                .AddControllers()
                .AddApplicationPart(typeof(CallbackController).Assembly);

            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Minimal endpoints to sanity-check quickly in a browser
            app.MapGet("/", () => Results.Ok("root ok"));
            app.MapGet("/health", () => Results.Ok("healthy"));
            app.MapGet("/api/callback/ping", () => Results.Ok("pong"));

            // Map attribute-routed controllers
            app.MapControllers();

            // (Optional) Dump the discovered routes to logs so you can see what’s registered
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            var sources = app.Services.GetRequiredService<IEnumerable<EndpointDataSource>>();
            foreach (var s in sources)
            {
                foreach (var e in s.Endpoints.OfType<RouteEndpoint>())
                {
                    logger.LogInformation("Endpoint: {pattern} ({display})",
                        e.RoutePattern.RawText, e.DisplayName);
                }
            }

            app.Run();
        }
    }
}
