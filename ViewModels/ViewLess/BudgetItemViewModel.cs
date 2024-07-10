using BudgetWatcher.Enums;
using BudgetWatcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetWatcher.ViewModels.ViewLess
{
    public class BudgetItemViewModel : BaseViewModel
    {

        // Properties & Fields
        #region Properties & Fields

        private BudgetItem _BudgetItem;
        public BudgetItem GetBudgetItem => _BudgetItem;

        public BudgetViewModel BudgetViewModel { get; }


        public BudgetIntervals Interval
        {
            get { return _BudgetItem.Interval; }
            set
            {
                _BudgetItem.Interval = value;
                OnPropertyChanged(nameof(Interval));

                ValueChange?.Invoke(this, EventArgs.Empty);
            }
        }


        public BudgetTypes Type
        {
            get { return _BudgetItem.Type; }
            set
            {
                _BudgetItem.Type = value;
                OnPropertyChanged(nameof(Type));

                ValueChange?.Invoke(this, EventArgs.Empty);
            }
        }


        public DateTime Date
        {
            get { return _BudgetItem.Date; }
            set
            {
                _BudgetItem.Date = value;
                OnPropertyChanged(nameof(Date));

                ValueChange?.Invoke(this, EventArgs.Empty);
            }
        }


        public string Item
        {
            get { return _BudgetItem.Item; }
            set
            {
                _BudgetItem.Item = value;
                OnPropertyChanged(nameof(Item));

                ValueChange?.Invoke(this, EventArgs.Empty);
            }
        }
        

        public int Quantity
        {
            get { return _BudgetItem.Quantity; }
            set
            {
                _BudgetItem.Quantity = value;
                OnPropertyChanged(nameof(Quantity));
                OnPropertyChanged(nameof(Result));

                ValueChange?.Invoke(this, EventArgs.Empty);
            }
        }


        public double Sum
        {
            get { return _BudgetItem.Sum; }
            set
            {
                _BudgetItem.Sum = value;
                OnPropertyChanged(nameof(Sum));
                OnPropertyChanged(nameof(Result));

                ValueChange?.Invoke(this, EventArgs.Empty);
            }
        }


        public double Result
        {
            get { return _BudgetItem.Result; }
        }
        #endregion

        // event Properties & Fields
        #region event Properties & Fields

        public EventHandler ValueChange;

        #endregion


        // Constructors
        #region Constructors

        public BudgetItemViewModel(BudgetItem budgetItem, BudgetViewModel budgetViewModel)
        {
            BudgetViewModel = budgetViewModel;
            _BudgetItem = budgetItem;
        }

        #endregion

    }
}
