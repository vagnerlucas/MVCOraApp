//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TB_SUBCATEGORY")]
    public partial class TB_SUBCATEGORY
    {
        public TB_SUBCATEGORY()
        {
            this.TB_PRODUCT = new HashSet<TB_PRODUCT>();
        }

        [Key]
        [Display(Name = "ID ")]
        public int ID_SUBCATEGORY { get; set; }
        [Required]
        [Display(Name = "Categoria ")]
        public int ID_CATEGORY { get; set; }
        [Required]
        [Display(Name = "SubCategoria ")]
        public string DESCRIPTION { get; set; }

        [Display(Name = "Categoria ")]
        public virtual TB_CATEGORY TB_CATEGORY { get; set; }
        public virtual ICollection<TB_PRODUCT> TB_PRODUCT { get; set; }
    }
}
