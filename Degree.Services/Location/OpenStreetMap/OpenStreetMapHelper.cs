using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Degree.Models.Twitter;
using Newtonsoft.Json;
namespace Degree.Services.Location.OpenStreetMap
{
    public class OpenStreetMapHelper : ILocation
    {

        public async Task<List<GeoUser>> GeoReverse(string place)
        {
            try
            {

                var url = $"https://nominatim.openstreetmap.org/search?q={place}&format=json";
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync(url);
                    var content = await result.Content.ReadAsStringAsync();
                    var geoUsers = JsonConvert.DeserializeObject<List<GeoUser>>(content);
                    return geoUsers;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
