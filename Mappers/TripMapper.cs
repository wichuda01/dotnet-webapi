using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_webapi_ef.DTOs.Trip;
using dotnet_webapi_ef.Models;

namespace dotnet_webapi_ef.Mappers
{
    public static class TripMapper
    {
        //method's map from Model to DTO
        public static TripDTO ToTripDTO(this Trip trip)
        {// (this Trip trip) => add ToTripDTO into Trip class
            return new TripDTO
            {
                Idx = trip.Idx,
                Name = trip.Name,
                Country = trip.Country,
                Destinationid = trip.Destinationid,
                Coverimage = trip.Coverimage,
                Detail = trip.Detail,
                Price = trip.Price,
                Duration = trip.Duration,
                Destination=trip.Destination.ToDestinationDTO()
            };
        }
    }
}