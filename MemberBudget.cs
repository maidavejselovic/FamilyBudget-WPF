//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FamilyBudgetApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class MemberBudget
    {
        public int id { get; set; }
        public Nullable<int> memberId { get; set; }
        public Nullable<double> totalExpenses { get; set; }
        public Nullable<double> totalIncome { get; set; }
        public Nullable<double> budget { get; set; }
    
        public virtual Member Member { get; set; }
    }
}