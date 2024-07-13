using System.Net.Http.Headers;
var client = new HttpClient();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Post,
    RequestUri = new Uri("https://distanceto.p.rapidapi.com/distance/point"),
    Headers =
    {
        { "x-rapidapi-key", "0d20806c26mshfe42a78c11d546bp1ea460jsn1fbe62d152a8" },
        { "x-rapidapi-host", "distanceto.p.rapidapi.com" },
    },
    Content = new StringContent("{\"point\":{\"country\":\"DEU\",\"name\":\"Berlin\"}}")
    {
        Headers =
        {
            ContentType = new MediaTypeHeaderValue("application/json")
        }
    }
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    Console.WriteLine(body);
}