using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonSterHp : MonoBehaviour
{
    
        [SerializeField]
        private Slider Hp;

        private float maxHp = 100f;
        private float curHp = 100f;

        // Start is called before the first frame update
        void Start()
        {
            Hp.value = (float)curHp / (float)maxHp;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                curHp -= 10f;
            }
            HandleHp();
        }

        private void HandleHp()
        {
            Hp.value =  (float)curHp / (float)maxHp;
        }
    }

