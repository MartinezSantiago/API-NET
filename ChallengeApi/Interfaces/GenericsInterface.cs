namespace ChallengeApi.Interfaces
{
    public interface GenericsInterface<T>
    {
        public T ?Get(int ?id);


        /*
        public List<TShort> GetAll();
        */

        public void Post(T t);
        


        public void Put(T t);
        


        public void Delete(int id);
      
    }
}
