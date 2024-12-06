using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WEB3.Models;

namespace WEB3.Models
{
    public class EmployeeAvailability
    {
        [Key]
        public int availabilityid { get; set; } // Birincil Anahtar


        public int employeeid { get; set; } // Çalışan ID'si


        [DataType(DataType.Date)]
        public DateTime Date { get; set; } // Uygunluk tarihi


        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; } // Çalışma başlangıç saati


        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; } // Çalışma bitiş saati

        // Navigation Property (İlişkili model)
        //[ForeignKey("employeeid")]
        //public Employees employees { get; set; } // Çalışan nesnesi ile ilişki
                                                 // Navigation property for Employee
        public Employees employeeids { get; set; }

    }
}

