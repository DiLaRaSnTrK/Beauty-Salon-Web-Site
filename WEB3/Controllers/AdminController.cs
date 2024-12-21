
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB3.Data;
using System.Linq;
using WEB3.Models; // ViewModel'i doğru import ettiğinizden emin olun

namespace WEB3.Controllers
{

    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var appointments = _context.appointments
                .Include(a => a.customerids)
                .Include(a => a.employeeids)
                .Include(a => a.serviceids)
                .Select(a => new Appointments
                {
                    appointmentid = a.appointmentid,
                    CustomerName = a.customerids.firstname + " " + a.customerids.lastname, // Müşteri adı
                    EmployeeName = a.employeeids.firstname + " " + a.employeeids.lastname, // Çalışan adı
                    ServiceName = a.serviceids.servicename, // Hizmet l
                    process=a.process,
                    totalprice = a.totalprice,
                    appointmentdatetime = a.appointmentdatetime,
                    approvalstatus = a.approvalstatus
                })
                .ToList();

            return View(appointments); // View'e AppointmentViewModel'leri gönderiyoruz.
        }

        public IActionResult ManageServices()
        {
            // Hizmet yönetimi sayfası.
            return View();
        }

        public IActionResult ManageEmployees()
        {
            // Çalışan yönetimi sayfası.
            return View();
        }

        public IActionResult ManageAppointments()
        {
            // Randevu yönetimi sayfası.
            return View();
        }

        public IActionResult ManageSalons()
        {
            // Salon yönetimi sayfası.
            return View();
        }
    }

}
