using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! this script stores player functions
public class PlayerScript : MonoBehaviour
{
    [Header("BASICs")]
    public int atk;
    [Header("SLASHes")]
    public GameObject endOfPath;
    public Vector3 slashTarget;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime;
    public bool slashing;
    public bool slashIntiated;
    public GameObject slashPath;
    public int slashesPerformed;
    [Header("HIT STUNs")]
    public float hitStun_duration;
    private float hitStun_timer;
    public bool hitStunned;
    [Header("VISUALs")] 
    public GameObject imagePlayer;
    public GameObject shadowPlayer;
    public Vector3 rotSpd;
    public Vector3 rotSpd_current;
    private Vector3 rotVelocity;
    public float warmUp_smoothTime;
    [Header("HITs")]
    public float hitStop_duration;
    private GameObject enemyHit;
    [Header("HURTs")]
    public Collider2D hitBox;
    public float flashDuration;
    private Material ogMat;
    public Material hurtMat;
    public float hurt_bulletTime_scale;
    public float hurt_bulletTime_duration;
    public GameObject ps_blood;
    [Header("TESTs")]
    public bool hitBulletTime;
    public bool hitStop;
    public GameObject debugCirc;
    #region SINGLETON
    public static PlayerScript me;
    private void Awake()
    {
        me = this;
    }
    #endregion
    private void Start()
    {
        ogMat = imagePlayer.GetComponent<SpriteRenderer>().material;
        hitStunned = false;
    }
    private void Update()
    {
        if (!slashIntiated) // reset slash target to player pos when not starting to slash
        {
            slashTarget = transform.position;
        }
        SlashDash();
        slashing = Vector3.Distance(transform.position, slashTarget) > 0.1f; // record if in the middle of slashing
        if (slashing)
        {
            SlashRotate(); // if slashing, rotate
        }
        else // if reached slash target pos
        {
            slashIntiated = false;
            if (InteractionScript.me.dragging)
            {
                WarmUp(); // if not slashing but dragging, warm up
            }
            else // if not slashing nor dragging, reset everything
            {
                rotSpd_current = new(0, 0, 0);
                slashPath.GetComponent<SlashPathHolderScript>().baseScaleY = slashPath.GetComponent<SlashPathHolderScript>().ogBaseScaleY;
                ReturnRotate();
            }
        }
        // hit stun cd
        if (hitStun_timer > 0)
        {
            hitStun_timer -= Time.deltaTime;
            hitStunned = true;
        }
        else
        {
            hitStunned = false;
        }
    }
    private void SlashDash()
    {
        if (ReflectionSlashScript.me.targetPoses.Count > 0
            && !slashing
            && slashPath.GetComponent<SlashPathHolderScript>().myImage.GetComponent<SlashPathCollisionScript>().valid)
        {
            slashIntiated = true;
            slashTarget = ReflectionSlashScript.me.targetPoses[0];
            ReflectionSlashScript.me.targetPoses.RemoveAt(0);
            slashing = true;
        }
        transform.position = Vector3.SmoothDamp(transform.position, slashTarget, ref velocity, smoothTime); // actual slash dash
    }
    private void SlashRotate() // rotate the player image when slashing
    {
        imagePlayer.transform.Rotate(rotSpd * Time.deltaTime);
        shadowPlayer.transform.Rotate(rotSpd * Time.deltaTime);
    }
    private void ReturnRotate() // when not slashing rotate player image back
    {
        Quaternion currentRot = imagePlayer.transform.rotation;
        Quaternion targetRot = Quaternion.Euler(0f, 0f, 45f);
        float angleDiff = Quaternion.Angle(currentRot, targetRot);
        if (Mathf.Abs(angleDiff) < 179.5f)
        {
            Quaternion target = Quaternion.Euler(0, 0, 45);
            imagePlayer.transform.rotation = Quaternion.RotateTowards(imagePlayer.transform.rotation, target, Time.deltaTime * -500f);
            shadowPlayer.transform.rotation = Quaternion.RotateTowards(shadowPlayer.transform.rotation, target, Time.deltaTime * -500f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) // hit things
    {
        if (slashing)
        {
            if ((collision.CompareTag("Bullet") ||
                collision.CompareTag("Enemy")) &&
                hitBox.IsTouching(collision))
            {
                collision.GetComponent<EnemyScript>().GetHit(atk);
                enemyHit = collision.gameObject; 
                if (hitStop)
                {
                    StartCoroutine(HitStop());
                }
            }
        }
        else // not slashing
        {
            if (hitBox.IsTouching(collision) // check if the actual hit box got hit and not other colliders
                && collision.CompareTag("Enemy")  // got hit by an enemy
                && collision.GetComponent<EnemyScript>().myEnemyType != EnemyScript.EnemyType.score) 
            {
                GotHit();
            }
            else if (collision.CompareTag("Bullet") &&
                hitBox.IsTouching(collision))
            {
                GotHit();
                Destroy(collision.gameObject);
            }
        }
    }
    private void GotHit() // get hurt
    {
        GameManager.me.playerHp--;
        KnockBackAreaScript.me.KnockBackEnemies();
        StartCoroutine(HitFlash());
        CameraScript.me.CamShake_PlayerHit();
        StartCoroutine(Hurt_BulletTime());
        GameObject psBlood = Instantiate(ps_blood);
        psBlood.transform.position = transform.position;
        hitStun_timer = hitStun_duration;
        if (GameManager.me.playerHp <= 0)
        {
            transform.position = new Vector3(CamHolderScript.me.transform.position.x, CamHolderScript.me.transform.position.y, 0);
        }
    }
    #region HIT VFXs
    IEnumerator HitFlash()
    {
        imagePlayer.GetComponent<SpriteRenderer>().material = hurtMat;
        yield return new WaitForSecondsRealtime(flashDuration);
        imagePlayer.GetComponent<SpriteRenderer>().material = ogMat;
    }
    IEnumerator Hurt_BulletTime()
    {
        Time.timeScale = hurt_bulletTime_scale;
        yield return new WaitForSecondsRealtime(hurt_bulletTime_duration);
        Time.timeScale = 1;
    }
    #endregion
    #region ATK VFXs
    public void WarmUp()
    {
        if (rotSpd_current.magnitude < rotSpd.magnitude)
        {
            rotSpd_current = Vector3.SmoothDamp(rotSpd_current, rotSpd, ref rotVelocity, warmUp_smoothTime);
        }
        imagePlayer.transform.Rotate(rotSpd_current * Time.deltaTime);
        shadowPlayer.transform.Rotate(rotSpd_current * Time.deltaTime);
    }
    IEnumerator HitStop()
    {
        Vector3 ogTargetPos = slashTarget;
        slashTarget = enemyHit.transform.position;
        rotSpd = new(0, 0, 0);
        yield return new WaitForSecondsRealtime(hitStop_duration);
        rotSpd = new(0, 0, 5);
        slashTarget = ogTargetPos;
    }
    #endregion
}
