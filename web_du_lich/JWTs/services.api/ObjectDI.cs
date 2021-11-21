using Microsoft.Extensions.DependencyInjection;
using Security;
using services.svc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace services.api
{
    public class ObjectDI
    {
        public static void LoadInjection(IServiceCollection services)
        {
            services.AddScoped<Ijwt, jwt>();
            services.AddScoped<IEmployeeService, EmployeesService>();
            services.AddScoped<IFeedBackService, FeedBackService>();
            services.AddScoped<IHolidayService, HolidayService>();
            services.AddScoped<IAppParamService, AppParamService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ITourDetailService, TourDetailService>();
            services.AddScoped<IComboService, ComboService>();
            services.AddScoped<IKhachHangService, KhachHangService>();
            services.AddScoped<IBookTourService, BookTourService>();
            services.AddScoped<ITripService, TripService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<ISendMailServices, SendMailServices>();





        }
    }
}
