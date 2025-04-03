using AutoMapper;
using MovieRentalApp.Models.DTOs;
using MovieRentalApp.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MovieRentalApp.Service.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Client mappings
            CreateMap<Client, ClientDto>();
            CreateMap<ClientCreateDto, Client>();
            CreateMap<ClientUpdateDto, Client>();

            // Movie mappings
            CreateMap<Movie, MovieDto>();
            CreateMap<MovieCreateDto, Movie>();
            CreateMap<MovieUpdateDto, Movie>();
        }
    }
}