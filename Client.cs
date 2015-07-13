using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.InteropServices;
using AutoMapper;
using LeanKit.API.Client.Library;
using LeanKit.API.Client.Library.TransferObjects;

namespace LeanKit.API.ExcelHelper
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	public class Client
	{
		private ILeanKitApi _api;
		private readonly ObjectCache _cache = MemoryCache.Default;

		public Client()
		{
			Mappings.Initialize();
		}

		private void InitCheck()
		{
			if (_api == null) throw new ArgumentException("You must first call Initialize() to set account authentication");
		}

		public bool Initialize(string accountName, string emailAddress, string password)
		{
			try
			{
				var lkFactory = new LeanKitClientFactory();
				var lkAuth = new LeanKitBasicAuth
				{
					Hostname = accountName,
					Username = emailAddress,
					Password = password
				};
				_api = lkFactory.Create(lkAuth);
				// Test authentication
				var boards = _api.GetBoards();
				if (boards == null) return false;
				_cache.Set("boards", boards.ToArray(), DateTimeOffset.Now.AddMinutes(5));
				return true;
			}
			catch (Exception)
			{
				_api = null;
				return false;
			}
		}

		public LeanKitBoard GetBoard(long boardId)
		{
			InitCheck();
			var board = _api.GetBoard(boardId);
			return Mapper.Map<LeanKitBoard>(board);
		}

		public LeanKitBoardMetadata GetBoardMetadata(long boardId)
		{
			InitCheck();
			var cacheKey = "boardmetadata-" + boardId;
			var cachedMetadata = _cache.Get(cacheKey);
			BoardIdentifiers identifiers;
			if (cachedMetadata != null)
			{
				identifiers = (BoardIdentifiers) cachedMetadata;
			}
			else
			{
				identifiers = _api.GetBoardIdentifiers(boardId);
				_cache.Set(cacheKey, identifiers, DateTimeOffset.Now.AddMinutes(5));
			}
			return Mapper.Map<LeanKitBoardMetadata>(identifiers);
		}

		public LeanKitBoardListItem[] GetBoards()
		{
			InitCheck();
			var cachedBoards = _cache.Get("boards");
			BoardListing[] boards;
			if (cachedBoards != null)
			{
				boards = (BoardListing[]) cachedBoards;
			}
			else
			{
				Console.WriteLine("Getting boards from API");
				boards = _api.GetBoards().ToArray();
				_cache.Set("boards", boards, DateTimeOffset.Now.AddMinutes(5));
			}
			return Mapper.Map<LeanKitBoardListItem[]>(boards);
		}

		public LeanKitCard GetCard(long boardId, long cardId)
		{
			InitCheck();
			var c = _api.GetCard(boardId, cardId);
			return Mapper.Map<LeanKitCard>(c);
		}

		public LeanKitCard GetCardByExternalId(long boardId, string cardId)
		{
			InitCheck();
			var c = _api.GetCardByExternalId(boardId, cardId);
			return Mapper.Map<LeanKitCard>(c);
		}

		public long AddCardComment(long boardId, long cardId, string comment)
		{
			InitCheck();
			var c = new Comment {Text = comment};
			return _api.PostComment(boardId, cardId, c);
		}

		public long AddCard(long boardId, string title, string description, int priority, int size, int index, long laneId,
			string cardType, string customIcon, long parentCardId, bool isBlocked, string blockReason,
			string externalCardId, string externalSystemName, string externalSystemUrl,
			string startDate, string dueDate, string tags, string assignedUsers)
		{
			InitCheck();
			var card = new Card
			{
				Title = title,
				Description = description,
				Priority = priority,
				Index = index,
				LaneId = laneId,
				IsBlocked = isBlocked,
				BlockReason = blockReason,
				ExternalCardID = externalCardId,
				ExternalSystemName = externalSystemName,
				ExternalSystemUrl = externalSystemUrl,
				StartDate = startDate,
				DueDate = dueDate,
				Tags = tags
			};

			if (size > 0) card.Size = size;
			if (parentCardId > 0) card.ParentCardId = parentCardId;
			card.AssignedUserIds = MapAssignedUsers(boardId, assignedUsers);
			card.TypeId = MapCardType(boardId, cardType);
			card.ClassOfServiceId = MapCustomIcon(boardId, customIcon);

			var result = _api.AddCard(boardId, card);
			return result.CardId;
		}

		public long UpdateCard(long boardId, long cardId, string title, string description, int priority, int size, int index,
			long laneId, string cardType, string customIcon, long parentCardId, bool isBlocked, string blockReason,
			string externalCardId, string externalSystemName, string externalSystemUrl,
			string startDate, string dueDate, string tags, string assignedUsers)
		{
			InitCheck();
			var card = new Card
			{
				Id = cardId,
				Title = title,
				Description = description,
				Priority = priority,
				Index = index,
				LaneId = laneId,
				IsBlocked = isBlocked,
				BlockReason = blockReason,
				ExternalCardID = externalCardId,
				ExternalSystemName = externalSystemName,
				ExternalSystemUrl = externalSystemUrl,
				StartDate = startDate,
				DueDate = dueDate,
				Tags = tags
			};

			if (size > 0) card.Size = size;
			if (parentCardId > 0) card.ParentCardId = parentCardId;
			card.AssignedUserIds = MapAssignedUsers(boardId, assignedUsers);
			card.TypeId = MapCardType(boardId, cardType);
			card.ClassOfServiceId = MapCustomIcon(boardId, customIcon);

			var result = _api.UpdateCard(boardId, card);
			return result.CardDTO.Id;
		}

		private long[] MapAssignedUsers(long boardId, string assignedUsers)
		{
			if (string.IsNullOrWhiteSpace(assignedUsers)) return new long[0];
			var metaData = GetBoardMetadata(boardId);
			var ids = new List<long>();
			var emails = assignedUsers.Split(',');
			foreach (var email in emails)
			{
				var user = metaData.BoardUsers.FirstOrDefault(u => u.Name.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase));
				if (user != null) ids.Add(user.Id);
			}
			return ids.ToArray();
		}

		private long MapCardType(long boardId, string typeName)
		{
			var cacheKey = string.Format("cardtype-{0}-{1}", boardId, typeName.Trim().ToLower());
			var cachedObj = _cache.Get(cacheKey);
			LeanKitIdentifier cardType;
			if (cachedObj != null)
			{
				cardType = (LeanKitIdentifier) cachedObj;
			}
			else
			{
				var metaData = GetBoardMetadata(boardId);
				cardType =
					metaData.CardTypes.FirstOrDefault(x => x.Name.Equals(typeName.Trim(), StringComparison.OrdinalIgnoreCase)) ??
					metaData.CardTypes[0];
				_cache.Set(cacheKey, cardType, DateTimeOffset.Now.AddMinutes(5));
			}
			return cardType.Id;
		}

		private long MapCustomIcon(long boardId, string customIcon)
		{
			if (string.IsNullOrWhiteSpace(customIcon)) return 0;
			var cacheKey = string.Format("custom-icon-{0}-{1}", boardId, customIcon.Trim().ToLower());
			var cachedObj = _cache.Get(cacheKey);
			LeanKitIdentifier cos;
			if (cachedObj != null)
			{
				cos = (LeanKitIdentifier) cachedObj;
			}
			else
			{
				var metaData = GetBoardMetadata(boardId);
				cos =
					metaData.ClassesOfService.FirstOrDefault(
						x => x.Name.Equals(customIcon.Trim(), StringComparison.OrdinalIgnoreCase)) ??
					new LeanKitIdentifier {Id = 0, Name = customIcon.Trim().ToLower()};
				_cache.Set(cacheKey, cos, DateTimeOffset.Now.AddMinutes(5));
			}
			return cos.Id;
		}
	}
}
