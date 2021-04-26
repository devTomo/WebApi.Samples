using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

using WebApi.Samples.Application.Interfaces;
using WebApi.Samples.Core.Entities;

namespace WebApi.Samples.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration configuration;
        public ProductRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(Product entity)
        {
            entity.ModifiedDate = DateTime.Now;
            var sql = "INSERT INTO [Production].[Product] ([Name],[ProductNumber],[MakeFlag],[FinishedGoodsFlag],[Color],[SafetyStockLevel],[ReorderPoint]" +
                ",[StandardCost],[ListPrice],[Size],[SizeUnitMeasureCode],[WeightUnitMeasureCode],[Weight],[DaysToManufacture],[ProductLine],[Class],[Style]," +
                "[ProductSubcategoryID],[ProductModelID],[SellStartDate],[SellEndDate],[DiscontinuedDate],[rowguid],[ModifiedDate])VALUE" +
                " (@Name,@MakeFlag, @FinishedGoodsFlag,@Color, @SafetyStockLevel, @ReorderPoint, @StandardCost, @ListPrice, @Size, @SizeUnitMeasureCode, " +
                "@WeightUnitMeasureCode, @Weight, @DaysToManufacture,@ProductLine, @Class,@Style,@ProductSubcategoryID, @ProductModelID,@SellStartDate, @SellEndDate, " +
                "@DiscontinuedDate, @rowguid,  @ModifiedDate)";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async  Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM [Production].[Product] WHERE ProductID = @ProductID";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            var sql = "SELECT * FROM [Production].[Product]";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Product>(sql);
                return result.ToList();
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM [Production].[Product] WHERE ProductID = @ProductID";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Product>(sql, new { ProductID = id });
                return result;

            }
        }

        public async Task<int> UpdateAsync(Product entity)
        {
            entity.ModifiedDate = DateTime.Now;
            var sql = "UPDATE [Production].[Product] SET" +
                "Name = @Name, MakeFlag = @MakeFlag,FinishedGoodsFlag = @FinishedGoodsFlag,Color = @Color,SafetyStockLevel = @SafetyStockLevel,ReorderPoint = @ReorderPoint, " +
                "StandardCost = @StandardCost, ListPrice = @ListPrice,Size = @Size,SizeUnitMeasureCode = @SizeUnitMeasureCode, WeightUnitMeasureCode = @WeightUnitMeasureCode, " +
                "Weight = @Weight, DaysToManufacture = @DaysToManufacture, ProductLine = @ProductLine, Class = @Class, Style = @Style, ProductSubcategoryID = @ProductSubcategoryID," +
                "ProductModelID = @ProductModelID,SellStartDate = @SellStartDate,SellEndDate = @SellEndDate,DiscontinuedDate = @DiscontinuedDate,rowguid = @rowguid," +
                "ModifiedDate = @ModifiedDate WHERE ProductID = @ProductID";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;

            }
        }
    }
}
