using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTextureRandomizer : MonoBehaviour
{
    static Dictionary<int,int>weightedIndex;//oof singletons :( #GameJamProblems
    static bool initialized = false;
    static int size;

    public SkinnedMeshRenderer rend;
    [System.Serializable]
   public struct WeightedTexture
    {
        [SerializeField]
        public Texture2D tex;
        [SerializeField]
        public int chance;
    }; 
    public List<WeightedTexture> options;
    // Start is called before the first frame update
    void Start()
    {
        if(!initialized)
        {
            initDistribution();
        }

        SetRandomTexture();
    }


    void SetRandomTexture()
    {
        int sampleIndex = UnityEngine.Random.Range(0, size);
        int index = weightedIndex[sampleIndex];

        rend.material.SetTexture("_BaseMap", options[index].tex);
    }

    void initDistribution()
    {
        weightedIndex = new Dictionary<int, int>();
        size = 0;
        initialized = true;
        int elementNum = 0;
        foreach(WeightedTexture current in options)
        {
            for(int i=0;i<current.chance;i++)
            {
                weightedIndex.Add(size, elementNum);
                size++;                
            }
            elementNum++;
        }
    }

}
