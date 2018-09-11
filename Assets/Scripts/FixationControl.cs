// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FixationControl : MonoBehaviour {

// 	public CanvasGroup fixationCanvas;
//     public static bool isFixationSet = true;
    
	
// 	// Update is called once per frame
// 	void Update () {
// 		if(timeController.isContinue)
// 		{
//             if (timeController.isTimeSet)
// 			{
// 				if(timeController.isFixation)
// 				{
// 					if(isFixationSet)
// 					{
// 						timeController.timeSet = timeController.timeSet + 2*timeController.timeFixation;
// 						isFixationSet = false;
// 					}

// 					if(Time.time - timeController.timeStart < timeController.timeFixation | Time.time - timeController.timeStart > timeController.timeSet - timeController.timeFixation)
// 					{
// 						fixationCanvas.alpha = 1;
// 					}                    
// 					else
// 					{
// 						fixationCanvas.alpha = 0;
// 					}   
// 				}
// 			}
// 		}
// 		else
// 		{
// 			fixationCanvas.alpha = 0;
// 			isFixationSet = true;
// 		}
// 	}
// }
