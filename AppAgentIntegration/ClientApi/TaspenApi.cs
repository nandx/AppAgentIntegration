using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using AppAgentIntegration.Constant;
using AppAgentIntegration.Dao;
using AppAgentIntegration.Dto;
using Newtonsoft.Json;

namespace AppAgentIntegration.ClientApi
{
    public class TaspenApi
    {
         public AgentDto GetValue()
        {
            var token = GetToken();
            if (token == null)
                return null;

            var endpoint = GetParamValue(AuthTokenEnum.TASPEN_API_URL);
            var urlTaspen = endpoint + "/agent/profiles";
            var client = new HttpClient {BaseAddress = new Uri(urlTaspen)};
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = client.GetAsync("").Result;

            String result = null;
            //if (response.IsSuccessStatusCode) result = response.Content.ReadAsAsync<AgentDto>().Result;
            if (response.IsSuccessStatusCode) result = response.Content.ReadAsStringAsync().Result;
            
            client.Dispose();

            return result != null ? JsonConvert.DeserializeObject<AgentDto>(result) : null;
        }

        public string CreateNewAgent(string requestbody)
        {
            var token = GetToken();
            if (token == null)
                return null;

            var endpoint = GetParamValue(AuthTokenEnum.TASPEN_API_URL);
            var urlTaspen = endpoint + "/agent/profiles";
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlTaspen);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var data = new StringContent(requestbody, Encoding.UTF8, "application/json");

            var response = client.PostAsync(urlTaspen, data).Result;
            var result = response.Content.ReadAsStringAsync().Result;


            client.Dispose();
            return result;
        }


        public string UpdateAgent(string requestbody, int id)
        {
            Console.WriteLine("UpdateAgent - id : "+id);
            var token = GetToken();
            if (token == null)
                return null;

            var endpoint = GetParamValue(AuthTokenEnum.TASPEN_API_URL);
            var urlTaspen = endpoint + "/agent/profiles/" + id;
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlTaspen);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var data = new StringContent(requestbody, Encoding.UTF8, "application/json");

            var response = client.PutAsync(urlTaspen, data).Result;
            var result = response.Content.ReadAsStringAsync().Result;


            client.Dispose();
            return result;
        }

        public string DeleteAgent(int idprofile)
        {
            var token = GetToken();
            if (token == null)
                return null;

            var endpoint = GetParamValue(AuthTokenEnum.TASPEN_API_URL);
            var urlTaspen = endpoint + "/agent/profiles/" + idprofile;
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlTaspen);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.DeleteAsync(urlTaspen).Result;

            var result = response.StatusCode.GetHashCode() + " - " + response.StatusCode;
            return result;
        }


        public string SearchInstanceProfile(string paramName)
        {
            var token = GetToken();
            if (token == null)
                return null;

            var endpoint = GetParamValue(AuthTokenEnum.TASPEN_API_URL);
            var urlTaspen = endpoint +
                            "/instance/profiles?filters[0][field]=name&filters[0][operator]==&filters[0][keyword]=" +
                            paramName;
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlTaspen);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = client.GetAsync(urlTaspen).Result;
            var result = response.Content.ReadAsStringAsync().Result;


            client.Dispose();
            return result;
        }

        public string SearchAgentPosition(string paramName)
        {
            var token = GetToken();
            if (token == null)
                return null;

            var endpoint = GetParamValue(AuthTokenEnum.TASPEN_API_URL);
            var urlTaspen = endpoint +
                            "/agent/positions?filters[0][field]=name&filters[0][operator]=like&filters[0][keyword]=" +
                            paramName;
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlTaspen);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = client.GetAsync(urlTaspen).Result;
            var result = response.Content.ReadAsStringAsync().Result;


            client.Dispose();
            return result;
        }

        private static string GetToken()
        {
            var dto = GetTokenResponseDto();
            return dto?.access_token;
        }

        private static AuthTokenResponseDto GetTokenResponseDto()
        {
            var endpoint = GetParamValue(AuthTokenEnum.TASPEN_API_URL);
            Console.WriteLine("GetTokenResponseDto - endpoint : "+endpoint);
            var urlTaspen = endpoint + "/oauth/access_token";
            var client = new HttpClient();
            client.BaseAddress = new Uri(urlTaspen);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var payload = GetAuthTokenDto();

            var requestbody = JsonConvert.SerializeObject(payload);
            Console.WriteLine("requestbody :: "+requestbody);
            var data = new StringContent(requestbody, Encoding.UTF8, "application/json");

            var response = client.PostAsync(urlTaspen, data).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine("GetTokenResponseDto :: "+result);

            if (result != null)
                return JsonConvert.DeserializeObject<AuthTokenResponseDto>(result);

            return null;
        }


        private static AuthTokenDto GetAuthTokenDto()
        {
            var payload = new AuthTokenDto
            {
                grant_type = GetParamValue(AuthTokenEnum.AUTH_GRANT_TYPE),
                client_id = GetParamValue(AuthTokenEnum.AUTH_CLIENT_ID),
                client_secret = GetParamValue(AuthTokenEnum.AUTH_CLIENT_SECRET),
                username = GetParamValue(AuthTokenEnum.AUTH_USERNAME),
                password = GetParamValue(AuthTokenEnum.AUTH_PASSWORD)
            };
            return payload;
        }


        private static string GetParamValue(AuthTokenEnum authTokenEnum)
        {
            var dao = new SysParamDao();
            var data = dao.GetParamValue(authTokenEnum.ToString());
            return data?.PARAMVALUE;
        }


        public int FindByInstanceProfileName(string name)
        {
            var result = SearchInstanceProfile(name);
            var dto = JsonConvert.DeserializeObject<InstanceProfileResponseDto>(result);
            if (dto?.ListInstanceProfile != null && dto.ListInstanceProfile.Count > 0)
            {
                return dto.ListInstanceProfile[0].Id;
            }

            return 2;
        }
    }
}