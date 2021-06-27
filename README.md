# MK7_3D_KMP_Editor
  
*Overview*  
This program is for editing KMP files of "Mario Kart 7".  
  
*Project purpose*  
This project was originally created to understand how to work with 3D data using Helix Toolkit.  
I chose the KMP file because of my pure curiosity, "I want to express this file in 3D," and the merit of being able to achieve my original purpose by reading this file.  
  
*how to use*  
To actually use this program, you need to build a project.  
  
*About the build environment*  
HelixToolkit v2.12.0  
HelixToolkit.Wpf v2.12.0  
Visual Studio 2019  
  
*Things to prepare before build*  
ObjFlow.bin  
KMPObjectFlow.zip  
  
*About KMPObjectFlow.zip*  
KMPObjectFlow.zip contains in-game models (enemy, environment objects, etc.), but this project uses KMPObjectFlow.zip, which is a compressed empty folder.  
If you want to use the in-game model, do the following (start at step 4 if you want to use the default model):  
  
1. Unzip KMPObjectFlow.zip.  
2. Place all the models (.obj) extracted from the game in the KMPObjectFlow folder.  
3. Compress the KMPObjectFlow folder and replace it with KMPObjectFlow.zip.  
4. Place ObjFlow.bin in the same directory as KMPObjectFlow.zip.  
5. Build and run the program.  
6. Click "OBJFlow => ObjFlow.bin -> ObjFlowData.Xml" to create ObjFlowData.Xml.  
7. The ObjFlowData.xml file is created.  
