//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Poprijenok
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee_pasport
    {
        public int employee_ID { get; set; }
        public string patronymic { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public System.DateTime birthday { get; set; }
        public System.DateTime date_of_issue { get; set; }
        public string issued_by { get; set; }
        public string department_code { get; set; }
        public int series { get; set; }
        public int number { get; set; }
    
        public virtual Employes Employes { get; set; }
    }
}
