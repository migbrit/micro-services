using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private NpgsqlConnection GetConnectionPostgreSQL()
        {
            return new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }

        public async Task<Coupon?> GetDiscountAsync(string productName)
        {
            var connection = GetConnectionPostgreSQL();

            return await connection
                .QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName = @ProductName"
                , new { ProductName = productName });
        }

        public async Task<bool> CreateDiscountAsync(Coupon coupon)
        {
            var connection = GetConnectionPostgreSQL();

            var affected = await connection.ExecuteAsync("INSERT INTO Coupon (ProductName, Description, Amount)" +
                " VALUES (@ProductName, @Description, @Amount)",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            if (affected == 0) return false;

            return true;
        }
        public async Task<bool> UpdateDiscountAsync(Coupon coupon)
        {
            var connection = GetConnectionPostgreSQL();

            var affected = await connection
                .ExecuteAsync("UPDATE coupon SET productname = @ProductName, description = @Description, amount = @Amount WHERE id = @Id",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

            if (affected == 0) return false;

            return true;
        }

        public async Task<bool> DeleteDiscountAsync(string id)
        {
            var connection = GetConnectionPostgreSQL();

            var affected = await connection
                .ExecuteAsync("DELETE FROM Coupon WHERE Id = @Id",
                new { Id = id });

            if (affected == 0) return false;

            return true;
        }
    }
}
