using BudgetWatcher.Models;
using BudgetWatcher.ViewModels.ViewLess;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace BudgetWatcher.Utility
{
    public class Persistance
    {
        string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\budgets\\";
        string filter = "*.xml";

        private async Task ClearFolder(ObservableCollection<BudgetViewModel> budgets)
        {

            List<string> files = Directory.GetFiles(folder, filter, SearchOption.TopDirectoryOnly).ToList();

            if (files.Count > budgets.Count)
            {
                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }
        }


        public ObservableCollection<BudgetViewModel> DeSerializeNotes()
        {
            ObservableCollection<BudgetViewModel> notes = new ObservableCollection<BudgetViewModel>();

            List<string> files = Directory.GetFiles(folder, filter, SearchOption.TopDirectoryOnly).ToList();
            var xmlSerializer = new XmlSerializer(typeof(Budget));

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);

                if (fileName.StartsWith("budget_"))
                {
                    using (var writer = new StreamReader(file))
                    {
                        try
                        {
                            var member = new BudgetViewModel((Budget)xmlSerializer.Deserialize(writer));

                            notes.Add(member);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }

            return notes;
        }

        public async void SerializeNotes(ObservableCollection<BudgetViewModel> budgets)
        {
            await ClearFolder(budgets);

            int counter = 0;


            foreach (BudgetViewModel budget in budgets)
            {
                counter++;

                var xmlSerializer = new XmlSerializer(typeof(Budget));

                using (var writer = new StreamWriter($"{folder}budget_{counter}.xml"))
                {
                    xmlSerializer.Serialize(writer, budget.GetBudget);
                }
            }
        }

        public void SerializeResources()
        {
            var xmlSerializer = new XmlSerializer(typeof(Resources.ResourceSet));
            Resources.ResourceSet resources = new Resources.ResourceSet().GetResources();


            using (var writer = new StreamWriter("resources.xml"))
            {
                xmlSerializer.Serialize(writer, resources);
            }
        }
    }
}
// EOF