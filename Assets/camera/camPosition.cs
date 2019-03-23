using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camPosition : MonoBehaviour
{
    [SerializeField]
    List<GameObject> elemsToFollow = new List<GameObject>();

    const float maxRange = 10;

    [SerializeField]
    [Range(0, maxRange)]
    float smoothness = maxRange;

    float camX = 0, camY = 0;

    float camVerticalSize;
    float camHorizontalSize;

    Camera camera;

    [SerializeField]
    float camStartVerticalSize = 5;
    float camStartHorizontalSize;

    [SerializeField]
    float stretchMargin = 1;
    

    // Start is called before the first frame update
    void Start()
    {
   
        camera = GetComponent<Camera>();
        camera.orthographicSize = camStartVerticalSize;
        camStartHorizontalSize = camStartVerticalSize * camera.aspect;
        camVerticalSize =  camera.orthographicSize;
        camHorizontalSize = camVerticalSize * camera.aspect;

        setCamPosition();
        setCamSize();
    }

    // Update is called once per frame
    [ExecuteInEditMode]
    void Update()
    {
        setCamPosition();
        setCamSize();
    }

    private void setCamPosition()
    {
        if (elemsToFollow.Count <= 0) return;

        camX = 0;
        camY = 0;

        foreach(GameObject elem in elemsToFollow)
        {
            if (elem == null) continue;
            camX += elem.transform.position.x;
            camY += elem.transform.position.y;
        }

        camX /= elemsToFollow.Count;
        camY /= elemsToFollow.Count;

        float smooth = (maxRange - (smoothness - 0.01f)) / maxRange;
        

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, camX, smooth), Mathf.Lerp(transform.position.y, camY, smooth), -10);
    }

    private void setCamSize()
    {
        

        float maxHorizontalDist = 0;
        float maxVerticalDist = 0;



        

        foreach (GameObject elem in elemsToFollow)
        {
            float xDist = Mathf.Abs(elem.transform.position.x - transform.position.x);
            float yDist = Mathf.Abs(elem.transform.position.y - transform.position.y);

            if (xDist > maxHorizontalDist) maxHorizontalDist = xDist;
            if (yDist > maxVerticalDist) maxVerticalDist = yDist;
        }

        if(maxVerticalDist > camStartVerticalSize - stretchMargin || maxHorizontalDist > camStartHorizontalSize - stretchMargin)
        {
            Debug.Log("some objects are out of view");

            //create script fot camera stretch, remember to save default camera size!
            //add margin to avoid stretching camera after object goes out of view

            if(maxVerticalDist > camVerticalSize - stretchMargin)
            {

                camera.orthographicSize = maxVerticalDist + stretchMargin;

            }else if(maxHorizontalDist > camHorizontalSize - stretchMargin)
            {
                camera.orthographicSize = (maxHorizontalDist * (1 / camera.aspect)) + stretchMargin;
            }
            else
            {
                camera.orthographicSize = camStartVerticalSize;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawSphere(new Vector3(camX, camY, 0), 0.3f);
        foreach(GameObject elem in elemsToFollow)
        {
            if (elem == null) continue;
            Gizmos.DrawLine(elem.transform.position, new Vector3(camX, camY));
        }

        Gizmos.color = new Color(0, 1, 0);
        Gizmos.DrawWireCube(transform.position, new Vector3((camHorizontalSize * 2) - stretchMargin * 2, (camVerticalSize * 2) - stretchMargin * 2, 0));
    }
}
