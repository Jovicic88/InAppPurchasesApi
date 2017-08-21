﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using InAppPurchasesApi.Models;

namespace InAppPurchasesApi.DataAccessLayer
{
    public class PurchasesRespository : IPuchasesRespository, IDisposable
    {
        private PurchasesIAPEntities context;

        public PurchasesRespository(PurchasesIAPEntities context)
        {
            this.context = context;
        }

        #region Guns
        public IQueryable<Gun> GetGuns()
        {
            return context.Guns;
        }

        public List<Gun> GetGunsToList()
        {
            return context.Guns.ToList();
        }

        public Gun GetGunById(int? gunId)
        {
            return context.Guns.Find(gunId);
        }

        public void AddGun(Gun gun)
        {
            context.Guns.Add(gun);
        }

        public void EditGun(Gun gun)
        {
            context.Entry(gun).State = EntityState.Modified;
        }

        public void DeleteGun(int? gunId)
        {
            var gun = context.Guns.Find(gunId);
            context.Guns.Remove(gun);
        }
#endregion

        #region WeaponLevels

        public IQueryable<Level> GetLevels()
        {
            return context.Levels;
        }

        public List<Level> GetLevelsToList()
        {
            return context.Levels.Include(l => l.Gun).ToList();
        }

        public Level GetLevelById(int? levelId)
        {
            return context.Levels.Find(levelId);
        }

        public void AddLevel(Level level)
        {
            context.Levels.Add(level);
        }

        public void EditLevel(Level level)
        {
            context.Entry(level).State = EntityState.Modified;
        }

        public void DeleteLevel(int? levelId)
        {
            var level = context.Levels.Find(levelId);
            context.Levels.Remove(level);
        }
#endregion

        #region UserWeapons
        public List<UserWeapon> GetUserWeaponsToList(int userId, int gameId)
        {
            return context.UserWeapons.Where(u => u.UserId == userId && u.GameId == gameId).ToList();
        }

        public UserWeapon GetUserWeapon(UserWeapon userWeapon)
        {
           return context.UserWeapons.First(u => u.WeaponId == userWeapon.WeaponId && u.GameId == userWeapon.GameId && u.UserId == userWeapon.UserId);
        }

        public void AddUserWeapon(UserWeapon userWeapon)
        {
            context.UserWeapons.Add(userWeapon);
        }

        public void EditUserWeapon(UserWeapon userWeapon)
        {
            context.Entry(userWeapon).State = EntityState.Modified;
        }

        public void DeleteUserWeapon(int? userWeaponId)
        {
            var userWeapon = context.UserWeapons.Find(userWeaponId);
            context.UserWeapons.Remove(userWeapon);
        }
        #endregion

        #region UserGames
        public UserGame GetUserGame(int userId, int gameId)
        {
            return context.UserGames.First(u => u.UserId == userId && u.GameId == gameId);
        }

        public void AddUserGame(UserGame userGame)
        {
            context.UserGames.Add(userGame);
        }

        public void EditUserGame(UserGame userGame)
        {
            context.Entry(userGame).State = EntityState.Modified;
        }

        public void DeleteUserGame(int? userGameId)
        {
            var userGame = context.UserGames.Find(userGameId);
            context.UserGames.Remove(userGame);
        }
        #endregion
#region Games
        public List<Game> GetGames()
        {
            return context.Games.ToList();
        }

        public Game GetGameById(int? gameId)
        {
            return context.Games.Find(gameId);
        }

        public void AddGame(Game game)
        {
            context.Games.Add(game);
        }

        public void EditGame(Game game)
        {
            context.Entry(game).State = EntityState.Modified;
        }

        public void DeleteGame(int? gameId)
        {
            var game = context.Games.Find(gameId);
            context.Games.Remove(game);
        }
        #endregion

        #region User

        public User GetUser(User user)
        {
            return context.Users.First(u => u.Email == user.Email && u.Password == user.Password);
        }

        public void AddUser(User user)
        {
            context.Users.Add(user);
        }

        public bool CheckIfUserExists(string email)
        {
            return context.Users.Any(u => u.Email == email);
        }
        #endregion

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}