#as per advertised, update 1.0. In this page of my current development, you will find nothing more and nothing less than my project:

# sccsunity-voxel-portefolio-draft-wip

developed in Unity 2020.3.42f1 . 

uploading some new work of bringing back to life my old projects one by one and developing new things. all around voxels.

My voxel planet generator revision1-makes use of what i learned from watching Craig Perko's first minecraft tutorial on youtube. basically, every voxels that i code are based off how to do the basics of generating a voxel map and vertices/triangles from Craig Perko's first minecraft tutorial on youtube, the new stuff i developed
myself with a lot of trial and error and headaches. rev4 released and i developed "per frame voxel mesh flat 3d looped" (xyz) solution, that dictate how fast the per frame 3d flat loop has to loop to unqueue the Queues, with manual settings in the inspector currently only, and this revision is much faster than my previous revisions and it's smoother on the cpu to generate a voxel mesh (neighboor chunks checks now fixed and working). more inspector settings for dequeuing speed iterations options will be coming in my later revisions:

<img WIDTH=250 src="https://i.ibb.co/JspKZ08/Capture-2023-01-08-121649.png" alt="Capture-2023-01-08-121649" border="0"><img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-01-11%20112013.png" border="0"><img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-01-11%20112112.png" border="0">

My development on movement inside a chunk-wise area in the scene. This is not an infinite terrain generator and the camera has to be moved through the editor scene to see the chunks spawning clamped to the limits of the area chunk. I built this after watching Sebastian Lagues Procedural Landmass generation and needed to develop a way to do the same thing with voxels instead of created meshes as shown in Sebastian Lague's Procedural Landmass generation. The way i succeeded was to build the maps and vertexes in a threadpool, referenced from sebastian lagues procedural landmass generation tutorial that shows how to use threadstarts to offload work to threads, so that i could offload to the mainthread/uithread the voxel meshes data where meshes can be created. using my development on my homemade vertex/triangle reducer where i didn't build a proper shader yet so in order to see the vertex/triangle reducer in action you have to select Shaded Wireframe in the unity scene display options. sc_terrain.cs:

<img WIDTH=350 src="https://i.ibb.co/QPrdmr3/Capture-2023-01-08-121808.png" alt="Capture-2023-01-08-121808" border="0"><img WIDTH=350 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-01-02%20172652.png" border="0">

My development on trying to make compute shaders work with at least receiving the map from the shader but working with creating the vertices on the CPU. Creating the map from the shader referenced from Sebastian Lague's Marching Cubes tutorial and compute shader access and noise, and sebastian lagues procedural landmass terrain generation tutorial. using my development on my homemade vertex/triangle reducer where i didn't build a proper shader yet so in order to see the vertex/triangle reducer in action you have to select Shaded Wireframe in the unity scene display options:

<img WIDTH=350 src="https://i.ibb.co/4WBCd8s/Capture-2023-01-08-121836.png" alt="Capture-2023-01-08-121836" border="0"><img WIDTH=350 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-01-10%20113750.png" border="0">

My development on trying to make compute shaders work with receiving the map and that creates the vertexes locations from the shader and outputs for the cpu to build the vertices.. using 2 script per type of faces (12 total scripts) and shaders for each face types with rev1. in rev2 is my development on trying to make compute shaders work with receiving the map and that creates the vertexes locations from the shader and outputs for the cpu to build the vertices. it's the same thing as the above in terms of results, but developed differently in code as i am only using 2 scripts (2 total) for all faces and shaders for each face types::

<img WIDTH=350 src="https://i.ibb.co/7pb2mCR/Capture-2023-01-08-121906.png" alt="Capture-2023-01-08-121906" border="0"><img WIDTH=350 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-01-10%20113304.png" border="0"><img WIDTH=350 src="https://i.ibb.co/74YLjPY/Capture-2023-01-08-121949.png" alt="Capture-2023-01-08-121949" border="0"/><img WIDTH=350 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-01-10%20114755.png" border="0">

My new development on building a very basic simple example of a chunk that is constructed using my WIP homemade vertex/triangle reducer, and also creating a second
mesh for the top face by having compute shaders to work on the map and vertex location and face dimensions but constructing the vertices vector3 on the cpu (i failed at trying to use the append method in hlsl to append to a list, so for the moment expect the cpu to work on creating only the vector3 for each vertex). I use the same principles as the above 2 projects for the topface and create it to the scene, and use a second chunk generation system for the gray chunk underneath that uses my vertex triangle reducer.

<img WIDTH=500 src="https://i.ibb.co/CKXzkGN/Capture-d-cran-2023-01-09-183439.png" alt="Capture-d-cran-2023-01-09-183439" border="0">

My old development brought back to life, a script called universegen that i based off of watching the tutorial of Sebastian Lague's Procedural Landmass generation 
and successfully making the threadpool work on generating the map/vertices/triangles and using bytes (as shown in Craig Perkos first and second minecraft tutorial) for a very fluid performance for small chunks. universe Rev1:

