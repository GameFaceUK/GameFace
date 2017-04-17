using GameFace.Controllers;
using GameFace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameFace.Utils
{
    public static class InsertingRecords
    {

        public static async Task InsertAchievements(UsersAchievements userachieve)
        {
            using (var database = new GameFaceContext())
            {                
                try
                {
                    database.UsersAchievements.Add(userachieve);
                    await database.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                }
            }
        }





    }
}
