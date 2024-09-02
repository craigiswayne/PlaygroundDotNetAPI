using PlaygroundDotNetAPI.Data;
using PlaygroundDotNetAPI.Models;

namespace PlaygroundDotNetAPI.Services;

public interface IEmployeeService
{
    IQueryable<Employee> List();
}

public class EmployeeService(MyDbContextSqLite dbContext) : IEmployeeService
{
    private readonly MyDbContextSqLite _dbContext = dbContext;

    public IQueryable<Employee> List()
    {
        return _dbContext.Employees.OrderBy(columns => columns.Id).Skip(0).Take(5);
    }
}