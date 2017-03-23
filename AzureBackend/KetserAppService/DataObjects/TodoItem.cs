using Microsoft.Azure.Mobile.Server;

namespace KetserAppService.DataObjects
{
    public class TodoItem : EntityData
    {
        //kjhfds
        public string Text { get; set; }
        public bool Complete { get; set; }
    }
}