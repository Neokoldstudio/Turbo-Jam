using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public static AnimationController Instance { get; private set; }

    public CanvasAnimation canvasAnim;
    public GameObject menu;
    public Animator armadillo;
    public Animator boss;
    public Animator bossHands;
    public GameObject sand;

    public Animator anim;

    public PlayerMovement player;

    private void OnValidate()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

        player.gameObject.SetActive(false);
        player.inEvent = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AnimationClip clipinfo = anim.GetCurrentAnimatorClipInfo(0)[0].clip;
            if (clipinfo.name == "DropPenguin")
                menu.SetActive(false);
            anim.Play(clipinfo.name,0, .98f);
        }
    }

    public void EnableParry()
    {
        player.needParry = true;
    }

    public void KillArmadillo()
    {
        armadillo.SetTrigger("Die");
        anim.SetTrigger("SwordDrop");
    }

    public void ThrowKnife()
    {
        bossHands.SetTrigger("ThrowKnife");
    }
    public void ParryKnife()
    {
        TimeManager.Instance.SlowTimeSmooth(0.1f, 0.1f, 0.1f);
        boss.SetTrigger("Parry");
        bossHands.SetTrigger("Parry");
        anim.SetTrigger("Parry");
    }

    public void SlowDownTime()
    {
        TimeManager.Instance.SlowTimeSmooth(0.3f, Mathf.Infinity, 0.3f);
    }

    public void PickSword()
    {
        player.weaponManager.gameObject.SetActive(true);
        player.hands.SetActive(false);
        sand.SetActive(false);
        player.cantMove = true;
        player.inEvent = false;
    }

    public void ArmadilloIdle()
    {
        armadillo.SetBool("Running", false);
    }
    public void ArmadilloRun()
    {
        armadillo.SetBool("Running", true);
    }
    public void BossIdle()
    {
        boss.SetBool("Running", false);
    }
    public void BossRun()
    {
        boss.SetBool("Running", true);
    }

    public void ShowMenu()
    {
        canvasAnim.ShowTitle();
        menu.SetActive(true);
    }

    public void DisableTitle()
    {
        Debug.Log("test");
        canvasAnim.anim.SetTrigger("DisableTitle");
    }

    public void HideTitle()
    {
        canvasAnim.HideTitle();
    }

    public void DropPenguin()
    {
        anim.SetTrigger("IntroAnimation");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void FinishIntroAnimation()
    {
        player.inEvent = false;
    }
}
