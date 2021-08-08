using System.Collections;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PayrocTest.Data;
using PayrocTest.Data.Repositories;
using PayrocTest.Models;
using PayrocTest.Tests.Helpers;
using Xunit;

namespace PayrocTest.Tests.Test.Data
{
    public class UnitOfWorkTests
    {
        [Fact]
        public void GetRepository_WhenCalled_StoresRepositoryInHashTableAndReturnsRepository()
        {
            var sut = new UnitOfWork(new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>()));

            var result = sut.GetRepository<Link, long>();

            Assert.NotNull(result);
            Assert.IsType<Repository<Link, long>>(result);

            var repositories = sut.GetPrivateField<UnitOfWork, Hashtable>("_repositories");
            Assert.True(repositories.ContainsKey(nameof(Link)));
        }
    }
}