<img WIDTH=350 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-01-08%20122041.png" border="0"><img WIDTH=350 src="https://github.com/ninekorn/gif-resources/blob/main/GifMaker_20230112174527516.gif" border="0">

My new development on using compute shaders to calculate the map needed for my universe script to create vertices/triangles from a threadpool. Referenced from how sebastian lague did it in his marching cubes tutorial on github and youtube and based off the basics of voxel generation of craig perko's first minecraft tutorial on youtube. But i am currently only building the map in the shader and not the vertex locations yet. universe Rev2:

<img WIDTH=500 src="https://i.ibb.co/7KKHtTK/Capture-2023-01-08-122113.png" alt="Capture-2023-01-08-122113" border="0">

My old development with a few tweaks. using my old homemade vertex/triangle reducer, i build the faces one after the other and my vertex/triangle reducer gives better results since the arrays of vertices position aren't shared between the functions that build the faces/verts for the chunk. This is a basic example of how to correctly use my homemade vertex/triangle reducer as i never was done in making sure if it was working or not. I have many variations of my vertex/triangle reducer development, but building the faces one after the other always was the one iteration that gave better results. And it seems compatible with unity's recalculatebounds() and recalculatenormals() functions because the normals are beautiful with a standard material only and no shaders, although sometimes the perlin seed seems to break the logic of my vertex triangle reducer. i need to do more tests.

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-01-10%20152954.png" border="0"><img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-01-10%20152625.png" border="0"><img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-01-10%20145011.png" border="0">

My planet generator revision 3 that i brought back to life coming from my sccsunityVR portefolio. Trying to bring back my 1st and 2nd revision also. You can find my sccsunityvr portefolio in the link at the bottom. The perlin noise never worked correctly for my revision 3 if i remember correctly, hence why it's chunks filled with unbroken bytes... Here i developed a custom projectile tracer, that runs in a task per frame. The speed of the projectile/bullet/ray can be adjusted in code. I developed this without using any physics engine and there are no colliders on the bullet/ray and no colliders on the chunk. If not for the number of draw calls (not using compute shaders here so for faster on cpu generation i use my vertex triangle reducer with tiny chunk meshes of 10x10y10z that are hard on the GPU. Spawning bigger meshes with the help of compute shaders and there is a very high chance that my solution for breaking bytes per frame with a cpuray projectile would still work but how big would the mesh then be and how fast to dig voxels with changing my script variables to try and make my example lag regarding breaking the mesh and rebuilding it on the cpu, if a better technique wwas used to render the voxels on screen? My example is about how fast could it go on the cpu with using a precise ray and without colliders and rebuilding the mesh on the cpu, and i hadn't started learning compute shaders yet. so in the future i might calculate the bytemap for each chunk and the vertex location and face dimensions from a compute shader and benchmark from there. currently slow at chunk sized 40x40y40z, the ray makes the cpu bottleneck for a small cubic planet see screenshot GifMaker_20230113115233479.gif . first 5 screenshots in 10x10y10z chunk size, and the 6th is 20x20y20z sized chunk)

<img src="https://github.com/ninekorn/gif-resources/blob/main/GifMaker_20230111183030529.gif" width="150"/><img src="https://github.com/ninekorn/gif-resources/blob/main/GifMaker_20230112163533283.gif" width="150"/><img src="https://github.com/ninekorn/gif-resources/blob/main/GifMaker_20230112172120215.gif" width="150"/><img src="https://github.com/ninekorn/gif-resources/blob/main/GifMaker_20230112172402622.gif" width="150"/><img src="https://github.com/ninekorn/gif-resources/blob/main/GifMaker_20230112172543029.gif" width="150"/><img src="https://github.com/ninekorn/gif-resources/blob/main/GifMaker_20230113115126113.gif" width="150"/><img src="https://github.com/ninekorn/gif-resources/blob/main/GifMaker_20230113115233479.gif" width="150"/>

My development on breaking cubes in unity3d back in 2016-2017. i started with creating my scripts with names cubicfrac because i wanted to break cubes and only cubes and climb the rope to move to breaking other types of objects later. I then changed my scripts name to fracture and then shatter when i was able to make a random point inside of a cube and link all the triangles to that random point. but never continued development. Now those asset names are picked on the store, so i am reverting back to my first name CubicFrac and releasing my stuff as that scene name instead CubicFrac, although using my old scripts names in my released folder Fracture4/Shatter1/cubicFrac. Don't worry, those scripts are mine, i developed them, and don't know who's on the store that picked my fav names but now i am stuck with cubicfrac but i am happy anyways. I could procedurally name my development the name that i want them to be. i developed this 2016-2017. although i will be continuing development on the three of my development that i developed back then. I might change it to cubiccrack or something. or i will call them sccsfracture and sccscubicfrac and sccsshatter. 2 of them would be named derivatives to what the assets are named in the store simply because of the same name in the scripts, but i assure you that i created them.

