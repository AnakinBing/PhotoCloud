namespace PhotoCloud.DataService
{
    public interface IBaseService<T>
    {
        bool Add(T t);

        int AddAndReturnAutoID(T t);

        bool Delete(int id);

        bool Delete(IList<int> ids);

        bool Update(T t);

        T? Get(int id);

        IEnumerable<T> GetList(bool isDESC = false);

        IEnumerable<T> GetList(out int count, int pageIndex = 1, int pageSize = 50);
    }
}
