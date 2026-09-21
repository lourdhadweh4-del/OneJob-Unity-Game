using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class Player : Entity
{
    public PlayerInputSet InputSet { get; private set; }

    public Vector2 MoveInput { get; private set; }
	[SerializeField] private GameObject pizzaPrefab;
	[SerializeField] private Transform pizzaSpawnPoint;
	[SerializeField] private float pizzaSpeed = 12f;

    [field: Header("Movement details")]
    [field: SerializeField]
    public float MoveSpeed { get; private set; } = 8f;

    [field: SerializeField]
    public float JumpForce { get; private set; } = 12f;

    [field: SerializeField, Range(0, 1)]
    public float InAirMultiplier { get; private set; } = 0.65f;


    [Header("Timer details")]
    private float timer;
    private float waitDuration = 3f;
    public TMP_Text timerText;
    public float amountOfTime = 51f;
    public bool ShouldTimerRun { get; private set; }



    [field: Header("UI details")]
    [field:SerializeField] public GameObject EndingGameScreen { get; private set;  }
    public FadingScreen fadingScreen;

    public bool IsGameOver { get; private set; }

    #region States



    public PlayerIdleState IdleState { get; private set; }
    public PlayerThrowState ThrowState { get; private set; }
	public PlayerMoveState MoveState { get; private set; }
	public PlayerJumpState JumpState { get; private set; }
	public PlayerFallState FallState { get; private set; }


    #endregion


    #region Animation Hashes

    private static readonly int _idleHash =
        Animator.StringToHash("Idle");

    private static readonly int _moveHash =
        Animator.StringToHash("Move");

    private static readonly int _jumpFallHash =
        Animator.StringToHash("JumpFall");
    
    private static readonly int _throw =
        Animator.StringToHash("Throw");



    #endregion


    protected override void Awake()
    {
        base.Awake();

        InputSet = new PlayerInputSet();
        IsGameOver = false;

        #region State Initialization

        IdleState = new PlayerIdleState(
            StateMachine,
            _idleHash,
            this
        );

        MoveState = new PlayerMoveState(
            StateMachine,
            _moveHash,
            this
        );

        JumpState = new PlayerJumpState(
            StateMachine,
            _jumpFallHash,
            this
        );

        FallState = new PlayerFallState(
            StateMachine,
            _jumpFallHash,
            this
        );
	
        ThrowState = new PlayerThrowState(
            StateMachine,
            _throw,
            this);

        #endregion
    }



    #region Player Input Methods

    private void OnEnable()
    {
        InputSet.Enable();

        InputSet.Player.Movement.performed += OnMovement;
        InputSet.Player.Movement.canceled += OnMovementCanceled;

		//INPUTS
		
        // Jump 
        InputSet.Player.Jump.performed += OnJump;
		
		// Throw
		InputSet.Player.Throw.performed += OnThrow;

    }


    private void OnDisable()
    {
        InputSet.Player.Movement.performed -= OnMovement;
        InputSet.Player.Movement.canceled -= OnMovementCanceled;

       	//INPUTS
		
        // Jump 
        InputSet.Player.Jump.performed -= OnJump;
		// Throw
		InputSet.Player.Throw.performed -= OnThrow;
		
        InputSet.Disable();
		

    }


    private void OnMovement(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }


    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        MoveInput = Vector2.zero;
    }


    private void OnJump(InputAction.CallbackContext context)
    {
        if (GroundDetected)
        {
            StateMachine.ChangeState(JumpState);
        }
    }

	private void OnThrow(InputAction.CallbackContext context)
	{
		StateMachine.ChangeState(ThrowState);
	}

    #endregion


    protected override void Start()
    {
        base.Start();

        //StartCoroutine(StartTimer());
        
        StateMachine.Initialize(IdleState);

        timer = amountOfTime;
        StartCoroutine(StartTimer(waitDuration));
    }

    protected override void Update()
    {
        base.Update();

        if (ShouldTimerRun && !IsGameOver)
            HandleTimer();

    }



    public void ThrowPizza()
	{
		if (pizzaPrefab == null || pizzaSpawnPoint == null)
			return;

		GameObject pizzaObject = Instantiate(
			pizzaPrefab,
			pizzaSpawnPoint.position,
			Quaternion.identity
		);

		Pizza pizza = pizzaObject.GetComponent<Pizza>();

		if (pizza != null)
		{
			float direction = transform.localScale.x >= 0f ? 1f : -1f;

			pizza.SetDirection(
				new Vector2(direction, 0f),
				pizzaSpeed
			);
		}
	}

    private void HandleTimer()
    {
        timer -= Time.deltaTime;
        int minutes = timer <= 0 ? 0 : Mathf.FloorToInt(timer / 60);
        int seconds = timer <= 0 ? 0 : Mathf.FloorToInt(timer % 60);

        string minuteString = minutes < 10 ? "0" + minutes : minutes.ToString();
        string secondString = seconds < 10 ? "0" + seconds : seconds.ToString();

        timerText.text = $"{minuteString}:{secondString}";

        if(minutes <= 0 && seconds <= 0)
        {
            ShouldTimerRun = false;
            GameIsOver();

        }
    }


    public void Slip()
    {
        RB.freezeRotation = false;
        
        Anim.speed = 0f;

        Invoke(nameof(GameIsOver), 0.3f);
    }

    public void GameIsOver()
    {
        IsGameOver = true;
        InputSet.Disable();
        StateMachine.PreventChangingToNewState();
        RB.simulated = false;
        fadingScreen.FadeTo(EndingGameScreen);
    }

    private IEnumerator StartTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ShouldTimerRun = true;
    }


}
