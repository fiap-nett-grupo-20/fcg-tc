using FCG.Domain.Entities;
using FCG.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Tests.Context;
public class DbFCGAPIContextTests
{
    [Fact]
    public void CanSaveUserWithEmail()
    {
        var options = new DbContextOptionsBuilder<DbFCGAPIContext>()
            .UseInMemoryDatabase(databaseName: "TestDB")
            .Options;

        using (var context = new DbFCGAPIContext(options))
        {
            var user = new User("Test", "valid@fiap.com.br");
            context.Users.Add(user);
            context.SaveChanges();
        }

        using (var context = new DbFCGAPIContext(options))
        {
            Assert.Equal(1, context.Users.Count());
        }
    }
}