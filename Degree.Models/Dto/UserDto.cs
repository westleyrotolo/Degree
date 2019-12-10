using System;
namespace Degree.Models.Dto
{
    public class UserDto
    {

        public string Name { get; set; }
        public string ScreenName { get; set; }
        public string ProfileImage { get; set; }
        public int Statuses { get; set; }
        public int Retweets { get; set; }
        public int Favorites { get; set; }  
    }
}
