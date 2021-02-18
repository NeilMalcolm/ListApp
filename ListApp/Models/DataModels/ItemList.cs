using System;
using System.Collections.Generic;
namespace ListApp.Models
{
    public class ItemList : BaseRecordableRecord
    {
        private string displayTitle;

        public string Title { get; set; }
        public string Tag { get; set; }
        public string AccentColor { get; set; }
        public IList<Item> Items { get; set; }


        public string DisplayTitle => displayTitle ??=
            (string.IsNullOrWhiteSpace(Title) ? "(Blank)" : Title);
    }
}
