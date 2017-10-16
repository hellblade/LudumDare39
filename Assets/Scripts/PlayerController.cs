using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Speed
{
    NoPower,
    Medium,
    Boost
}

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 4.0f;
    [SerializeField] float boostSpeed = 8.0f;
    [SerializeField] float slowSpeed = 2.0f;
    [SerializeField] float boostPowerDrain = 10;
    Rigidbody2D body;

    RangedWeapon weapon;
    Health health;
    Power power;
    float currentSpeed;

    Vector3 basisX;
    Vector3 basisY;

    [SerializeField] GameObject pauseMenu;    

    public Speed CurrentSpeed
    {
        get
        {
            if (speed == currentSpeed)
                return Speed.Medium;

            if (currentSpeed == boostSpeed)
                return Speed.Boost;

            return Speed.NoPower;
        }
    }

    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        weapon = GetComponent<RangedWeapon>();
        
        health = GetComponent<Health>();
        power = GetComponent<Power>();

        health.SetMaxHealth(GameManager.Instance.MaxHealth);
        power.SetMax(GameManager.Instance.MaxPower);

        health.killed.AddListener(GameManager.Instance.GameOver);

        if (!GameManager.Instance.UseRelativeControls)
        {
            basisX = Vector3.up;
            basisY = Vector3.right;
        }
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;

        if (power.CurrentPower <= 0)
        {
            currentSpeed = slowSpeed;
        }
        else if (CurrentSpeed == Speed.NoPower || ( CurrentSpeed == Speed.Boost && body.velocity.sqrMagnitude > 0 && !power.UsePower(boostPowerDrain * Time.deltaTime)))
        {
            currentSpeed = speed;
        }

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90.0f;
        body.rotation = angle;

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        if (GameManager.Instance.UseRelativeControls)
        {
            basisX = transform.up;
            basisY = transform.right;
        }

        var direction = moveVertical * basisX + moveHorizontal * basisY;
        direction.Normalize();
        body.velocity = direction * currentSpeed;

        //// Badly done wrap around...
        //if (transform.position.y >= map.height)
        //{
        //    body.MovePosition(transform.position - new Vector3(0, map.height * 2, 0));
        //}
        //else if (transform.position.y <= -map.height)
        //{
        //    body.MovePosition(transform.position + new Vector3(0, map.height * 2, 0));
        //}

        //if (transform.position.x >= map.width)
        //{
        //    body.MovePosition(transform.position - new Vector3(map.width * 2, 0, 0));
        //}
        //else if (transform.position.x <= -map.width)
        //{
        //    body.MovePosition(transform.position + new Vector3(map.width * 2, 0, 0));
        //}

        if (Input.GetButtonDown("Cancel"))
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            return;
        }

        if (Input.GetButtonDown("Fire3"))
        {
            if (CurrentSpeed == Speed.Boost)
            {
                currentSpeed = speed;
            }
            else if (power.CurrentPower > 0)
            {
                currentSpeed = boostSpeed;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            health.SetShield(!health.ShieldEnabled);
        }        

        if (Input.GetButton("Fire1"))
        {
            weapon.Fire();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void MainMenu()
    {
        GameManager.Instance.ToMainMenu();
    }

}
