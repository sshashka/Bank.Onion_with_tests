using System;
using AutoMapper;
using Bank.Data;
using Bank.Data.Banks;
using Bank.Data.Clients;
using Microsoft.EntityFrameworkCore;

namespace Bank.Tests
{
    public class UnitTestHelper
    {
        public static DbContextOptions<BankContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new BankContext(options))
            {
                SeedData(context);
            }
            return options;
        }

        public static void SeedData(BankContext context)
        {
            context.Banks.Add(new Data.Banks.Bank { Id = 1, Count = 12313, Head = "A song of ice and fire", Location = "1996" });
            context.Clients.Add(new Client {Id = 1, Name = "ddqwq", SecondName = "dqwqwqd", Sum = 332412, BankId = 1});
            context.SaveChanges();
        }

        public static Mapper CreateMapperProfile()
        {
            var myProfile = new BankDaoProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
        }
    }
}