using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Degree.Models.Twitter;

namespace Degree.Services.Location
{
    public interface ILocation
    {
       Task<List<GeoUser>> GeoReverse(string place);
    }
}
