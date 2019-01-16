using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.IO;
using UnityEditor;
using SFB;
using UnityEngine.EventSystems;
[RequireComponent( typeof( Button ) )]
public class ImageLoader : MonoBehaviour
{
    string path;
    public GameObject productPrefab;
    public GameObject player;

    public void OpenFileExplorer()
    {
#if UNITY_EDITOR
        path = EditorUtility.OpenFilePanel( "Overwrite with png" , "" , "png" );
        GetImage();

#endif
         var extensions = new[] 
        {
            new ExtensionFilter("Image Files", "png" ),
           
        };
        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", extensions, true);
        if (paths.Length > 0) 
        {
            StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
        }

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
        GameObject newProduct= Instantiate( productPrefab , player.transform.position + ( player.transform.forward * 2 ) , player.transform.rotation );
        newProduct.GetComponent<Renderer>().material.mainTexture = www.texture;
    }

    private IEnumerator OutputRoutine( string url )
    {
        var loader = new WWW( url );
        yield return loader;
        GameObject newProduct = Instantiate( productPrefab , player.transform.position + ( player.transform.forward * 2 ) , player.transform.rotation );

        newProduct.GetComponent<Renderer>().material.mainTexture = loader.texture;
    }
   
}