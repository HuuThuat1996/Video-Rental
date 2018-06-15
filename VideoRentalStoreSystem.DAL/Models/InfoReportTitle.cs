namespace VideoRentalStoreSystem.DAL.Models
{
    public class InfoReportTitle
    {
        public string Title { get; set; }
        public string StatusDisk { get; set; }
        public int Quantity { get; set; }
        public override bool Equals(object obj)
        {
            var _obj = obj as InfoReportTitle;
            if (Title == _obj.Title && StatusDisk == _obj.StatusDisk && Quantity == _obj.Quantity)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
