# LandXMLCleaner
A simple console application to remove elements from .XML files.
While this is set up for use with Civil 3D LandXML files, any .XMl file should work.

## The Problem
Once a surface has been edited in Civil 3D and exported to a LandXML file, elements get added for every line edit and breakline added to the surface. These extra elements can take up a large amount of space in the file (several megabytes depending on the number of edits) and cause Civil 3D to hang for an extended period of time when imported.
The LandXML surfaces seem to be just fine after removing these PlanFeatures and SourceData elements (as far as I can tell).

## The Solution
Drag and drop an XML file into this handy dandy console application and press ENTER to start. The application goes through the file and removes elements matching those specified in the Elements List.

Comes set up for removing \<PlanFeatures\> and \<SourceData\> elements from files exported from AutoCAD Civil 3D.
