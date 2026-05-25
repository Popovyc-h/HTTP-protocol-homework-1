namespace ConsoleApp1;

class HttpGetExample
{
    static async Task Main()
    {
        try
        {
            using var client = new HttpClient();

            // Серіалізація об'єкту в JSON
            var payload = new { name = "Student", course = "Networking" };
            var json = System.Text.Json.JsonSerializer.Serialize(payload);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            // Надіслати POST
            var response = await client.PostAsync("https://httpbin.org/post", content);

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
