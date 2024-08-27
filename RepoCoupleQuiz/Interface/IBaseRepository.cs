namespace RepoCoupleQuiz.Interface
{
    public interface IBaseRepository<TEntity>
    {
        public Task<TEntity> Create(TEntity entity);
        public Task<TEntity> Update(TEntity entity);
        public Task<TEntity> Delete(TEntity entity);
        public Task<List<TEntity>> Get();

    }
}
