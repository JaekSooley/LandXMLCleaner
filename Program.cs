// See https://aka.ms/new-console-template for more information
using ConsoleUI;
using System;
using System.Xml;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;


List<string> elementList = new() // List of elements to remove
{
    "PlanFeatures",
    "SourceData",
    "Alignments"
};
List<string> fileList = new();

string fnameAppend = "-clean";
string xmlExt = ".xml";

bool mainLoop = true;


while (mainLoop)
{
    UI.Header("Home");
    UI.Write("This program removes unwanted elements from LandXML files to reduce bloat.");
    UI.Write("");
    UI.Write("Drag and drop an XML file and press ENTER to begin.");
    UI.Write("");
    UI.Option("[S]ETTINGS", "Add or remove elements");
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
            // Load/process all files in directory
            if (Directory.Exists(input))
            {
                fileList.Clear();

                string[] files = Directory.GetFiles(input);
                foreach(string file in files)
                {
                    if (Path.GetExtension(file).ToLower() == xmlExt)
                    {
                        fileList.Add(file);
                    }
                }

                if (fileList.Count > 0)
                {

                    bool menuProcess = true;
                    while (menuProcess)
                    {
                        UI.Header("Folder Found");
                        UI.Write($"Current files:");
                        foreach (string file in fileList)
                        {
                            UI.Write($"\t{file}");
                        }
                        UI.Write();
                        UI.Write("Remove elements from files?");
                        UI.Option("[Y]", "Yes");
                        UI.Option("[N]", "No");
                        UI.Write();
                        UI.Write("Note: Files will be automatically renamed.");
                        UI.Write();
                        UI.Option("[S]ETTINGS");

                        switch (Input.GetString("Y").ToUpper())
                        {
                            case "Y":
                                UI.Header("Cleaning LandXML files...");
                                foreach (string file in fileList)
                                {
                                    ProcessXmlFile(file, true);
                                }
                                UI.Pause();
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
                    UI.Error("No .xml files found!");
                }
            }

            // Process single file
            else if (File.Exists(input))
            {
                string ext = Path.GetExtension(input).ToLower();
                if (ext == ".json")
                {
                    UI.Header("JSON File Found");
                    UI.Write("Load elements from JSON file?");
                    UI.Option("[Y]", "Yes");
                    UI.Option("[N]", "No");

                    switch(Input.GetString("Y").ToUpper())
                    {
                        case "Y":
                            ProcessJsonFile(input);
                            break;
                        default:
                            break;
                    }
                }
                else if (ext == xmlExt)
                {
                    fileList.Clear();
                    fileList.Add(input);

                    bool menuProcess = true;
                    while (menuProcess)
                    {
                        UI.Header("File Found");
                        UI.Write($"Current file(s):");
                        foreach (string file in fileList)
                        {
                            UI.Write($"\t{file}");
                        }
                        UI.Write();
                        UI.Write("Remove elements from file?");
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
                    UI.Error("Unsupported file extension.");

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
    bool active = true;
    while (active)
    {
        UI.Header("Settings");
        UI.Option("[A]PPEND", "Change text added to new files.");
        UI.Option("[E]LEMENTS", "Modify elements list.");

        switch (Input.GetString().ToUpper())
        {
            case "A":
            case "APPEND":
                SettingsChangeAppend();
                break;
            case "E":
            case "ELEMENTS":
                SettingsChangeElements();
                break;
            default:
                active = false;
                break;
        }
    }
}

void SettingsChangeAppend()
{
    UI.Header("Change Append Text");
    UI.Write("Change string automatically appended to new files");

    string newAppend = Input.GetString(fnameAppend);
    fnameAppend = newAppend;
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
        UI.Option("[E]xport", "Export elements list to JSON file");

        string input = Input.GetString();

        switch (input.ToUpper())
        {
            case "A":
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
            case "D":
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
            case "E":
            case "EXPORT":
                UI.Header("Export Elements");
                UI.Write("Enter path to save file to");
                UI.Write("This doesn't actually work yet, but it'd be cool if it did.");
                UI.Pause();
                break;
            default:
                menuModifyElements = false;
                break;
        }
    }
}


void ProcessXmlFile(string fname, bool autosave = false)
{
    UI.Write($"Cleaning {Path.GetFileName(fname)}...");
    XmlDocument xmlDoc = new XmlDocument();
    xmlDoc.PreserveWhitespace = true;
    xmlDoc.Load(fname);

    if (elementList.Count > 0)
    {
        for (int i = 0; i < elementList.Count; i++)
        {
            Console.Write($"\tRemoving \"{elementList[i]}\"");

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
                        UI.Error($"Element returned null.");
                    }
                }
                UI.Write();
            }
            else
            {
                // Couldn't find element in file
                UI.Write("... Not found.");
            }
        }

        string fnameNew;

        string dir = Path.GetDirectoryName(fname);
        string fnameNoExt = Path.GetFileNameWithoutExtension(fname);

        if (!autosave)
        {

            UI.Write("Done");
            UI.Write("Press enter to save, or define a new filename.");

            fnameNew = Input.GetString(fnameNoExt + fnameAppend);
        }
        else
        {
            fnameNew = fnameNoExt + fnameAppend;
        }

        xmlDoc.Save(dir + "\\" + fnameNew + xmlExt);

        UI.Write($"\tSaved {fnameNew + xmlExt}");
        UI.Write();
    }
    else
    {
        UI.Error("No defined elements in list!");
        UI.Write("The alements list defines the XML elements that are to be removed by the program.");
        UI.Write("Go to Settings > Elements > Add to add elements to the list, or input a .json file into the main menu.");
    }
}

void ProcessJsonFile(string fname)
{
    UI.Header("Import Elements");
    string jsonText = File.ReadAllText(fname);

    elementList.Clear();

    var jsonList = JsonSerializer.Deserialize<List<String>>(jsonText);
    if (jsonList != null)
    {
        elementList = jsonList.ToList();
    }

    UI.Write($"Imported {elementList.Count} elements:");

    foreach(string element in elementList)
    {
        UI.Write();
        UI.Write("\t" + element);
    }

    UI.Pause();
}