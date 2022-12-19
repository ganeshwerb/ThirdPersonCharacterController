using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    [SerializeField] private PlayerInputRegister inputRegister;
    [SerializeField] private GameObject crosshairs;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera aimCam;
    private bool boosted = false;

    private void Update()
    {
        if (aimCam != null)
        {
            if (inputRegister.SeeIsAiming())
            {
                if (!boosted)
                {
                    aimCam.Priority += 10;
                    boosted = true;
                }
            }
            else if (boosted)
            {
                aimCam.Priority -= 10;
                boosted = false;
            }
        }
        if (crosshairs != null)
        {
            crosshairs.SetActive(boosted);
        }
    }
}