using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace UJAS.Application.Assessments
{
    public static class AssessmentModule
    {
        public static IServiceCollection AddAssessmentModule(this IServiceCollection services)
        {
            // Register all handlers in this assembly
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}