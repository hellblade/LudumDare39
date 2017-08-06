using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up = 0,
    Left,
    Right,
    Down
}

public class MapGenerator : MonoBehaviour
{
    public float width;
    public float height;

    public Sprite[] meteors;
    public SpriteRenderer meteorPrfab;
    public Vector2 smallMeteorSize;

    public SpriteRenderer background;
    public float backgroundScale = 2.5f;

    public Station[] stations;

    public Goal[] goalPrefabs;

    public Direction GoalDirection { get; private set; }

    public PlayerController player;

    public float playerStartAmount = 50;

    public float gridSize = 50;

    GameManager manager;

    public DistanceDisplay display;

    public BadZone badZonePrefab;
    public float badZoneWaitTime = 50.0f;

    public SpriteRenderer[] stationDefenders;
    public float defenderOffset = 6;

    public SpriteAnimationController animationController;
    public SpriteAnimation[] killedEffects;

    public HealthPickup healthPickup;
    public int hpDropInChance;

    public Pickup[] upgrades;
    public int upgradeAmount = 2;

    // Use this for initialization
    void Start ()
    {
        manager = FindObjectOfType<GameManager>();


#if UNITY_EDITOR
        if (!manager)
        {
            var obj = new GameObject();
            manager = obj.AddComponent<GameManager>();
            manager.Reset();
            manager.StartNextLevel();
            return;
        }

#endif

        GoalDirection = (Direction)Random.Range(0, 4);

        Debug.Log(GoalDirection);

        if (GoalDirection == Direction.Down || GoalDirection == Direction.Up)
        {
            width = 50 + manager.Level * 50;
            height = 150 + manager.Level * 50;
        }
        else
        {
            height = 50 + manager.Level * 50;
            width = 150 + manager.Level * 50;
        }

        PlacePlayer();
        PlaceUpdgrades();
        PlaceGoal();
        GenerateBadZone();
        CreateBackground();
        GenerateStations();
        GenerateMeteors();

        SetupDeathAnimations();

        display.enabled = true;
    }

    void PlaceUpdgrades()
    {
        int num = manager.Level * upgradeAmount;
        Vector3 pos = Vector3.zero;

        int maxTries = 100;

        while (num > 0 && maxTries-- > 0)
        {
            var upgrade = upgrades[Random.Range(0, upgrades.Length)];

            var sprite = upgrade.GetComponent<SpriteRenderer>().sprite;

            var x = Random.Range(-width, width);
            var y = Random.Range(-height, height);

            pos.x = x;
            pos.y = y;

            if (Vector3.Distance(pos, player.transform.position) < 50)
                continue;

            bool valid = true;
            var others = FindObjectsOfType<Pickup>();
            foreach (var o in others)
            {
                if (Vector3.Distance(pos, o.transform.position) < 50)
                {
                    valid = false;
                    break;
                }
            }

            if (!valid)
                continue;

            if (!Physics2D.BoxCast(new Vector2(x, y), new Vector2(sprite.bounds.size.x, sprite.bounds.size.y), 0.0f, Vector3.forward).collider)
            {
                Instantiate(upgrade, pos, Quaternion.identity);
                num--;
            }

        }
    }

    void SetupDeathAnimations()
    {
        var destructibles = FindObjectsOfType<Health>();

        foreach (var x in destructibles)
        {
            var effect = killedEffects[Random.Range(0, killedEffects.Length)];
            x.killed.AddListener(() =>
            {
                animationController.PlayAnimationAt(effect, x.transform);

                if (Random.Range(0, hpDropInChance) == 0)
                {
                    Instantiate(healthPickup, x.transform.position, x.transform.rotation);
                }
            });
        }
    }

    void GenerateBadZone()
    {
        Vector3 position = Vector3.zero;
        Vector2 moveDir = Vector2.zero;

        switch (GoalDirection)
        {
            case Direction.Up:
                position.y = -height * 2;
                moveDir.y = GameManager.Instance.Level / 2.0f;

                break;
            case Direction.Left:
                position.x = width * 2;
                moveDir.x = -GameManager.Instance.Level / 2.0f;

                break;
            case Direction.Right:
                position.x = -width * 2;
                moveDir.x = GameManager.Instance.Level / 2.0f;

                break;
            case Direction.Down:
                position.y = height * 2;
                moveDir.y = -GameManager.Instance.Level / 2.0f;

                break;
            default:
                break;
        }

        float waitTime = Mathf.Max(badZoneWaitTime - GameManager.Instance.Level * 2.0f, 0);
        // Calculate worst time to finish - which is the wait time plus the time it'd take to travel all the way to the end
        manager.WorstPossibleTime = waitTime + Mathf.Max(width, height) * 2 / Mathf.Max(moveDir.y, moveDir.x);

        moveDir = moveDir * Time.fixedDeltaTime;

        var zone = Instantiate(badZonePrefab, position, Quaternion.identity);
       
        zone.SetWaitTime(waitTime);
        zone.moveVector = moveDir;

        var size = zone.GetComponent<SpriteRenderer>().sprite.bounds.size;
        zone.transform.localScale = new Vector3(2 * width / size.x, 2 * height / size.y, 1);

       
    }

