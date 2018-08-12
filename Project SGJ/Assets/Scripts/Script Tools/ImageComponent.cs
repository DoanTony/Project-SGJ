using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageComponent : MonoBehaviour {

    public Image image;
	
    public void DisableImage(float alpha)
    {
        Color _color = image.color;
        _color.a = alpha;
        image.color = _color;
    }
}
