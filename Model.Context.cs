﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class budgetEntities : DbContext
    {
        public budgetEntities()
            : base("name=budgetEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<FamilyBudget> FamilyBudgets { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberBudget> MemberBudgets { get; set; }
        public virtual DbSet<MemberExpense> MemberExpenses { get; set; }
        public virtual DbSet<MemberIncome> MemberIncomes { get; set; }
        public virtual DbSet<SavingGoal> SavingGoals { get; set; }
    }
}
