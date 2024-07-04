
using Domain.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public class ApiClient
{
    private readonly HttpClient client;
    public static ApiClient O { get { return Instance.Value; } }
    private static Lazy<ApiClient> Instance = new Lazy<ApiClient>(() => new ApiClient());
    public ApiClient()
    {
        client = new HttpClient();
    }

    public async Task<HttpResponse> PostAsync(string URL, string PostData = "", string AccessToken = "", string ContentType = "application/json", int timeout = 0)
    {
        HttpResponse httpResponse = new HttpResponse();
        HttpWebRequest http = (HttpWebRequest)WebRequest.Create(URL);
        if (!string.IsNullOrEmpty(AccessToken))
        {
            http.Headers.Add("Authorization", "Bearer " + AccessToken);
        }
        http.Headers.Add("User-Agent", UserAgent.Name);
        http.Timeout = timeout == 0 ? 5 * 60 * 1000 : timeout;
        var data = Encoding.ASCII.GetBytes(PostData ?? "");
        http.Method = "POST";
        http.ContentType = ContentType;
        http.ContentLength = data.Length;
        using (Stream stream = await http.GetRequestStreamAsync().ConfigureAwait(false))
        {
            await stream.WriteAsync(data, 0, data.Length).ConfigureAwait(false);
        }
        try
        {
            WebResponse response = await http.GetResponseAsync().ConfigureAwait(false);
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                httpResponse.HttpStatusCode = HttpStatusCode.OK;
                httpResponse.Result = await sr.ReadToEndAsync().ConfigureAwait(false);
            }
        }
        catch (UriFormatException ufx)
        {
            throw new Exception(ufx.Message);
        }
        catch (WebException wx)
        {
            if (wx.Response != null)
            {
                using (var ErrorResponse = wx.Response)
                {
                    using (StreamReader sr = new StreamReader(ErrorResponse.GetResponseStream()))
                    {
                        httpResponse.Result = await sr.ReadToEndAsync().ConfigureAwait(false);
                        httpResponse.HttpMessage = wx.Message;
                        httpResponse.HttpStatusCode = ((HttpWebResponse)wx.Response).StatusCode;
                    }
                }
            }
            else
            {
                throw new Exception(wx.Message);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return httpResponse;
    }
}
