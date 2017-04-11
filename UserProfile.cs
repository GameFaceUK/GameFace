using System;
using System.Collections.Generic;

namespace GameFace.Struct
{
    public static class Helper
    {
        public static List<SprintRecords> DevideBySprints(List<UserRecords> list)
        {
            DateTime date = list[0].Date;
            int quarterNumber = (date.Month - 1) / 3 + 1;
            DateTime firstDayOfQuarter = new DateTime(date.Year, (quarterNumber - 1) * 3 + 1, 1);
            DateTime lastDayOfQuarter = firstDayOfQuarter.AddMonths(3).AddDays(-1);

            var listtemp = new List<UserRecords>();
            var sprint = new List<SprintRecords>();
            foreach (var record in list)
            {
                if (firstDayOfQuarter < record.Date && record.Date < lastDayOfQuarter)
                {
                    listtemp.Add(record);
                }
                else
                {
                    sprint.Add(new SprintRecords(firstDayOfQuarter, lastDayOfQuarter, listtemp));
                    listtemp = new List<UserRecords> { record };
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
        public List<UserRecords> Records;
        public UserProfile(string nickname, int experience, List<UserRecords> records)
        {
            NickName = nickname;
            Experience = experience;
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

    public struct UserRecords
    {
        public int Level;
        public string Achievement;
        public string Task;
        public DateTime Date;
        public int Value;
        public UserRecords(int level, string achievement, string task, DateTime date, int value)
        {
            Level = level;
            Achievement = achievement;
            Task = task;
            Date = date;
            Value = value;
        }
    }
}
