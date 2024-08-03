using System.Net.Http.Headers;
var client = new HttpClient();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://india-pincode-with-latitude-and-longitude.p.rapidapi.com/api/v1/pincode/226022"),
    Headers =
    {
        { "x-rapidapi-key", "0d20806c26mshfe42a78c11d546bp1ea460jsn1fbe62d152a8" },
        { "x-rapidapi-host", "india-pincode-with-latitude-and-longitude.p.rapidapi.com" },
    },
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    Console.WriteLine(body);
}