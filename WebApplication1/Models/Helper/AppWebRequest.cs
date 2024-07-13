
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Security.Policy;
using Domain.Models;

namespace WEBAPP.Models.Helper
{
    public class AppWebRequest : IAppWebRequest
    {
        public static AppWebRequest O { get { return Instance.Value; } }
        private static Lazy<AppWebRequest> Instance = new Lazy<AppWebRequest>(() => new AppWebRequest());
        private AppWebRequest() { }
        #region HttpGet
        public async Task<string> CallUsingHttpWebRequest_GET(string URL)
        {
            HttpWebRequest http = (HttpWebRequest)System.Net.WebRequest.Create(URL);
            http.Method = "GET";
            http.ContentType = "application/json";
            http.Timeout = 2 * 60 * 1000;
            http.Headers.Add("User-Agent", UserAgent.Name);
            WebResponse response = http.GetResponse();
            string result = string.Empty;
            try
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
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
                            result = sr.ReadToEnd();
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

            return result;
        }
        #endregion

        #region HttpPost
        public async Task<Domain.Models.HttpResponse> PostAsync(string URL, string PostData = "", string AccessToken = "", string ContentType = "application/json", int timeout = 0)
        {
            Domain.Models.HttpResponse httpResponse = new Domain.Models.HttpResponse();
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
        #endregion
        public async Task<string> PostJsonDataUsingHWRTLS(string URL, object PostData, IDictionary<string, string> headers)
        {
            string result = "";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest http = (HttpWebRequest)System.Net.WebRequest.Create(URL);
                http.Timeout = 3 * 60 * 1000;
                var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(PostData));
                http.Method = "POST";
                http.Accept = ContentType.application_json;
                http.ContentType = ContentType.application_json;
                http.MediaType = ContentType.application_json;
                http.ContentLength = data.Length;
                http.Headers.Add("User-Agent", UserAgent.Name);
                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        http.Headers.Add(item.Key, item.Value);
                    }
                }
                using (Stream stream = http.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                WebResponse response = await http.GetResponseAsync().ConfigureAwait(false);

                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    result = await sr.ReadToEndAsync().ConfigureAwait(false);
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
                            result = await sr.ReadToEndAsync();
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
            return result;
        }
        public async Task<string> CallUsingHttpWebRequest_POSTAsync(string URL, string PostData, string ContentType = "application/x-www-form-urlencoded")
        {
            HttpWebRequest http = (HttpWebRequest)System.Net.WebRequest.Create(URL);
            http.Timeout = 5 * 60 * 1000;
            var data = Encoding.ASCII.GetBytes(PostData);
            http.Method = "POST";
            http.ContentType = ContentType;
            http.ContentLength = data.Length;
            http.Headers.Add("User-Agent", UserAgent.Name);
            using (Stream stream = await http.GetRequestStreamAsync().ConfigureAwait(false))
            {
                await stream.WriteAsync(data, 0, data.Length).ConfigureAwait(false);
            }
            string result = "";
            try
            {
                WebResponse response = await http.GetResponseAsync().ConfigureAwait(false);
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    result = await sr.ReadToEndAsync().ConfigureAwait(false);
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
                            result = await sr.ReadToEndAsync().ConfigureAwait(false);
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
            return result;
        }
        public async Task<string> PostJsonDataUsingHWRTLS(string URL, object PostData)
        {
            string result = "";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest http = (HttpWebRequest)System.Net.WebRequest.Create(URL);
                var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(PostData, Formatting.Indented));
                http.Method = "POST";
                http.Accept = ContentType.application_json;
                http.ContentType = ContentType.application_json;
                http.ContentLength = data.Length;
                http.Headers.Add("User-Agent", UserAgent.Name);
                using (Stream stream = http.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                WebResponse response = await http.GetResponseAsync().ConfigureAwait(false);

                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    result = await sr.ReadToEndAsync().ConfigureAwait(false);
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
                            result = await sr.ReadToEndAsync();
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
            return result;
        }
        public string CallUsingHttpWebRequest_POST(string URL, string PostData, IDictionary<string, string> headers = null, string ContentType = "application/x-www-form-urlencoded")
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var http = (HttpWebRequest)System.Net.WebRequest.Create(URL);
            var data = Encoding.ASCII.GetBytes(PostData);
            http.Method = HttpMethod.Post.ToString();
            http.ContentType = ContentType;
            http.ContentLength = data.Length;
            http.Timeout = 5 * 60 * 1000;
            http.Headers.Add("User-Agent", UserAgent.Name);
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    http.Headers.Add(item.Key, item.Value);
                }
            }
            using (Stream stream = http.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            string result = "";
            try
            {
                WebResponse response = http.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                }
            }
            catch (WebException wx)
            {
                if (wx.Response != null)
                {
                    using (var ErrorResponse = wx.Response)
                    {
                        using (StreamReader sr = new StreamReader(ErrorResponse.GetResponseStream()))
                        {
                            result = sr.ReadToEnd();
                        }
                    }
                }
                else
                {
                    throw new Exception(wx.Message);
                }
            }
            catch (Exception EX)
            {
                throw new Exception(EX.Message);
            }

            return result;
        }
        public async Task<string> PostMultipartUsingHttpClient(string URL, IDictionary<string, string> customHeaders, MultipartFormDataContent formData)
        {
            string result = string.Empty;
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    foreach (var header in customHeaders)
                    {
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                    }

                    HttpResponseMessage response = await httpClient.PostAsync(URL, formData);

                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        result = response.StatusCode.ToString();
                    }
                }
            }
            catch (UriFormatException ufx)
            {
                throw new Exception(ufx.Message);
            }
            catch (HttpRequestException hrx)
            {
                if (hrx.InnerException is WebException wx && wx.Response != null)
                {
                    using (var errorResponse = wx.Response)
                    {
                        using (StreamReader sr = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            result = await sr.ReadToEndAsync();
                        }
                    }
                }
                else
                {
                    throw new Exception(hrx.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;

        }
        public async Task<HttpResponseMessage> SendFileAndContentAsync<TContent>(string apiUrl, string authToken, TContent contentData, Microsoft.AspNetCore.Http.IFormFile file, Microsoft.AspNetCore.Http.IFormFile file1 = null)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Post, apiUrl))
                    {

                        if (!string.IsNullOrEmpty(authToken))
                        {
                            request.Headers.Add("Authorization", $"Bearer {authToken}");
                        }
                        var formData = new MultipartFormDataContent();
                        if (contentData != null)
                        {
                            // Add content parameters
                            foreach (var property in typeof(TContent).GetProperties())
                            {
                                var __value = property.GetValue(contentData);
                                if (__value!=null)
                                {
                                    if (__value.GetType().Name.ToUpper()=="FORMFILE")
                                    {
                                        file = (Microsoft.AspNetCore.Http.FormFile)__value;
                                        if (file != null)
                                        {
                                            // Add the file to the FormData
                                            var fileStream = file.OpenReadStream();
                                            var fileContent = new StreamContent(fileStream);
                                            formData.Add(fileContent, property.Name, file.FileName);
                                        }
                                    }
                                    else
                                    {
                                        formData.Add(new StringContent(__value.ToString()), property.Name);
                                    }
                                }
                            }
                        }
                        request.Content = formData;
                        // Send the request
                        var res = await httpClient.SendAsync(request);
                        return res;
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<HttpResponseMessage> SendFileAndContentAsync<TContent>(string apiUrl, string authToken, TContent contentData)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Post, apiUrl))
                    {
                        if (!string.IsNullOrEmpty(authToken))
                        {
                            request.Headers.Add("Authorization", $"Bearer {authToken}");
                        }

                        var formData = new MultipartFormDataContent();
                        if (contentData != null)
                        {
                            foreach (var property in typeof(TContent).GetProperties())
                            {
                                var value = property.GetValue(contentData);
                                if (value != null)
                                {
                                    if (value is IFormFile file)
                                    {
                                        var fileContent = new StreamContent(file.OpenReadStream());
                                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                                        formData.Add(fileContent, property.Name, file.FileName);
                                    }
                                    else if (value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(List<>))
                                    {
                                        foreach (var item in (IList<IFormFile>)value)
                                        {
                                            var fileContent = new StreamContent(item.OpenReadStream());
                                            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(item.ContentType);
                                            formData.Add(fileContent, property.Name, item.FileName);
                                        }
                                    }
                                    else
                                    {
                                        formData.Add(new StringContent(value.ToString()), property.Name);
                                    }
                                }
                            }
                        }

                        request.Content = formData;
                        var response = await httpClient.SendAsync(request);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }



    }
}
