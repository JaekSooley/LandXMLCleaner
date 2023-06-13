// See https://aka.ms/new-console-template for more information
using ConsoleUI;
using System;
using System.Xml;


List<string> elementList = new List<string>(); // List of elements to remove

bool mainLoop = true;

elementList.Add("PlanFeatures");
elementList.Add("SourceData");

while (mainLoop)
{
    UI.Header("Home");
    Console.WriteLine("This program removes unwanted elements from LandXML files to reduce bloat.");
    Console.WriteLine("");
    Console.WriteLine("Drag and drop an XML file and press ENTER to begin.");
    Console.WriteLine("");
    UI.Option("[S]ETTINGS");
    UI.Option("E[X]IT");

    string input = Input.GetString();
    input = input.Replace("\"", "");

    switch (input.ToUpper())
    {

        // More options here.
        case "S":
        case "SETTINGS":
            Settings();
            break;

        case "X":
        case "EXIT":
            mainLoop = false;
            break;

        case "":
            break;

        default:
            // Open the file and do the thing
            if (File.Exists(input))
            {
                bool menuProcess = true;
                while (menuProcess)
                {
                    UI.Header("File Found");
                    Console.WriteLine($"Current file: \"{input}\"");
                    Console.WriteLine();
                    Console.WriteLine("Remove elements from this file?");
                    UI.Option("[Y]", "Yes");
                    UI.Option("[N]", "N-no... Nevermind.");
                    UI.Write();
                    UI.Option("[S]ETTINGS");

                    switch (Input.GetString("Y").ToUpper())
                    {
                        case "Y":
                            ProcessXmlFile(input);
                            menuProcess = false;
                            break;
                        case "S":
                        case "SETTINGS":
                            Settings();
                            break;
                        default:
                            menuProcess = false;
                            break;
                    }
                }
            }
            else
            {
                UI.Error("Unrecognised command!");
            }
            break;
    }
}

UI.Header("Goodbye");


void Settings()
{
    UI.Header("Settings");
    UI.Option("[E]LEMENTS", "Modify elements list.");

    switch (Input.GetString().ToUpper())
    {
        case "E":
        case "ELEMENTS":
            SettingsChangeElements();
            break;
    }

}


void SettingsChangeElements()
{
    bool menuModifyElements = true;

    while (menuModifyElements)
    {
        UI.Header("Modify Elements");
        UI.Write("Current elements:");
        UI.Write();
        foreach (string element in elementList) UI.Write($"\t{element}");
        UI.Write();
        UI.Write("Add or remove elements from the list.");
        UI.Write("The list defines elements that will be removed from XML files.");
        UI.Write();
        UI.Option("[ADD]", "Add elements to list");
        UI.Option("[DEL]", "Remove elements from list");

        string input = Input.GetString();

        switch (input.ToUpper())
        {
            case "ADD":
                bool menuadd = true;
                while (menuadd)
                {
                    UI.Header("Add Elements");
                    UI.Write("Current elements:");
                    UI.Write();
                    foreach (string element in elementList) UI.Write($"\t{element}");
                    UI.Write();
                    UI.Write("Type an element name and press ENTER to add them to the list.");
                    UI.Write("Press ENTER with a blank line to finish.");
                    string newElement = Input.GetString();
                    if (newElement != "")
                    {
                        elementList.Add(newElement);
                    }
                    else
                    {
                        menuadd = false;
                    }
                }
                break;
            case "DEL":
                bool menudel = true;
                while (menudel)
                {
                    UI.Header("Delete Elements");
                    UI.Write("Current elements:");
                    UI.Write();
                    foreach (string element in elementList) UI.Write($"\t{element}");
                    UI.Write();
                    UI.Write("Type an element's name and press ENTER to remove them from the list.");
                    UI.Write("Press ENTER with a blank line to finish.");
                    string removeElement = Input.GetString();
                    if (removeElement != "")
                    {
                        elementList.Remove(removeElement);
                    }
                    else
                    {
                        menudel = false;
                    }
                }
                break;
            default:
                menuModifyElements = false;
                break;
        }
    }
}


void ProcessXmlFile(string fname)
{
    UI.Header("Cleaning LandXML file...");

    XmlDocument xmlDoc = new XmlDocument();
    xmlDoc.Load(fname);

    if (elementList.Count > 0)
    {
        for (int i = 0; i < elementList.Count; i++)
        {
            UI.Write($"\tRemoving \"{elementList[i]}\"... ");

            XmlNodeList elementsToRemove = xmlDoc.GetElementsByTagName(elementList[i]);

            if (elementsToRemove.Count > 0)
            {
                for (int j = elementsToRemove.Count - 1; j >= 0; j--)
                {
                    XmlNode element = elementsToRemove[j];

                    if (element != null)
                    {
                        if (element.ParentNode != null)
                        {
                            element.ParentNode.RemoveChild(element);
                        }
                        else
                        {
                            UI.Error("ParentNode returned null.");
                        }
                    }
                    else
                    {
                        UI.Error($"element returned null.");
                    }
                }
            }
            else
            {
                UI.Write($"\t\"{elementList[i]}\" not found.");
                UI.Write();
            }
        }

        Console.WriteLine("Done");
        Console.WriteLine("Press enter to save, or define a new filename.");

        string fnameNew = Input.GetString(fname);

        xmlDoc.Save(fnameNew);

        Console.WriteLine("Saved!");

        UI.Pause();
    }
    else
    {
        UI.Error("No defined elements in list!");
        UI.Write("The alements list defines the XML elements that are to be removed by the program.");
        UI.Write("Go to Settings > Elements > Add to add elements to the list.");
    }
}
