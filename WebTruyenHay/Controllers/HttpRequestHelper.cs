﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class MailService
{
    public async Task<HttpRequestHelper.HttpResponse> SendMailAsync(string to, string subject, string message)
    {
        string requestUrl = "http://localhost:3000/api";

        string requestBody = $@"
        {{
            ""to"": ""{to}"",
            ""subject"": ""{subject}"",
            ""message"": ""{message}""
        }}";

        return await HttpRequestHelper.SendPostRequestAsync(requestUrl, requestBody, null);
    }
}

public static class HttpRequestHelper
{
    private static readonly HttpClient httpClient = new HttpClient();

    public static async Task<HttpResponse> SendPostRequestAsync(string requestUrl, string requestBody, Dictionary<string, string> headers)
    {
        var response = new HttpResponse();
        try
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
            };

            // Add headers
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }
            }

            var httpResponse = await httpClient.SendAsync(requestMessage);
            response.StatusCode = (int)httpResponse.StatusCode;
            response.Body = await httpResponse.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            response.StatusCode = 500;
            response.Body = e.Message;
        }
        return response;
    }

    public static async Task<HttpResponse> SendDeleteRequestAsync(string requestUrl, Dictionary<string, string> headers)
    {
        var response = new HttpResponse();
        try
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUrl);

            // Add headers
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }
            }

            var httpResponse = await httpClient.SendAsync(requestMessage);
            response.StatusCode = (int)httpResponse.StatusCode;
            response.Body = await httpResponse.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            response.StatusCode = 500;
            response.Body = e.Message;
        }
        return response;
    }

    public static async Task<HttpResponse> SendGetRequestAsync(string requestUrl, Dictionary<string, string> headers)
    {
        var response = new HttpResponse();
        try
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            // Add headers
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }
            }

            var httpResponse = await httpClient.SendAsync(requestMessage);
            response.StatusCode = (int)httpResponse.StatusCode;
            response.Body = await httpResponse.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            response.StatusCode = 500;
            response.Body = e.Message;
        }
        return response;
    }

    public static async Task<HttpResponse> SendPatchRequestAsync(string requestUrl, string requestBody, Dictionary<string, string> headers)
    {
        var response = new HttpResponse();
        try
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUrl)
            {
                Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
            };

            // Workaround for PATCH using X-HTTP-Method-Override
            requestMessage.Headers.Add("X-HTTP-Method-Override", "PATCH");

            // Add headers
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }
            }

            var httpResponse = await httpClient.SendAsync(requestMessage);
            response.StatusCode = (int)httpResponse.StatusCode;
            response.Body = await httpResponse.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            response.StatusCode = 500;
            response.Body = e.Message;
        }
        return response;
    }

    public class HttpResponse
    {
        public int StatusCode { get; set; }
        public string Body { get; set; }
    }
}
