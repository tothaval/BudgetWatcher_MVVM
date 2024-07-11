/*  BudgetWatcher (by Stephan Kammel, Dresden, Germany, 2024)
 *  
 *  BudgetItem
 * 
 *  serializable data model class
 */
using BudgetWatcher.Enums;

namespace BudgetWatcher.Models
{
    [Serializable]
    public class BudgetItem
    {

        // Properties & Fields
        #region Properties & Fields

        public BudgetIntervals Interval { get; set; } = BudgetIntervals.Once;

        public BudgetTypes Type { get; set; } = BudgetTypes.Expense;

        public DateTime Date { get; set; } = DateTime.Now;

        public string Item { get; set; } = "description";

        public int Quantity { get; set; } = 1;

        public double Sum { get; set; } = 0.0;

        public double Result => Sum * Quantity;

        #endregion


        // Constructors
        #region Constructors

        public BudgetItem()
        {

        } 

        #endregion

    }
}
// EOF