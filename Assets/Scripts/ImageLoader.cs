using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using UnityEditor;

public class ImageLoader : MonoBehaviour
{
    string path;
    //public RawImage image;
    public GameObject productPrefab;
    public GameObject player;
    public void OpenFileExplorer()
    {
        path = EditorUtility.OpenFilePanel("Overwrite with png", "", "png");
        GetImage();
    }


    void GetImage()
    {
        if (path != null)
        {
            UpdateImage();
        }
    }

    void UpdateImage()
    {
        WWW www = new WWW("file:///" + path);
        //image.texture = www.texture;
        productPrefab.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = www.texture;
        Instantiate(productPrefab, player.transform.position + (player.transform.forward * 2), player.transform.rotation);
        
    }

}