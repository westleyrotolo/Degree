using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Degree.Models.Twitter;
using Degree.Services.Location.GoogleMaps.Models;
using Newtonsoft.Json;
using System.Linq;
namespace Degree.Services.Location.GoogleMaps
{
    public class GoogleMapsHelper : ILocation
    {
        public async Task<List<GeoUser>> GeoReverse(string place)
        {
            try
            {

                var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={place}&key=AIzaSyBmStPxH7X6GV1zlLBfnsbE6mLX1R7e2j4";
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync(url);
                    var content = await result.Content.ReadAsStringAsync();
                    var geoUsers = JsonConvert.DeserializeObject<RootGeo>(content);
                    return AdaptGeoUsers(geoUsers.results);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<GeoUser> AdaptGeoUsers(List<GoogleGeo> googleGeo)
        {
            return googleGeo.Select(x => new GeoUser
            {
                Id = x.place_id,
                DisplayName = x.formatted_address,
                Type = string.Join(",",x.types),
                Lat = x.geometry.location.lat,
                Lon = x.geometry.location.lng
                 
            })
            .ToList();
        }
    }
}
