using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;

public class Movement : MonoBehaviour
{   [SerializeField]
    Camera cam;
	Rigidbody2D _rgb2D;
	Dictionary<string , float> states = new Dictionary<string, float >();
	private string _stay = "_stay";
	private string _first = "_first";
	private string _second = "_second";
	private string _third = "_third";
	private string _lastState;
	private float _time;
	private float _lastTime = 0;
	[SerializeField]
	[Range(1, 3)] private float _firstVel = 1;
	[SerializeField]
	[Range(3, 6)] private float _secondVel = 3;
	[SerializeField]
	[Range(6, 12)] private float _thirdVel = 6;
	
	void Start()
	{
		_rgb2D = GetComponent<Rigidbody2D>();
		states.Add(_stay, 0);
        states.Add(_first , _firstVel);
        states.Add(_second , _secondVel);
        states.Add(_third , _thirdVel);
        _lastState = _stay;
        
	}

    
	void Update()
	{
		
		if (Input.GetMouseButtonUp(0))
        { 
			Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition);
			Vector2 m = mouse;
			Vector2 myVec =  m - (Vector2)this.transform.position;
			move(myVec);
		}

		reduceVelocityAfterTime();

	}


//	private void OnMouseDown()
//	{
//		
//		Vector3 mouse = Input.mousePosition;
//		Vector2 m = mouse;
//		Vector2 myVec = (Vector2) transform.position - m;
//		move(myVec);
//		
//	}
		
	
	void move(Vector2 vec) 
	{
			_lastTime = Time.time;
			upgradeState();
			var velocity = checkState();
			_rgb2D.velocity = new Vector2(vec.x , vec.y).normalized * velocity;
			Debug.Log("my state : " + _lastState);
	}


	void reduceVelocityAfterTime()
	{
		if (Time.time - _lastTime >= 3)
		{
			reduceState();
			slowDown();
			Debug.Log("my state : " + _lastState);
			_lastTime = Time.time;
		}
	}
	
	void slowDown()
	{
		 var velocity = checkState();
		_rgb2D.velocity = this._rgb2D.velocity.normalized * velocity;
	}
	float checkState()
	{
		foreach(KeyValuePair<string, float> el in states)
		{
			if (_lastState == el.Key) return el.Value;
		}

		return 0;
	}

	void reduceState()
	{

		if (_lastState == _first)
		{
			_lastState = _stay;
		}
		
		else if (_lastState == _second)
		{
			_lastState = _first;
		}

		else if (_lastState == _third)
		{
			_lastState = _second;
		}
		
	}


	void upgradeState()
	{

		if (_lastState != _third && _lastState != _second && _lastState != _first)
		{
			_lastState = _first;
		}
		
		else if (_lastState != _third && _lastState != _second)
		{
			_lastState = _second;

		}
	
		else
		{
			_lastState = _third;
		}

	}

}
