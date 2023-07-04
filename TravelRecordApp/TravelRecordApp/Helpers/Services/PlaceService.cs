using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TravelRecordApp.Deserializers;
using TravelRecordApp.Helpers.Interfaces;
using TravelRecordApp.Models;
using static System.Net.WebRequestMethods;

namespace TravelRecordApp.Helpers.Services
{
    public class PlaceService : IPlaceRepository
    {
        private static readonly HttpClient httpClient;

        static PlaceService()
        {
            httpClient = new HttpClient();
        }

        //Server will attempt to retrieve the IP address from the request, and geolocate that IP address.
        public static async Task<Places> GetPlacesAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Constants.nearbyPlacesUrl),
                Headers =
                {
                    { "accept", "application/json" }
                },
            };

            request.Headers.TryAddWithoutValidation("Authorization", Constants.authenticationKey);

            using (var response = await httpClient.SendAsync(request))
            {
                return response.IsSuccessStatusCode ?
                    await Deserializer.DeserializeToGeneric<Places>(response) : null;
            }
        }

        //Get places nearby user location
        public static async Task<Places> GetPlacesAsync(double latitude, double longitude)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(string.Concat(
                        Constants.nearbyPlacesUrl, "?", string.Format(Constants.llQuery, latitude, longitude))),
                    Headers =
                    {
                        { "accept", "application/json" }
                    },
                };

                request.Headers.TryAddWithoutValidation("Authorization", Constants.authenticationKey);

                using (var response = await httpClient.SendAsync(request))
                {
                    return response.IsSuccessStatusCode ?
                        await Deserializer.DeserializeToGeneric<Places>(response) : null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
