using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
public class Gun : MonoBehaviour
{
    private float rotateOffset = 180f;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bullet;
    [SerializeField] float shotdelay = 0.15f;
    private float nextShot;
    [SerializeField] private int maxArmo = 24;
    public int currentArmo;
    [SerializeField] private TextMeshProUGUI armoText;
    [SerializeField] private AudioManager audioManager;
    void Start()
    {
        currentArmo = maxArmo;
        UpdateTextArmo();
    }

    // Update is called once per frame
    void Update()
    {
        rotateGun();
        Shot();
      //  reLoadAudio();
        reLoad();
    }
    void rotateGun()
    {
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0
            || Input.mousePosition.y > Screen.height)
        {
            return;
        }
        Vector3 displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + rotateOffset);
        if (angle < -90 || angle > 90)
        {
            transform.localScale = new Vector3(1, 1, 1);

        }
        else
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
    }
    void Shot()
    {
        if (Input.GetMouseButton(0) && currentArmo > 0 && Time.time > nextShot)
        {
            nextShot = Time.time + shotdelay;
            Instantiate(bullet, firePos.position, firePos.rotation);
            currentArmo--;
            UpdateTextArmo();
            audioManager.playShootClip();
        }
    }
    void reLoadAudio()
    {
        if (currentArmo == 0)
        {
            audioManager.playReloadClip();
        }
    }
    void reLoad()
    {
        if (currentArmo == 0)
        {
           // 
           
            
                
            StartCoroutine(ReloadAfterDelay(2f)); // Gọi Coroutine để đợi 2 giây         
            IEnumerator ReloadAfterDelay(float delay)
            {
                yield return new WaitForSeconds(delay); // Chờ delay giây        
                currentArmo = maxArmo;
                UpdateTextArmo();
            }

        }
        
        
    }

    
    private void UpdateTextArmo()
    {
        if(currentArmo > 0)
        {
            armoText.text = currentArmo.ToString();
        }
        else
        {
            armoText.text = "ĐANG NẠP ĐẠN";
        }
    }
}
