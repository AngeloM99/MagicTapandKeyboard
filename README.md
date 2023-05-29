# MagicTapandKeyboard
Working in Progress development files for magic tap and keyboard. 
Main development files are in the Master stream

**Core functions are contained in AcceStimulate.cs and CrossPlane.cs**

Only a few functions from AcceStimulate.cs are inherited in the MagicTap keyboard codebase. This is primarily because of the implementation of a new fast tap function, which incorporates a line crossing mechanism. The detailed explanation of how the line crossing mechanism works can be found in the CrossPlane.cs file, along with a thorough description.

All the actual implementation of behaviors in the MagicTap keyboard is written in the KeyTriggeringBehavior.cs file. This file utilizes the power of UnityEvent to define and handle various functionalities.


**I will only list relevant Unity Event for the keyboard:**
### AcceStimulate.cs






