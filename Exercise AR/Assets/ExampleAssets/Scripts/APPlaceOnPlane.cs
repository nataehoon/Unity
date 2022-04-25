using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class APPlaceOnPlane : MonoBehaviour
{
    public ARRaycastManager arRaycaster;
    public GameObject placeObject;
    
    GameObject spwanObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        placeObjectByTouch();
        // UpdateCenterObject();
    }

    private void placeObjectByTouch()
    {
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);

            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if(arRaycaster.Raycast(touch.position, hits, TrackableType.Planes)){
                Pose hitPose = hits[0].pose;

                if(!spwanObject){
                    spwanObject = Instantiate(placeObject, hitPose.position, hitPose.rotation);
                }
                else{
                    spwanObject.transform.position = hitPose.position;
                    spwanObject.transform.rotation = hitPose.rotation;
                }
                
            }

        } 
    }

    private void UpdateCenterObject()
    {
        Vector3 screnCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRaycaster.Raycast(screnCenter, hits, TrackableType.Planes);

        if(hits.Count > 0)
        {
            Pose placementPose = hits[0].pose;
            placeObject.SetActive(true);
            placeObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);

        }
        else
        {
            placeObject.SetActive(false);
        }
        

    }
}
