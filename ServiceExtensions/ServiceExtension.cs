using david_suvarski_kt_31_23.Interfaces.StudentsInterfaces;
using david_suvarski_kt_31_23.Interfaces.GroupsInterfaces;

namespace david_suvarski_kt_31_23.ServiceExtensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IGroupService, GroupService>();
            return services;
        }
    }
}