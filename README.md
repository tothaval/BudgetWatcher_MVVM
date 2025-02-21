# BudgetWatcher_MVVM
 simple budget calculation tool


 #Issues:

 resource initialization is crap, because
	- objects are not initialized correctly, have to rebuild this part and pack it into a separate class (or class library) XAMLresourcemanagement

resource binding to ui fiels is partially crap, because
	- background value is used in reverse in some ui controls, which is misleading when setting up the colors

 initial startup is crap, because
	- nothing indicates where i have to go to create a budget

i suggest to rebuild or rework the ui and the model and to modularize the entire project:

	ui
	-> work with pages or some sort of guidance to create a budget first, then navigate to that budget

	model
	-> extrude everything into the budgetmanagement class library or something similar

	ui
	-> later, once everything is done, integrate some sort of resource management


__WIP__