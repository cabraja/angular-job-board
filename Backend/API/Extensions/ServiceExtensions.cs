using API.Helpers.Validators;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddTransient<EditEmployerDTOValidator>();
        }
    }
}
