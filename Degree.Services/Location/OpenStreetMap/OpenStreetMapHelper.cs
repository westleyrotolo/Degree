using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Degree.Models.Twitter;
using Newtonsoft.Json;
using Nominatim.API.Geocoders;

namespace Degree.Services.Location.OpenStreetMap
{
    public class OpenStreetMapHelper : ILocation
    {

        public async Task<List<GeoUser>> GeoReverse(string place)
        {
            try
            {


                var geoReverse = new ForwardGeocoder();
                var result = await geoReverse.Geocode(new Nominatim.API.Models.ForwardGeocodeRequest
                {
                    queryString = place
                });
                if (result.Length > 0)
                {
                    var geoCode = result[0];
                    return new List<GeoUser>() {
                        new GeoUser()
                        {
                            DisplayName = geoCode.DisplayName,
                            Id = geoCode.PlaceID.ToString(),
                            Importance = geoCode.Importance,
                            Lat = geoCode.Latitude,
                            Lon = geoCode.Longitude,
                            Type = geoCode.ClassType
                        }
                    };
                }
                return null; ;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
