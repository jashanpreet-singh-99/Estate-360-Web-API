using AutoMapper;
using EState_360.Core.Entities;
using EState_360.Web_API.DTOs;

namespace EState_360.Web_API.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<ListingCreateDto, Listing>()
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.PostedOn, opt => opt.Ignore())
				.ForMember(dest => dest.Rating, opt => opt.Ignore())
                .ForMember(dest => dest.RatingCount, opt => opt.Ignore());
        }
	}
}

