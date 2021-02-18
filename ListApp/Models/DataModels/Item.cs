namespace ListApp.Models
{
    public class Item : BaseRecordableRecord
    {
        public int Order { get; set; }
        public bool Selected { get; set; }
        public string Text { get; set; }
    }
}
