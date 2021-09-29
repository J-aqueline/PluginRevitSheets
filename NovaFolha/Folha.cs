using System;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;

//código para a criação da folha no revit
namespace NovaFolha
{
    [Transaction(TransactionMode.Manual)]

    public class Command : IExternalCommand
    {
       

        public Result Execute(
          ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            //ElementId titleblockid = FEC.FirstElementId();
            FilteredElementCollector FEC = new FilteredElementCollector(doc);
            FEC.OfCategory(BuiltInCategory.OST_TitleBlocks);
            FEC.WhereElementIsElementType();
            ElementId titleBlockId = FEC.FirstElementId();

           
            string wrline = null;


            //Criando a folha
            FilteredElementCollector sheets = new FilteredElementCollector(doc).OfClass(typeof(ViewSheet));
            foreach (ViewSheet sht in sheets)
            {
                if (sht.SheetNumber.StartsWith("L20-")) sht.SheetNumber = sht.SheetNumber.Replace("L20-", "A");
            }

            ViewSheet SHEET = null;
            Transaction actrans = new Transaction(doc);
            actrans.Start("sheet");
            SHEET = ViewSheet.Create(doc, titleBlockId);
            SHEET.Name = "NOME";
            //SHEET.SheetNumber = "";
            actrans.Commit();

            return Result.Succeeded;

        }
    }

    /*private void CreateSheetsView(Autodesk.Revit.DB.Document document)
    {

        // Get an available title block from document
        FilteredElementCollector collector = new FilteredElementCollector(document);
        collector.OfClass(typeof(FamilySymbol));
        collector.OfCategory(BuiltInCategory.OST_TitleBlocks);

        FamilySymbol fs = collector.FirstElement() as FamilySymbol;
        if (fs != null)
        {
            using (Transaction t = new Transaction(document, "Create a new ViewSheet"))
            {
                t.Start();
                try
                {
                    // Create a sheet view
                    ViewSheet viewSheet = ViewSheet.Create(document, fs.Id);
                    if (null == viewSheet)
                    {
                        throw new Exception("Failed to create new ViewSheet.");
                    }

                    // Add passed in view onto the center of the sheet
                    UV location = new UV((viewSheet.Outline.Max.U - viewSheet.Outline.Min.U) / 2,
                                         (viewSheet.Outline.Max.V - viewSheet.Outline.Min.V) / 2);
    

                // Print the sheet out
                    if (viewSheet.CanBePrinted)
                    {
                        TaskDialog taskDialog = new TaskDialog("Revit");
                        taskDialog.MainContent = "Print the sheet?";
                        TaskDialogCommonButtons buttons = TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No;
                        taskDialog.CommonButtons = buttons;
                        TaskDialogResult result = taskDialog.Show();

                        if (result == TaskDialogResult.Yes)
                        {
                            viewSheet.Print();
                        }
                    }

                    t.Commit();
                }
                catch
                {
                    t.RollBack();
                }
            }
        }
    }*/











    /*public class Folha
    {
        //create a filter to get all the title block type
        FilteredElementCollector FEC = new FilteredElementCollector(doc);
        FEC.OfCategory(BuiltInCategory.OST_TitleBlocks);
        FEC.WhereElementIsElementType();

      //get elementid of first title block type ElementId titleblockid = FEC.FirstElementId();
        string wrline = null;

      //Create sheet
        ViewSheet SHEET = null;
        Transaction actrans = new Transaction(doc);
        actrans.Start("sheet");
        SHEET = ViewSheet.Create(doc, titleblockid);
        SHEET.Name = "SHEET NAME";
        SHEET.SheetNumber = "A-01";
         actrans.Commit();
    }*/
}
