namespace WEB3.Models
{
    public class CustomerAppointment
    {
        public int customerAppointmentId { get; set; }
        public int customerId { get; set; }
        public int appointmentId { get; set; }
        public string approvalStatus { get; set; }
        public int serviceId { get; set; }
        public int employeeId { get; set; }
    }
}
