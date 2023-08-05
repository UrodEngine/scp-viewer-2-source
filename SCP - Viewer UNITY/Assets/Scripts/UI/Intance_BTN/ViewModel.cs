using UnityEngine;
using UnityEditor;

public class ViewModel : MonoBehaviour
{
    #region Alterable values
    /*========================================================================================================================*/
    public  static  ViewModel       ViewModelStatic;
    public          GameObject      bro;
    public          MeshFilter      MFilter;
    public          MeshRenderer    MRenderer;
    /*========================================================================================================================*/
    #endregion
    private void Awake(){
        MFilter         = GetComponent<MeshFilter>();
        MRenderer       = GetComponent<MeshRenderer>();
        ViewModelStatic = this;
    }
    private void CheckBroObject(){
        if (SPAWN_lists.instance.SelectedInstance != null){
            bro = SPAWN_lists.instance.SelectedInstance;
            SearchMesh();
        }
    }
    private void SearchMesh(){
        if (bro.GetComponent<MeshFilter>()) MFilter.sharedMesh = bro.GetComponent<MeshFilter>().sharedMesh;
        else if (bro.transform.GetChild(0).GetComponent<MeshFilter>()) MFilter.sharedMesh = bro.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh;
        else if (bro.transform.GetChild(0).GetChild(0).GetComponent<MeshFilter>()) MFilter.sharedMesh = bro.transform.GetChild(0).GetChild(0).GetComponent<MeshFilter>().sharedMesh;

        else if (bro.GetComponent<SkinnedMeshRenderer>()) MFilter.sharedMesh = bro.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        else if (bro.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>()) MFilter.sharedMesh = bro.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMesh;
        else if (bro.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>()) MFilter.sharedMesh = bro.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMesh;


    }
    public  void ERRORModel(in GameObject errormodel){
        if (errormodel.GetComponent<MeshFilter>()) MFilter.sharedMesh = errormodel.GetComponent<MeshFilter>().sharedMesh;
    }
    private void Update(){
        transform.Rotate(0.2f, 0.2f, 0.2f);
        if (SPAWN_lists.instance.SelectedInstance == null) 
            ERRORModel(SPAWN_lists.instance.ErrorModel);
    }
}
