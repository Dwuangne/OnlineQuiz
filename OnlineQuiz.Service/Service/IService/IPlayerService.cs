using OnlineQuiz.Repository.Entities;
using OnlineQuiz.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQuiz.Service.Service.IService
{
    public interface IPlayerService
    {
        Task<(IEnumerable<Player>, int PageIndex, int TotalPages, int TotalRecords)> GetPlayersAsync(
            int? pageIndex = null,
            int? pageSize = null);
        Task<IEnumerable<Player>> GetAllPlayersAsync();
        Task<Player?> GetPlayerByIdAsync(int playerId);
        Task<IEnumerable<Player>> GetPlayersByRoomIdAsync(int roomId);
        Task<Player> InsertAsync(PlayerBusiness playerBusiness); // ??i t? CreatePlayerAsync
        Task<Player?> UpdateAsync(int id, PlayerBusiness playerBusiness); // ??i t? UpdatePlayerAsync
        Task<Player?> DeleteAsync(int playerId); // ??i t? DeletePlayerAsync
        Task<IEnumerable<Player>> GetPlayersByTeamCodeAsync(int teamCode);
    }
}