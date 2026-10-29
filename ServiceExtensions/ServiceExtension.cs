using david_suvarski_kt_31_23.Interfaces.StudentsInterfaces;
using david_suvarski_kt_31_23.Interfaces.GroupsInterfaces;
using david_suvarski_kt_31_23.Interfaces.DisciplinesInterfaces;
using david_suvarski_kt_31_23.Interfaces.GradesInterfaces;
using david_suvarski_kt_31_23.Interfaces.CreditsInterfaces;

namespace david_suvarski_kt_31_23.ServiceExtensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IDisciplineService, DisciplineService>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<ICreditService, CreditService>();
            return services;
        }
    }
}
