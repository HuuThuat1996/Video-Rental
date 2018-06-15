namespace VideoRentalStoreSystem.DAL.Interfaces
{
    public interface IRentalRecordDetailRepository<T> where T : class
    {
        T GetLatest(int diskId);
    }
}
