# Description

Once a surface has been edited in Civil 3D and exported to a LandXML file, elements are added which define drawing objects (breaklines, alignments, etc.) used by the surface. This can cause signficant bloat for large enough surfaces.

The LandXML surfaces only really need points and faces, and seem to be just fine after removing PlanFeatures/SourceData/Alignments.

_LandXMLCleaner_ is simple console application to remove elements from .XML files.
While this is set up for use with Civil 3D LandXML files, any .XML file should work.

# Usage

Drag and drop an XML file or directory containing XML files into this handy-dandy console application and press ENTER to start. The application should create a copy of each file in the original file's directory, sans specified elements.

Drag and drop a JSON file into the command line to quickly load a pre-defined list of elements to remove. This overwrites any existing elements.

Directories containing multiple .xml files can be entered into the console for batch processing.

**Expected JSON format**:

```
[
    "element1",
    "element2"
]
```
