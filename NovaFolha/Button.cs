using System;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;


namespace NovaFolha
{
        class App : IExternalApplication
        {
            public Result OnStartup(UIControlledApplication a)
            {
                // Method to add Tab and Panel 
                RibbonPanel panel = ribbonPanel(a);
                // Reflection to look for this assembly path 
                string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
                // Add button to panel 
                PushButton button = panel.AddItem(new PushButtonData("Button", "Criação de folhas", thisAssemblyPath, "NovaFolha.Command")) as PushButton;
                // Add tool tip 
                button.ToolTip = "Folhas";
                // Reflection of path to image 
                var globePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "LACMA DOORS2.png");
                Uri uriImage = new Uri(globePath);
                // Apply image to bitmap
                BitmapImage largeImage = new BitmapImage(uriImage);
                // Apply image to button 
                button.LargeImage = largeImage;

                a.ApplicationClosing += a_ApplicationClosing;

                //Set Application to Idling
                a.Idling += a_Idling;

                return Result.Succeeded;
            }

            //*****************************a_Idling()*****************************
            void a_Idling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e)
            {

            }
            //*****************************a_ApplicationClosing()*****************************
            void a_ApplicationClosing(object sender, Autodesk.Revit.UI.Events.ApplicationClosingEventArgs e)
            {
                throw new NotImplementedException();
            }

            public RibbonPanel ribbonPanel(UIControlledApplication a)
            {
                // Tab name 
                string tab = "Addin";
                // Empty ribbon panel 
                RibbonPanel ribbonPanel = null;
                // Try to create ribbon tab. 
                try
                {
                    //a.CreateRibbonPanel("My Test Tools");
                    a.CreateRibbonTab(tab);
                }
                catch { }
                // Try to create ribbon panel. 
                try
                {
                    RibbonPanel panel = a.CreateRibbonPanel(tab, "Addin");
                }
                catch { }
                // Search existing tab for your panel. 
                List<RibbonPanel> panels = a.GetRibbonPanels(tab);
                foreach (RibbonPanel p in panels)
                {
                    if (p.Name == "Addin")
                    {
                        ribbonPanel = p;
                    }
                }
                //return panel 
                return ribbonPanel;
            }
            public Result OnShutdown(UIControlledApplication a)
            {
                return Result.Succeeded;
            }
        }
}

