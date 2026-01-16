using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TenantOps.Application.Common.Interfaces;
using TenantOps.Domain.Contracts.Repositories;
using TenantOps.Infrastructure.Persistence;
using TenantOps.Infrastructure.Persistence.Repositories;
using TenantOps.Infrastructure.Security;
using TenantOps.Infrastructure.Security.Authorization;
using TenantOps.Infrastructure.Security.Jwt;
using TenantOps.Infrastructure.UnitOfWork;

namespace TenantOps.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        // DbContext
        services.AddDbContext<TenantOpsDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IAttendanceRecordRepository, AttendanceRecordRepository>();
        services.AddScoped<IAssetRepository, AssetRepository>();
        services.AddScoped<IAssetAssignmentRepository, AssetAssignmentRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();

        // Unit of Work
        services.AddScoped<IUnitOfWork, _unitOfWork>();


        // Security
        services.AddHttpContextAccessor();
        services.AddScoped<ITenantProvider, TenantProvider>();

        // Authorization Policies
        services.AddAuthorization(options =>
        {
            Policies.Register(options);
        });

        // JWT configuration
        var jwtOptions = new JwtOptions();
        configuration.GetSection(JwtOptions.SectionName).Bind(jwtOptions);

        services.AddSingleton(jwtOptions);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),

                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddScoped<IJwtTokenGenerator, JwtTokenService>();


        return services;
    }
}