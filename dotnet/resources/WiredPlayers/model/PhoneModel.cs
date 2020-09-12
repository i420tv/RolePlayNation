using System.Collections.Generic;

namespace WiredPlayers.model
{
    public class PhoneModel
    {
        public int itemId { get; set; }
        public int number { get; set; }
        public List<ContactModel> contacts { get; set; }
    }
}
