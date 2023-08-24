using FluentValidation;
using KUSYS.Business.Handlers.Students.Commands;
using KUSYS.Business.Handlers.Students.ValidationRules;
using KUSYS.Business.Handlers.Users.Commands;
using KUSYS.Business.Handlers.Users.ValidationRules;
using KUSYS.Business.Infrastructure;
using KUSYS.Core.Abstracts;
using KUSYS.DataAccess.Repositories;
using KUSYS.DataAccess.Repositories.Abstracts;
using KUSYS.DataAccess.Repositories.Base;
using MediatR;

namespace KUSYS.Api.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void InjectionRegisters(this IServiceCollection services)
        {
            var assemblies = BusinessAssembly.GetAssemblies();
            services.AddMediatR(assemblies);

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();

            // Validations
            services.AddScoped<IValidator<CreateStudentCommand>, CreateStudentValidator>();
            services.AddScoped<IValidator<UpdateStudentCommand>, UpdateStudentValidator>();
            services.AddScoped<IValidator<CreateUserCommand>, CreateUserValidator>();
            services.AddScoped<IValidator<UpdateUserCommand>, UpdateUserValidator>();
        }
    }
}
