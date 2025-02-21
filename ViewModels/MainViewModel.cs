/*  BudgetWatcher (by Stephan Kammel, Dresden, Germany, 2024)
 *  
 *  MainViewModel : BaseViewModel
 *  
 */

using BudgetWatcher.Commands;
using BudgetWatcher.Models;
using BudgetWatcher.ViewModels.ViewLess;

using System.Collections;
using System.Windows;


namespace BudgetWatcher.ViewModels;


public partial class MainViewModel : ObservableObject
{

    // Properties & Fields
    #region Properties & Fields

    private readonly BudgetChangeViewModel _BudgetChangeViewModel;
    public BudgetChangeViewModel BudgetChangeViewModel => _BudgetChangeViewModel;


    public SetupFieldViewModel SetupField { get; }


    [ObservableProperty]
    private bool _ShowBudget;


    [ObservableProperty]
    private bool _ShowBudgetOverview;


    [ObservableProperty]
    private bool _ShowNotes;


    [ObservableProperty]
    private bool _ShowSetup;

    #endregion


    // Constructors
    #region Constructors


    /// <summary>
    /// für dependency injection den budget manager auf dieser ebene entgegen nehmen        
    /// </summary>

    public MainViewModel(BudgetChangeViewModel budgetChangeViewModel, SetupFieldViewModel setupFieldViewModel)
    {

        _BudgetChangeViewModel = budgetChangeViewModel;

        SetupField = setupFieldViewModel;

        ShowBudget = true;

        SetupField.GainExpenseColorChange += GainExpenseColorChangeEvent;

    }

    #endregion



    // Methods
    #region Methods

    [RelayCommand]
    private void AddBudget(object? parameter)
    {
        _BudgetChangeViewModel.AddBudget(
            new BudgetViewModel(new Budget()));
    }


    [RelayCommand]
    private void RemoveBudget(object? parameter)
    {
        IList selection = (IList)parameter;

        if (selection != null)
        {
            MessageBoxResult result = MessageBox.Show(
                $"Do you want to delete selected budget(s)?",
                "Remove Budget Item(s)", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var selected = selection.Cast<BudgetViewModel>().ToArray();

                foreach (var item in selected)
                {
                    _BudgetChangeViewModel.RemoveBudget(item);
                }
            }
        }
    }




    [RelayCommand]
    private void Close(object? parameter)
    {
        ContextMenuCommandsHandler.CloseMainWindow(parameter);
    }


    [RelayCommand]
    private void LeftPress(object? parameter)
    {
        ContextMenuCommandsHandler.DragMoveMainWindow(parameter);
    }


    [RelayCommand]
    private void Maximize(object? parameter)
    {
        ContextMenuCommandsHandler.MaximizeMainWindow(parameter);
    }


    [RelayCommand]
    private void Minimize(object? parameter)
    {
        ContextMenuCommandsHandler.MinimizeMainWindow(parameter);
    }

    #endregion


    // Events
    #region Events

    private void GainExpenseColorChangeEvent(object? sender, EventArgs e)
    {
        _BudgetChangeViewModel.UpdateGainExpenseBrush();
    }

    #endregion


}
// EOF