# NeoKCLCreate
The power of NEO came together and created NeoKCLCreate.


Visually, it looks very similar to the original KCLCreate. There are a few key differences between KCLCreate and NeoKCLCreate:
- NeoKCLCreate doesn't bug out and not assign KCL codes
- The names of the KCL Codes are more descriptive
- NeoKCLCreate has KCL presets for common setups (like Grass, Lava, Ice, etc.)

![image](https://user-images.githubusercontent.com/44330283/165541265-52c87e23-8502-4844-a3d0-c8cd08bfc7d5.png)

![image](https://user-images.githubusercontent.com/44330283/165542573-d39e4a1d-6766-47e3-aa63-4c935952ebea.png)



# How to use
## How to create a new KCL file
1. When creating your model, you must make sure you triangulate it!
2. Export it in the Wavefront Obj (.obj) file format
3. In NeoKCLCreate, use the File>New ([CTRL]+[N]) and load the obj.
4. Setup the collision codes for each material. (The presets can be found in Edit>Presets or by pressing [CTRL]+[P])
5. When all materials have the settings you want, Save the new KCL (File>Save/File>Save As, or [CTRL]+[S]/[CTRL]+[SHIFT]+[S]).<br/>It is important to note that doing the normal save function will create the KCL/PA files under the same name as the input obj file. Use Save As to specify a different name.
6. Wait for the KCL/PA to be created. Depending on the size of your model, this might take a moment. (The title of the window will show a %)
7. You're done! Both a KCL and PA file have been created.


## How to dump an existing KCL file
1. Open the KCL with NeoKCLCreate.<br>Note that there must be a matching PA file in order to do this. MyCollision.kcl must have MyCollision.pa
2. Select File>Export ([CTRL]+[E])
3. Wait for the KCL to be exported
4. You're done! The KCL has been converted to a Wavefront Obj (.obj) file format.
