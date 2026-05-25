using System.Data;
using System.Net;
using System.Text;

namespace ConsoleApp1;

class HttpServer
{
    static async Task Main()
    {
        try
        {
            var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            listener.Start();
            Console.WriteLine("Server started at http://localhost:8080/");

            while (true)
            {
                var context = await listener.GetContextAsync();
                var request = context.Request;
                var response = context.Response;

                if (request.Url.AbsolutePath == "/")
                {
                    response.StatusCode = 200;
                    response.ContentType = "text/html; charset=utf-8";
                    var html = $"<h1>Дата: {DateTime.Now}</h1>";
                    var buffer = Encoding.UTF8.GetBytes(html);
                    await response.OutputStream.WriteAsync(buffer);
                    response.Close();
                }
                else if (request.Url.AbsolutePath == "/info")
                {
                    response.StatusCode = 200;
                    response.ContentType = "application/json";
                    var json = $"{{\"host\": \"{Dns.GetHostName()}\", \"dotnet\": \"{Environment.Version}\"}}";
                    var buffer = Encoding.UTF8.GetBytes(json);
                    await response.OutputStream.WriteAsync(buffer);
                    response.Close();
                }
                else
                {
                    response.StatusCode = 404;
                    response.ContentType = "text/html";
                    var body = "<h1>404 Not Found</h1>";
                    var buffer = Encoding.UTF8.GetBytes(body);
                    await response.OutputStream.WriteAsync(buffer);
                    response.Close();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
