using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextNeedsScript : MonoBehaviour
{
    private TextMeshProUGUI instruction;
    [SerializeField] private float readSpeed = 0.6f;
    private float extraReadFloat;
    [SerializeField] private string[] whatToSay;
    private int numberOfSayings;
    private int currentSaying = 0;
    [SerializeField] private bool useBGForText = true;
    [SerializeField] private GameObject bgImage;

    // Start is called before the first frame update
    void Start()
    {
        extraReadFloat = readSpeed;
        numberOfSayings = whatToSay.Length;
        instruction = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetButtonDown("Interact") && currentSaying < numberOfSayings)
        {
            instruction.text = whatToSay[currentSaying];
            currentSaying += 1;
        }
        */
        if (instruction.text.Length > 0)
        {
            if (useBGForText)
            {
                bgImage.SetActive(true);
            }
            extraReadFloat -= Time.deltaTime;
        }
        if (extraReadFloat <= 0f)
        {
            if (useBGForText)
            {
                bgImage.SetActive(false);
            }
            instruction.text = whatToSay[currentSaying];
            currentSaying += 1;
            extraReadFloat = readSpeed;
        }
    }

    public void ChooseAText(int theNumber)
    {
        instruction.text = whatToSay[theNumber];
        currentSaying = theNumber + 1;
    }
}
