using Microsoft.AspNetCore.Http;

namespace DATINGAPP.API.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response,string message){

                response.Headers.Add("Application-Error",message);
                response.Headers.Add("Application-Control-Expose-Error","Application-Error");
                response.Headers.Add("Application-Control-Allow-Origin","*");

        }
    }
}