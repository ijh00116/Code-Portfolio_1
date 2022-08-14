using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Net;
using System.Net.Http;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.Networking;

public class WebManager
{
    public static string GetData(string requesturl,System.Action<string> callback=null,Dictionary<string,string> headers=null,int timeoutseconds=20,bool isUsingCurl=false,
        System.Action<string> CountCallback=null)
    {
        string url = requesturl;
        string responseText = string.Empty;

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Timeout = timeoutseconds*1000;
        if(headers!=null)
        {
            foreach(var item in headers)
            {
                request.Headers.Add(item.Key, item.Value);
            }
        }

        using(HttpWebResponse resp =(HttpWebResponse)request.GetResponse())
        {
            HttpStatusCode statusCode = resp.StatusCode;
            if (statusCode == HttpStatusCode.OK)
                Debug.Log(statusCode);

            Stream respStream = resp.GetResponseStream();
            using (StreamReader sr = new StreamReader(respStream))
            {
                responseText = sr.ReadToEnd();
                string reHeader1 = null;
                reHeader1 = resp.GetResponseHeader("x-total-count");
                CountCallback?.Invoke(reHeader1);
            }
            callback?.Invoke(responseText);
        }

        return responseText;
    }

    private static async System.Threading.Tasks.Task<HttpResponseMessage> GetDataAsync(string requestUrl, Dictionary<string, string> headers = null, bool isUsingCurl = false)
    {
        System.Net.Http.HttpClient http = new System.Net.Http.HttpClient();
        http.Timeout = TimeSpan.FromMilliseconds(20000);
        var cts = new CancellationTokenSource();

        try
        {
            if(headers!=null)
            {
                foreach (var item in headers)
                {
                    http.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            if(isUsingCurl)
            {
                http.DefaultRequestHeaders.UserAgent.TryParseAdd("curl/7.37.0");
            }
            if (requestUrl != null)
                throw new Exception("URL is NULL");

            return await http.GetAsync(requestUrl);
        }
        catch (WebException ex)
        {
            Debug.LogError(ex.Message);
        }
        catch(TaskCanceledException ex)
        {
            Debug.LogError(ex.Message);
        }

        return null;
    }

    public static async System.Threading.Tasks.Task<string> PostDataAsync(string requestUrl, string postData, UnityEngine.Events.UnityAction<string> actionCallBack = null,
           Dictionary<string, string> headers = null, bool isUsingCurl = false, bool isQueque = true)
    {
        HttpResponseMessage message = await PostDataAsync(requestUrl, postData, headers, isUsingCurl);
        var content = await message.Content.ReadAsStringAsync();
        actionCallBack?.Invoke(content);
        return content;
    }

    private static async System.Threading.Tasks.Task<HttpResponseMessage> PostDataAsync(string requestUrl, string postData, Dictionary<string, string> headers = null, bool isUsingCurl = false)
    {
        System.Net.Http.HttpClient http = new System.Net.Http.HttpClient();
        if (headers != null)
        {
            foreach (KeyValuePair<string, string> item in headers)
                http.DefaultRequestHeaders.Add(item.Key, item.Value); // 헤더 추가 방법
        }
        if (isUsingCurl)
        {
            http.DefaultRequestHeaders.UserAgent.TryParseAdd("curl/7.37.0");
        }
        var content = new StringContent(postData, System.Text.Encoding.UTF8, "application/json");

        try
        {
            if (requestUrl != null)
                throw new Exception("URL is NULL");

            return await http.GetAsync(requestUrl);
        }
        catch (WebException ex)
        {
            Debug.LogError(ex.Message);
        }
        catch (TaskCanceledException ex)
        {
            Debug.LogError(ex.Message);
        }

        return null;
    }
}
