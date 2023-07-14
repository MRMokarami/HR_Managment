using System.Net.Http.Headers;
using HR_Managment.MVC.Contracts;

namespace HR_Managment.MVC.Services.Base
{
    public class BaseHttpService
    {
        protected readonly ILocalStorageService localStorage;
        protected readonly IClient client;


        public BaseHttpService(ILocalStorageService localStorage, IClient client)
        {
            this.localStorage = localStorage;
            this.client = client;
        }
        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException exception)
        {
            var result = new Response<Guid>();
            switch (exception.StatusCode)
            {
                case 400:
                {
                    result.Message = "Validation errors have occurred";
                    result.ValidationErrors.Add(exception.Response);
                    result.IsSuccess = false;
                    break;
                }
                case 404:
                {
                    result.Message = "Not Found";
                    result.IsSuccess = false;
                    break;
                }
                default:
                {
                    result.Message = exception.Message;
                    result.ValidationErrors.Add(exception.Response);
                    result.IsSuccess = false;
                    break;
                }
            }
            return result;
        }

        protected void AddBearerToken()
        {
            if (localStorage.Exists("token"))
            {
                client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", localStorage.GetStorageValue<string>("token"));

            }
        }
    }

}
