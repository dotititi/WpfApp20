//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp20
{
    using System;
    using System.Collections.Generic;
    
    public partial class TreatmentPlan
    {
        public int id { get; set; }
        public int doctor_id { get; set; }
        public int patient_id { get; set; }
        public string plan_detailt { get; set; }
    
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}