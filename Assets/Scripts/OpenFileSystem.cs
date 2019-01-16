using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;

[RequireComponent( typeof( Button ) )]
public class OpenFileSystem : MonoBehaviour, IPointerDownHandler
{
    public GameObject productPrefab;
    public GameObject player;

#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public void OnPointerDown(PointerEventData eventData) 
    {
        UploadFile(gameObject.name, "OnFileUpload", ".png", false);
    }

    // Called from browser
    public void OnFileUpload(string url) {
        StartCoroutine(OutputRoutine(url));
    }
#else
    //
    // Standalone platforms & editor
    //
    public void OnPointerDown( PointerEventData eventData )
    {
    }

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener( OnClick );
    }

    private void OnClick()
    {

        var extensions = new[]
       {
            new ExtensionFilter("Image Files", "png" ),

        };
        var paths = StandaloneFileBrowser.OpenFilePanel( "Title" , "" , extensions , true );
        if( paths.Length > 0 )
        {
            StartCoroutine( OutputRoutine( new System.Uri( paths[0] ).AbsoluteUri ) );
        }
    }
#endif

    private IEnumerator OutputRoutine( string url )
    {
        var loader = new WWW( url );
        yield return loader;

        GameObject newProduct = Instantiate( productPrefab , player.transform.position + ( player.transform.forward * 1 ) , player.transform.rotation );

        newProduct.GetComponent<Renderer>().material.mainTexture = loader.texture;
    }
}
