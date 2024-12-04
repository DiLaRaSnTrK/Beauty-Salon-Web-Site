using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB3.Models
{
    public class EmployeeAvailability
    {
        [Key] // availabilityId'nin birincil anahtar olduğunu belirtiyoruz.
        public int availabilityId { get; set; }

        // Yabancı Anahtar
        public int employeeId { get; set; }  // employeeId'nin, Employee tablosuyla ilişkilendirileceğini belirtmeliyiz.

        // Veritabanında integer olarak tanımlı sütunlar
        public int startTime { get; set; }  // Zaman başlangıcı (dakika cinsinden)
        public int endTime { get; set; }    // Zaman bitişi (dakika cinsinden)

        // Integer değerlerini TimeSpan'e dönüştüren sanal özellikler
        [NotMapped] // Bu özellik veritabanına yansıtılmaz
        public TimeSpan StartTime => TimeSpan.FromMinutes(startTime);

        [NotMapped] // Bu özellik veritabanına yansıtılmaz
        public TimeSpan EndTime => TimeSpan.FromMinutes(endTime);

        // Navigation property (yabancı anahtar ilişkisi)
        public virtual Employee Employee { get; set; }  // Employee ile ilişkiyi burada belirtiyoruz
    }
}
