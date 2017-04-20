using System;
using System.Collections.Generic;
using System.Linq;
using GameFace.Utils;

namespace GameFace.Struct
{
    public static class Helper
    {
        public static List<UserRecords> AddElement(List<UserRecords> list, UserRecords element)
        {
            for (var i= 0; i < list.Count; i++)
            {
                if (!list[i].Task.Equals(element.Task)) continue;
                var newrecord = new UserRecords(list[i].Task, list[i].NoPerforms + 1, list[i].Value + element.Value);
                list.Remove(list[i]);
                list.Add(newrecord);
                return  list;
            }

            list.Add(element);

            return list;
        }


        public static List<SprintRecords> DevideInSprints(List<UserRecordsDate> list)
        {
            list = list.OrderByDescending(c => c.Date).ToList();

            var dividedSprints = list
                .GroupBy(
                    record => new { First = record.Date.FirstDayOfQuarter(), Last = record.Date.LastDayOfQuarter() },
                    record => record)
                .Select(group =>
                {
                    var listOfUserRecords = new List<UserRecords>();
                    listOfUserRecords = group
                        .Select(record => new UserRecords(record.Task, 1, record.Value))
                        .Aggregate(listOfUserRecords, AddElement);

                    return new SprintRecords(group.Key.First, group.Key.Last, listOfUserRecords);
                })
                .ToList();
            
            return dividedSprints;
        }

        
    }


   


    public struct UserProfile
    {
        public string NickName;
        public int Experience;
        public List<UserAchieve> Achievements;
        public List<UserRecords> Records;
        public UserProfile(string nickname, int experience, List<UserAchieve> achievements, List<UserRecords> records)
        {
            NickName = nickname;
            Experience = experience;
            Achievements = achievements;
            Records = records;
        }
    }
    public struct UserHistory
    {
        public string NickName;
        public int Experience;
        public List<SprintRecords> Sprints;
        public UserHistory(string nickname, int experience, List<SprintRecords> sprints)
        {
            NickName = nickname;
            Experience = experience;
            Sprints = sprints;
        }
    }
    public struct SprintRecords
    {
        public DateTime StartDate;
        public DateTime EndDate;
        public List<UserRecords> Records;
        public SprintRecords(DateTime startDate, DateTime endDate, List<UserRecords> records)
        {
            StartDate = startDate;
            EndDate = endDate;
            Records = records;
        }
    }

    public struct UserRecordsDate
    {
        public string Task;
        public DateTime Date;
        public int Value;
        public UserRecordsDate(string task, DateTime date, int value)
        {
            Task = task;
            Date = date;
            Value = value;
        }
    }
    public struct UserRecords
    {
        public string Task { get; set; }
        public int NoPerforms { get; set; }
        public int Value { get; set; }

        public UserRecords(string task, int noPerforms, int value)
        {
            Task = task;
            NoPerforms = noPerforms;
            Value = value;
        }
    }

    public struct UserAchieve
    {
        public string Achieve { get; set; }
        public int IdTask { get; set; }
      
        public UserAchieve(string achieve, int idTask)
        {
            Achieve = achieve;
            IdTask = idTask;
        }
    }
}
