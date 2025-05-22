using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioCheckScene2 : MonoBehaviour
{
    public TMPro.TextMeshProUGUI messageText;
    private string message;
    public AudioSource audioSource;
    private bool isTriggered = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(StartRadio());
            audioSource.Play();
        }
    }

    IEnumerator StartRadio()
    {
        yield return new WaitForSeconds(1f);
        message = "N: What are you gonna do, plan to take care of him forever?";
        messageText.text = message;

        yield return new WaitForSeconds(4f);
        message = "D: This is the only way now, sis.";
        messageText.text = message;

        yield return new WaitForSeconds(4.5f);
        message = "N: What if we… unplug the life support?";
        messageText.text = message;

        yield return new WaitForSeconds(2.5f);
        message = "He can't do anything anymore";
        messageText.text = message;

        yield return new WaitForSeconds(5f);
        message = "D: Hey, don’t start this again. He’s our brother.";
        messageText.text = message;

        yield return new WaitForSeconds(4f);
        message = "N: So what are you gonna do? ";
        messageText.text = message;

        yield return new WaitForSeconds(2f);
        message = "He’s not waking up from the coma.";
        messageText.text = message;

        yield return new WaitForSeconds(3.5f);
        message = "D: How dare you say that, sis?";
        messageText.text = message;

        yield return new WaitForSeconds(2.5f);
        message = "N: Wake up already, seriously. You’ve still got your own life.";
        messageText.text = message;

        yield return new WaitForSeconds(6f);
        message = "D: Shut up, sis. He’s the one taking care of us";
        messageText.text = message;

        yield return new WaitForSeconds(4f);
        message = "You have no right to say that.";
        messageText.text = message;

        yield return new WaitForSeconds(3.5f);
        message = "*slap";
        messageText.text = message;

        yield return new WaitForSeconds(0.5f);
        message = "N: You’re crazy. Tomorrow, I’m going to sign to unplug his life support.";
        messageText.text = message;

        yield return new WaitForSeconds(4f);
        message = "D: No, no, I won’t let that happen.";
        messageText.text = message;

        yield return new WaitForSeconds(1.675f);
        message = "N: Stop it. I’m doing this for your own good.";
        messageText.text = message;

        yield return new WaitForSeconds(2f);
        message = "You should listen to your big sister.";
        messageText.text = message;

        yield return new WaitForSeconds(2.5f);
        message = "D: If you want to kill him, then kill me too.";
        messageText.text = message;

        yield return new WaitForSeconds(2f);
        message = "We already lost our parents. He is our only family left. You want to kill him now?";
        messageText.text = message;

        yield return new WaitForSeconds(4.25f);
        message = "N: Stop bringing emotions into this";
        messageText.text = message;

        yield return new WaitForSeconds(2f);
        message = "He can no longer support you, so why should you take care of him?";
        messageText.text = message;

        yield return new WaitForSeconds(3.5f);
        message = "D: Just stop this. If you ever dare to sign, we are no longer sisters anymore.";
        messageText.text = message;

        yield return new WaitForSeconds(4.4f);
        message = "N: Damn it. Then don’t say I never warned you. Do what you like.";
        messageText.text = message;

        yield return new WaitForSeconds(5.2f);
        message = " ";
        messageText.text = message;
    }
}

