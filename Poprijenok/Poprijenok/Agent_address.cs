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
    
    public partial class Agent_address
    {
        public int Agent_ID { get; set; }
        public string index { get; set; }
        public string region { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string frame { get; set; }
    
        public virtual Agents Agents { get; set; }
    }
}
