//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hotcakes.Commerce.Data.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class hcc_PromotionTranslation
    {
        public long PromotionTranslationId { get; set; }
        public long PromotionId { get; set; }
        public string Culture { get; set; }
        public string CustomerDescription { get; set; }
    
        public virtual hcc_Promotions hcc_Promotions { get; set; }
    }
}
