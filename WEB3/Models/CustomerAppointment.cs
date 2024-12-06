namespace WEB3.Models
{
    public class CustomerAppointment
    {
        public int customerappointmentid { get; set; }
        public int customerid { get; set; }
        public int appointmentid { get; set; }
        public string approvalstatus { get; set; }
        public int serviceid { get; set; }
        public int employeeid { get; set; }

        // Navigation properties
        public Customer customer { get; set; }  // Customer ile ilişki
        public Services services { get; set; }  // Service ile ilişki
        public Employees employees { get; set; }  // Employee ile ilişki
        public Appointments appointments { get; set; }  // Appointment ile ilişki
    }
}
