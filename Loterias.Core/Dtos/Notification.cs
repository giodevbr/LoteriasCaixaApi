namespace Loterias.Core.Dtos
{
    public class Notification
    {
        public Notification()
        {
            Key = Guid.NewGuid();
            Date = DateTime.Now;
        }

        public Notification(string value)
        {
            Key = Guid.NewGuid();
            Value = value;
            Date = DateTime.Now;
        }

        public Guid Key { get; set; }
        public string Value { get; set; }
        public DateTime Date { get; set; }
    }
}
