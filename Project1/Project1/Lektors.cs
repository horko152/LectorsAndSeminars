//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Lektors
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lektors()
        {
            this.SeminarsAndLektors = new HashSet<SeminarsAndLektors>();
        }
    
        public string LektorName { get; set; }
        public Nullable<int> Age { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SeminarsAndLektors> SeminarsAndLektors { get; set; }
    }
}