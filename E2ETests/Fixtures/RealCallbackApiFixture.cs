using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using CallbackAPI;

public class RealCallbackApiFixture : IAsyncLifetime
{
    private IHost? _host;
    public int Port { get; } = 7002;
    public string BaseUrl => $"http://0.0.0.0:{Port}";

    public async Task InitializeAsync()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(web =>
            {
                web.UseKestrel();                        // IMPORTANT: real web server
                web.UseUrls(BaseUrl);                    // bind on all interfaces
                web.UseStartup<Program>();               // your app
            })
            .Build();

        await _host.StartAsync();
    }

    public async Task DisposeAsync()
    {
        if (_host != null)
        {
            await _host.StopAsync();
            _host.Dispose();
        }
    }
}
