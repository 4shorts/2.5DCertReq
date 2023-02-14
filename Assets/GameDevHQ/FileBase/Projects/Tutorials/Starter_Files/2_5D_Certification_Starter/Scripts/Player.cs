using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private float _gravity = 1f;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpHeight = 10f;
    private Vector3 _direction;
    private Animator _anim;
    private bool _jumping;
    private bool _onLedge;
    private Ledge _activeLedge;
    [SerializeField] private int _coins;
    private bool _rolling;
    private bool _onLadder;
    private Ladder _activeLadder;
    [SerializeField] private Transform _target;
    private LadderExit _ladderExit;
    private UIManager _uiManager;
    [SerializeField] private int _lives = 3;
 
   
   


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }

        _uiManager.UpdateLivesDisplay(_lives);

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (_onLedge == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _anim.SetTrigger("ClimbUp");
            }
        }
    }

    void CalculateMovement()
    {
        if (_onLadder == true && _anim.GetBool("ExitLadder") == false)
        {
            float v = Input.GetAxis("Vertical");
            transform.Translate(Vector3.up * v * 2f * Time.deltaTime);
        }
        else if (_controller.isGrounded == true)
        {
            //if (_rolling == true)
            //{
            //    _rolling = false;
            //    _anim.SetBool("Roll", _rolling);
            //}

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _rolling = true;
                _anim.SetBool("Roll", _rolling);
                StartCoroutine(RollingTimer());
            }

            if (_jumping == true)
            {
                _jumping = false;
                _anim.SetBool("Jump", _jumping);
            }

            float h = Input.GetAxis("Horizontal");
            _direction = new Vector3(0, 0, h) * _speed;
            _anim.SetFloat("Speed", Mathf.Abs(h));

            if (h != 0)
            {
                Vector3 facing = transform.eulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180;
                transform.eulerAngles = facing;
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                _direction.y += _jumpHeight;
                _jumping = true;
                _anim.SetBool("Jump", _jumping);
            }

        }

        if (_onLadder == false)
        {
            _direction.y -= _gravity * Time.deltaTime;
            _controller.Move(_direction * Time.deltaTime);
        }

    }

    private IEnumerator RollingTimer()
    {
        yield return new WaitForSeconds(2.4f);
        _rolling = false;
        _anim.SetBool("Roll", _rolling);
    }

   public void GrabLadder(Vector3 snapPoint, Ladder currentLadder)
    {
        Debug.Log("grab ladder");
        _controller.enabled = false;
        _anim.SetTrigger("ClimbingLadder");
        _anim.SetFloat("Speed", 0);
        _onLadder = true;
        transform.position = snapPoint;
        _activeLadder = currentLadder;
        _ladderExit = _activeLadder.transform.GetComponentInChildren<LadderExit>();
    }

    public void ExitLadder()
    {
        
        _anim.SetBool("ExitLadder", true);
        _anim.ResetTrigger("ClimbingLadder");
        //_onLadder = false;
        StartCoroutine(ExitLadderTimer());
        //transform.position = _ladderExit.GetLadderStandPos();
       // _controller.enabled = true;
    }

    private IEnumerator ExitLadderTimer()
    {
        yield return new WaitForSeconds(4.0f);
        transform.position = _ladderExit.GetLadderStandPos();
        _onLadder = false;
        _controller.enabled = true;
    }

    public void GrabLedge(Vector3 handPos, Ledge currentLedge)
    {
        _controller.enabled = false;
        _anim.SetBool("GrabLedge", true);
        _anim.SetFloat("Speed", 0);
        _anim.SetBool("Jump", false);
        _onLedge = true;
        transform.position = handPos;
        _activeLedge = currentLedge;
    }

    public void ClimbUpComplete()
    {
        transform.position = _activeLedge.GetStandPos();
        _anim.SetBool("GrabLedge", false);
        _controller.enabled = true;
    }

    public void AddCoins()
    {
        _coins++;
        _uiManager.UpdateCoinDisplay(_coins);
        if (_coins == 25)
        {
            _uiManager.UpdateEndTextDisplay();
        }
    }

    public void Damage()
    {
        _lives--;
        _uiManager.UpdateLivesDisplay(_lives);

        if (_lives <1)
        {
            SceneManager.LoadScene(0);
        }
    }

    public int CoinCount()
    {
        return _coins;
    }

    
}