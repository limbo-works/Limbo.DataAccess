# How to setup a DbContext

To use the generic classes in this repository your context needs to implement the IDbContext. This can be done like so:

```csharp
using Microsoft.EntityFrameworkCore;

namespace MyNamespace.Contexts {
    public class MyContext : DbContext, IMycontext {
        public MyContext(DbContextOptions<MyContext> options) : base(options) {
        }

        public DbContext Context => this;
    }
}
```

```csharp
using Microsoft.EntityFrameworkCore;

namespace MyNamespace.Contexts {
    public interface IMyContext : IDbContext {
    }
}
```