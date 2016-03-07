using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickMove : MonoBehaviour {

    Camera cam;
    Vector3 moveDest = Vector3.zero;
    Vector3 startBoxSelect = Vector3.zero;
    Vector3 endBoxSelect = Vector3.zero;
    float selectDepth = 3f;//distance from camera that select colliders are drawn
    public Image selectBox;
    public Vector2 mouseDownPoint;//starting point for box select

	// Use this for initialization
	void Awake () {
        cam = gameObject.GetComponent<Camera>();
        selectDepth = cam.transform.position.y - 1;
        selectBox = GameObject.FindGameObjectWithTag("Select Box").GetComponent<Image>();
        selectBox.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //hold down for selection box
        if(Input.GetButtonDown("Fire1"))
        {
            startBoxSelect = cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, selectDepth));
            selectBox.gameObject.SetActive(true);

            //selection box UI
            mouseDownPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        //update box position
        if (Input.GetButton("Fire1"))
        {
            Vector2 startPoint = mouseDownPoint;
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 difference = (mousePos - startPoint);
            if(difference.x < 0)
            {
                startPoint.x += difference.x;
                difference.x *= -1;
            }
            if (difference.y < 0)
            {
                startPoint.y += difference.y;
                difference.y *= -1;
            }
            selectBox.rectTransform.anchoredPosition = difference / 2 + startPoint;
            selectBox.rectTransform.sizeDelta = difference;
            
        }
        //release to do box cast and select units
        if(Input.GetButtonUp("Fire1"))
        {
            GameManager.instance.ClearSelected();
            endBoxSelect = cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, selectDepth));
            GameManager.instance.BoxSelect(startBoxSelect, endBoxSelect);
            selectBox.rectTransform.anchoredPosition = Vector2.zero;
            selectBox.rectTransform.sizeDelta = Vector2.zero;
        }

        if(Input.GetButton("Fire2"))
        {
            //if in the middle of selecting, cancel selection
            selectBox.rectTransform.anchoredPosition = Vector2.zero;
            selectBox.rectTransform.sizeDelta = Vector2.zero;

            moveDest = cam.ScreenToWorldPoint(Input.mousePosition);

            if(GameManager.instance.SelectCount > 0)//if followers are selected, move followers
            {
                GameManager.instance.FollowerMove(moveDest);
            }
            else//if no followers are selected, move camera
            {
                cam.GetComponent<SmoothCamera>().moveDest = moveDest;
            }
        }
            
	}
}
