using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class RegionProfile: Profile
    {

        public RegionProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTOs.Region>();

            //If properties are not the same
            //CreateMap<Models.Domain.Region, Models.DTOs.Region>()
            //    .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id));

            //Reversing a map
            //CreateMap<Models.Domain.Region, Models.DTOs.Region>().ReverseMap();
        }
    }
}
