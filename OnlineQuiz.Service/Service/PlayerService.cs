using OnlineQuiz.Repository.Entities;
using OnlineQuiz.Repository.UnitOfWork;
using OnlineQuiz.Service.BusinessModel;
using OnlineQuiz.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQuiz.Service.Service
{
    public class PlayerService : IPlayerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlayerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(IEnumerable<Player>, int PageIndex, int TotalPages, int TotalRecords)> GetPlayersAsync(
            int? pageIndex = null,
            int? pageSize = null)
        {
            return await _unitOfWork.PlayerRepository.GetAsync(
                filter: null,
                orderBy: null,
                includeProperties: "Room", // Bao g?m Room n?u c?n
                pageIndex,
                pageSize);
        }

        public async Task<IEnumerable<Player>> GetAllPlayersAsync()
        {
            var result = await _unitOfWork.PlayerRepository.GetAsync(
                filter: null,
                includeProperties: "Room"); // Bao g?m Room n?u c?n
            return result.Item1;
        }

        public async Task<Player?> GetPlayerByIdAsync(int playerId)
        {
            var entities = await _unitOfWork.PlayerRepository.GetAsync(
                filter: p => p.PlayerId == playerId,
                orderBy: null,
                includeProperties: "Room,PlayerAnswers", // Bao g?m các navigation properties
                pageIndex: 1,
                pageSize: 1
            );
            return entities.Item1.FirstOrDefault();
        }

        public async Task<IEnumerable<Player>> GetPlayersByRoomIdAsync(int roomId)
        {
            var result = await _unitOfWork.PlayerRepository.GetAsync(
                filter: p => p.RoomId == roomId,
                includeProperties: "Room");
            return result.Item1;
        }

        public async Task<Player> InsertAsync(PlayerBusiness playerBusiness)
        {
            if (playerBusiness == null) throw new ArgumentNullException(nameof(playerBusiness));
            if (string.IsNullOrEmpty(playerBusiness.Nickname)) throw new ArgumentException("Nickname cannot be empty.");

            var player = new Player
            {
                TeamCode = playerBusiness.TeamCode,
                Nickname = playerBusiness.Nickname,
                RoomId = playerBusiness.RoomId,
                JoinedAt = DateTime.UtcNow,
                PlayerAnswers = new List<PlayerAnswer>() // Kh?i t?o ?? kh?p v?i entity
            };

            var playerEntity = await _unitOfWork.PlayerRepository.InsertAsync(player);
            await _unitOfWork.SaveChangesAsync();
            return playerEntity;
        }

        public async Task<Player?> UpdateAsync(int id, PlayerBusiness playerBusiness)
        {
            if (playerBusiness == null) throw new ArgumentNullException(nameof(playerBusiness));

            var existingPlayer = await GetPlayerByIdAsync(id);
            if (existingPlayer == null)
            {
                return null;
            }

            // C?p nh?t các tr??ng t? PlayerBusiness
            existingPlayer.Nickname = playerBusiness.Nickname; // B?t bu?c c?p nh?t vì không nullable
            if (playerBusiness.TeamCode != 0) existingPlayer.TeamCode = playerBusiness.TeamCode;
            if (playerBusiness.RoomId != 0) existingPlayer.RoomId = playerBusiness.RoomId;
            // JoinedAt không c?p nh?t vì th??ng immutable sau khi t?o

            var playerEntity = await _unitOfWork.PlayerRepository.UpdateAsync(existingPlayer);
            await _unitOfWork.SaveChangesAsync();
            return playerEntity;
        }

        public async Task<Player?> DeleteAsync(int playerId)
        {
            var player = await GetPlayerByIdAsync(playerId);
            if (player == null)
            {
                return null;
            }

            var playerEntity = await _unitOfWork.PlayerRepository.DeleteAsync(player);
            await _unitOfWork.SaveChangesAsync();
            return playerEntity;
        }

        public async Task<IEnumerable<Player>> GetPlayersByTeamCodeAsync(int teamCode)
        {
            var result = await _unitOfWork.PlayerRepository.GetAsync(
                filter: p => p.TeamCode == teamCode,
                includeProperties: "Room");
            return result.Item1;
        }
    }
}