using UnityEngine;

public class GraphicsSetter : MonoBehaviour
{
    public  static GraphicsSetter GraphicsSettinger;


    private     void    Start(){
        GraphicsSettinger = this;
    }
}
