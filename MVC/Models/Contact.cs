using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        // user ID from AspNetUser table.
        public string OwnerID { get; set; }

        public string Nazwa { get; set; }
        public string Miasto { get; set; }
        public string Nazwa_Centrum { get; set; }
        public string Metraz { get; set; }
        public string Brygadzista { get; set; }

        public ContactStatus Status { get; set; }
    }

    public enum ContactStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}