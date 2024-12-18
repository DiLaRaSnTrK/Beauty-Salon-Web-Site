using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB3.Models
{
    public class Appointments
    {
        [Key] // appointmentId'nin birincil anahtar olduğunu belirtiyoruz.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Otomatik artan olacağını belirtiyoruz
        public int appointmentid { get; set; }

        // Yabancı Anahtarlar
        public int employeeid { get; set; }    // employeeId'nin, Employee tablosuyla ilişkilendirileceğini belirtmeliyiz
        public int serviceid { get; set; }     // serviceId'nin, Service tablosuyla ilişkilendirileceğini belirtmeliyiz
        public int customerid { get; set; }    // customerId'nin, Customer tablosuyla ilişkilendirileceğini belirtmeliyiz

      
        public List<Services> services { get; set; }  // Yapılacak işlemler
        public List<Employees> employees { get; set; } // Çalışanlar


        public DateTime appointmentdatetime { get; set; }

        public Services serviceids { get; set; }

        public int totalprice { get; set; }

        public int process { get; set; }       // İşlem ID ya da başka bir mantık
        public string approvalstatus { get; set; }  // Onay durumu


        public Employees employeeids { get; set; }

        // Navigation property for Customer
        //public Customer customerids { get; set; }

        // Navigation property for AppointmentStatus
        //public AppointmentStatus approvalstatuses { get; set; }

     
    }
}
