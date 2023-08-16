using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Entities;

namespace WebAPI.Models
{
    public class ResponseHotel
    {

        public ResponseHotel(Hotel hotel)
        {
            Id = hotel.ID;
            Name = hotel.Name;
            CountOfStars = hotel.CountOfStars;
            CountryName = hotel.CountryCode;
            HotelImage = hotel.HotelImage.ToList().FirstOrDefault()?.ImageSource;
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public int CountOfStars { get; set; }

        public string CountryName { get; set; }
        public byte[] HotelImage { get; set; }
    }
}