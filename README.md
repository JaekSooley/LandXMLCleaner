# LandXMLCleaner

A simple console application to remove elements from .XML files.
While this is set up for use with Civil 3D LandXML files, any .XML file should work.

Drag and drop a JSON file into the command line to automatically load a list of pre-defined elements to remove. This overwrites any existing elements.

**Expected JSON format**:

```
[
    "element1",
    "element2"
]
```

## The Problem

Once a surface has been edited in Civil 3D and exported to a LandXML file, elements for every line edit and breakline added to the surface. This can cause signficant bloat for large enough surfaces.

The LandXML surfaces seem to be just fine after removing these PlanFeatures and SourceData elements, as they're only used to reconstruct surfaces in the case of missing 3D faces.

## The Solution

Drag and drop an XML file into this handy dandy console application and press ENTER to start. The application goes through the file and removes elements matching those specified in the Elements List.

Comes set up for removing \<PlanFeatures\> and \<SourceData\> elements from files exported from AutoCAD Civil 3D.