    void PlacePlayer()
    {
        Vector3 position = Vector3.zero;

        switch (GoalDirection)
        {
            case Direction.Up:
                position.y = -height + playerStartAmount;
                position.x = Random.Range(-width + playerStartAmount, width - playerStartAmount);

                break;
            case Direction.Left:
                position.x = width - playerStartAmount;
                position.y = Random.Range(-height + playerStartAmount, height - playerStartAmount);

                break;
            case Direction.Right:
                position.x = -width + playerStartAmount;
                position.y = Random.Range(-height + playerStartAmount, height - playerStartAmount);

                break;
            case Direction.Down:
                position.y = height - playerStartAmount;
                position.x = Random.Range(-width + playerStartAmount, width - playerStartAmount);

                break;
            default:
                break;
        }

        player.transform.position = position;
        FindObjectOfType<Follow>().transform.position = position;
    }

    void PlaceGoal()
    {
        Vector3 position = Vector3.zero;

        switch (GoalDirection)
        {
            case Direction.Down:
                position.y = -height + playerStartAmount;
                position.x = Random.Range(-width + playerStartAmount, width - playerStartAmount);

                break;
            case Direction.Right:
                position.x = width - playerStartAmount;
                position.y = Random.Range(-height + playerStartAmount, height - playerStartAmount);

                break;
            case Direction.Left:
                position.x = -width + playerStartAmount;
                position.y = Random.Range(-height + playerStartAmount, height - playerStartAmount);

                break;
            case Direction.Up:
                position.y = height - playerStartAmount;
                position.x = Random.Range(-width + playerStartAmount, width - playerStartAmount);

                break;
            default:
                break;
        }

        var goal = goalPrefabs[Random.Range(0, goalPrefabs.Length)];
        Instantiate(goal, position, Quaternion.identity, transform);
    }

    void GenerateStations()
    {
        int number = Mathf.CeilToInt(Mathf.Max(width, height) * 2 / gridSize);

        Vector3 position = Vector3.zero;

        if (GoalDirection == Direction.Down || GoalDirection == Direction.Up)
        {
            position.y = -height + gridSize / 2;
        }
        else
        {
            position.x = -width + gridSize / 2;
        }


        while (number > 0)
        {
            if (GoalDirection == Direction.Down || GoalDirection == Direction.Up)
            {
                position.x = Random.Range(-width, width);
            }
            else
            {
                position.y = Random.Range(-height, height);
            }
            var station = stations[Random.Range(0, stations.Length)];

            Instantiate(station, position, Quaternion.identity, transform);
            number--;

            int numDefenders = Random.Range(0, manager.Level + 1);

            // Later levels they might not all fit...
            int triesLeft = 20;

            while (numDefenders > 0 && triesLeft > 0)
            {
                var offset = Random.insideUnitCircle * defenderOffset;
                

                var defender = stationDefenders[Random.Range(0, stationDefenders.Length)];


                if (!Physics2D.BoxCast(new Vector2(position.x + offset.x, position.y + offset.y), new Vector2(defender.sprite.bounds.size.x, defender.sprite.bounds.size.y), 0.0f, Vector3.forward).collider)
                {
                    Instantiate(defender, new Vector3(position.x + offset.x, position.y + offset.y, 0), Quaternion.identity);
                    numDefenders--;
                }
                triesLeft--;
            }

            if (GoalDirection == Direction.Down || GoalDirection == Direction.Up)
            {
                position.y += gridSize;
            }
            else
            {
                position.x += gridSize;
            }



        }
    }

    void GenerateMeteors()
    {
        int numberOfMeteors = (int)Random.Range(width * height / 2, width * height / 1.5f);

        while (numberOfMeteors > 0)
        {
            var x = Random.Range(-width, width);
            var y = Random.Range(-height, height);
            var sprite = meteors[Random.Range(0, meteors.Length)];

            if (!Physics2D.BoxCast(new Vector2(x, y), new Vector2(sprite.bounds.size.x, sprite.bounds.size.y), 0.0f, Vector3.forward).collider)
            {
                CreateMeteor(x, y, sprite);
                numberOfMeteors--;
            }
        }
    }

    void CreateBackground()
    {
        // Could probably just generate one big quad with repeating texture which might be more performant...

        var bgSize = background.sprite.bounds.size;

        var sizeX = bgSize.x * backgroundScale;
        var sizeY = bgSize.y * backgroundScale;

        for (float x = -width - sizeX; (x + sizeX) < width + sizeX; x += sizeX)
        {
            for (float y = -height - sizeY; (y + sizeY) < height + sizeY; y += sizeY)
            {
                Instantiate(background, new Vector3(x, y, 0), Quaternion.identity, transform).transform.localScale = new Vector3(backgroundScale, backgroundScale, 1);
            }
        }
    }

    void CreateMeteor(float x, float y, Sprite sprite)
    {
        var meteor = (SpriteRenderer)Instantiate(meteorPrfab, new Vector3(x, y, 0), Quaternion.Euler(0, 0, Random.Range(0, 359.0f)), transform);
        meteor.sprite = sprite;

        var spriteSize = meteor.sprite.bounds.size;
        var collider = meteor.GetComponent<BoxCollider2D>();
        if (collider)
        {
            if (spriteSize.x < smallMeteorSize.x && spriteSize.y < smallMeteorSize.y)
            {
                collider.isTrigger = true;
            }

            collider.size = spriteSize;
        }

        var hp = meteor.GetComponent<Health>();
        if (hp)
        {
            hp.SetMaxHealth((int)(Mathf.Max(spriteSize.x, spriteSize.y) * 10));
        }
    }
}
