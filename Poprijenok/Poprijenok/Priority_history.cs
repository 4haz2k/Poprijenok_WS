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
    
    public partial class Priority_history
    {
        public int history_ID { get; set; }
        public int agent_ID { get; set; }
        public Nullable<System.DateTime> date { get; set; }
    
        public virtual Agents Agents { get; set; }
    }
}
