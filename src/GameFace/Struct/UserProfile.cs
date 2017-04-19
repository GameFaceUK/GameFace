using System;
using System.Collections.Generic;

namespace GameFace.Struct
{
    public static class Helper
    {
        public static List<UserRecords> AddElement(List<UserRecords> list, UserRecords element)
        {
            var found = false;
            if (list.Count == 0)
            {
                list.Add(element);
            }
            else
            {
                for (var i= 0; i < list.Count; i++)
                {
                    if (!list[i].Task.Equals(element.Task)) continue;
                    var newrecord = new UserRecords(list[i].Task, list[i].NoPerforms + 1, list[i].Value + element.Value);
                    list.Remove(list[i]);
                    list.Add(newrecord);
                    found = true;
                }
                if (!found)
                {
                    list.Add(element);
                }
            }
            
            return list;
        }


        public static List<SprintRecords> DevideBySprints(List<UserRecordsDate> list)
        {
            var date = list[0].Date;
            int quarterNumber = (date.Month - 1) / 3 + 1;
            var firstDayOfQuarter = new DateTime(date.Year, (quarterNumber - 1) * 3 + 1, 1);
            var lastDayOfQuarter = firstDayOfQuarter.AddMonths(3).AddDays(-1);

            var listtemp = new List<UserRecords>();
            var sprint = new List<SprintRecords>();
            foreach (var record in list)
            {
                if (firstDayOfQuarter < record.Date && record.Date < lastDayOfQuarter)
                {
                    listtemp = AddElement(listtemp, new UserRecords(record.Task,1,record.Value));
                }
                else
                {
                    sprint.Add(new SprintRecords(firstDayOfQuarter, lastDayOfQuarter, listtemp));
                    listtemp = new List<UserRecords> { new UserRecords(record.Task, 1, record.Value) };
                    date = record.Date;
                    quarterNumber = (date.Month - 1) / 3 + 1;
                    firstDayOfQuarter = new DateTime(date.Year, (quarterNumber - 1) * 3 + 1, 1);
                    lastDayOfQuarter = firstDayOfQuarter.AddMonths(3).AddDays(-1);
                }
            }
            if (listtemp.Count > 0)
            {
                sprint.Add(new SprintRecords(firstDayOfQuarter, lastDayOfQuarter, listtemp));
            }
            return sprint;
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
