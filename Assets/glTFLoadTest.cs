using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using GLTFast;
using System.Threading.Tasks;

/// <summary>
/// TestCode
/// </summary>
public class glTFLoadTest : MonoBehaviour
{
    [SerializeField]
    private Transform normalTransform;
    [SerializeField]
    private Transform yFlipTransform;

    void Start()
    {
        StartCoroutine(Load());    
    }

    private IEnumerator Load()
    {
        var url_normal = Application.streamingAssetsPath + "/TextureCoordinateTest.glb";
        var url_yflip = Application.streamingAssetsPath + "/TextureCoordinateTest_Yflip.glb";

        var noraml = UnityWebRequest.Get(url_normal);
        var yflip = UnityWebRequest.Get(url_yflip);

        yield return  noraml.SendWebRequest();

        if (noraml.isDone)
        {
            var normalData = noraml.downloadHandler.data;
            LoadNoamalKtxGltf(normalData);
        }

        yield return yflip.SendWebRequest();
        if (yflip.isDone)
        {
            var yflipData = yflip.downloadHandler.data;
            LoadYflipKtxGltf(yflipData);
        }
    }
    
    async void LoadNoamalKtxGltf(byte[] data)
    {
        GLTFast.GltfImport gltfImport = new();

        await gltfImport.LoadGltfBinary(data);
        await gltfImport.InstantiateMainSceneAsync(normalTransform);

    }

    /// <summary>
    /// yFlip
    /// </summary>
    /// <param name="data"></param>
    async void LoadYflipKtxGltf(byte[] data)
    {
        GLTFast.GltfImport gltfImport = new();

        await gltfImport.LoadGltfBinary(data);
        await gltfImport.InstantiateMainSceneAsync(yFlipTransform);
        
        

    }
}
