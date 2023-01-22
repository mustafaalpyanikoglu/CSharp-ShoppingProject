using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class DataAccessServiceRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ShoppingProjectConnectionString"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<IUserDal, EfUserDal>();
            services.AddScoped<IOperationClaimDal, EfOperationClaimDal>();
            services.AddScoped<IUserOperationClaimDal, EfUserOperationClaimDal>();
            services.AddScoped<IProductDal, EfProductDal>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<IOrderDal, EfOrderDal>();
            services.AddScoped<IPurseDal, EfPurseDal>();
            services.AddScoped<IUserCartDal,EfUserCartDal>();
            services.AddScoped<IOrderDetailDal, EfOrderDetailDal>();


            return services;
        }
    }
}
