using MailKit;
using MailTestDataAccess.Repositories.Application.Concrete;
using MailTestDataAccess.Repositories.Concrete;
using MailTestService.Abstract;
using MailTestService.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MailTestService
{
    public static class ServiceRegistration
    {
        public static void LoadMyPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IEmailService, EmailManager>();
            services.AddScoped<IEmailMessageRepository, EfEmailMessageRepository>();

        }
    }
}
