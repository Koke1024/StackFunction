using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StackFunction : MonoBehaviour {
	public delegate void delStackFunction(object obj);
	struct StrStack{
		public delStackFunction func;
		public float time;
		public object obj;
	};
	List<StrStack> stackList = new List<StrStack>();
	List<StrStack> subStackList = new List<StrStack>();
	bool inIterate = false;

	protected void StackUpdate(){
		inIterate = true;
		foreach (StrStack stack in stackList) {
			if(stack.time < Time.time){
				stack.func(stack.obj);
			}
		}
		inIterate = false;
		stackList.AddRange (subStackList);
		subStackList.Clear ();
		stackList.RemoveAll (stack => stack.time < Time.time);
	}
	
	public void SetStack(delStackFunction func, float time){
		SetStack (func, time, null);
	}
	
	public void SetStack(delStackFunction func, float time, object obj){
		StrStack stack = new StrStack ();
		stack.func = func;
		stack.obj = obj;
		stack.time = Time.time + time;
		if (inIterate) {
			subStackList.Add (stack);
		} else {
			stackList.Add (stack);
		}
	}
}
