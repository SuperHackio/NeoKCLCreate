# NeoKCLCreate
A modern and efficient collision importer for the Super Mario Galaxy games

<img width="497" height="387" alt="Setting collision codes for a custom planet!" src="https://github.com/user-attachments/assets/ef2117f2-20c8-466a-9417-19b9561d89c5" />




# How to use
## How to create a new KCL file
1. When creating your model, you must make sure you triangulate it before importing! Otherwise you will have holes in the resulting model.
2. Export it in the Wavefront Obj (.obj) file format
3. In NeoKCLCreate, use the File > New (<kbd>Ctrl</kbd>+<kbd>N</kbd>) and load the obj.
5. The default import settings are good for most cases, but you can change them if needed.
6. Once the import finishes, you can define the collision codes, which control how the material grouped triangles behave in-game.
7. Once you're happy, simply save with File > Save (<kbd>Ctrl</kbd>+<kbd>S</kbd>)


## How to dump an existing KCL file
1. Open the KCL with NeoKCLCreate.<br>Note that there must be a matching PA file in order to do this. MyCollision.kcl must have MyCollision.pa
2. Select File > Export Geometry (<kbd>Ctrl</kbd>+<kbd>E</kbd>)
3. Wait for the KCL to be exported
4. You're done! The KCL has been converted to a Wavefront Obj (.obj) file format.


## Tips
- If you want to get rid of materials with identical settings, you can use Edit > Crush.
- There are some presets available for you to use, which are commonly used materials in the galaxy games.
- Opening an existing KCL you imported and simply resaving may result in a more optimized file than before.
- The less triangles the Wii has to check, the better, so be sure to lower the quality of your collision model compared to the visual model.
- You can export a visual representation of the Octree as well as the collision Geometry to see for yourself how the game finds triangles.
- You can select and modify multiple materials at once by holding <kbd>Shift</kbd> or <kbd>Ctrl</kbd> when selecting materials in the list
- You can choose to include the names of the materials that you are working with inside the .pa file in case you want to edit the .pa file again later (or just want to preserve the material names in general)
