using AutoMapper;
using HPC.Application.Common.Mappings;
using HPC.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace HPC.Application.Dtos.Ships
{
    public class ShipDto : IMapFrom<Ship>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public decimal LengthInMetres { get; set; }
        [Required]
        public decimal WidthInMetres { get; set; }
        [Required]
        public DateTime CreatedInUtc { get; set; }
        [Required]
        public DateTime ModifiedInUtc { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Ship, ShipDto>()
                .ForMember(dest => dest.LengthInMetres, opt => opt.MapFrom(src => Math.Round(src.LengthInMetres, 2)))
                .ForMember(dest => dest.WidthInMetres, opt => opt.MapFrom(src => Math.Round(src.WidthInMetres, 2)));
        }
    }
}
