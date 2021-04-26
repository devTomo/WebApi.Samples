using WebApi.Samples.Application.Interfaces;

namespace WebApi.Samples.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IProductRepository productRepository)
        {
            Products = productRepository;
        }
        public IProductRepository Products { get; }  
    }
}