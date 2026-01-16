using Microsoft.Extensions.DependencyInjection;
using TenantOps.Application.Assets.Commands.AssignAsset;
using TenantOps.Application.Assets.Commands.CreateAsset;
using TenantOps.Application.Assets.Commands.ReturnAsset;
using TenantOps.Application.Attendance.Commands.ClockIn;
using TenantOps.Application.Auth.Commands.Login;
using TenantOps.Application.Departments.Commands.CreateDepartment;
using TenantOps.Application.Employees.Commands.CreateEmployee;
using TenantOps.Application.Users.Commands.CreateUser;


namespace TenantOps.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // Auth
        services.AddScoped<LoginHandler>();

        // Users
        services.AddScoped<CreateUserHandler>();

        // Employees
        services.AddScoped<CreateEmployeeHandler>();

        // Departments
        services.AddScoped<CreateDepartmentHandler>();

        // Attendance
        services.AddScoped<ClockInHandler>();

        // Assets
        services.AddScoped<CreateAssetHandler>();
        services.AddScoped<AssignAssetHandler>();
        services.AddScoped<ReturnAssetHandler>();

        return services;
    }
}