<img src="https://github.com/ninekorn/gif-resources/blob/main/GifMaker_20230112144708616.gif" width="350"/>

I am also halfway there in bringing back my sccsunityVR revision 1 of my VR ik rig character project, that can wield a pickaxe and a breakable voxel planet with fracturable cubic bytes that can also recombine into a bigger mesh after time passes by for draw call reduction. The spawning of fractures upon the pickaxe here is exaggerated so don't stay in the destroyed by area for a while otherwise it's permaspawn procedurally destroying cubes at the destroyed byte location. i will fix this later when i have a chance. Those projects, i have developped them myself, combining multiple assets, in my free time with my creativity and i will provide the references of certain scripts which aren't mine but easily available on unify wiki or/and the stackoverflow forums or/and the unity engine forums. To see the inverse kinematic in action in my projects, i have read/heard and understood the ik asset of a lot of people but not those that i cannot understand. So the ik i learned it pretty much at least from ProgramYourFace - YouTube (broken link) and dogzerx2 (broken link) and the chunk system i learned it from Craig Perko - YouTube.

(movement controls available in one example now):

<img src="https://github.com/ninekorn/gif-resources/blob/main/GifMaker_20230112020224749.gif" width="185"/><img src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-01-12%20113430.png" width="185"/><img src="https://github.com/ninekorn/gif-resources/blob/main/GifMaker_20230112114557048.gif" width="185"/><img src="https://github.com/ninekorn/gif-resources/blob/main/GifMaker_20230114011759150.gif" width="150"/><img src="https://github.com/ninekorn/gif-resources/blob/main/PhotoGIF_1_14_2023_8_37_11_PM.gif" width="150"/>

the pressure was too big to release on the unity store or not, so despite my portefolio not beeing that cleaned up and not having inquired about how to bring this to the store, when i just have too many bugs that users of my projects would complain too much after purchase anyway since this is an incomplete portefolio, i decided to release it here on github, MIT, and from here, maybe i can keep on piling up my development on top of my development to make better revisions in the future. And how could i be selling to the store when i use so many different MIT license anyway. It feels like a troublesome process which i don't have to go through when i am using github for release. A lot of my projects that i have coded and developed are old and/or with commented out discarded trial/error attempts, and most of my projects are blueprints/templates that can be simplified by anyone anyway, and/or improved upon.

thank you for trying my projects. it's far from perfect and it's not without bugs. i am happy if you find my projects inspiring in order for you to do your own work and programs with voxels in unity or not, i deliver my assets to you as per mit license. I will clean my scripts as i go. Please note that the assets that i developped are of beginner level more or less, and there is a lot of experimentals in that. Also, i am reusing my old code development and it's really far in terms of what i was doing in low-level in terms of how different i had been approaching how to generate voxels, and for how much long that i had been working in low-level but it doesn't mean that what i was doing in low-level was good anyway, it was functional. i just can't believe i managed to get my old scripts back together and reworked into something different than what was my sccsvrunity portefolio that i made such a long time ago, so i can at least be proud of that but this is a draft and it looks as such. My apologies that the speed of generation for such a small level isn't by far on par with my low-level stuff, except the flat loop example of my rev1 to build a planet fast without multithreading which is with multithreading what i want to attempt in my next revisions of generating a planet. Now updates are going to take longer for each ones of my examples, because i have to rework my examples with trying to porting my things from low-level barebone c# with sharpdx, and also because these past releases here in this repo until this official 1.0 release are all involving retrying my old unity development and seeing if it still was working and bringing it non vr and barely adding new things. And i had hard time doing that as so many things didn't work. Release 1.0 really is just a draft of more experiments to come on the same things and what surrounds them as long as i keep working on them. Anyone can work on my vertex triangle reducer logic still, i'm not even done rewritting it, it seems so imperfect and convoluted to do such a small thing. I've developped different variations in a compute shader and almost succeeded and now my example doesn't work anymore, it's like a big void example of nothing.

sc

And yes, i know, i don't even come close to other programmers projects regarding voxel terrain generation in unity3d. even outside of unity3d i've been struggling. 

I developed other projects in the past in Virtual reality (using the oculus rift cv1) for unity 2017.4.39f1 or 2017.4.40f1 : 

https://github.com/ninekorn/sccsvrunity-portefolio-draft-wip:

<img WIDTH=150 src="https://i.ibb.co/q9YBcCT/sccsvrunityv0.webp" alt="sccsvrunityv0" border="0"><img WIDTH=150 src="https://i.ibb.co/1TB9LWc/sccsvrunityv2.webp" alt="sccsvrunityv2" border="0"><img WIDTH=150 src="https://i.ibb.co/619MY3M/sccsvrunityv1.webp" alt="sccsvrunityv1" border="0"><img WIDTH=150 src="https://i.ibb.co/5jkgr8f/sccsvrunityv3.webp" alt="sccsvrunityv3" border="0">

https://www.patreon.com/posts/76960405
