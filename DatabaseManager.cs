using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Data.Entity;

namespace FamilyBudgetApp
{
    public class DatabaseManager
    {
        public static bool AddFamily(string familyName, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using (var context = new budgetEntities())
                {
                    // Provera da li porodica već postoji
                    if (context.Families.Any(f => f.familyName == familyName))
                    {
                        errorMessage = "Naziv porodice već postoji.";
                        return false;
                    }

                    var newFamily = new Family
                    {
                        familyName = familyName
                    };
                    context.Families.Add(newFamily);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Greska prilikom dodavanja porodice: " + ex.Message;
                return false;
            }
        }
        //prikazivanje svih porodica iz baze
        public static List<string> GetFamilyNames(out string errorMessage)
        {
            List<string> familyNames = new List<string>();
            errorMessage = string.Empty;
            try
            {
                using (var context = new budgetEntities())
                {
                    familyNames = context.Families.Select(f => f.familyName).ToList();
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Greska " + ex.Message;
            }

            return familyNames;
        }
        //prikazivanje clanova porodice
        public static List<Member> GetFamilyMembers(int familyId, out string errorMessage)
        {
            errorMessage = string.Empty;
            List<Member> familyMembers = new List<Member>();

            try
            {
                using (var context = new budgetEntities())
                {
                    familyMembers = context.Members.Where(m => m.familyId == familyId).ToList();
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Greska " + ex.Message;
            }

            return familyMembers;
        }
        //prikazivanje clanova porodice koji imaju status odobren
        public static List<Member> GetFamilyMembers1(int familyId, out string errorMessage)
        {
            errorMessage = string.Empty;
            List<Member> familyMembers = new List<Member>();

            try
            {
                using (var context = new budgetEntities())
                {
                    familyMembers = context.Members
                                          .Where(m => m.familyId == familyId && m.status == "Odobren")
                                          .ToList();
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Greska " + ex.Message;
            }

            return familyMembers;
        }

        public int GetSelectedFamilyId(ComboBox familyNameComboBox)
        {
            if (familyNameComboBox.SelectedItem != null)
            {
                string selectedFamilyName = familyNameComboBox.SelectedItem.ToString();
                string errorMessage;
                List<string> familyNames = DatabaseManager.GetFamilyNames(out errorMessage);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return -1; // Ako se dogodi greška, vraćamo -1 kao indikator greške
                }
                else
                {
                    using (var context = new budgetEntities())
                    {
                        Family selectedFamily = context.Families.FirstOrDefault(f => f.familyName == selectedFamilyName);
                        if (selectedFamily != null)
                        {
                            return selectedFamily.id; // Vraćamo ID porodice ako je pronađena
                        }
                    }
                }
            }
            return -1; // Ako nije izabrana porodica, takođe vraćamo -1 kao indikator greške
        }

        public static bool RegisterMember(string firstName, string lastName, string email, string password, string familyName, string status, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using (var context = new budgetEntities())
                {
                    // Provera da li email već postoji
                    if (context.Members.Any(m => m.email == email))
                    {
                        errorMessage = "Email adresa već postoji.";
                        return false;
                    }
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                    // Pronalaženje porodice
                    var family = context.Families.SingleOrDefault(f => f.familyName == familyName);
                    if (family == null)
                    {
                        errorMessage = "Porodica nije pronađena.";
                        return false;
                    }
                                                                                                                                                                                                                                                                                                                            
                    var newMember = new Member
                    {
                        firstName = firstName,
                        lastName = lastName,
                        email = email,
                        password = hashedPassword,
                        familyId = family.id,
                        status = status
                    };
                    context.Members.Add(newMember);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Greška prilikom registracije: " + ex.Message;
                return false;
            }
        }

        public static Member LoginMember(string email, string password, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using (var context = new budgetEntities())
                {
                    // Pronađi člana po email-u
                    var member = context.Members.SingleOrDefault(m => m.email == email);
                    if (member != null && BCrypt.Net.BCrypt.Verify(password, member.password))
                    {
                        if (member.status == "Odobren") 
                        {
                            return member;
                        }
                        else
                        {
                            errorMessage = "Jos uvek niste prihvaceni u porodicu, ne mozete pristupiti aplikaciji.";
                            return null;
                        }
                    }
                    else
                    {
                        errorMessage = "Neispravni email ili lozinka.";
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Greška prilikom logovanja: " + ex.Message;
                return null;
            }
        }
        //odobravanje clana porodice
        public static bool ApproveMember(int memberId)
        {
            try
            {
                using (var context = new budgetEntities())
                {
                    var member = context.Members.Find(memberId);
                    if (member != null)
                    {
                        member.status = "Odobren";
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false; // Član porodice nije pronađen
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //odbacivanje clana porodice, brisanje iz baze
        public static bool RejectMember(int memberId)
        {
            try
            {
                using (var context = new budgetEntities())
                {
                    var member = context.Members.Find(memberId);
                    if (member != null)
                    {
                        context.Entry(member).State = EntityState.Deleted;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine($"Član sa ID {memberId} nije pronađen za brisanje.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Greška prilikom brisanja člana: {ex.Message}");
                return false;
            }
        }
        public static List<Expense> GetExpensesForFamily(int familyId)
        {
            try
            {
                using (var context = new budgetEntities())
                {
                    return context.Expenses
                                  .Where(e => e.Member.familyId == familyId)
                                  .ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<Expense>();
            }
        }
        public static List<Income> GetIncomesForFamily(int familyId)
        {
            try
            {
                using (var context = new budgetEntities())
                {
                    return context.Incomes
                                  .Where(e => e.Member.familyId == familyId)
                                  .ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<Income>();
            }
        }
        //ukupni porodicni troskovi koje je dodao dati clana
        public static List<Expense> GetExpensesForMember(int memberId)
        {
            try
            {
                using (var context = new budgetEntities())
                {
                    return context.Expenses.Where(i => i.memberId == memberId).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<Expense>();
            }
        }
        public static List<Income> GetIncomesForMember(int memberId)
        {
            try
            {
                using (var context = new budgetEntities())
                {
                    return context.Incomes.Where(i => i.memberId == memberId).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<Income>();
            }
        }
        //troskovi po udelu clana 
        public static List<MemberExpense> GetMemberExpensesForMember(int memberId)
        {
            try
            {
                using (var context = new budgetEntities())
                {
                    return context.MemberExpenses
                                  .Include("Expense")
                                  .Where(mi => mi.memberId == memberId)
                                  .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching member expenses for member: {ex.Message}");
                return new List<MemberExpense>();
            }
        }

        public static List<MemberIncome> GetMemberIncomesForMember(int memberId)
        {
            try
            {
                using (var context = new budgetEntities())
                {
                    return context.MemberIncomes
                                  .Include("Income")
                                  .Where(mi => mi.memberId == memberId)
                                  .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching member incomes for member: {ex.Message}");
                return new List<MemberIncome>();
            }
        }
        public static double GetMemberBudget(int memberId, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using (var context = new budgetEntities())
                {
                    var memberBudget = context.MemberBudgets.FirstOrDefault(mb => mb.memberId == memberId);
                    if (memberBudget != null)
                    {
                        return memberBudget.budget ?? 0;
                    }
                    else
                    {
                        errorMessage = "Budžet za člana nije pronađen.";
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Greška prilikom preuzimanja budžeta: " + ex.Message;
                return 0;
            }
        }
        //funkcija za dohvatanje budzeta porodice
        public static double GetFamilyBudget(int familyId, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using (var context = new budgetEntities())
                {
                    var familyBudget = context.FamilyBudgets.FirstOrDefault(fb => fb.familyId == familyId);
                    if (familyBudget != null)
                    {
                        return familyBudget.budget ?? 0; 
                    }
                    else
                    {
                        errorMessage = "Budžet za porodicu nije pronađen.";
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Greška prilikom preuzimanja budžeta: " + ex.Message;
                return 0;
            }
        }
        //dodavanje ciljeva stednje
        public static bool AddSavingGoal(int memberId, double goalAmount, DateTime targetDate, string description, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using (var context = new budgetEntities())
                {
                    var newSavingGoal = new SavingGoal
                    {
                        memberId = memberId,
                        goalAmount = goalAmount,
                        targetDate = targetDate,
                        description = description,
                        status = "u toku..."
                    };
                    context.SavingGoals.Add(newSavingGoal);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Greška prilikom dodavanja cilja štednje: " + ex.Message;
                return false;
            }
        }
        //funkcija za dohvatanje porodicnih ciljeva stednje        
        public static List<SavingGoal> GetFamilySavingGoals(int familyId, out string errorMessage)
        {
            errorMessage = string.Empty;
            List<SavingGoal> familySavingGoals = new List<SavingGoal>();

            try
            {
                using (var context = new budgetEntities())
                {
                    familySavingGoals = context.SavingGoals
                        .Where(sg => sg.Member.familyId == familyId)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Greska: " + ex.Message;
            }

            return familySavingGoals;
        }

        //promena statusa cilja stednje
        public static string CheckSavingsGoalStatus(SavingGoal goal, double currentBudget, out string newStatus)
        {
            newStatus = goal.status;
            double remainingAmount = currentBudget - goal.goalAmount;

            if (goal.targetDate < DateTime.Today)  //ako je prosao datum
            {
                if (remainingAmount >= 0 && goal.status == "u toku...")
                {
                    newStatus = "uspesno";

                }
                else if (remainingAmount < 0 && goal.status != "neuspesno")
                {
                    newStatus = "neuspesno";
                }
            }
            //ako nije prosao datum, ali sam vec dostigla ciljni iznos
            else if (remainingAmount >= 0 && goal.status == "u toku...")
            {
                newStatus = "uspesno";
            }

            return null; // Nema poruke ako datum nije prošao i cilj nije postignut
        }

        public static void UpdateSavingGoalStatus(int goalId, string newStatus)
        {
            try
            {
                using (var context = new budgetEntities())
                {
                    var goal = context.SavingGoals.FirstOrDefault(sg => sg.id == goalId);
                    if (goal != null)
                    {
                        goal.status = newStatus;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom ažuriranja statusa cilja štednje: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static List<Expense> GetExpensesByCategory(int memberId, string category)
        {
            try
            {
                using (var context = new budgetEntities())
                {
                    return context.Expenses.Where(e => e.memberId == memberId && e.category == category).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<Expense>();
            }
        }

        public static List<Income> GetIncomesByCategory(int memberId, string category)
        {
            try
            {
                using (var context = new budgetEntities())
                {
                    return context.Incomes.Where(i => i.memberId == memberId && i.category == category).ToList();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return new List<Income>();
            }
        }
        public static List<Expense> GetExpensesByCategoryForFamily(int familyId, string category)
        {
            try
            {
                using (var context = new budgetEntities())
                {
                    return context.Expenses
                                  .Where(e => e.Member.familyId == familyId && e.category == category)
                                  .ToList();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return new List<Expense>();
            }
        }
        public static List<Income> GetIncomesByCategoryForFamily(int familyId, string category)
        {
            try
            {
                using (var context = new budgetEntities())
                {
                    return context.Incomes
                                  .Where(i => i.Member.familyId == familyId && i.category == category)
                                  .ToList();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return new List<Income>();
            }
        }

        public static List<string> GetCategoriesForFamily(int familyId)
        {
            List<string> categories = new List<string>();

            try
            {
                using (var context = new budgetEntities())
                {
                    // Dodaj sve jedinstvene kategorije troškova za članove porodice
                    var expenseCategories = context.Expenses
                        .Where(e => e.Member.familyId == familyId)
                        .Select(e => e.category)
                        .Distinct()
                        .ToList();
                    categories.AddRange(expenseCategories);

                    // Dodaj sve jedinstvene kategorije prihoda za članove porodice
                    var incomeCategories = context.Incomes
                        .Where(i => i.Member.familyId == familyId)
                        .Select(i => i.category)
                        .Distinct()
                        .ToList();
                    categories.AddRange(incomeCategories);

                    // Ukloni duplikate (u slučaju da isti naziv kategorije postoji u oba seta)
                    categories = categories.Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                // Rukovanje greškom, npr. logovanje greške
                Debug.WriteLine("Greška prilikom preuzimanja kategorija: " + ex.Message);
            }

            return categories;
        }


        public static bool AddExpenseAndMemberExpenses(int memberId, double amount, string category, DateTime date, string description, List<MemberExpense> memberExpenses, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                using (var context = new budgetEntities())
                {
                    // Pronalaženje ili dodavanje člana porodice
                    var member = context.Members.Find(memberId);
                    if (member == null)
                    {
                        errorMessage = "Član porodice nije pronađen.";
                        return false;
                    }

                    // Pronalaženje ili dodavanje budžeta porodice
                    var familyBudget = context.FamilyBudgets.FirstOrDefault(fb => fb.familyId == member.familyId);
                    if (familyBudget == null)
                    {
                        familyBudget = new FamilyBudget
                        {
                            familyId = member.familyId,
                            totalIncome = 0,
                            totalExpenses = 0,
                            budget = 0
                        };
                        context.FamilyBudgets.Add(familyBudget);
                    }

                    // Pronalaženje ili dodavanje budžeta člana porodice
                    var memberBudget = context.MemberBudgets.FirstOrDefault(mb => mb.memberId == memberId);
                    if (memberBudget == null)
                    {
                        memberBudget = new MemberBudget
                        {
                            memberId = memberId,
                            totalIncome = 0,
                            totalExpenses = 0,
                            budget = 0
                        };
                        context.MemberBudgets.Add(memberBudget);
                    }

                    // Kreiranje novog troška
                    var newExpense = new Expense
                    {
                        amount = amount,
                        category = category,
                        date = date,
                        description = description,
                        memberId = memberId
                    };
                    context.Expenses.Add(newExpense);
                    context.SaveChanges();  //Čuvanje troška da bi se dobio expenseId

                    int expenseId = newExpense.id;

                    // Povezivanje troška sa svim udelima članova porodice
                    foreach (var memberExpense in memberExpenses)
                    {
                        // Dodela expenseId svim memberExpense objektima
                        memberExpense.expenseId = expenseId;
                        context.MemberExpenses.Add(memberExpense);

                        // Ažuriranje ukupnih troškova za svakog člana porodice
                        var currentMemberBudget = context.MemberBudgets.FirstOrDefault(mb => mb.memberId == memberExpense.memberId);
                        if (currentMemberBudget != null)
                        {
                            currentMemberBudget.totalExpenses += memberExpense.memberAmount;
                            currentMemberBudget.budget = currentMemberBudget.totalIncome - currentMemberBudget.totalExpenses;
                        }
                    }

                    // Ažuriranje budžeta porodice
                    familyBudget.totalExpenses += amount;
                    familyBudget.budget = familyBudget.totalIncome - familyBudget.totalExpenses;

                    // Ažuriranje budžeta člana porodice koji je kreirao trošak
                    memberBudget.budget = memberBudget.totalIncome - memberBudget.totalExpenses;

                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Greška prilikom dodavanja troška i udeljenih troškova: " + ex.Message;
                return false;
            }
        }
        public static bool AddIncomeAndMemberIncomes(int memberId, double amount, string category, DateTime date, string description, List<MemberIncome> memberIncomes, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                using (var context = new budgetEntities())
                {
                    // Pronalaženje ili dodavanje člana porodice
                    var member = context.Members.Find(memberId);
                    if (member == null)
                    {
                        errorMessage = "Član porodice nije pronađen.";
                        return false;
                    }

                    // Pronalaženje ili dodavanje budžeta porodice
                    var familyBudget = context.FamilyBudgets.FirstOrDefault(fb => fb.familyId == member.familyId);
                    if (familyBudget == null)
                    {
                        familyBudget = new FamilyBudget
                        {
                            familyId = member.familyId,
                            totalIncome = 0,
                            totalExpenses = 0,
                            budget = 0
                        };
                        context.FamilyBudgets.Add(familyBudget);
                    }

                    // Pronalaženje ili dodavanje budžeta člana porodice
                    var memberBudget = context.MemberBudgets.FirstOrDefault(mb => mb.memberId == memberId);
                    if (memberBudget == null)
                    {
                        memberBudget = new MemberBudget
                        {
                            memberId = memberId,
                            totalIncome = 0,
                            totalExpenses = 0,
                            budget = 0
                        };
                        context.MemberBudgets.Add(memberBudget);
                    }

                    // Kreiranje novog prihoda
                    var newIncome = new Income
                    {
                        amount = amount,
                        category = category,
                        date = date,
                        description = description,
                        memberId = memberId
                    };
                    context.Incomes.Add(newIncome);

                    // Čuvanje prihoda da bi se dobio incomeId
                    context.SaveChanges();
                    int incomeId = newIncome.id;

                    // Povezivanje prihoda sa svim udelima članova porodice
                    foreach (var memberIncome in memberIncomes)
                    {
                        // Dodela incomeId svim memberIncome objektima
                        memberIncome.incomeId = incomeId;
                        context.MemberIncomes.Add(memberIncome);

                        // Ažuriranje ukupnih prihoda za svakog člana porodice
                        var currentMemberBudget = context.MemberBudgets.FirstOrDefault(mb => mb.memberId == memberIncome.memberId);
                        if (currentMemberBudget != null)
                        {
                            currentMemberBudget.totalIncome += memberIncome.memberAmount;
                            currentMemberBudget.budget = currentMemberBudget.totalIncome - currentMemberBudget.totalExpenses;
                        }
                    }
                    // Ažuriranje budžeta porodice
                    familyBudget.totalIncome += amount;
                    familyBudget.budget = familyBudget.totalIncome - familyBudget.totalExpenses;

                    // Ažuriranje budžeta člana porodice koji je kreirao prihod
                    memberBudget.budget = memberBudget.totalIncome - memberBudget.totalExpenses;

                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Greška prilikom dodavanja prihoda i udeljenih prihoda: " + ex.Message;
                return false;
            }
        }
    }
}


