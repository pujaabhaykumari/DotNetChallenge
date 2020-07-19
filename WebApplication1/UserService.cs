
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using WebApplication1.Helper;
using WebApplication1.Model;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace WebApplication1
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        ViewRecord AddRecord(Record rcd,string token);
      //  IEnumerable<User> GetAll();
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private string getTokenUrl = " https://infinitysmartapi-dev.azurewebsites.net/api/auth";

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            // return null if user not found
            if (model.Username == null) return null;
          

            // authentication successful so generate jwt token
            var token = generateJwtToken(model);

            return token;
        }


        public ViewRecord AddRecord(Record rcd,string token)
        {
            ViewRecord ViewRecord = new ViewRecord();

            try
            {
              
                var client = new HttpClient();
               client.DefaultRequestHeaders.Add("Authorization", token);
                var request = new HttpRequestMessage(HttpMethod.Post, new Uri(getTokenUrl));
                // Setup header(s)
                request.Headers.Add("Accept", "application/json");
               
                // Add body content
                var json = JsonConvert.SerializeObject(rcd);
                request.Content = new StringContent(
                    json.ToString(),
                    Encoding.UTF8,
                    "application/json"
                );
                // Send the request
                var result = client.SendAsync(request).Result;
                ViewRecord = result.Content.ReadAsAsync<ViewRecord>().Result;
                return ViewRecord;
            }
            catch (Exception ex)
            {
                return ViewRecord;
            }
        }

        // helper methods

        private AuthenticateResponse generateJwtToken(AuthenticateRequest user)
        {
            
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(getTokenUrl));
            // Setup header(s)
            request.Headers.Add("Accept", "application/json");
            // Add body content

            User userdata = new User();
            userdata.Username = user.Username;
            userdata.Password = user.Password;
            var json = JsonConvert.SerializeObject(user);
            request.Content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );
            var contentType = new MediaTypeWithQualityHeaderValue
("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            // Send the request
            var result = client.SendAsync(request).Result;
            var responseBody = result.Content.ReadAsAsync<AuthenticateResponse>().Result;
            //HttpContext.Session.SetString("token", jwt.Token);
            return responseBody;

        }
    }
     

    
    }

