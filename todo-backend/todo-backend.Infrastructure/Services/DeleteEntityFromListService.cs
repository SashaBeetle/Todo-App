namespace todo_backend.Infrastructure.Services
{
    public class DeleteEntityFromListService
    {
        public List<int> DeleteEntitysFromList(List<int> list)
        {
            foreach (var item in list)
            {
                    list.Remove(item);
            }
            
            return list;
        }
    }
}