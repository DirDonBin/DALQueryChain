# **DALQueryChain**

[![NuGet Version and Downloads count](https://buildstats.info/nuget/DALQueryChain?includePreReleases=true)](https://www.nuget.org/packages/DALQueryChain/)
[![License](https://img.shields.io/github/license/linq2db/linq2db)](LICENSE.txt)

DALQueryChain позволяет упростить работу с DAL уровнем, предоставляя гибкую систему составления запроса, закрытым от прямого доступа к ORM и IQuearyble.

## **Начало работы:**

Установка основного пакета из **NuGet**:
* `Install-Package DALQueryChain`

*Дополнительные пакеты:*

Для [**Linq2Db ORM**](https://github.com/linq2db/linq2db):
* `Install-Package DALQueryChain.Linq2Db`

Для [**EntityFramework ORM**](https://github.com/dotnet/efcore):
* `Install-Package DALQueryChain.EntityFramework`


## **Конфигурация проекта:**

В Startup/Program прописать регистрацию DALQueryChain

```cs
public class Startup
{
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        //...
        services.AddQueryChain(Assembly.GetAssembly("DALAssemblyName"));
        //...
    }
}
```

## **Использование библиотеки:**

Забираем из DI:

```cs
IDALQueryChain<TestContext> queryChain
```

Или создаем экземпляр:

```cs
using var queryChain = new BuildQuery<TestContext>(_context, _serviceProvider);
```

## Примеры использования:
### **Get:**

```cs
var maxUserId = queryChain.For<User>().Get.Select(x => x.Id).Max(); //Получение максимального Id
var activeUsers = await queryChain.For<User>().Get.Where(x => x.IsActive).ToListAsync(); //Получение активных записей
var workspaces = queryChain.For<Workspace>().Get.LoadWith(x => x.Permissions).ThenLoad(x => x.Users); //Получение записей с зависимостями
```

### **Insert:**

```cs
var user = new User
    {
        AccessFailedCount = 1,
        CreateAt = DateTime.Now,
        DeleteAt = DateTime.Now,
        Email = Guid.NewGuid().ToString(),
        EmailConfirmed = false,
        ModifyAt = DateTime.Now,
        PasswordHash = "",
        PhoneConfirmed = false,
        RoleId = 1,
        Salt = "",
        IsActive = true,
        Username = Guid.NewGuid().ToString()
    };

var user = await queryChain.For<User>().Insert.InsertWithObjectAsync(user); // Вставка записи в таблицу с ее возвратом
```

### **Delete:**

```cs
await queryChain.For<User>().Delete.DeleteAsync(x => true); //Удаление всех записей в таблице

await _qs.For<User>().Delete.BulkSoftDeleteAsync(users); //Мягкое удаление (необходимо переопределение в репозитории)
```

### **Update:**

```cs
queryChain.For<User>().Update.BulkUpdate(object); //Массовое обновление записей

_qs.For<User>().Update
    .Where(x => true)
    .Set(x => x.Email, () => Guid.NewGuid().ToString())
    .Update(); //Обновление указанных полей в записях

```

### **Репозитории и триггеры:**

UserRepository:
```cs
public class UserRepository : BaseRepository<TestContext, User>
{
    private readonly ITestDI _testDI;

    public UserRepository(TestContext context, ITestDI testDI) : base(context)
    {
        _testDI = testDI;
    }

    protected override async Task OnBeforeDelete(CancellationToken ctn = default)
    {
        var deletedEntities = await GetTriggerData(ctn);

        //Some Actions...
    }

    protected override async Task OnAfterDelete(CancellationToken ctn = default)
    {
        var deletedEntities = await GetTriggerData(ctn);

        //Some Actions...
    }

    protected override async Task OnBeforeUpdate(CancellationToken ctn = default)
    {
        var updatedEntities = await GetTriggerData(ctn);

        //Some Actions...
    }

    protected override async Task OnAfterUpdate(CancellationToken ctn = default)
    {
        var updatedEntities = await GetTriggerData(ctn);

        //Some Actions...
    }

    protected override async Task OnBeforeInsert(CancellationToken ctn = default)
    {
        var insertedEntities = await GetTriggerData(ctn);

        //Some Actions...
    }

    protected override async Task OnAfterInsert(CancellationToken ctn = default)
    {
        var insertedEntities = await GetTriggerData(ctn);

        //Some Actions...
    }

    protected override Task SoftBulkDelete(IEnumerable<User> model, CancellationToken ctn = default)
    {
        //Some Actions...
    }

    protected override Task SoftDelete(Expression<Func<User, bool>> predicate, CancellationToken ctn = default)
    {
        //Some Actions...
    }

    protected override Task SoftBulkDelete(Expression<Func<User, bool>> predicate, CancellationToken ctn = default)
    {
        //Some Actions...
    }

    protected override Task SoftDelete(User model, CancellationToken ctn = default)
    {
        //Some Actions...
    }

    public void Method()
    {
        _testDI.Test();
    }
}
```

Использование:

```cs
var repository = queryChain.Repository<UserRepository>();
repository.Method();
```

P.S. Если используете в продакшене, могу пожелать лишь удачи
