using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Rito.RadialMenu_v3.Test
{
    public class Test_RadialMenu : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public RadialMenu radialMenu;

        [Space]
        public Image[] drawButtonToolImages;

        public Sprite[] icons;
        public Sprite defaultIndicator;
        public Image indicatorIcon, cancelIndicatorIcon;

        private void Start()
        {
            radialMenu.SetPieceImageSprites(drawButtonToolImages);
            GlobalVariables.FinishDrawing += HandleMyEvent;
        }

        private void HandleMyEvent()
        {
            if (indicatorIcon != null)
            {
                indicatorIcon.sprite = defaultIndicator;
            }
            
        }

        private void Update()
        {
            // Check if the space bar is pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                radialMenu.Show();
                cancelIndicatorIcon.gameObject.SetActive(true);
            }

            // Check if the space bar is released
            if (Input.GetKeyUp(KeyCode.Space))
            {
                ProcessSelection();
                cancelIndicatorIcon.gameObject.SetActive(false);
            }
          
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ProcessSelection();
            cancelIndicatorIcon.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            radialMenu.Show();
            cancelIndicatorIcon.gameObject.SetActive(true);
        }



        public void ProcessSelection()
        {
            int selected = radialMenu.Hide();
               
            if (selected != -1)
            {
                if(selected != 0)
                {
                    GlobalVariables.TriggerFinishDrawing(); // trigger to disable the draw panel when any other tool is selected
                }
                if (radialMenu._pieceInstance[selected].gameObject.GetComponent<Toggle>())
                {
                    if (radialMenu._pieceInstance[selected].transform.Find("LockImage").gameObject.activeInHierarchy == false)
                    {
                        radialMenu._pieceInstance[selected].gameObject.GetComponent<Toggle>().isOn = true;

                        indicatorIcon.sprite = icons[selected];
                    }
                    else
                    {
                        indicatorIcon.sprite = defaultIndicator;
                    }


                }
            }
            else
            {
                foreach (RectTransform toggle in radialMenu._pieceInstance)
                {
                    toggle.transform.GetComponent<Toggle>().isOn = false;

                }
                indicatorIcon.sprite = defaultIndicator;
            }

            Debug.Log($"Selected : " + selected);
        }

    }
}