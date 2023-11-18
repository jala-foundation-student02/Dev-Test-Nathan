namespace ToDoList.API.Models
{
    public class ToDoListModel
    {
        public int Id { get; set; }

        public string TaskTitle { get; set; }

        public string TaskCategory { get; set; }

        public string TaskDescription { get; set; }

        public DateTime TaskDeadline { get; set; }

        public DateTime UpdateDate { get; set; }

        public bool IsDeleted { get; set; }

        public string Status = "OnGoing";

        public void Update(string title, string category, string description, DateTime deadline)
        {
            TaskTitle = title;
            TaskCategory = category;
            TaskDescription = description;
            TaskDeadline = deadline;
            UpdateDate = DateTime.Now;
        }

        public void Delete()
        {
            IsDeleted = true;
        }

        /*public void OnGoing()
        {
            Status = "OnGoing";
        }*/

        public void Completed()
        {
            Status = "Completed";
        }

        public void Overdue()
        {
            Status = "Overdue";
        }

        

    }
}
