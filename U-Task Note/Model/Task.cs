using System;

namespace U_Task_Note.Model
{
    public class Task
    {
        public int ID { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime DeadlineTime { get; set; }
        public Priority Priority { get; set; }
        public DateTime NoticeTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreationDate { get; set; }
        public RepeatFrequency RepeatFrequency { get; set; }
    }

    public enum Priority
    {
        Низький,
        Середній,
        Високий
    }

    public enum RepeatFrequency
    {
        Немає,
        Щоденно,
        Щотижня,
        Налаштувати
    }
}
