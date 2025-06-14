using GameHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameHub.Helpers
{
    public class AuthHelper
    {
        public static bool IsLoggedIn(HttpSessionStateBase session)
        {
            return session["userId"] != null;
        }

        public static bool IsAdmin(HttpSessionStateBase session)
        {
            return session["isAdmin"] is bool isAdmin && isAdmin;
        }

        public static string Username(HttpSessionStateBase session)
        {
            return session["username"]?.ToString();
        }

        public static int? UserId(HttpSessionStateBase session)
        {
            return session["userId"] as int?;
        }
        public static bool IsFavorite(int gameId, HttpSessionStateBase session)
        {
            if (session["userId"] == null) return false;

            int userId = (int)session["userId"];
            using (var db = new AppDBContext())
            {
                return db.FavGames.Any(f => f.UserId == userId && f.GameId == gameId);
            }
        }

    }
}