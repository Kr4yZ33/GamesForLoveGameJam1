using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    public static BallsManager Instance;
    public Ball ballPrefab;
    public int maxBalls;

    private List<Ball> _balls = new List<Ball>();
    public List<Ball> BallsInPlay { get { return _balls; } }

    private Ball _initialBall;
    private Transform _initialTransform;
    private bool levelStarted = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnLevelChange += OnLevelChange;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLevelChange -= OnLevelChange;
    }

    public Ball SpawnBall(Vector2 position, bool move = true)
    {
        Ball ball = Instantiate(ballPrefab, position, Quaternion.identity);

        if (move)
        {
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(3f, 3f);
        }

        ball.GetComponent<TrailRenderer>().enabled = move;
        _balls.Add(ball);

        return ball;
    }

    public void DuplicateBalls()
    {
        if(_balls.Count >= maxBalls)
        {
            return;
        }

        List<Ball> duplicates = new List<Ball>();

        foreach (Ball ball in _balls)
        {
            Vector2 currentVelocity = ball.Velocity();

            Ball b1 = Instantiate(ballPrefab, ball.transform.localPosition, Quaternion.identity);
            b1.GetComponent<Rigidbody2D>().velocity = new Vector2(currentVelocity.x - 1.5f, currentVelocity.y);
            duplicates.Add(b1);

            if (_balls.Count + duplicates.Count >= maxBalls)
            {
                break;
            }

            Ball b2 = Instantiate(ballPrefab, ball.transform.localPosition, Quaternion.identity);
            b2.GetComponent<Rigidbody2D>().velocity = new Vector2(currentVelocity.x + 1.5f, currentVelocity.y);

            duplicates.Add(b2);

            if (_balls.Count + duplicates.Count >= maxBalls)
            {
                break;
            }
        }

        if (duplicates.Count > 0)
        {
            _balls.AddRange(duplicates);
        }
    }

    //Clear balls in play when game/level is over
    public void ClearBalls()
    {
        foreach (Ball ball in BallsInPlay)
        {
            Destroy(ball.gameObject);
        }
        BallsInPlay.Clear();
    }
    
    private void OnLevelChange(int level)
    {
        ClearBalls();

        Paddle paddle = FindObjectOfType<Paddle>();
        _initialTransform = paddle.ballSpawnPoint;
        _initialBall = SpawnBall(_initialTransform.position, false);
        levelStarted = false;
    }

    private void Update()
    {
        if(levelStarted)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            _initialBall.GetComponent<Rigidbody2D>().velocity = new Vector2(3f, 3f);
            _initialBall.GetComponent<TrailRenderer>().enabled = true;
            GameManager.Instance.Begin();
            levelStarted = true;
        }

        _initialBall.transform.localPosition = _initialTransform.position;
    }
}
