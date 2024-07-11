/*  BudgetWatcher (by Stephan Kammel, Dresden, Germany, 2024)
 *  
 *  Budget
 * 
 *  serializable data model class
 */
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace BudgetWatcher.Models
{
    [Serializable]
    public class Budget
    {

        // Properties & Fields
        #region Properties & Fields        

        public DateTime Begin {  get; set; } = DateTime.Now;
        public DateTime End {  get; set; } = DateTime.Now;

        public double CurrentBalance => InitialBudget + Gains - Expenses;
        public double InitialBudget { get; set; } = 0.0;
        public double Expenses { get; set; } = 0.0;
        public double Gains { get; set; } = 0.0;
        public Note Note { get; set; } = new Note();

        #endregion


        // Collections
        #region Collections

        [XmlArray("BudgetChanges")]
        public ObservableCollection<BudgetItem> BudgetChanges { get; set; } = new ObservableCollection<BudgetItem>();

        #endregion


        // Constructors
        #region Constructors

        public Budget()
        {

        } 

        #endregion


    }
}
// EOF