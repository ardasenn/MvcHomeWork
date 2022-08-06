namespace MediumClone.Models
{
    public class RoleModificationVM
    {
        public string RoleName { get; set; }
        public string RoleId { get; set; }

        public string[] AddIds { get; set; }
        public string[] DeletedIds { get; set; }
    }
}
