namespace prjMVCAPIMavel.Models
{
    public class TblAvenger
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public virtual ICollection<TblContact> TblContacts { get; set; } = new List<TblContact>();
    }
}
