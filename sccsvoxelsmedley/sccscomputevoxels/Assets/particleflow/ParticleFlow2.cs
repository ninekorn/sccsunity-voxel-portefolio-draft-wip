using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using SPINACH.iSCentralDispatch;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.IO;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleFlow2 : MonoBehaviour
{
    //ParticleSystem m_System;
    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;// = new ParticleSystem.Particle[m_currentParticleEffect.particleCount];
    //ParticleSystem.Particle[] m_Particles;
    public float m_Drift = 0.01f;
    byte[] map;

    int width = 10;
    int height = 10;
    int depth = 10;
    float planeSize = 1;
    int seed = 3420;

    int detailScale = 5;
    int heightScale = 5;

    private byte block;

    private Vector3[] positions;
    private Vector3[] normals;
    private Vector2[] textureCoordinates;
    private int[] triangleIndices;

    private int counterVertexTop = 0;
    private int counterVertexBottom = 0;
    private int counterVertexRight = 0;
    private int counterVertexLeft = 0;
    private int counterVertexFront = 0;
    private int counterVertexBack = 0;

    private int vertzIndex = 0;
    private int trigsIndex = 0;

    private Vector3 forward = new Vector3(0, 0, 1);
    private Vector3 back = new Vector3(0, 0, -1);
    private Vector3 right = new Vector3(1, 0, 0);
    private Vector3 left = new Vector3(-1, 0, 0);
    private Vector3 up = new Vector3(0, 1, 0);
    private Vector3 down = new Vector3(0, -1, 0);
    int numberOfNeededParticlesForFaces = 0;

    bool positionsSet = false;

    void InitializeIfNeeded()
    {
        if (m_System == null)
            m_System = GetComponent<ParticleSystem>(); 

        /*if (m_Particles == null || m_Particles.Length < m_System.maxParticles)
            m_Particles = new ParticleSystem.Particle[m_System.maxParticles];*/
    }



    /*private void LateUpdate()
    {
        InitializeIfNeeded();
        int numParticlesAlive = m_System.GetParticles(m_Particles);
        m_System.SetParticles(m_Particles, numParticlesAlive);
    }*/


    private void Start()
    {
        InitializeIfNeeded();
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;

        if (Input.GetKeyDown(KeyCode.R))
        {
           
            
            for (int i = 0; i < m_Particles.Length; i++)
            {
                m_Particles[i].velocity = Vector3.zero;
            }

            Regenerate(currentPosition);
            //m_System.par(m_Particles, 1);

        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            starter();
            m_Particles = new ParticleSystem.Particle[numberOfNeededParticlesForFaces];
            m_System.GetParticles(m_Particles);

        }


        if (positionsSet)
        {
            m_System.SetParticles(m_Particles, m_System.particleCount);

            positionsSet = false;
        }

       

        /*for (int i = 0; i < m_Particles.Length; ++i)
        {
            m_Particles[i].position = newPosition;
        }*/

        //m_currentParticleEffect.SetParticles(m_Particles, m_currentParticleEffect.particleCount);

    }




    private void starter()
    {
    

      
        //int numParticlesAlive = m_System.GetParticles(m_Particles);

        Vector3 currentPosition = transform.position;
        /*for (int i = 0; i < m_Particles.Length; i++)
        {
            m_Particles[i].remainingLifetime = 100000;
        }*/

        map = new byte[width * height * depth];
        for (int x = 0; x < width; x++)
        {
            float noiseX = Math.Abs(((float)(x * planeSize + currentPosition.x + seed) / detailScale) * heightScale);

            for (int y = 0; y < height; y++)
            {
                float noiseY = Math.Abs(((float)(y * planeSize + currentPosition.y + seed) / detailScale) * heightScale);

                for (int z = 0; z < depth; z++)
                {
                    float noiseZ = Math.Abs(((float)(z * planeSize + currentPosition.z + seed) / detailScale) * heightScale);

                    //float noiser = Noise.Generate(noiseX, noiseY, noiseZ);

                    float temporaryY = 10f;
                    float temporaryZ = 10f;
                    float temporaryX = 10f;

                    temporaryY *= Mathf.PerlinNoise((x * planeSize + currentPosition.x + seed) / detailScale, (z * planeSize + currentPosition.z + seed) / detailScale) * heightScale;
                    temporaryX *= Mathf.PerlinNoise((y * planeSize + currentPosition.y + seed) / detailScale, (z * planeSize + currentPosition.z + seed) / detailScale) * heightScale;
                    temporaryZ *= Mathf.PerlinNoise((x * planeSize + currentPosition.x + seed) / detailScale, (y * planeSize + currentPosition.y + seed) / detailScale) * heightScale;

                    float size0 = (1 / planeSize) * currentPosition.y;
                    temporaryY -= size0;

                    float size1 = (1 / planeSize) * currentPosition.x;
                    temporaryX -= size1;

                    float size2 = (1 / planeSize) * currentPosition.z;
                    temporaryZ -= size2;

                    if ((int)Math.Round(temporaryY) >= y)
                    {
                        map[x + width * (y + height * z)] = 1;
                    }
                    else
                    {
                        map[x + width * (y + height * z)] = 0;
                    }

                    byte block = map[x + width * (y + height * z)];
                    if (block == 0) continue;
                    {
                        calculateNumberOfVertex(x, y, z);
                    }
                }
            }
        }

        numberOfNeededParticlesForFaces = counterVertexTop / 4;

        int rate = numberOfNeededParticlesForFaces;
        m_System.emissionRate = rate;
        //Debug.Log(numberOfNeededParticlesForFaces);
        /*while (m_Particles.Length < numberOfNeededParticlesForFaces)
        {
            m_System.startSpeed = 1000;
        }*/

        //m_System.startSpeed = 0;



        /*for (int i = 0; i < m_Particles.Length;i++)
        {
            m_Particles[i].velocity = Vector3.zero;
        }*/


        // In your function or Update
        //m_System.startSize = size;

        /*while (m_Particles.Length < numberOfNeededParticlesForFaces)
        {
            m_System.startSpeed += 1;
            m_System.emissionRate = rate;
            rate += 1;// You can change rate variable according to your requirement
        }*/

        //Debug.Log(m_Particles.Length);

        //m_Particles[i].velocity += Vector3.up * m_Drift;

        //m_Particles[i].position
        // Apply the particle changes to the particle system

    }

    public void calculateNumberOfVertex(int x, int y, int z)
    {

        //TOPFACE
        if (IsTransparent(x, y + 1, z))
        {
            counterVertexTop += 1;
        }

        /*//LEFTFACE
        if (IsTransparent(x - 1, y, z))
        {
            counterVertexLeft += 1;
        }

        //RIGHTFACE
        if (IsTransparent(x + 1, y, z))
        {
            counterVertexRight += 1;
        }

        //FRONTFACE
        if (IsTransparent(x, y, z - 1))
        {
            counterVertexFront += 1;
        }

        //BACKFACE
        if (IsTransparent(x, y, z + 1))
        {
            counterVertexBack += 1;
        }

        //BOTTOMFACE
        if (IsTransparent(x, y - 1, z))
        {
            counterVertexBottom += 1;
        }*/
    }
    int i = 0;
    public void Regenerate(Vector3 currentPosition)
    {
     
        /*for (int i = 0; i < m_Particles.Length; i++)
        {
        }*/

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    // block = map[x, y, z];
                    block = map[x + width * (y + height * z)];

                    if (block == 0) continue;
                    {
                        DrawBrick(x, y, z, currentPosition);
                        
                        //Debug.Log(i);
                    }
                }
            }
        }
        positionsSet = true;
    }

    //chunkPosBig chunkbig;

    public void DrawBrick(int x, int y, int z, Vector3 currentPosition)
    {

        Vector3 start = new Vector3(x * planeSize, y * planeSize, z * planeSize);
        Vector3 offset1, offset2;
        //TOPFACE
        if (IsTransparent(x, y + 1, z))
        {
            m_Particles[i].position = start;
            //
            m_Particles[i].axisOfRotation = new Vector3(1, 0, 0);
            m_Particles[i].rotation3D = new Vector3(90, 0, 0);
            i++;
        }




        //m_Particles[i].velocity = Vector3.zero;
        //m_Particles[i].axisOfRotation = new Vector3(90,0,0);
        /*offset1 = forward * planeSize;
        offset2 = right * planeSize;
        createTopFace(start + up * planeSize, offset1, offset2);
        vertzIndex += 4;
        trigsIndex += 6;*/







        /*//LEFTFACE
        if (IsTransparent(x - 1, y, z))
        {
            offset1 = back * planeSize;
            offset2 = down * planeSize;
            createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2);
            vertzIndex += 4;
            trigsIndex += 6;
        }

        //RIGHTFACE
        if (IsTransparent(x + 1, y, z))
        {
            offset1 = up * planeSize;
            offset2 = forward * planeSize;
            createRightFace(start + right * planeSize, offset1, offset2);
            vertzIndex += 4;
            trigsIndex += 6;
        }
        //FRONTFACE
        if (IsTransparent(x, y, z - 1))
        {
            offset1 = left * planeSize;
            offset2 = up * planeSize;
            createFrontFace(start + right * planeSize, offset1, offset2);
            vertzIndex += 4;
            trigsIndex += 6;
        }
        //BACKFACE
        if (IsTransparent(x, y, z + 1))
        {
            offset1 = right * planeSize;
            offset2 = up * planeSize;
            createBackFace(start + forward * planeSize, offset1, offset2);
            vertzIndex += 4;
            trigsIndex += 6;
        }
        //BOTTOMFACE
        if (IsTransparent(x, y - 1, z))
        {
            offset1 = right * planeSize;
            offset2 = forward * planeSize;
            createBottomFace(start, offset1, offset2);
            vertzIndex += 4;
            trigsIndex += 6;
        }*/
    }
    private void createTopFace(Vector3 start, Vector3 offset1, Vector3 offset2)
    {
        positions[0 + vertzIndex] = start;
        positions[1 + vertzIndex] = start + offset1;
        positions[2 + vertzIndex] = start + offset2;
        positions[3 + vertzIndex] = start + offset1 + offset2;

        /*normals[0 + vertzIndex] = new Vector3(-1, 1, 0);
        normals[1 + vertzIndex] = new Vector3(-1, 1, 0);
        normals[2 + vertzIndex] = new Vector3(-1, 1, 0);
        normals[3 + vertzIndex] = new Vector3(-1, 1, 0);


        textureCoordinates[0 + vertzIndex] = new Vector2(1f, 1f);
        textureCoordinates[1 + vertzIndex] = new Vector2(1f, 1f);
        textureCoordinates[2 + vertzIndex] = new Vector2(1f, 1f);
        textureCoordinates[3 + vertzIndex] = new Vector2(1f, 1f);*/

        triangleIndices[0 + trigsIndex] = 0 + vertzIndex;
        triangleIndices[1 + trigsIndex] = 1 + vertzIndex;
        triangleIndices[2 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[3 + trigsIndex] = 3 + vertzIndex;
        triangleIndices[4 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[5 + trigsIndex] = 1 + vertzIndex;
    }



    private void createBottomFace(Vector3 start, Vector3 offset1, Vector3 offset2)
    {
        positions[0 + vertzIndex] = start;
        positions[1 + vertzIndex] = start + offset1;
        positions[2 + vertzIndex] = start + offset2;
        positions[3 + vertzIndex] = start + offset1 + offset2;

        /*normals[0 + vertzIndex] = new Vector3(0, 1, -1);
        normals[1 + vertzIndex] = new Vector3(0, 1, -1);
        normals[2 + vertzIndex] = new Vector3(0, 1, -1);
        normals[3 + vertzIndex] = new Vector3(0, 1, -1);

        textureCoordinates[0 + vertzIndex] = new Vector2(0f, 1f);
        textureCoordinates[1 + vertzIndex] = new Vector2(0f, 1f);
        textureCoordinates[2 + vertzIndex] = new Vector2(0f, 1f);
        textureCoordinates[3 + vertzIndex] = new Vector2(0f, 1f);*/

        triangleIndices[0 + trigsIndex] = 0 + vertzIndex;
        triangleIndices[1 + trigsIndex] = 1 + vertzIndex;
        triangleIndices[2 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[3 + trigsIndex] = 3 + vertzIndex;
        triangleIndices[4 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[5 + trigsIndex] = 1 + vertzIndex;
    }


    private void createFrontFace(Vector3 start, Vector3 offset1, Vector3 offset2)
    {
        positions[0 + vertzIndex] = start;
        positions[1 + vertzIndex] = start + offset1;
        positions[2 + vertzIndex] = start + offset2;
        positions[3 + vertzIndex] = start + offset1 + offset2;

        /*normals[0 + vertzIndex] = new Vector3(-1, 0, 0);
        normals[1 + vertzIndex] = new Vector3(-1, 0, 0);
        normals[2 + vertzIndex] = new Vector3(-1, 0, 0);
        normals[3 + vertzIndex] = new Vector3(-1, 0, 0);

        textureCoordinates[0 + vertzIndex] = new Vector2(1f, 0f);
        textureCoordinates[1 + vertzIndex] = new Vector2(1f, 1f);
        textureCoordinates[2 + vertzIndex] = new Vector2(1f, 0f);
        textureCoordinates[3 + vertzIndex] = new Vector2(0f, 1f);*/

        triangleIndices[0 + trigsIndex] = 0 + vertzIndex;
        triangleIndices[1 + trigsIndex] = 1 + vertzIndex;
        triangleIndices[2 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[3 + trigsIndex] = 3 + vertzIndex;
        triangleIndices[4 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[5 + trigsIndex] = 1 + vertzIndex;
    }
    private void createBackFace(Vector3 start, Vector3 offset1, Vector3 offset2)
    {
        positions[0 + vertzIndex] = start;
        positions[1 + vertzIndex] = start + offset1;
        positions[2 + vertzIndex] = start + offset2;
        positions[3 + vertzIndex] = start + offset1 + offset2;

        /*normals[0 + vertzIndex] = new Vector3(0, 0, -1);
        normals[1 + vertzIndex] = new Vector3(0, 0, -1);
        normals[2 + vertzIndex] = new Vector3(0, 0, -1);
        normals[3 + vertzIndex] = new Vector3(0, 0, -1);

        textureCoordinates[0 + vertzIndex] = new Vector2(1f, 1f);
        textureCoordinates[1 + vertzIndex] = new Vector2(1f, 0f);
        textureCoordinates[2 + vertzIndex] = new Vector2(1f, 1f);
        textureCoordinates[3 + vertzIndex] = new Vector2(0f, 1f);*/

        triangleIndices[0 + trigsIndex] = 0 + vertzIndex;
        triangleIndices[1 + trigsIndex] = 1 + vertzIndex;
        triangleIndices[2 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[3 + trigsIndex] = 3 + vertzIndex;
        triangleIndices[4 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[5 + trigsIndex] = 1 + vertzIndex;
    }

    private void createRightFace(Vector3 start, Vector3 offset1, Vector3 offset2)
    {
        positions[0 + vertzIndex] = start;
        positions[1 + vertzIndex] = start + offset1;
        positions[2 + vertzIndex] = start + offset2;
        positions[3 + vertzIndex] = start + offset1 + offset2;

        /* normals[0 + vertzIndex] = new Vector3(-1, 0, -1);
         normals[1 + vertzIndex] = new Vector3(-1, 0, -1);
         normals[2 + vertzIndex] = new Vector3(-1, 0, -1);
         normals[3 + vertzIndex] = new Vector3(-1, 0, -1);

         textureCoordinates[0 + vertzIndex] = new Vector2(1f, 0f);
         textureCoordinates[1 + vertzIndex] = new Vector2(1f, 0f);
         textureCoordinates[2 + vertzIndex] = new Vector2(1f, 0f);
         textureCoordinates[3 + vertzIndex] = new Vector2(0f, 1f);*/

        triangleIndices[0 + trigsIndex] = 0 + vertzIndex;
        triangleIndices[1 + trigsIndex] = 1 + vertzIndex;
        triangleIndices[2 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[3 + trigsIndex] = 3 + vertzIndex;
        triangleIndices[4 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[5 + trigsIndex] = 1 + vertzIndex;
    }

    private void createleftFace(Vector3 start, Vector3 offset1, Vector3 offset2)
    {
        positions[0 + vertzIndex] = start;
        positions[1 + vertzIndex] = start + offset1;
        positions[2 + vertzIndex] = start + offset2;
        positions[3 + vertzIndex] = start + offset1 + offset2;

        /*normals[0 + vertzIndex] = new Vector3(-1, 1, -1);
        normals[1 + vertzIndex] = new Vector3(-1, 1, -1);
        normals[2 + vertzIndex] = new Vector3(-1, 1, -1);
        normals[3 + vertzIndex] = new Vector3(-1, 1, -1);

        textureCoordinates[0 + vertzIndex] = new Vector2(0f, 0f);
        textureCoordinates[1 + vertzIndex] = new Vector2(0f, 0f);
        textureCoordinates[2 + vertzIndex] = new Vector2(0f, 0f);
        textureCoordinates[3 + vertzIndex] = new Vector2(0f, 0f);*/

        triangleIndices[0 + trigsIndex] = 0 + vertzIndex;
        triangleIndices[1 + trigsIndex] = 1 + vertzIndex;
        triangleIndices[2 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[3 + trigsIndex] = 3 + vertzIndex;
        triangleIndices[4 + trigsIndex] = 2 + vertzIndex;
        triangleIndices[5 + trigsIndex] = 1 + vertzIndex;
    }

    public bool IsTransparent(int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= height) || (z >= depth)) return true;
        {
            //return map[x, y, z] == 0;
            return map[x + width * (y + height * z)] == 0;
            //return map[x + width * (y + depth * z)] == 0;
        }
    }
    public byte GetByte(int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
        {
            return 0;
        }
        //return map[x, y, z];
        return map[x + width * (y + height * z)];
        //return map[x + width * (y + depth * z)];
    }
}





























/*using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleFlow : MonoBehaviour
{
    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;
    public float m_Drift = 0.01f;

    private void LateUpdate()
    {
        InitializeIfNeeded();

        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = m_System.GetParticles(m_Particles);

        // Change only the particles that are alive
        for (int i = 0; i < numParticlesAlive; i++)
        {
            m_Particles[i].velocity += Vector3.up * m_Drift;
        }

        // Apply the particle changes to the particle system
        m_System.SetParticles(m_Particles, numParticlesAlive);
    }

    void InitializeIfNeeded()
    {
        if (m_System == null)
            m_System = GetComponent<ParticleSystem>();

        if (m_Particles == null || m_Particles.Length < m_System.maxParticles)
            m_Particles = new ParticleSystem.Particle[m_System.maxParticles];
    }
}*/
