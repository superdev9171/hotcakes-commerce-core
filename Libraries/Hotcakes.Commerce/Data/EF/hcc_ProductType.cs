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
    
    public partial class hcc_ProductType
    {
        public hcc_ProductType()
        {
            this.hcc_ProductTypeXProductProperty = new HashSet<hcc_ProductTypeXProductProperty>();
            this.hcc_Product = new HashSet<hcc_Product>();
            this.hcc_ProductTypeTranslations = new HashSet<hcc_ProductTypeTranslation>();
            this.hcc_CatalogRoles = new HashSet<hcc_CatalogRoles>();
        }
    
        public System.Guid bvin { get; set; }
        public bool IsPermanent { get; set; }
        public System.DateTime LastUpdated { get; set; }
        public long StoreId { get; set; }
        public string TemplateName { get; set; }
    
        public virtual ICollection<hcc_ProductTypeXProductProperty> hcc_ProductTypeXProductProperty { get; set; }
        public virtual hcc_MembershipProductType hcc_MembershipProductType { get; set; }
        public virtual ICollection<hcc_Product> hcc_Product { get; set; }
        public virtual ICollection<hcc_ProductTypeTranslation> hcc_ProductTypeTranslations { get; set; }
        public virtual ICollection<hcc_CatalogRoles> hcc_CatalogRoles { get; set; }
    }
}
