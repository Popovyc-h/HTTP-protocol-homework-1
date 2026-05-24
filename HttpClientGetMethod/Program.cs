namespace ConsoleApp1;

class HttpGetExample
{
    static async Task Main()
    {
        try
        {
            using var client = new HttpClient();

            // 1. Надіслати GET-запит
            var response = await client.GetAsync("https://httpbin.org/get");

            // 2. Вивести StatusCode
            Console.WriteLine($"StatusCode: {(int)response.StatusCode} {response.StatusCode}");

            // 3. Перебрати response.Headers та вивести кожен
            foreach (var r in response.Headers)
                Console.WriteLine($"{r.Key}: {string.Join(", ", r.Value)}");

            // 4. Вивести тіло через await response.Content.ReadAsStringAsync()
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
