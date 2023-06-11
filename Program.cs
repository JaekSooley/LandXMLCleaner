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
    Console.WriteLine("This program removes unwanted elements from LandXML files to reduce size and remove breaklines when importing into drawings.");
    Console.WriteLine("");
    Console.WriteLine("Drag and drop xml file and press ENTER to begin.");
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
            UI.Header("Settings");
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
                ProcessXmlFile(input);
            }
            else
            {
                UI.Header("Error");
                Console.WriteLine("Unrecognised command!");
                UI.Pause();
            }
            break;
    }
}

UI.Header("Goodbye");

void Settings()
{
    UI.Header("Settings");
    Console.WriteLine("Elements to Remove:");

    foreach (string element in elementList)
    {
        Console.WriteLine($"\t{element}");
    }

    UI.Pause();
}

void ProcessXmlFile(string fname)
{
    UI.Header("Cleaning LandXML file...");

    XmlDocument xmlDoc = new XmlDocument();
    xmlDoc.Load(fname);

    for (int i = 0; i < elementList.Count; i++)
    {
        Console.WriteLine($"\tRemoving \"{elementList[i]}\"... ");

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

            xmlDoc.Save(fname);
        }
        else
        {
            UI.Error($"No matching element \"{elementList[i]}\" found in the XML file.");
        }
    }

    Console.WriteLine("Done!");

    UI.Pause();
}
