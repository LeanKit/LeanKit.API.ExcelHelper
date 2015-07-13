using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LeanKit.API.Client.Library.TransferObjects;

namespace LeanKit.API.ExcelHelper
{
	internal class Mappings
	{
		public static void Initialize()
		{
			Mapper.CreateMap<Identifier, LeanKitIdentifier>();
			Mapper.CreateMap<LaneIdentifier, LeanKitLaneIdentifier>();
			Mapper.CreateMap<CardType, LeanKitCardType>();
			Mapper.CreateMap<CardView, LeanKitCard>()
				.ForMember(s => s.CustomIcon, opt => opt.ResolveUsing(src => src.ClassOfServiceTitle))
				.ForMember(s => s.AssignedUsers, opt => opt.ResolveUsing(src =>
				{
					if (src.AssignedUsers != null && src.AssignedUsers.Count > 0)
					{
						return src.AssignedUsers.Select(u => u.EmailAddress).ToArray();
					}
					return new string[0];
				}));
			Mapper.CreateMap<Lane, LeanKitLane>();
			Mapper.CreateMap<Board, LeanKitBoard>();
			Mapper.CreateMap<BoardListing, LeanKitBoardListItem>();
			Mapper.CreateMap<BoardIdentifiers, LeanKitBoardMetadata>();
			Mapper.AssertConfigurationIsValid();
		}
	}
}
