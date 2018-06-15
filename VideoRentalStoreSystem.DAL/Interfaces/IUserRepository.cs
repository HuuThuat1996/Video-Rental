namespace VideoRentalStoreSystem.DAL.Interfaces
{
    public interface IUserRepository<T> where T: class
    {
        T Get(string userName, string password);
    }
}
