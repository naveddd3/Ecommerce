namespace Domain.Entities
{
    public class ErrorLog
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime LogDate { get; set; } = DateTime.Now;
        public string LogLevel { get; set; } // Example: "Error", "Warning", "Info"
    }
}
