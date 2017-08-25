using InAppPurchasesApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace InAppPurchasesApi.DataAccessLayer
{
    interface IPuchasesRespository
    {
        IQueryable<Gun> GetGuns();
        List<Gun> GetGunsToList();
        Gun GetGunById(int? gunId);
        void AddGun(Gun gun);
        void EditGun(Gun gun);
        void DeleteGun(int? gunId);

        IQueryable<Level> GetLevels();
        List<Level> GetLevelsToList();
        Level GetLevelById(int? levelId);
        void AddLevel(Level level);
        void EditLevel(Level level);
        void DeleteLevel(int? levelId);

        UserGame GetUserGame(int userId, int gameId);
        void AddUserGame(UserGame userGame);
        void EditUserGame(UserGame userGame);
        void DeleteUserGame(int? userGameId);

        List<UserWeapon> GetUserWeaponsToList(int userId, int gameId);
        UserWeapon GetUserWeapon(UserWeapon userWeapon);
        void AddUserWeapon(UserWeapon userWeapon);
        void EditUserWeapon(UserWeapon userWeapon);
        void DeleteUserWeapon(int? userWeaponId);

        List<Game> GetGames();
        Game GetGameById(int? gameId);
        void AddGame(Game game);
        void EditGame(Game game);
        void DeleteGame(int? gameId);

        User GetUser(User user);
        void AddUser(User user);
        bool CheckIfUserExists(string email);

        List<UserMap> GetUserMapsToList(int userId, int gameId);
        UserMap GetUserMap(UserMap userMap);
        void AddUserMap(UserMap userMap);
        void EditUserMap(UserMap userMap);
        void DeleteUserMap(int? userMapId);

        void Save();
    }
}
