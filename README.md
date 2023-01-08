# sccsunity-voxel-portefolio-draft-wip
developed in Unity 2020.3.42f1 . uploading some new work of bringing back to life my old projects one by one and developing new things. all around voxels.

My voxel planet generator revision1-makes use of what i learned from watching Craig Perko's first minecraft tutorial on youtube. basically, every voxels that i code are based off how to do the basics of generating a voxel map and vertices/triangles from Craig Perko's first minecraft tutorial on youtube, the new stuff i developed
myself with a lot of trial and error and headaches:

<img WIDTH=500 src="https://i.ibb.co/JspKZ08/Capture-2023-01-08-121649.png" alt="Capture-2023-01-08-121649" border="0">

My development on movement inside a chunk-wise area in the scene. This is not an infinite terrain generator and the camera has to be moved through the editor scene to see the chunks spawning clamped to the limits of the area chunk. But i built this after watching Sebastian Lagues Procedural Landmass generation and needed to develop a way to do the same thing with voxels instead of created meshes as shown in Sebastian Lague's Procedural Landmass generation. The way i succeeded was to build the maps and vertexes in a threadpool, referenced from sebastian lagues procedural landmass generation tutorial that shows how to use threadstarts to offload work to threads, so that i could offload to the mainthread/uithread the voxel meshes data where meshes can be created. using my development on my homemade vertex/triangle reducer where i didn't build a proper shader yet so in order to see the vertex/triangle reducer in action you have to select Shaded Wireframe in the unity scene display options. sc_terrain.cs:

<img WIDTH=500 src="https://i.ibb.co/QPrdmr3/Capture-2023-01-08-121808.png" alt="Capture-2023-01-08-121808" border="0">

My development on trying to make compute shaders work with at least receiving the map from the shader but working with creating the vertices on the CPU. Creating the map from the shader referenced from Sebastian Lague's Marching Cubes tutorial and compute shader access and noise. using my development on my homemade vertex/triangle reducer where i didn't build a proper shader yet so in order to see the vertex/triangle reducer in action you have to select Shaded Wireframe in the unity scene display options:

<img WIDTH=500 src="https://i.ibb.co/4WBCd8s/Capture-2023-01-08-121836.png" alt="Capture-2023-01-08-121836" border="0">

My development on trying to make compute shaders work with receiving the map and that creates the vertexes locations from the shader and outputs for the cpu to build the vertices.. using 1 script per type of faces (6) and shaders for each face types:

<img WIDTH=500 src="https://i.ibb.co/7pb2mCR/Capture-2023-01-08-121906.png" alt="Capture-2023-01-08-121906" border="0">

My development on trying to make compute shaders work with receiving the map and that creates the vertexes locations from the shader and outputs for the cpu to build the vertices. using 1 script for all faces and shaders for each face types:

<img WIDTH=500 src="https://i.ibb.co/74YLjPY/Capture-2023-01-08-121949.png" alt="Capture-2023-01-08-121949" border="0">

My old development brought back to life, a script called universegen that i based off of watching the tutorial of Sebastian Lague's Procedural Landmass generation 
and successfully making the threadpool work on generating the map/vertices/triangles and using bytes (as shown in Craig Perkos first and second minecraft tutorial) for a very fluid performance for small chunks. This is the results of my development after having built sc_terrain.cs. universe Rev1:

<img WIDTH=500 src="https://i.ibb.co/zJqGB6Y/Capture-2023-01-08-122041.png" alt="Capture-2023-01-08-122041" border="0">

My new development on using compute shaders to calculate the map needed for my universe script to create vertices/triangles from a threadpool. Referenced from how sebastian lague did it in his marching cubes tutorial on github and youtube and based off the basics of voxel generation of craig perko's first minecraft tutorial on youtube. But i am currently only building the map in the shader and not the vertex locations yet. universe Rev2:

<img WIDTH=500 src="https://i.ibb.co/7KKHtTK/Capture-2023-01-08-122113.png" alt="Capture-2023-01-08-122113" border="0">
