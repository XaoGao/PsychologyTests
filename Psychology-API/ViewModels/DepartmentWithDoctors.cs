using System.Collections.Generic;
using Psychology_Domain.Domain;
using System;

namespace Psychology_API.ViewModels
{
    /// <summary>
    /// Отдел и все доктора, которые работают в данном отделе.
    /// </summary>
    public class DepartmentWithDoctors
    {
        /// <summary>
        /// Отдел.
        /// </summary>
        /// <value></value>
        public Department Department { get; set; }
        /// <summary>
        /// Работники.
        /// </summary>
        /// <value></value>
        public IEnumerable<Doctor> Doctors { get; set; }
        /// <summary>
        /// Создание нового экземпляра класса
        /// </summary>
        /// <param name="department"> Отдел. </param>
        /// <param name="doctors"> Работники.</param>
        public DepartmentWithDoctors(Department department, IEnumerable<Doctor> doctors)
        {
            if (department == null)
                throw new ArgumentNullException(nameof(department));
            
            Department = department;
        }
    }
}